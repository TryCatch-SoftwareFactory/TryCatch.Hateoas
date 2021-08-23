// <copyright file="PageModel{TModel}.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Models
{
    using System;
    using System.Collections.Generic;

    public class PageModel<TModel> : IItemModel
        where TModel : IItemModel
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
        public PageModel(IEnumerable<TModel> items, IEnumerable<Link> links, int offset, int limit, long matched, long count)
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
        /// Gets the number of the total items.
        /// </summary>
        public long Count { get; }

        /// <summary>
        /// Gets the results item collection.
        /// </summary>
        public IEnumerable<TModel> Items { get; }

        /// <summary>
        /// Gets the page links collection.
        /// </summary>
        public IEnumerable<Link> Links { get; }

        /// <summary>
        /// Gets the limit applied on the query.
        /// </summary>
        public int Limit { get; }

        /// <summary>
        /// Gets the number of items matched with the query applied.
        /// </summary>
        public long Matched { get; }

        /// <summary>
        /// Gets the offset applied on the query.
        /// </summary>
        public int Offset { get; }

        private static void ThrowIfLessThan(int threashold, int value, string name, string message)
        {
            if (value < threashold)
            {
                throw new ArgumentOutOfRangeException(name, message);
            }
        }

        private static void ThrowIfLessThan(long threashold, long value, string name, string message)
        {
            if (value < threashold)
            {
                throw new ArgumentOutOfRangeException(name, message);
            }
        }
    }
}
