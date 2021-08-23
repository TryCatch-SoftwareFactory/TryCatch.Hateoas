// <copyright file="LinkInfo.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the basic info of the item link.
    /// </summary>
    public class LinkInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkInfo"/> class.
        /// </summary>
        public LinkInfo()
        {
            this.Relation = "self";
            this.Action = "GET";
            this.DefaultQueryParams = new Dictionary<string, string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkInfo"/> class.
        /// </summary>
        /// <param name="relation">Link relation (p.e.: self, read, update, delete...)[Required].</param>
        /// <param name="action">Link action (GET|POST|PUT|PATCH|DELETE...)[Required].</param>
        /// <param name="queryParams">List of default value for query params (orderBy="Id"...)[Optional].</param>
        public LinkInfo(string relation, string action, IDictionary<string, string> queryParams = null)
        {
            if (string.IsNullOrWhiteSpace(relation))
            {
                throw new ArgumentException($"{nameof(relation)} can't be null, emtpy or whitespace.", nameof(relation));
            }

            if (string.IsNullOrWhiteSpace(action))
            {
                throw new ArgumentException($"{nameof(action)} can't be null, emtpy or whitespace", nameof(action));
            }

            this.Relation = relation;
            this.Action = action;
            this.DefaultQueryParams = queryParams is null ? new Dictionary<string, string>() : queryParams;
        }

        /// <summary>
        /// Gets the link action.
        /// </summary>
        public string Action { get; }

        /// <summary>
        /// Gets the link relation.
        /// </summary>
        public string Relation { get; }

        /// <summary>
        /// Gets the default value for query params (optional).
        /// </summary>
        public IDictionary<string, string> DefaultQueryParams { get; }
    }
}
