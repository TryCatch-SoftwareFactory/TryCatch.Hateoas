// <copyright file="LinksService.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TryCatch.Hateoas.Models;

    /// <summary>
    /// Abstract class to implement LinksServices.
    /// </summary>
    public abstract class LinksService : ILinksService
    {
        /// <summary>
        /// Gets or sets the API base url as Uri: https://servername:port/relative-path-to-api.
        /// </summary>
        protected Uri UrlCollectionBase { get; set; }

        /// <summary>
        /// Gets or sets relation name for the links on pagination.
        /// </summary>
        protected string ListRelation { get; set; }

        /// <summary>
        /// Gets or sets action for the links on pagination.
        /// </summary>
        protected string ListAction { get; set; }

        /// <summary>
        /// Gets or sets the max number of link pages.
        /// </summary>
        protected int MaxLinks { get; set; }

        /// <summary>
        /// Gets or sets the Identity key used on entity/summary links.
        /// </summary>
        protected string IdentityKey { get; set; }

        /// <summary>
        /// Gets the base links collection - as templated - to add to the entities DTO.
        /// </summary>
        protected ICollection<Link> BaseEntityLinks { get; } = new HashSet<Link>();

        /// <summary>
        /// Gets the base links collection - as templated - to add to the Summary DTO.
        /// </summary>
        protected ICollection<Link> BaseSummaryLinks { get; } = new HashSet<Link>();

        /// <summary>
        /// Gets the default links for pagination like "offset change control" or "search control".
        /// </summary>
        protected ICollection<Link> BaseListLinks { get; } = new HashSet<Link>();

        /// <summary>
        /// Gets the collection of default query params for list result links.
        /// </summary>
        protected IDictionary<string, string> DefaultQueryParams { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets the collection of current query params.
        /// </summary>
        protected IDictionary<string, string> CurrentQueryParams { get; } = new Dictionary<string, string>();

        /// <inheritdoc/>
        public IEnumerable<Link> GetEntityLinks<TEntity>(TEntity entity)
            where TEntity : class
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var id = this.GetEntityId(entity);

            return this.BaseEntityLinks
                .Where(x => this.CanAddTheLinkByRel(x.Rel))
                .Select(x => new Link()
                {
                    Action = x.Action,
                    Rel = x.Rel,
                    Href = x.Href.ReplaceQueryParam(this.IdentityKey, id),
                });
        }

        /// <inheritdoc/>
        public IEnumerable<Link> GetSummaryLinks<TEntity>(TEntity entity)
            where TEntity : class
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var id = this.GetEntityId(entity);

            return this.BaseSummaryLinks
                .Where(x => this.CanAddTheLinkByRel(x.Rel))
                .Select(x => new Link()
                {
                    Action = x.Action,
                    Rel = x.Rel,
                    Href = x.Href.ReplaceQueryParam(this.IdentityKey, id),
                });
        }

        /// <inheritdoc/>
        public IEnumerable<Link> GetPageResultLinks(int offset = 1, int limit = 10, long total = 0)
        {
            if (offset < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), $"{nameof(offset)} cannot be less than 1");
            }

            if (limit < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(limit), $"{nameof(limit)} cannot be less than 1");
            }

            if (total < offset)
            {
                total = offset;
            }

            var links = new List<Link>();
            links.AddRange(this.GetPaginationLinks(offset, limit, total));
            links.AddRange(this.GetDefaultPaginationLinks());
            return links;
        }

        /// <inheritdoc/>
        public IEnumerable<Link> GetListResultLinks(int offset = 1, int limit = 10, long total = 0)
        {
            if (offset < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), $"{nameof(offset)} cannot be less than 1");
            }

            if (limit < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(limit), $"{nameof(limit)} cannot be less than 1");
            }

            if (total < offset)
            {
                total = offset;
            }

            var args = this.CurrentQueryParams.FilterKeys(new[] { "offset", "limit" }).AsQueryString();

            var links = new List<Link>();

            var prevPage = offset - limit;

            if (prevPage > 0)
            {
                links.Add(this.CreateLink(prevPage, limit, args, $"prev"));
            }

            var nextPage = offset + limit;

            if (nextPage <= total)
            {
                links.Add(this.CreateLink(nextPage, limit, args, $"next"));
            }

            return links;
        }

        protected virtual IEnumerable<Link> GetDefaultPaginationLinks() =>
            this.BaseListLinks
                .Where(x => this.CanAddTheLinkByRel(x.Rel))
                .Select(x => new Link()
                {
                    Action = x.Action,
                    Rel = x.Rel,
                    Href = x.Href
                        .ReplaceQueryParams(this.CurrentQueryParams)
                        .ReplaceDefaultQueryParams(this.DefaultQueryParams),
                });

        protected virtual List<Link> GetPaginationLinks(int offset = 1, int limit = 10, long total = 0)
        {
            var args = this.CurrentQueryParams.FilterKeys(new[] { "offset", "limit" }).AsQueryString();
            var lastOffset = (total - limit >= 0)
                ? ((total % limit == 0) ? ((total - 1) / limit) * limit : (total / limit) * limit) + 1
                : offset;

            var links = new List<Link>
            {
                this.CreateLink(1, limit, args, "first"),
                this.CreateLink(lastOffset, limit, args, "last"),
            };

            var maxLinks = this.MaxLinks + 2;

            for (var offset1 = offset; offset1 < total; offset1 += limit)
            {
                links.Add(this.CreateLink(offset1, limit, args, $"page_{(offset1 / limit) + 1}"));

                if (links.Count >= maxLinks)
                {
                    break;
                }
            }

            return links;
        }

        protected virtual Link CreateLink(long offset, int limit, string args, string relSuffix) => new Link()
        {
            Href = $"{this.UrlCollectionBase}?offset={offset}&limit={limit}{args}",
            Rel = $"{this.ListRelation}_{relSuffix}",
            Action = this.ListAction,
        };

        protected abstract string GetEntityId<TEntity>(TEntity entity)
            where TEntity : class;

        protected abstract bool CanAddTheLinkByRel(string linkRelation);
    }
}
