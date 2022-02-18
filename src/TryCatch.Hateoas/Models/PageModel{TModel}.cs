// <copyright file="PageModel{TModel}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Models
{
    using System;
    using System.Collections.Generic;

    public class PageModel<TModel> : IResourceModel
        where TModel : IResourceModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageModel{TModel}"/> class.
        /// </summary>
        public PageModel()
        {
            this.Items = new HashSet<TModel>();
            this.Links = new HashSet<Link>();
            this.Count = 0;
            this.Limit = 1;
            this.Offset = 1;
            this.Matched = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PageModel{TModel}"/> class.
        /// </summary>
        /// <param name="items">A <see cref="IEnumerable{TModel}"/> reference to the result items collection.</param>
        /// <param name="links">A <see cref="IEnumerable{Link}"/> reference to the page links collection.</param>
        /// <param name="offset">Offset applied on the source query.</param>
        /// <param name="limit">Limit applied on the source query.</param>
        /// <param name="matched">The number of items matched with the query applied.</param>
        /// <param name="count">The number of the total items.</param>
        public PageModel(IEnumerable<TModel> items, IEnumerable<Link> links, long offset, long limit, long matched, long count)
        {
            ThrowIfLessThan(1, offset, nameof(offset), $"Offset can't be less than 1: {offset}");
            ThrowIfLessThan(1, limit, nameof(limit), $"Limit can't be less than 1: {offset}");
            ThrowIfLessThan(0, matched, nameof(matched), $"Matched can't be less than 0: {matched}");
            ThrowIfLessThan(0, count, nameof(count), $"Count can't be less than 0: {count}");

            this.Items = items ?? throw new ArgumentNullException(nameof(items));
            this.Links = links ?? throw new ArgumentNullException(nameof(links));
            this.Offset = offset;
            this.Limit = limit;
            this.Matched = matched;
            this.Count = count;
        }

        /// <summary>
        /// Gets or sets the number of the total items.
        /// </summary>
        public long Count { get; set; }

        /// <summary>
        /// Gets or sets the results item collection.
        /// </summary>
        public IEnumerable<TModel> Items { get; set; }

        /// <summary>
        /// Gets or sets the page links collection.
        /// </summary>
        public IEnumerable<Link> Links { get; set; }

        /// <summary>
        /// Gets or sets the limit applied on the query.
        /// </summary>
        public long Limit { get; set; }

        /// <summary>
        /// Gets or sets the number of items matched with the query applied.
        /// </summary>
        public long Matched { get; set; }

        /// <summary>
        /// Gets or sets the offset applied on the query.
        /// </summary>
        public long Offset { get; set; }

        private static void ThrowIfLessThan(long threashold, long value, string name, string message)
        {
            if (value < threashold)
            {
                throw new ArgumentOutOfRangeException(name, message);
            }
        }
    }
}
