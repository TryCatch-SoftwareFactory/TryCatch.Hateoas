// <copyright file="LinksService.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using TryCatch.Hateoas.Models;

    /// <summary>
    /// Links service. Allows getting the hypermedia links for items and collections.
    /// </summary>
    public class LinksService : ILinksService
    {
        private readonly IPagingEngine pagingEngine;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HttpRequest httpRequest;
        private readonly IDictionary<string, string> currentQueryParams = new Dictionary<string, string>();
        private readonly Uri urlCollectionBase;
        private readonly Uri urlResourceBase;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinksService"/> class.
        /// </summary>
        /// <param name="pagingEngine">The <see cref="IPagingEngine"/> reference.</param>
        /// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/> reference.</param>
        public LinksService(IPagingEngine pagingEngine, IHttpContextAccessor httpContextAccessor)
        {
            this.pagingEngine = pagingEngine ?? throw new ArgumentNullException(nameof(pagingEngine));
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            this.httpRequest = this.httpContextAccessor.HttpContext.Request ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            if (!this.httpRequest.Host.HasValue)
            {
                throw new ArgumentException($"Host can't be null or empty");
            }

            var uriBuilder = this.httpRequest.Host.Port.HasValue
                ? new UriBuilder(this.httpRequest.Scheme, this.httpRequest.Host.Host, this.httpRequest.Host.Port.Value)
                : new UriBuilder(this.httpRequest.Scheme, this.httpRequest.Host.Host);

            var relativeUri = this.httpRequest.Path.HasValue ? this.httpRequest.Path.Value : string.Empty;
            this.urlCollectionBase = new Uri(uriBuilder.Uri, relativeUri);
            this.urlResourceBase = new Uri(uriBuilder.Uri, string.Empty);
            var queryParams = this.httpRequest.Query.ToDictionary(x => x.Key, x => x.Value.ToString());
            this.currentQueryParams.AddRange(queryParams);
        }

        /// <inheritdoc/>
        public IEnumerable<Link> GetEntityLinks(IEnumerable<LinkInfo> templates, string identity, string path)
        {
            ThrowIfNull(templates, nameof(templates), "Templates can't be null");
            ThrowIfNull(identity, nameof(identity), "Templates can't be null");
            ThrowIfNull(path, nameof(path), "Templates can't be null");

            var uri = new Uri(this.urlResourceBase, path);

            return templates
                .Select(x => LinkBuilder.Build().WithUri(uri).WithAction(x.Action).WithRel(x.Relation).Create()
                    .AddOrUpdateQueryParam(x.DefaultQueryParams).AddIdentity(identity));
        }

        /// <inheritdoc/>
        public IEnumerable<Link> GetPageLinks(
            long offset,
            long limit,
            long total,
            IDictionary<string, string> defaultQueryParams = null,
            IEnumerable<LinkInfo> templates = null,
            int maxNumberOfPages = 5,
            string pageRel = "list",
            string pageAction = "GET")
        {
            ThrowIfLessThan(1, offset, nameof(offset), $"{nameof(offset)} cannot be less than 1");
            ThrowIfLessThan(1, limit, nameof(limit), $"{nameof(limit)} cannot be less than 1");
            ThrowIfLessThan(0, total, nameof(total), $"{nameof(total)} cannot be less than zero");

            if (total < offset)
            {
                total = offset;
            }

            var lastOffset = this.pagingEngine.GetLastOffset(limit, total);
            var pageNumbers = this.pagingEngine.GetPages(offset, limit, total, maxNumberOfPages);
            var queryParams = defaultQueryParams is null ? this.currentQueryParams : defaultQueryParams.MergeWith(this.currentQueryParams);

            var baseListLink = LinkBuilder.Build().WithUri(this.urlCollectionBase).WithAction(pageAction).With(queryParams).Create();
            var firstPage = baseListLink.CloneWithRel($"{pageRel}_first").AddOrUpdateQueryParam("offset", "1").AddOrUpdateQueryParam("limit", $"{limit}");
            var lastPage = firstPage.CloneWithRel($"{pageRel}_last").AddOrUpdateQueryParam("offset", $"{lastOffset}");
            var pages = pageNumbers.Select(x => firstPage.CloneWithRel($"{pageRel}_page_{x.Index}").AddOrUpdateQueryParam("offset", $"{x.Offset}"));

            var links = new List<Link>
            {
                firstPage,
                lastPage,
            };

            if (pages.Any())
            {
                links.AddRange(pages);
            }

            if (templates != null && templates.Any())
            {
                var commonLinks = templates.Select(x => firstPage
                    .CloneWithRelAndAction(x.Relation, x.Action)
                    .AddOrUpdateQueryParam("offset", "1")
                    .AddOrUpdateQueryParam(x.DefaultQueryParams));

                links.AddRange(commonLinks);
            }

            return links;
        }

        /// <inheritdoc/>
        public IEnumerable<Link> GetNextPageLinks(
            long offset,
            long limit,
            IDictionary<string, string> defaultQueryParams = null,
            IEnumerable<LinkInfo> templates = null,
            string pageRel = "list",
            string pageAction = "GET")
        {
            ThrowIfLessThan(1, offset, nameof(offset), $"{nameof(offset)} cannot be less than 1");
            ThrowIfLessThan(1, limit, nameof(limit), $"{nameof(limit)} cannot be less than 1");

            var prevOffset = this.pagingEngine.GetPrevOffset(offset, limit);
            var queryParams = defaultQueryParams is null ? this.currentQueryParams : defaultQueryParams.MergeWith(this.currentQueryParams);
            var baseLink = LinkBuilder.Build().WithUri(this.urlCollectionBase).WithAction(pageAction).With(queryParams).Create().AddOrUpdateQueryParam("limit", $"{limit}");
            var prevPage = baseLink.CloneWithRel($"{pageRel}_prev").AddOrUpdateQueryParam("offset", $"{prevOffset}");
            var nextPage = baseLink.CloneWithRel($"{pageRel}_next").AddOrUpdateQueryParam("offset", $"{offset + limit}");

            var links = new List<Link>
            {
                prevPage,
                nextPage,
            };

            if (templates != null && templates.Any())
            {
                var commonLinks = templates.Select(x => baseLink
                    .CloneWithRelAndAction(x.Relation, x.Action)
                    .AddOrUpdateQueryParam("offset", "1")
                    .AddOrUpdateQueryParam(x.DefaultQueryParams));

                links.AddRange(commonLinks);
            }

            return links;
        }

        private static void ThrowIfNull<TArgument>(TArgument argument, string name, string message = "")
        {
            if (argument is null)
            {
                throw new ArgumentNullException(name, message);
            }
        }

        private static void ThrowIfLessThan(long threashold, long value, string name, string message)
        {
            if (value < threashold)
            {
                throw new ArgumentOutOfRangeException(name, message);
            }
        }
    }
}
