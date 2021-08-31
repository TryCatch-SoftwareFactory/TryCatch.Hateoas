// <copyright file="Given.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.FunctionalTests
{
    using System.Collections.Generic;
    using TryCatch.Hateoas.FunctionalTests.Models;

    public static class Given
    {
        public static IEnumerable<object[]> PageQueriesInput()
        {
            var items = GetItems();

            yield return new object[]
            {
                new Dictionary<string, string>()
                {
                    { "offset", "1" },
                    { "limit", "40" },
                },
                new BooksPage()
                {
                    Count = 10000,
                    Offset = 1,
                    Matched = 1000,
                    Limit = 40,
                    Items = items,
                    Links = GetLinks(1, 40, "Title", "ASC", 9961),
                },
            };

            yield return new object[]
            {
                new Dictionary<string, string>()
                {
                    { "offset", "41" },
                    { "limit", "40" },
                },
                new BooksPage()
                {
                    Count = 10000,
                    Offset = 41,
                    Matched = 1000,
                    Limit = 40,
                    Items = items,
                    Links = GetLinks(41, 40, "Title", "ASC", 9961),
                },
            };

            yield return new object[]
            {
                new Dictionary<string, string>()
                {
                    { "offset", "41" },
                    { "limit", "20" },
                    { "orderBy", "Author" },
                    { "sortAs", "DESC" },
                },
                new BooksPage()
                {
                    Count = 10000,
                    Offset = 41,
                    Matched = 1000,
                    Limit = 20,
                    Items = items,
                    Links = GetLinks(41, 20, "Author", "DESC", 9981),
                },
            };
        }

        public static IEnumerable<object[]> PageNextQueriesInput()
        {
            var items = GetItems();

            yield return new object[]
            {
                new Dictionary<string, string>()
                {
                    { "offset", "1" },
                    { "limit", "40" },
                },
                new BooksNextPage()
                {
                    Offset = 1,
                    Limit = 40,
                    Items = items,
                    Links = GetNextLinks(1, 40, "Title", "ASC", "/next"),
                },
            };

            yield return new object[]
            {
                new Dictionary<string, string>()
                {
                    { "offset", "41" },
                    { "limit", "40" },
                },
                new BooksNextPage()
                {
                    Offset = 41,
                    Limit = 40,
                    Items = items,
                    Links = GetNextLinks(41, 40, "Title", "ASC", "/next"),
                },
            };

            yield return new object[]
            {
                new Dictionary<string, string>()
                {
                    { "offset", "41" },
                    { "limit", "20" },
                    { "orderBy", "Author" },
                    { "sortAs", "DESC" },
                },
                new BooksNextPage()
                {
                    Offset = 41,
                    Limit = 20,
                    Items = items,
                    Links = GetNextLinks(41, 20, "Author", "DESC", "/next"),
                },
            };
        }

        private static IEnumerable<Link> GetLinks(int offset, int limit, string orderBy, string sortAs, int last, string path = "") =>
            new HashSet<Link>
            {
                new Link { Action = "GET", Href = $"http://localhost/api/books{path}?limit={limit}&offset=1&orderBy={orderBy}&sortAs={sortAs}&filterBy=", Rel = "filterBy" },
                new Link { Action = "GET", Href = $"http://localhost/api/books{path}?limit={limit}&offset=1&sortAs={sortAs}&orderBy=",    Rel = "orderBy" },
                new Link { Action = "GET", Href = $"http://localhost/api/books{path}?limit={limit}&offset=1&orderBy={orderBy}&sortAs=",    Rel = "sortAs" },
                new Link { Action = "GET", Href = $"http://localhost/api/books{path}?offset=1&orderBy={orderBy}&sortAs={sortAs}&limit=",   Rel = "limit" },
                new Link { Action = "GET", Href = $"http://localhost/api/books{path}?limit={limit}&offset=1&orderBy={orderBy}&sortAs={sortAs}",   Rel = "list_first" },
                new Link { Action = "GET", Href = $"http://localhost/api/books{path}?limit={limit}&offset={last}&orderBy={orderBy}&sortAs={sortAs}", Rel = "list_last" },
                new Link { Action = "GET", Href = $"http://localhost/api/books{path}?limit={limit}&offset={offset}&orderBy={orderBy}&sortAs={sortAs}", Rel = $"list_page_{(int)(offset / limit) + 1}" },
                new Link { Action = "GET", Href = $"http://localhost/api/books{path}?limit={limit}&offset={offset + limit}&orderBy={orderBy}&sortAs={sortAs}", Rel = $"list_page_{(int)((offset + (1 * limit)) / limit) + 1}" },
                new Link { Action = "GET", Href = $"http://localhost/api/books{path}?limit={limit}&offset={offset + (2 * limit)}&orderBy={orderBy}&sortAs={sortAs}", Rel = $"list_page_{(int)((offset + (2 * limit)) / limit) + 1}" },
                new Link { Action = "GET", Href = $"http://localhost/api/books{path}?limit={limit}&offset={offset + (3 * limit)}&orderBy={orderBy}&sortAs={sortAs}", Rel = $"list_page_{(int)((offset + (3 * limit)) / limit) + 1}" },
                new Link { Action = "GET", Href = $"http://localhost/api/books{path}?limit={limit}&offset={offset + (4 * limit)}&orderBy={orderBy}&sortAs={sortAs}", Rel = $"list_page_{(int)((offset + (4 * limit)) / limit) + 1}" },
            };

        private static IEnumerable<Link> GetNextLinks(int offset, int limit, string orderBy, string sortAs, string path = "") =>
            new HashSet<Link>
            {
                new Link { Action = "GET", Href = $"http://localhost/api/books{path}?limit={limit}&offset={offset + limit}&orderBy={orderBy}&sortAs={sortAs}", Rel = "list_next" },
                new Link { Action = "GET", Href = $"http://localhost/api/books{path}?limit={limit}&offset={((offset - limit) < 1 ? 1 : offset - limit)}&orderBy={orderBy}&sortAs={sortAs}", Rel = "list_prev" },
            };

        private static IEnumerable<Book> GetItems()
        {
            var items = new HashSet<Book>();

            for (var i = 1; i < 41; i++)
            {
                var href = $"http://localhost/api/books/{1000 + i}";

                items.Add(new Book()
                {
                    Author = $"Author_{i}",
                    Id = 1000 + i,
                    Title = $"Title_{i}",
                    Category = $"Terror",
                    Links = new HashSet<Link>()
                    {
                        new Link { Action = "UPDATE", Href = href, Rel = "edit" },
                        new Link { Action = "GET", Href = href, Rel = "details" },
                        new Link { Action = "DELETE", Href = href, Rel = "remove" },
                    },
                });
            }

            return items;
        }
    }
}
