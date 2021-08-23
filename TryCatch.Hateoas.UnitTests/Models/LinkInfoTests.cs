// <copyright file="LinkInfoTests.cs" company="TryCatch Software Factory">
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

    public class LinkInfoTests
    {
        [Fact]
        public void Construct_with_default_values()
        {
            // Arrange

            // Act
            var actual = new LinkInfo();

            // Asserts
            actual.Relation.Should().Be("self");
            actual.Action.Should().Be("GET");
            actual.DefaultQueryParams.Should().BeEmpty();
        }

        [Theory]
        [InlineData(null, "GET")]
        [InlineData("", "GET")]
        [InlineData(" ", "GET")]
        [InlineData("self", null)]
        [InlineData("self", "")]
        [InlineData("self", " ")]
        public void Construct_with_invalid_values(string rel, string action)
        {
            // Arrange

            // Act
            Action actual = () => _ = new LinkInfo(rel, action);

            // Asserts
            actual.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Construc_without_query_params()
        {
            // Arrange
            var relation = "list";
            var action = "GET";

            // Act
            var actual = new LinkInfo(relation, action);

            // Asserts
            actual.Relation.Should().Be(relation);
            actual.Action.Should().Be(action);
            actual.DefaultQueryParams.Should().BeEmpty();
        }

        [Fact]
        public void Construc_with_empty_query_params()
        {
            // Arrange
            var relation = "list";
            var action = "GET";
            var queryParams = new Dictionary<string, string>();

            // Act
            var actual = new LinkInfo(relation, action, queryParams);

            // Asserts
            actual.Relation.Should().Be(relation);
            actual.Action.Should().Be(action);
            actual.DefaultQueryParams.Should().BeEmpty();
        }

        [Fact]
        public void Construc_with_query_params()
        {
            // Arrange
            var relation = "list";
            var action = "GET";
            var queryParams = new Dictionary<string, string>()
            {
                { "key", string.Empty }
            };

            // Act
            var actual = new LinkInfo(relation, action, queryParams);

            // Asserts
            actual.Relation.Should().Be(relation);
            actual.Action.Should().Be(action);
            actual.DefaultQueryParams.Should().HaveCount(1);
            actual.DefaultQueryParams.ContainsKey("key").Should().BeTrue();
            actual.DefaultQueryParams["key"].Should().BeEmpty();
        }
    }
}
