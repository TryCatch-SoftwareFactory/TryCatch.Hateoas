// <copyright file="BooksExtensions.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace MyBooksApi.Features.Books
{
    using Microsoft.Extensions.DependencyInjection;
    using MyBooksApi.Features.Books.Services;

    public static class BooksExtensions
    {
        public static IServiceCollection AddBooksModule(this IServiceCollection service) =>
            service
                .AddTransient<IBooksMapper, BooksMapper>()
                .AddTransient<IBooksService, BooksService>();
    }
}
