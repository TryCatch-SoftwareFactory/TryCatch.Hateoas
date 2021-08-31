﻿// <copyright file="PageFilter.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace MyBooksApi.Models
{
    public class PageFilter
    {
        private const string SortAsc = "ASC";

        public PageFilter()
        {
            this.FilterBy = string.Empty;
            this.Limit = 40;
            this.Offset = 1;
            this.OrderBy = string.Empty;
            this.SortAs = SortAsc;
        }

        public string FilterBy { get; set; }

        public int Limit { get; set; }

        public int Offset { get; set; }

        public string OrderBy { get; set; }

        public string SortAs { get; set; }
    }
}
