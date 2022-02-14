// <copyright file="IBooksService.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace MyBooksApi.Features.Books.Services
{
    using System.Threading.Tasks;
    using MyBooksApi.Features.Books.Models;
    using MyBooksApi.Models;

    public interface IBooksService
    {
        Task<Book> GetById(int id);

        Task<NextPage<Book>> GetBooksNextPage(PageFilter filter);

        Task<Page<Book>> GetBooksPage(PageFilter filter);
    }
}