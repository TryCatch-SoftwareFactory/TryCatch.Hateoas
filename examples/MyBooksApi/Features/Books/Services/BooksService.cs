// <copyright file="BooksService.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace MyBooksApi.Features.Books.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MyBooksApi.Features.Books.Models;
    using MyBooksApi.Models;

    public class BooksService : IBooksService
    {
        public async Task<Page<Book>> GetBooksPage(PageFilter filter)
        {
            var page = new Page<Book>()
            {
                Items = GetFakeResults(),
                Limit = filter.Limit,
                Offset = filter.Offset,
                Matched = 1000,
                Total = 10000,
            };

            return await Task.FromResult(page).ConfigureAwait(false);
        }

        public async Task<NextPage<Book>> GetBooksNextPage(PageFilter filter)
        {
            var page = new NextPage<Book>()
            {
                Items = GetFakeResults(),
                Limit = filter.Limit,
                Offset = filter.Offset,
            };

            return await Task.FromResult(page).ConfigureAwait(false);
        }

        public async Task<Book> GetById(int id)
        {
            var book = GetFakeResults().First();

            book.Id = id;

            return await Task.FromResult(book).ConfigureAwait(false);
        }

        private static IEnumerable<Book> GetFakeResults()
        {
            var items = new HashSet<Book>();

            for (var i = 1; i < 41; i++)
            {
                items.Add(new Book()
                {
                    Author = $"Author_{i}",
                    Id = 1000 + i,
                    Title = $"Title_{i}",
                    Category = $"Terror",
                });
            }

            return items;
        }
    }
}
