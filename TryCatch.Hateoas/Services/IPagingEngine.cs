// <copyright file="IPagingEngine.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Services
{
    using System.Collections.Generic;
    using TryCatch.Hateoas.Models;

    public interface IPagingEngine
    {
        long GetPrevOffset(long offset, long limit);

        long GetLastOffset(long limit, long total);

        IEnumerable<PageInfo> GetPages(long offset, long limit, long total, int maxPagesToBeGetting = 0);
    }
}
