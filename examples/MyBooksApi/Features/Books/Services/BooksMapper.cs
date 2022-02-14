// <copyright file="BooksMapper.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace MyBooksApi.Features.Books.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MyBooksApi.Features.Books.Models;
    using MyBooksApi.Models;
    using TryCatch.Hateoas.Models;
    using TryCatch.Hateoas.Services;

    // better if use Automapper
    public class BooksMapper : IBooksMapper
    {
        private readonly ILinksService linkService;
        private readonly IEnumerable<LinkInfo> itemTemplates;
        private readonly IEnumerable<LinkInfo> pageTemplates;
        private readonly IDictionary<string, string> defaultQueryParams;

        public BooksMapper(ILinksService linkService)
        {
            this.linkService = linkService;

            this.itemTemplates = new HashSet<LinkInfo>()
            {
               new LinkInfo("edit", "UPDATE"),
               new LinkInfo("details", "GET"),
               new LinkInfo("remove", "DELETE"),
            };

            this.pageTemplates = new HashSet<LinkInfo>()
            {
               new LinkInfo("filterBy", "GET", new Dictionary<string, string>() { { "filterBy", string.Empty } }),
               new LinkInfo("orderBy", "GET", new Dictionary<string, string>() { { "orderBy", string.Empty } }),
               new LinkInfo("sortAs", "GET", new Dictionary<string, string>() { { "sortAs", string.Empty } }),
               new LinkInfo("limit", "GET", new Dictionary<string, string>() { { "limit", string.Empty } }),
            };

            this.defaultQueryParams = new Dictionary<string, string>()
            {
                { "offset", "1" },
                { "limit", "40" },
                { "orderBy", "Title" },
                { "sortAs", "ASC" },
            };
        }

        public BookDto MapTo(Book book) =>
            new BookDto()
            {
                Author = book.Author,
                Category = book.Category,
                Title = book.Title,
                Id = book.Id,
                Links = this.linkService.GetEntityLinks(this.itemTemplates, $"{book.Id}", "/api/books"),
            };

        public PageModel<BookDto> MapTo(Page<Book> page)
        {
            var links = this.linkService.GetPageLinks(
                offset: page.Offset,
                limit: page.Limit,
                total: page.Total,
                templates: this.pageTemplates,
                defaultQueryParams: this.defaultQueryParams);

            return new PageModel<BookDto>(
                items: page.Items.Select(x => this.MapTo(x)).ToList(),
                links: links,
                offset: page.Offset,
                limit: page.Limit,
                matched: page.Matched,
                count: page.Total);
        }

        public NextPageModel<BookDto> MapTo(NextPage<Book> page)
        {
            var links = this.linkService.GetNextPageLinks(
                offset: page.Offset,
                limit: page.Limit,
                defaultQueryParams: this.defaultQueryParams);

            return new NextPageModel<BookDto>(
                items: page.Items.Select(x => this.MapTo(x)).ToList(),
                links: links,
                offset: page.Offset,
                limit: page.Limit);
        }
    }
}
