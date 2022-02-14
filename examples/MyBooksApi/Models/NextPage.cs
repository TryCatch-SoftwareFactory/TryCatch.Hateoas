// <copyright file="NextPage.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace MyBooksApi.Models
{
    using System.Collections.Generic;

    public class NextPage<TModel>
        where TModel : class
    {
        public NextPage()
        {
            this.Items = new HashSet<TModel>();
            this.Limit = 40;
            this.Offset = 1;
        }

        public IEnumerable<TModel> Items { get; set; }

        public int Limit { get; set; }

        public int Offset { get; set; }
    }
}
