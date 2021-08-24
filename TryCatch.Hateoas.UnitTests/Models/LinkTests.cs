// <copyright file="LinkTests.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.UnitTests.Models
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using TryCatch.Hateoas.Models;
    using Xunit;

    public class LinkTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void AddIndentity_with_invalid_arguments(string identity)
        {
            // Arrange
            var link = new Link();

            // Act
            Action act = () => _ = link.AddIdentity(identity);

            // Asserts
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Construct_with_default_values()
        {
            // Arrange

            // Act
            var actual = new Link();

            // Asserts
            actual.Rel.Should().BeEmpty();
            actual.Action.Should().BeEmpty();
            actual.Uri.Should().Be("/");
            actual.Href.Should().Be("/");
        }

        [Fact]
        public void Construct_with_default_values_and_relative_uri()
        {
            // Arrange

            // Act
            var actual = new Link
            {
                Uri = new Uri("/", UriKind.Relative)
            };

            // Asserts
            actual.Rel.Should().BeEmpty();
            actual.Action.Should().BeEmpty();
            actual.Uri.Should().Be("/");
            actual.Href.Should().Be("/");
        }

        [Fact]
        public void Construct_with_relative_uri_and_identity()
        {
            // Arrange
            var identity = Guid.NewGuid().ToString();

            // Act
            var actual = new Link
            {
                Uri = new Uri("/", UriKind.Relative)
            };
            actual.AddIdentity(identity);

            // Asserts
            actual.Rel.Should().BeEmpty();
            actual.Action.Should().BeEmpty();
            actual.Uri.Should().Be("/");
            actual.Href.Should().Be($"/{identity}");
        }

        [Fact]
        public void Add_one_query_params_with_relative_uri_ok()
        {
            // Arrange

            // Act
            var actual = new Link
            {
                Uri = new Uri("/", UriKind.Relative)
            };
            actual.AddOrUpdateQueryParam("offset", "1");

            // Asserts
            actual.Rel.Should().BeEmpty();
            actual.Action.Should().BeEmpty();
            actual.Uri.Should().Be("/");
            actual.Href.Should().Be($"/?offset=1");
        }

        [Fact]
        public void Add_one_query_params_and_identity_with_relative_uri_ok()
        {
            // Arrange
            var identity = Guid.NewGuid().ToString();

            // Act
            var actual = new Link
            {
                Uri = new Uri("/", UriKind.Relative)
            };
            actual.AddIdentity(identity);
            actual.AddOrUpdateQueryParam("offset", "1");

            // Asserts
            actual.Rel.Should().BeEmpty();
            actual.Action.Should().BeEmpty();
            actual.Uri.Should().Be("/");
            actual.Href.Should().Be($"/{identity}?offset=1");
        }

        [Fact]
        public void Update_one_query_params_Ok()
        {
            // Arrange

            // Act
            var actual = new Link
            {
                Uri = new Uri("/", UriKind.Relative)
            };
            actual.AddOrUpdateQueryParam("offset", "1");
            actual.AddOrUpdateQueryParam("offset", "2");

            // Asserts
            actual.Rel.Should().BeEmpty();
            actual.Action.Should().BeEmpty();
            actual.Uri.Should().Be("/");
            actual.Href.Should().Be($"/?offset=2");
        }

        [Fact]
        public void Add_a_list_of_query_params_ok()
        {
            // Arrange
            var identity = Guid.NewGuid().ToString();
            var queryParameters = new Dictionary<string, string>()
            {
                { "offset", "1" },
                { "limit", "100" },
            };
            var actual = new Link
            {
                Uri = new Uri("/", UriKind.Relative)
            };
            actual.AddIdentity(identity);

            // Act
            actual.AddOrUpdateQueryParam(queryParameters);

            // Asserts
            actual.Rel.Should().BeEmpty();
            actual.Action.Should().BeEmpty();
            actual.Uri.Should().Be("/");
            actual.Href.Should().Be($"/{identity}?offset=1&limit=100");
        }

        [Fact]
        public void Update_a_list_of_query_params_Ok()
        {
            // Arrange
            var identity = Guid.NewGuid().ToString();
            var queryParameters = new Dictionary<string, string>()
            {
                { "offset", "1" },
                { "limit", "100" },
            };
            var updatedQueryParameters = new Dictionary<string, string>()
            {
                { "limit", "500" },
                { "orderBy", "Id" }
            };
            var actual = new Link
            {
                Uri = new Uri("/", UriKind.Relative)
            };
            actual.AddIdentity(identity);
            actual.AddOrUpdateQueryParam(queryParameters);


            // Act
            actual.AddOrUpdateQueryParam(updatedQueryParameters);

            // Asserts
            actual.Rel.Should().BeEmpty();
            actual.Action.Should().BeEmpty();
            actual.Uri.Should().Be("/");
            actual.Href.Should().Be($"/{identity}?offset=1&limit=500&orderBy=Id");
        }

        [Fact]
        public void Clone_without_identity_ok()
        {
            // Arrange
            var queryParameters = new Dictionary<string, string>()
            {
                { "offset", "1" },
                { "limit", "100" },
            };
            var source = new Link
            {
                Uri = new Uri("/", UriKind.Relative)
            };            
            source.AddOrUpdateQueryParam(queryParameters);

            // Act
            var actual = source.Clone();

            // Asserts
            actual.Rel.Should().BeEmpty();
            actual.Action.Should().BeEmpty();
            actual.Uri.Should().Be("/");
            actual.Href.Should().Be($"/?offset=1&limit=100");
        }

        [Fact]
        public void Clone_ok()
        {
            // Arrange
            var identity = Guid.NewGuid().ToString();
            var queryParameters = new Dictionary<string, string>()
            {
                { "offset", "1" },
                { "limit", "100" },
            };
            var source = new Link
            {
                Uri = new Uri("/", UriKind.Relative)
            };
            source.AddIdentity(identity);
            source.AddOrUpdateQueryParam(queryParameters);

            // Act
            var actual = source.Clone();

            // Asserts
            actual.Rel.Should().BeEmpty();
            actual.Action.Should().BeEmpty();
            actual.Uri.Should().Be("/");
            actual.Href.Should().Be($"/{identity}?offset=1&limit=100");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Clone_with_rel_with_invalid_arguments(string relation)
        {
            // Arrange
            var identity = Guid.NewGuid().ToString();
            var queryParameters = new Dictionary<string, string>()
            {
                { "offset", "1" },
                { "limit", "100" },
            };
            var source = new Link
            {
                Uri = new Uri("/", UriKind.Relative)
            };
            source.AddIdentity(identity);
            source.AddOrUpdateQueryParam(queryParameters);

            // Act
            Action actual = () => _ = source.CloneWithRel(relation);

            // Asserts
            actual.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Clone_with_rel()
        {
            // Arrange
            var identity = Guid.NewGuid().ToString();
            var queryParameters = new Dictionary<string, string>()
            {
                { "offset", "1" },
                { "limit", "100" },
            };
            var source = new Link
            {
                Uri = new Uri("/", UriKind.Relative)
            };
            source.AddIdentity(identity);
            source.AddOrUpdateQueryParam(queryParameters);

            // Act
            var actual = source.CloneWithRel("self");

            // Asserts
            actual.Rel.Should().Be("self");
            actual.Action.Should().BeEmpty();
            actual.Uri.Should().Be("/");
            actual.Href.Should().Be($"/{identity}?offset=1&limit=100");
        }
    }
}
