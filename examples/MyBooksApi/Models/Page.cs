// <copyright file="Page.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace MyBooksApi.Models
{
    using System.Collections.Generic;

    public class Page<TModel>
        where TModel : class
    {
        public Page()
        {
            this.Items = new HashSet<TModel>();
            this.Limit = 40;
            this.Offset = 1;
            this.Total = 0;
            this.Matched = 0;
        }

        public IEnumerable<TModel> Items { get; set; }

        public int Limit { get; set; }

        public long Matched { get; set; }

        public int Offset { get; set; }

        public long Total { get; set; }
    }
}
