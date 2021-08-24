// <copyright file="LinkBuilder.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.Services
{
    using System;
    using System.Collections.Generic;
    using TryCatch.Hateoas.Models;

    public class LinkBuilder
    {
        private readonly Link link;

        private LinkBuilder()
        {
            this.link = new Link();
        }

        public static LinkBuilder Build() => new LinkBuilder();

        public Link Create() => this.link;

        public LinkBuilder WithUri(Uri uri)
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            this.link.Uri = uri;

            return this;
        }

        public LinkBuilder WithAction(string action)
        {
            if (string.IsNullOrWhiteSpace(action))
            {
                throw new ArgumentException("Action can't be null, empty or whitespace");
            }

            this.link.Action = action;

            return this;
        }

        public LinkBuilder WithRel(string relation)
        {
            if (string.IsNullOrWhiteSpace(relation))
            {
                throw new ArgumentException("Relation can't be null, empty or whitespace");
            }

            this.link.Rel = relation;

            return this;
        }

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
