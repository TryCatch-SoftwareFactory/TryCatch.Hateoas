// <copyright file="BookDto.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace MyBooksApi.Features.Books.Models
{
    using System.Collections.Generic;
    using TryCatch.Hateoas.Models;

    public class BookDto : Book, IItemModel
    {
        public BookDto()
        {
            this.Links = new HashSet<Link>();
        }

        public IEnumerable<Link> Links { get; set; }
    }
}
