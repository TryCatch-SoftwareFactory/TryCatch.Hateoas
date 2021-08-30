// <copyright file="PageFilter.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace MyBooksApi.Models
{
    using System;

    public class PageFilter
    {
        private const string SortAsc = "ASC";

        private string sortAs;

        public PageFilter()
        {
            this.FilterBy = string.Empty;
            this.Limit = 40;
            this.Offset = 1;
            this.OrderBy = string.Empty;
            this.sortAs = SortAsc;
        }

        public string FilterBy { get; set; }

        public int Limit { get; set; }

        public int Offset { get; set; }

        public string OrderBy { get; set; }

        public string SortAs
        {
            get => this.sortAs;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = string.Empty;
                }

                this.sortAs = value;
            }
        }

        public bool SortAsAscending => this.sortAs.Equals(SortAsc, StringComparison.Ordinal);
    }
}
