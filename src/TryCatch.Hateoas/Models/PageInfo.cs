// <copyright file="PageInfo.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Models
{
    /// <summary>
    /// Represents the query page info requested.
    /// </summary>
    public class PageInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageInfo"/> class.
        /// </summary>
        /// <param name="offset">Offset applied on the query.</param>
        /// <param name="index">Page index equivalent with the current offset.</param>
        public PageInfo(long offset, long index)
        {
            this.Offset = offset;
            this.Index = index;
        }

        /// <summary>
        /// Gets the offset applied on the query.
        /// </summary>
        public long Offset { get; }

        /// <summary>
        /// Gets the page index equivalent with the current offset.
        /// </summary>
        public long Index { get; }
    }
}
