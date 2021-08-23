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

        private IDictionary<string, string> queryParams;

        public Link()
        {
            this.queryParams = new Dictionary<string, string>();

            this.identity = string.Empty;
        }

        public string Href
        {
            get
            {
                var queryParams = this.queryParams.AsQueryString();

                var uri = this.Uri is null ? new Uri("/", UriKind.Relative) : this.Uri;

                if (!string.IsNullOrWhiteSpace(this.identity))
                {
                    uri = new Uri(uri, new Uri($"/{this.identity}", UriKind.Relative));
                }

                uri = new Uri(uri, new Uri($"?{queryParams}", UriKind.Relative));

                return uri.ToString();
            }
        }

        public string Rel { get; internal set; }

        public string Action { get; internal set; }

        public Uri Uri { get; internal set; }

        public Link AddIdentity(string identity)
        {
            if (string.IsNullOrWhiteSpace(identity))
            {
                throw new ArgumentException($"{nameof(identity)} can't be null, empty or whitespace.", nameof(identity));
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

            return link;
        }

        public Link CloneWithRel(string relation)
        {
            var link = this.Clone();

            link.Rel = relation;

            return link;
        }
    }
}
