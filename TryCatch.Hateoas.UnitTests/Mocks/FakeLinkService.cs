// <copyright file="FakeLinkService.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.UnitTests.Mocks
{
    using System;
    using System.Collections.Generic;
    using TryCatch.Hateoas.Models;
    using TryCatch.Hateoas.Services;

    public class FakeLinkService : LinksService
    {
        private const int MaxNumberOfPages = 5;
        private const string DefaultListAction = "GET";
        private const string DefaultListRelation = "list";
        private const string DefaultQueryString = "?offset={offset}&limit={limit}&orderBy={orderBy}&sortAs={sortAs}&search={search}";
        private const string DefaultFirstPage = "?offset=1&limit={limit}&orderBy={orderBy}&sortAs={sortAs}&search={search}";

        public FakeLinkService()
        {
            this.MaxLinks = MaxNumberOfPages;
            this.ListAction = DefaultListAction;
            this.ListRelation = DefaultListRelation;
            this.IdentityKey = "Id";
            this.SetDefaultQueryParams();
            this.SetHttpContext();
            this.SetCurrentQueryParams();
            this.SetBaseEntityLinks();
            this.SetBaseListLinks();
            this.SetBaseSummaryLinks();
        }

        public void SetFakeQueryParams(int offset, int limit)
        {
            this.CurrentQueryParams["offset"] = offset.ToString();
            this.CurrentQueryParams["limit"] = limit.ToString();
        }

        protected override bool CanAddTheLinkByRel(string linkRelation) => true;

        protected override string GetEntityId<TEntity>(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (entity is FakeEntity)
            {
                var fakeEntity = entity as FakeEntity;

                return fakeEntity.Id;
            }

            throw new NotImplementedException();
        }

        private void SetDefaultQueryParams()
        {
            this.DefaultQueryParams.Add("search", string.Empty);
            this.DefaultQueryParams.Add("orderBy", "Id");
            this.DefaultQueryParams.Add("sortAs", "ASC");
            this.DefaultQueryParams.Add("offset", "1");
            this.DefaultQueryParams.Add("limit", "10");
        }

        private Link CreateLink(string action, string rel, string relativePath) => new Link()
        {
            Action = action,
            Rel = rel,
            Href = $"{this.UrlCollectionBase}{relativePath}",
        };

        private void SetBaseEntityLinks()
        {
            var key = "/{Id}";
            this.BaseEntityLinks.Add(this.CreateLink("GET", "self_read", key));
            this.BaseEntityLinks.Add(this.CreateLink("PUT", "self_update", key));
            this.BaseEntityLinks.Add(this.CreateLink("DELETE", "self_delete", key));
        }

        private void SetBaseSummaryLinks() => this.BaseSummaryLinks.Add(this.CreateLink("GET", "self_read", "/summary/{Id}"));

        private void SetBaseListLinks()
        {
            this.BaseListLinks.Add(this.CreateLink("GET", "list_limit", DefaultFirstPage.SetQueryParamAsLast("limit")));
            this.BaseListLinks.Add(this.CreateLink("GET", "list_search", DefaultFirstPage.SetQueryParamAsLast("search")));
            this.BaseListLinks.Add(this.CreateLink("GET", "list_order_by", DefaultQueryString.SetQueryParamAsLast("orderBy")));
            this.BaseListLinks.Add(this.CreateLink("GET", "list_sort_as", DefaultQueryString.SetQueryParamAsLast("sortAs")));
        }

        private void SetCurrentQueryParams()
        {
            var queryParams = new Dictionary<string, string>()
            {
                { "offset", "1200" },
                { "limit", "50" },
                { "orderBy", "Date" },
                { "sortAs", "ASC" },
            };            

            this.CurrentQueryParams.AddRange(queryParams);
        }

        private void SetHttpContext() => this.UrlCollectionBase = new Uri(new Uri("https://localhost:5001"), "api/fakes");
    }
}
