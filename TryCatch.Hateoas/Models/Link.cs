// <copyright file="Link.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>
[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("TryCatch.Hateoas.UnitTests")]

namespace TryCatch.Hateoas.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a Hypermedia link for a resource.
    /// </summary>
    public class Link
    {
        private string identity;

        private Uri uri;

        private IDictionary<string, string> queryParams;

        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        public Link()
        {
            this.queryParams = new Dictionary<string, string>();
            this.identity = string.Empty;
            this.Rel = string.Empty;
            this.Action = string.Empty;
            this.uri = new Uri("/", UriKind.Relative);
        }

        /// <summary>
        /// Gets the current hypermedia link reference.
        /// </summary>
        public string Href
        {
            get
            {
                var queryParams = this.queryParams.AsQueryString();

                var path = this.uri.IsAbsoluteUri ? this.uri.AbsolutePath : string.Empty;

                var relativePath = path.EndsWith($"/{this.identity}")
                    ? $"{path}?{queryParams}".CleanUri()
                    : $"{path}/{this.identity}?{queryParams}".CleanUri();

                var uri = this.uri.IsAbsoluteUri
                    ? new Uri(this.Uri, new Uri(relativePath, UriKind.Relative))
                    : new Uri(relativePath, UriKind.Relative);

                var href = uri.ToString();

                return href.CleanUri();
            }
        }

        /// <summary>
        /// Gets the hypermedia relation type (self, update, list...).
        /// </summary>
        public string Rel { get; internal set; }

        /// <summary>
        /// Gets the action that must be used when executing the hypermedia link.
        /// </summary>
        public string Action { get; internal set; }

        /// <summary>
        /// Gets the identity of the resource associated with the hypermedia link.
        /// </summary>
        internal string Identity => this.identity;

        /// <summary>
        /// Gets the base URI for the resource.
        /// </summary>
        internal Uri Uri
        {
            get
            {
                return this.uri;
            }

            set
            {
                this.uri = value is null ? new Uri("/", UriKind.Relative) : value;
            }
        }

        /// <summary>
        /// Allows setting the identity of the resoruce.
        /// </summary>
        /// <param name="identity">The resource identity on string format.</param>
        /// <returns>A <see cref="Link"/> reference to the current hypermedia link.</returns>
        public Link AddIdentity(string identity)
        {
            if (string.IsNullOrWhiteSpace(identity))
            {
                identity = string.Empty;
            }

            this.identity = identity;

            return this;
        }

        /// <summary>
        /// Allows adding or updating a query parameter for the hypermedia link.
        /// </summary>
        /// <param name="key">The key name.</param>
        /// <param name="value">The value associated to the key on string format.</param>
        /// <returns>A <see cref="Link"/> reference to the current hypermedia link.</returns>
        public Link AddOrUpdateQueryParam(string key, string value)
        {
            if (this.queryParams.ContainsKey(key))
            {
                this.queryParams[key] = value;
            }
            else
            {
                this.queryParams.Add(key, value);
            }

            return this;
        }

        /// <summary>
        /// Allows adding or updating a query parameters collection for the hypermedia link.
        /// </summary>
        /// <param name="queryParams">The <see cref="IDictionary{TKey, TValue}"/> reference to the current collection to be added.</param>
        /// <returns>A <see cref="Link"/> reference to the current hypermedia link.</returns>
        public Link AddOrUpdateQueryParam(IDictionary<string, string> queryParams)
        {
            this.queryParams = this.queryParams.MergeWith(queryParams);

            return this;
        }

        /// <summary>
        /// Allows cloning the current link.
        /// </summary>
        /// <returns>A <see cref="Link"/> reference to the new hypermedia link.</returns>
        public Link Clone()
        {
            var link = new Link
            {
                Rel = this.Rel,
                Action = this.Action,
                Uri = this.Uri,
            };

            link.AddOrUpdateQueryParam(this.queryParams);

            if (!string.IsNullOrWhiteSpace(this.identity))
            {
                link.AddIdentity(this.identity);
            }

            return link;
        }

        /// <summary>
        /// Allows cloning the current link setting a new hypermedia relation type.
        /// </summary>
        /// <param name="relation">The new relation type.</param>
        /// <returns>A <see cref="Link"/> reference to the new hypermedia link.</returns>
        public Link CloneWithRel(string relation)
        {
            if (string.IsNullOrWhiteSpace(relation))
            {
                throw new ArgumentException($"{nameof(relation)} can't be null, emtpy or whitespace.", nameof(relation));
            }

            var link = this.Clone();

            link.Rel = relation;

            return link;
        }

        /// <summary>
        /// Allows cloning the current link setting a new hypermedia relation type and action.
        /// </summary>
        /// <param name="relation">The new relation type.</param>
        /// <param name="action">The new action.</param>
        /// <returns>A <see cref="Link"/> reference to the new hypermedia link.</returns>
        public Link CloneWithRelAndAction(string relation, string action)
        {
            if (string.IsNullOrWhiteSpace(relation))
            {
                throw new ArgumentException($"{nameof(relation)} can't be null, emtpy or whitespace.", nameof(relation));
            }

            if (string.IsNullOrWhiteSpace(action))
            {
                throw new ArgumentException($"{nameof(action)} can't be null, emtpy or whitespace.", nameof(action));
            }

            var link = this.Clone();

            link.Rel = relation;
            link.Action = action;

            return link;
        }
    }
}
