// <copyright file="LinkBuilder.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Services
{
    using System;
    using System.Collections.Generic;
    using TryCatch.Hateoas.Models;

    /// <summary>
    /// The hypermedia link builder.
    /// </summary>
    public class LinkBuilder
    {
        private readonly Link link;

        private LinkBuilder()
        {
            this.link = new Link();
        }

        /// <summary>
        /// Gets a new LinkBuilder instance.
        /// </summary>
        /// <returns>A <see cref="LinkBuilder"/> new instance.</returns>
        public static LinkBuilder Build() => new LinkBuilder();

        /// <summary>
        /// Gets the reference to the current link built.
        /// </summary>
        /// <returns>A <see cref="Link"/> reference to the current link.</returns>
        public Link Create() => this.link.Clone();

        /// <summary>
        /// Allows setting the resource URI for the current hypermedia link.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> reference for the hypermedia link.</param>
        /// <returns>A <see cref="LinkBuilder"/> reference to the current builder.</returns>
        public LinkBuilder WithUri(Uri uri)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            this.link.Uri = uri;

            return this;
        }

        /// <summary>
        /// Allows setting the resource action for the current hypermedia link.
        /// </summary>
        /// <param name="action">The action of the hypermedia link.</param>
        /// <returns>A <see cref="LinkBuilder"/> reference to the current builder.</returns>
        public LinkBuilder WithAction(string action)
        {
            if (string.IsNullOrWhiteSpace(action))
            {
                throw new ArgumentException("Action can't be null, empty or whitespace");
            }

            this.link.Action = action;

            return this;
        }

        /// <summary>
        /// Allows setting the relation type for the current hypermedia link.
        /// </summary>
        /// <param name="relation">The relation type of the hypermedia link.</param>
        /// <returns>A <see cref="LinkBuilder"/> reference to the current builder.</returns>
        public LinkBuilder WithRel(string relation)
        {
            if (string.IsNullOrWhiteSpace(relation))
            {
                throw new ArgumentException("Relation can't be null, empty or whitespace");
            }

            this.link.Rel = relation;

            return this;
        }

        /// <summary>
        /// Allows setting a key-value pair for the current hypermedia link query parameters.
        /// </summary>
        /// <param name="key">The key name of the query parameter.</param>
        /// <param name="value">The value of the query parameter.</param>
        /// <returns>A <see cref="LinkBuilder"/> reference to the current builder.</returns>
        public LinkBuilder With(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("key can't be null, empty or whitespace");
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                value = string.Empty;
            }

            this.link.AddOrUpdateQueryParam(key, value);

            return this;
        }

        /// <summary>
        /// Allows setting a key-value collection for the current hypermedia link query parameters.
        /// </summary>
        /// <param name="queryParams">The <see cref="IDictionary{TKey, TValue}"/> reference to the collection.</param>
        /// <returns>A <see cref="LinkBuilder"/> reference to the current builder.</returns>
        public LinkBuilder With(IDictionary<string, string> queryParams)
        {
            if (queryParams is null)
            {
                throw new ArgumentNullException(nameof(queryParams));
            }

            this.link.AddOrUpdateQueryParam(queryParams);

            return this;
        }
    }
}
