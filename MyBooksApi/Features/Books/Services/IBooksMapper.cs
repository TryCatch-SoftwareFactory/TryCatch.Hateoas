// <copyright file="IBooksMapper.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace MyBooksApi.Features.Books.Services
{
    using MyBooksApi.Features.Books.Models;
    using MyBooksApi.Models;
    using TryCatch.Hateoas.Models;

    public interface IBooksMapper
    {
        BookDto MapTo(Book book);

        NextPageModel<BookDto> MapTo(NextPage<Book> page);

        PageModel<BookDto> MapTo(Page<Book> page);
    }
}