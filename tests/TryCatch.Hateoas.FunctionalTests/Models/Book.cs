// <copyright file="Book.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.FunctionalTests.Models
{
    using System.Collections.Generic;

    public class Book
    {
        public Book()
        {
            this.Links = new HashSet<Link>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Category { get; set; }

        public IEnumerable<Link> Links { get; set; }
    }
}
