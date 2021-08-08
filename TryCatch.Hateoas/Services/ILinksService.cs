// <copyright file="ILinksService.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Services
{
    using System.Collections.Generic;
    using TryCatch.Hateoas.Models;

    /// <summary>
    /// Links service interface. Allows to create Hateoas links for specific entity/DTO.
    /// </summary>
    public interface ILinksService
    {
        /// <summary>
        /// Allows to get all links for an entity DTO reference, filtered by authorization rule.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="entity">A reference to the entity.</param>
        /// <returns>A new links collection for the entity DTO.</returns>
        IEnumerable<Link> GetEntityLinks<TEntity>(TEntity entity)
            where TEntity : class;

        /// <summary>
        /// Allows to get all links for Summary DTO reference, filtered by authorization rule.
        /// </summary>
        /// <typeparam name="TEntity">Type of entity.</typeparam>
        /// <param name="entity">A reference to the entity.</param>
        /// <returns>A new links collection for the summary DTO.</returns>
        IEnumerable<Link> GetSummaryLinks<TEntity>(TEntity entity)
            where TEntity : class;

        /// <summary>
        /// Allows to get all links for pagination result DTO reference.
        /// </summary>
        /// <param name="offset">Current offest used in the query.</param>
        /// <param name="limit">Current limit used in the query.</param>
        /// <param name="total">Number of matched entities on the current query.</param>
        /// <returns>A links collection for pagination result DTO.</returns>
        IEnumerable<Link> GetPageResultLinks(int offset = 1, int limit = 10, long total = 0);

        /// <summary>
        /// Allows to get all links for list result DTO reference.
        /// </summary>
        /// <param name="offset">Current offest used in the query.</param>
        /// <param name="limit">Current limit used in the query.</param>
        /// <param name="total">Number of matched entities on the current query.</param>
        /// <returns>A links collection for list result DTO.</returns>
        IEnumerable<Link> GetListResultLinks(int offset = 1, int limit = 10, long total = 0);
    }
}
