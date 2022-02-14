// <copyright file="IPagingEngine.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Services
{
    using System.Collections.Generic;
    using TryCatch.Hateoas.Models;

    /// <summary>
    /// Paging engine interface. Allows calculating the value of differents paging parameters.
    /// </summary>
    public interface IPagingEngine
    {
        /// <summary>
        /// Allows getting the offset of the previous page.
        /// </summary>
        /// <param name="offset">The offset of the current request.</param>
        /// <param name="limit">The size of the page requested.</param>
        /// <returns>The previous page offset.</returns>
        long GetPrevOffset(long offset, long limit);

        /// <summary>
        /// Allows getting the offset of the last page.
        /// </summary>
        /// <param name="limit">The size of the page requested.</param>
        /// <param name="total">The total number of entries on the datasource.</param>
        /// <returns>The last page offset.</returns>
        long GetLastOffset(long limit, long total);

        /// <summary>
        /// Allows getting the paging information for the current parameters.
        /// </summary>
        /// <param name="offset">The offset of the current request.</param>
        /// <param name="limit">The size of the page requested.</param>
        /// <param name="total">The total number of entries on the datasource.</param>
        /// <param name="maxPagesToBeGetting">The max number of pages requested.</param>
        /// <returns>The <see cref="IEnumerable{PageInfo}"/> collection with the paging data.</returns>
        IEnumerable<PageInfo> GetPages(long offset, long limit, long total, int maxPagesToBeGetting = 0);
    }
}
