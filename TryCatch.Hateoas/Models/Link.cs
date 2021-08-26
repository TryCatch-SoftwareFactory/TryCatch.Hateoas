// <copyright file="Link.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Models
{
    using System;
    using System.Collections.Generic;

    public class Link
    {
        private string identity;

        private Uri uri;

        private IDictionary<string, string> queryParams;

        public Link()
        {
            this.queryParams = new Dictionary<string, string>();
            this.identity = string.Empty;
            this.Rel = string.Empty;
            this.Action = string.Empty;
            this.uri = new Uri("/", UriKind.Relative);
        }

        public string Href
        {
            get
            {
                var queryParams = this.queryParams.AsQueryString();

                var path = this.uri.IsAbsoluteUri ? this.uri.AbsolutePath : string.Empty;

                var relativePath = $"{path}/{this.identity}?{queryParams}".CleanUri();

                var uri = this.uri.IsAbsoluteUri
                    ? new Uri(this.Uri, new Uri(relativePath, UriKind.Relative))
                    : new Uri(relativePath, UriKind.Relative);

                var href = uri.ToString();

                return href.CleanUri();
            }
        }

        public string Rel { get; internal set; }

        public string Action { get; internal set; }

        public string Identity => this.identity;

        public Uri Uri
        {
            get
            {
                return this.uri;
            }

            internal set
            {
                this.uri = value is null ? new Uri("/", UriKind.Relative) : value;
            }
        }

        public Link AddIdentity(string identity)
        {
            if (string.IsNullOrWhiteSpace(identity))
            {
                identity = string.Empty;
            }

            this.identity = identity;

            return this;
        }

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

        public Link AddOrUpdateQueryParam(IDictionary<string, string> queryParams)
        {
            this.queryParams = this.queryParams.MergeWith(queryParams);

            return this;
        }

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
    }
}
