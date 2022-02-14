// <copyright file="BooksPage.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.FunctionalTests.Models
{
    using System.Collections.Generic;

    public class BooksPage
    {
        public long Count { get; set;  }

        public IEnumerable<Book> Items { get; set; }

        public IEnumerable<Link> Links { get; set; }

        public int Limit { get; set; }

        public long Matched { get; set; }

        public int Offset { get; set; }
    }
}
