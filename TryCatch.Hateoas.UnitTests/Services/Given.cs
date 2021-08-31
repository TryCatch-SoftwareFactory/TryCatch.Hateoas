// <copyright file="Given.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.UnitTests.Services
{
    using System;
    using System.Collections.Generic;
    using TryCatch.Hateoas.Models;

    public static class Given
    {
        public static IEnumerable<object[]> NextPageLinksInput()
        {
            // int offset, int limit, long total, IEnumerable<Link> expected
            var uri = new Uri("https://localhost/api", UriKind.Absolute);
            var defaultQueryParams = new Dictionary<string, string>
            {
                { "offset", "1" },
                { "limit", "20" },
                { "orderBy", "Id" },
                { "sortAs", "ASC" },
            };
            var templates = new HashSet<LinkInfo>()
            {
                new LinkInfo("list_limit", "GET", new Dictionary<string, string> { { "limit", string.Empty }, }),
                new LinkInfo("list_search", "GET", new Dictionary<string, string> { { "search", string.Empty }, }),
                new LinkInfo("list_order_by", "GET", new Dictionary<string, string> { { "orderBy", string.Empty }, }),
                new LinkInfo("list_sort_as", "GET", new Dictionary<string, string> { { "sortAs", string.Empty }, }),
            };

            var baseLineLink = new Link() { Action = "GET", Rel = "list_first", Uri = uri }
                .AddOrUpdateQueryParam(defaultQueryParams)
                .AddOrUpdateQueryParam("limit", "40");

            var firstPage = baseLineLink
                .CloneWithRel("list_page_1")
                .AddOrUpdateQueryParam("offset", "1");

            yield return new object[]
            {
                1, 40, defaultQueryParams, templates,
                new HashSet<Link>()
                {
                    baseLineLink.CloneWithRel("list_prev").AddOrUpdateQueryParam("offset", "1").AddOrUpdateQueryParam("limit", "40"),
                    baseLineLink.CloneWithRel("list_next").AddOrUpdateQueryParam("offset", "41").AddOrUpdateQueryParam("limit", "40"),
                    firstPage.CloneWithRel("list_limit").AddOrUpdateQueryParam("limit", string.Empty),
                    firstPage.CloneWithRel("list_search").AddOrUpdateQueryParam("search", string.Empty),
                    firstPage.CloneWithRel("list_order_by").AddOrUpdateQueryParam("orderBy", string.Empty),
                    firstPage.CloneWithRel("list_sort_as").AddOrUpdateQueryParam("sortAs", string.Empty),
                },
            };

            yield return new object[]
            {
                121, 40, defaultQueryParams, templates,
                new HashSet<Link>()
                {
                    baseLineLink.CloneWithRel("list_prev").AddOrUpdateQueryParam("offset", "81").AddOrUpdateQueryParam("limit", "40"),
                    baseLineLink.CloneWithRel("list_next").AddOrUpdateQueryParam("offset", "161").AddOrUpdateQueryParam("limit", "40"),
                    firstPage.CloneWithRel("list_limit").AddOrUpdateQueryParam("limit", string.Empty),
                    firstPage.CloneWithRel("list_search").AddOrUpdateQueryParam("search", string.Empty),
                    firstPage.CloneWithRel("list_order_by").AddOrUpdateQueryParam("orderBy", string.Empty),
                    firstPage.CloneWithRel("list_sort_as").AddOrUpdateQueryParam("sortAs", string.Empty),
                },
            };
        }

        public static IEnumerable<object[]> NextPageLinksInputWithDefaultValues()
        {
            var uri = new Uri("https://localhost/api", UriKind.Absolute);

            yield return new object[]
            {
                1, 40,
                new HashSet<Link>()
                {
                    new Link() { Action = "GET", Rel = "list_prev", Uri = uri }.AddOrUpdateQueryParam("offset", "1").AddOrUpdateQueryParam("limit", "40"),
                    new Link() { Action = "GET", Rel = "list_next", Uri = uri }.AddOrUpdateQueryParam("offset", "41").AddOrUpdateQueryParam("limit", "40"),
                },
            };

            yield return new object[]
            {
                121, 40,
                new HashSet<Link>()
                {
                    new Link() { Action = "GET", Rel = "list_prev", Uri = uri }.AddOrUpdateQueryParam("offset", "81").AddOrUpdateQueryParam("limit", "40"),
                    new Link() { Action = "GET", Rel = "list_next", Uri = uri }.AddOrUpdateQueryParam("offset", "161").AddOrUpdateQueryParam("limit", "40"),
                },
            };
        }

        public static IEnumerable<object[]> PageLinksInputWithDefaultValues()
        {
            // int offset, int limit, long total, IEnumerable<Link> expected
            var uri = new Uri("https://localhost/api", UriKind.Absolute);

            yield return new object[]
            {
                1, 40, 1000,
                new HashSet<Link>()
                {
                    new Link() { Action = "GET", Rel = "list_first", Uri = uri }.AddOrUpdateQueryParam("offset", "1").AddOrUpdateQueryParam("limit", "40"),
                    new Link() { Action = "GET", Rel = "list_last", Uri = uri }.AddOrUpdateQueryParam("offset", "961").AddOrUpdateQueryParam("limit", "40"),
                    new Link() { Action = "GET", Rel = "list_page_1", Uri = uri }.AddOrUpdateQueryParam("offset", "1").AddOrUpdateQueryParam("limit", "40"),
                    new Link() { Action = "GET", Rel = "list_page_2", Uri = uri }.AddOrUpdateQueryParam("offset", "41").AddOrUpdateQueryParam("limit", "40"),
                    new Link() { Action = "GET", Rel = "list_page_3", Uri = uri }.AddOrUpdateQueryParam("offset", "81").AddOrUpdateQueryParam("limit", "40"),
                    new Link() { Action = "GET", Rel = "list_page_4", Uri = uri }.AddOrUpdateQueryParam("offset", "121").AddOrUpdateQueryParam("limit", "40"),
                    new Link() { Action = "GET", Rel = "list_page_5", Uri = uri }.AddOrUpdateQueryParam("offset", "161").AddOrUpdateQueryParam("limit", "40"),
                },
            };

            yield return new object[]
            {
                101, 40, 1000,
                new HashSet<Link>()
                {
                    new Link() { Action = "GET", Rel = "list_first", Uri = uri }.AddOrUpdateQueryParam("offset", "1").AddOrUpdateQueryParam("limit", "40"),
                    new Link() { Action = "GET", Rel = "list_last", Uri = uri }.AddOrUpdateQueryParam("offset", "961").AddOrUpdateQueryParam("limit", "40"),
                    new Link() { Action = "GET", Rel = "list_page_3", Uri = uri }.AddOrUpdateQueryParam("offset", "101").AddOrUpdateQueryParam("limit", "40"),
                    new Link() { Action = "GET", Rel = "list_page_4", Uri = uri }.AddOrUpdateQueryParam("offset", "141").AddOrUpdateQueryParam("limit", "40"),
                    new Link() { Action = "GET", Rel = "list_page_5", Uri = uri }.AddOrUpdateQueryParam("offset", "181").AddOrUpdateQueryParam("limit", "40"),
                    new Link() { Action = "GET", Rel = "list_page_6", Uri = uri }.AddOrUpdateQueryParam("offset", "221").AddOrUpdateQueryParam("limit", "40"),
                    new Link() { Action = "GET", Rel = "list_page_7", Uri = uri }.AddOrUpdateQueryParam("offset", "261").AddOrUpdateQueryParam("limit", "40"),
                },
            };

            yield return new object[]
            {
                101, 40, 100,
                new HashSet<Link>()
                {
                    new Link() { Action = "GET", Rel = "list_first", Uri = uri }.AddOrUpdateQueryParam("offset", "1").AddOrUpdateQueryParam("limit", "40"),
                    new Link() { Action = "GET", Rel = "list_last", Uri = uri }.AddOrUpdateQueryParam("offset", "81").AddOrUpdateQueryParam("limit", "40"),
                },
            };
        }

        public static IEnumerable<object[]> PageLinksInput()
        {
            // int offset, int limit, long total, IEnumerable<Link> expected
            var uri = new Uri("https://localhost/api", UriKind.Absolute);
            var defaultQueryParams = new Dictionary<string, string>
            {
                { "offset", "1" },
                { "limit", "20" },
                { "orderBy", "Id" },
                { "sortAs", "ASC" },
            };
            var templates = new HashSet<LinkInfo>()
            {
                new LinkInfo("list_limit", "GET", new Dictionary<string, string> { { "limit", string.Empty }, }),
                new LinkInfo("list_search", "GET", new Dictionary<string, string> { { "search", string.Empty }, }),
                new LinkInfo("list_order_by", "GET", new Dictionary<string, string> { { "orderBy", string.Empty }, }),
                new LinkInfo("list_sort_as", "GET", new Dictionary<string, string> { { "sortAs", string.Empty }, }),
            };

            var baseLineLink = new Link() { Action = "GET", Rel = "list_first", Uri = uri }
                .AddOrUpdateQueryParam(defaultQueryParams)
                .AddOrUpdateQueryParam("limit", "40");

            var firstPage = baseLineLink
                .CloneWithRel("list_page_1")
                .AddOrUpdateQueryParam("offset", "1");

            yield return new object[]
            {
                1, 40, 1000, defaultQueryParams, templates,
                new HashSet<Link>()
                {
                    baseLineLink.Clone().AddOrUpdateQueryParam("offset", "1"),
                    baseLineLink.CloneWithRel("list_last").AddOrUpdateQueryParam("offset", "961"),
                    firstPage,
                    baseLineLink.CloneWithRel("list_page_2").AddOrUpdateQueryParam("offset", "41"),
                    baseLineLink.CloneWithRel("list_page_3").AddOrUpdateQueryParam("offset", "81"),
                    baseLineLink.CloneWithRel("list_page_4").AddOrUpdateQueryParam("offset", "121"),
                    baseLineLink.CloneWithRel("list_page_5").AddOrUpdateQueryParam("offset", "161"),
                    firstPage.CloneWithRel("list_limit").AddOrUpdateQueryParam("limit", string.Empty),
                    firstPage.CloneWithRel("list_search").AddOrUpdateQueryParam("search", string.Empty),
                    firstPage.CloneWithRel("list_order_by").AddOrUpdateQueryParam("orderBy", string.Empty),
                    firstPage.CloneWithRel("list_sort_as").AddOrUpdateQueryParam("sortAs", string.Empty),
                },
            };
        }

        public static IEnumerable<object[]> EntityLinksInput()
        {
            var identity = Guid.NewGuid().ToString();
            var uri = new Uri("https://localhost/api", UriKind.Absolute);

            yield return new object[]
            {
                new HashSet<LinkInfo>() { new LinkInfo("self_read", "GET"), new LinkInfo("self_update", "UPDATE"), new LinkInfo("self_delete", "DELETE") },
                identity,
                new HashSet<Link>()
                {
                    new Link() { Action = "GET", Rel = "self_read", Uri = uri }.AddIdentity(identity),
                    new Link() { Action = "UPDATE", Rel = "self_update", Uri = uri }.AddIdentity(identity),
                    new Link() { Action = "DELETE", Rel = "self_delete", Uri = uri }.AddIdentity(identity),
                },
            };
        }

        public static IEnumerable<object[]> HrefInputs()
        {
            yield return new object[] { "https://localhost:5001/", "20021", new Dictionary<string, string> { { "offset", "1" }, { "limit", "100" } }, "https://localhost:5001/20021?limit=100&offset=1" };
            yield return new object[] { "https://localhost:5001", "20021", new Dictionary<string, string> { { "offset", "1" }, { "limit", "100" } }, "https://localhost:5001/20021?limit=100&offset=1" };
            yield return new object[] { "http://localhost:5001/", "20021", new Dictionary<string, string> { { "offset", "1" }, { "limit", "100" } }, "http://localhost:5001/20021?limit=100&offset=1" };
            yield return new object[] { "http://localhost:5001", "20021", new Dictionary<string, string> { { "offset", "1" }, { "limit", "100" } }, "http://localhost:5001/20021?limit=100&offset=1" };
            yield return new object[] { "https://localhost/", "20021", new Dictionary<string, string> { { "offset", "1" }, { "limit", "100" } }, "https://localhost/20021?limit=100&offset=1" };
            yield return new object[] { "https://localhost", "20021", new Dictionary<string, string> { { "offset", "1" }, { "limit", "100" } }, "https://localhost/20021?limit=100&offset=1" };
            yield return new object[] { "http://localhost/", "20021", new Dictionary<string, string> { { "offset", "1" }, { "limit", "100" } }, "http://localhost/20021?limit=100&offset=1" };
            yield return new object[] { "http://localhost", "20021", new Dictionary<string, string> { { "offset", "1" }, { "limit", "100" } }, "http://localhost/20021?limit=100&offset=1" };
            yield return new object[] { "https://localhost/api/", "20021", new Dictionary<string, string> { { "offset", "1" }, { "limit", "100" } }, "https://localhost/api/20021?limit=100&offset=1" };
            yield return new object[] { "https://localhost/api", "20021", new Dictionary<string, string> { { "offset", "1" }, { "limit", "100" } }, "https://localhost/api/20021?limit=100&offset=1" };
            yield return new object[] { "http://localhost/api/", "20021", new Dictionary<string, string> { { "offset", "1" }, { "limit", "100" } }, "http://localhost/api/20021?limit=100&offset=1" };
            yield return new object[] { "http://localhost/api", "20021", new Dictionary<string, string> { { "offset", "1" }, { "limit", "100" } }, "http://localhost/api/20021?limit=100&offset=1" };
            yield return new object[] { "https://localhost:5001/", "20021", new Dictionary<string, string>(), "https://localhost:5001/20021" };
            yield return new object[] { "https://localhost:5001", "20021", new Dictionary<string, string>(), "https://localhost:5001/20021" };
            yield return new object[] { "http://localhost:5001/", "20021", new Dictionary<string, string>(), "http://localhost:5001/20021" };
            yield return new object[] { "http://localhost:5001", "20021", new Dictionary<string, string>(), "http://localhost:5001/20021" };
            yield return new object[] { "https://localhost/", "20021", new Dictionary<string, string>(), "https://localhost/20021" };
            yield return new object[] { "https://localhost", "20021", new Dictionary<string, string>(), "https://localhost/20021" };
            yield return new object[] { "http://localhost/", "20021", new Dictionary<string, string>(), "http://localhost/20021" };
            yield return new object[] { "http://localhost", "20021", new Dictionary<string, string>(), "http://localhost/20021" };
            yield return new object[] { "https://localhost/api/", "20021", new Dictionary<string, string>(), "https://localhost/api/20021" };
            yield return new object[] { "https://localhost/api", "20021", new Dictionary<string, string>(), "https://localhost/api/20021" };
            yield return new object[] { "http://localhost/api/", "20021", new Dictionary<string, string>(), "http://localhost/api/20021" };
            yield return new object[] { "http://localhost/api", "20021", new Dictionary<string, string>(), "http://localhost/api/20021" };
        }

        public static IEnumerable<object[]> PagesInput()
        {
            /// long offset, long limit, long total, int maxPages, IEnumerable<PageInfo> pages
            yield return new object[]
            {
                1, 10, 100, 5, new HashSet<PageInfo>
                {
                    new PageInfo(1, 1),
                    new PageInfo(11, 2),
                    new PageInfo(21, 3),
                    new PageInfo(31, 4),
                    new PageInfo(41, 5),
                },
            };

            yield return new object[]
            {
                10, 10, 100, 5, new HashSet<PageInfo>
                {
                    new PageInfo(10, 2),
                    new PageInfo(20, 3),
                    new PageInfo(30, 4),
                    new PageInfo(40, 5),
                    new PageInfo(50, 6),
                },
            };

            yield return new object[]
            {
                61, 10, 100, 5, new HashSet<PageInfo>
                {
                    new PageInfo(61, 7),
                    new PageInfo(71, 8),
                    new PageInfo(81, 9),
                    new PageInfo(91, 10),
                },
            };
        }
    }
}
