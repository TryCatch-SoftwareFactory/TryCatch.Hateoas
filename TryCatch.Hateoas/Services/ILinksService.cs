// <copyright file="ILinksService.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Services
{
    using System.Collections.Generic;
    using TryCatch.Hateoas.Models;

    /// <summary>
    /// Links service interface. Allows creating hateoas links.
    /// </summary>
    public interface ILinksService
    {
        /// <summary>
        /// Allows getting links for an entity.
        /// </summary>
        /// <param name="templates">A <see cref="IEnumerable{LinkInfo}"/> templates collection.</param>
        /// <param name="identity">The identity value of the current item.</param>
        /// <returns>A <see cref="IEnumerable{Link}"/> reference to the created links collection.</returns>
        IEnumerable<Link> GetEntityLinks(IEnumerable<LinkInfo> templates, string identity);

        /// <summary>
        /// Allows getting all links for pagination results.
        /// </summary>
        /// <param name="offset">Current offest used in the query.</param>
        /// <param name="limit">Current limit used in the query.</param>
        /// <param name="total">Number of matched entities on the current query.</param>
        /// <param name="defaultQueryParams">A <see cref="IDictionary{string,string}"/> default query params collection. Default values is null.</param>
        /// <param name="templates">A <see cref="IEnumerable{LinkInfo}"/> templates collection. Default values is null.</param>
        /// <param name="maxNumberOfPages">Max number of links pages to be created. Default value is 5.</param>
        /// <param name="pageRel">Name of the relation to be used on page links. Default value is "list".</param>
        /// <param name="pageAction">Name of the action to be used on page links. Default value is "GET".</param>
        /// <returns>A <see cref="IEnumerable{Link}"/> reference to the created links collection.</returns>
        IEnumerable<Link> GetPageLinks(
            int offset,
            int limit,
            long total,
            IDictionary<string, string> defaultQueryParams = null,
            IEnumerable<LinkInfo> templates = null,
            int maxNumberOfPages = 5,
            string pageRel = "list",
            string pageAction = "GET");

        /// <summary>
        /// Allows getting all links for next page result.
        /// </summary>
        /// <param name="offset">Current offest used in the query.</param>
        /// <param name="limit">Current limit used in the query.</param>
        /// <param name="defaultQueryParams">A <see cref="IDictionary{string,string}"/> default query params collection. Default values is null.</param>
        /// <param name="templates">A <see cref="IEnumerable{LinkInfo}"/> templates collection. Default values is null.</param>
        /// <param name="pageRel">Name of the relation to be used on page links. Default value is "list".</param>
        /// <param name="pageAction">Name of the action to be used on page links. Default value is "GET".</param>
        /// <returns>A <see cref="IEnumerable{Link}"/> reference to the created links collection.</returns>
        IEnumerable<Link> GetNextPageLinks(
            int offset,
            int limit,
            IDictionary<string, string> defaultQueryParams = null,
            IEnumerable<LinkInfo> templates = null,
            string pageRel = "list",
            string pageAction = "GET");
    }
}
