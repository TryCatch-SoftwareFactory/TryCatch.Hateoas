// <copyright file="PagingEngine.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Services
{
    using System;
    using System.Collections.Generic;
    using TryCatch.Hateoas.Models;

    public class PagingEngine : IPagingEngine
    {
        /// <inheritdoc/>
        public long GetLastOffset(long limit, long total)
        {
            ThrowIfLessThan(1, limit, "limit can't be less than 1");
            ThrowIfLessThan(0, total, "total can't be less than 0");

            var offset = 1L;

            while (offset < total)
            {
                offset += limit;
            }

            return offset is 1L ? offset : offset - limit;
        }

        /// <inheritdoc/>
        public IEnumerable<PageInfo> GetPages(long offset, long limit, long total, int maxPagesToBeGetting = 0)
        {
            ThrowIfLessThan(1, offset, "offset can't be less than 1");
            ThrowIfLessThan(1, limit, "limit can't be less than 1");
            ThrowIfLessThan(0, total, "total can't be less than 0");
            ThrowIfLessThan(0, maxPagesToBeGetting, "maxPagesToBeGetting can't be less than 0");

            var dic = new HashSet<PageInfo>();

            for (var offset1 = offset; offset1 < total; offset1 += limit)
            {
                var pageNumber = (offset1 / limit) + 1;

                dic.Add(new PageInfo(offset1, pageNumber));

                if (maxPagesToBeGetting > 0 && dic.Count >= maxPagesToBeGetting)
                {
                    break;
                }
            }

            return dic;
        }

        /// <inheritdoc/>
        public long GetPrevOffset(long offset, long limit)
        {
            ThrowIfLessThan(1, offset, "offset can't be less than 1");
            ThrowIfLessThan(1, limit, "limit can't be less than 1");

            var newOffset = offset - limit;

            if (newOffset < 1)
            {
                newOffset = 1;
            }

            return newOffset;
        }

        private static void ThrowIfLessThan(long thresholder, long value, string message)
        {
            if (value < thresholder)
            {
                throw new ArgumentOutOfRangeException(message: message, innerException: null);
            }
        }
    }
}
