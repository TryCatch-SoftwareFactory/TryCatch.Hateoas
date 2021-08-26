// <copyright file="LinkBuilderTests.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.UnitTests.Services
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using TryCatch.Hateoas.Services;
    using Xunit;

    public class LinkBuilderTests
    {
        [Fact]
        public void Build_default_link()
        {
            // Arrange
            var builder = LinkBuilder.Build();

            // Act
            var actual = builder.Create();

            // Asserts
            actual.Rel.Should().BeEmpty();
            actual.Action.Should().BeEmpty();
            actual.Uri.Should().Be("/");
            actual.Href.Should().Be("");
        }

        [Fact]
        public void Build_with_invalid_uri()
        {
            // Arrange
            var builder = LinkBuilder.Build();
            Uri uri = null;

            // Act
            Action actual = () => _ = builder.WithUri(uri);

            // Asserts
            actual.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Build_with_uri_ok()
        {
            // Arrange
            var builder = LinkBuilder.Build();
            var uri = new Uri("http://localhost", UriKind.Absolute);

            // Act
            var actual = builder.WithUri(uri).Create();

            // Asserts
            actual.Rel.Should().BeEmpty();
            actual.Action.Should().BeEmpty();
            actual.Uri.Should().Be("http://localhost");
            actual.Href.Should().Be("http://localhost");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Build_with_invalid_action(string action)
        {
            // Arrange
            var builder = LinkBuilder.Build();

            // Act
            Action actual = () => _ = builder.WithAction(action);

            // Asserts
            actual.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Build_with_action_ok()
        {
            // Arrange
            var builder = LinkBuilder.Build();
            var action = "action";

            // Act
            var actual = builder.WithAction(action).Create();

            // Asserts
            actual.Rel.Should().BeEmpty();
            actual.Action.Should().Be(action);
            actual.Uri.Should().Be("/");
            actual.Href.Should().Be("");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Build_with_invalid_rel(string relation)
        {
            // Arrange
            var builder = LinkBuilder.Build();

            // Act
            Action actual = () => _ = builder.WithRel(relation);

            // Asserts
            actual.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Build_with_rel_ok()
        {
            // Arrange
            var builder = LinkBuilder.Build();
            var relation = "self";

            // Act
            var actual = builder.WithRel(relation).Create();

            // Asserts
            actual.Rel.Should().Be(relation);
            actual.Action.Should().BeEmpty();
            actual.Uri.Should().Be("/");
            actual.Href.Should().Be("");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Build_with_invalid_keyValue(string key)
        {
            // Arrange
            var builder = LinkBuilder.Build();

            // Act
            Action actual = () => _ = builder.With(key, "some-value");

            // Asserts
            actual.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Build_and_add_keyValue_with_invalid_value_ok(string value)
        {
            // Arrange
            var builder = LinkBuilder.Build();
            var key = "offset";            

            // Act
            var actual = builder.With(key, value).Create();

            // Asserts
            actual.Rel.Should().BeEmpty();
            actual.Action.Should().BeEmpty();
            actual.Uri.Should().Be("/");
            actual.Href.Should().Be($"?{key}=");
        }

        [Fact]
        public void Build_with_keyValue_ok()
        {
            // Arrange
            var builder = LinkBuilder.Build();
            var key = "offset";
            var value = "1";

            // Act
            var actual = builder.With(key, value).Create();

            // Asserts
            actual.Rel.Should().BeEmpty();
            actual.Action.Should().BeEmpty();
            actual.Uri.Should().Be("/");
            actual.Href.Should().Be($"?{key}={value}");
        }

        [Fact]
        public void Build_with_invalid_keyValue_collection()
        {
            // Arrange
            IDictionary<string, string> keyValuesCollection = null;
            var builder = LinkBuilder.Build();            

            // Act
            Action actual = () => _ = builder.With(keyValuesCollection);

            // Asserts
            actual.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Build_with_keyValue_collection_ok()
        {
            // Arrange
            var builder = LinkBuilder.Build();
            var key = "offset";
            var value = "1";
            var keyValuesCollection = new Dictionary<string, string>() { { key, value } };

            // Act
            var actual = builder.With(keyValuesCollection).Create();

            // Asserts
            actual.Rel.Should().BeEmpty();
            actual.Action.Should().BeEmpty();
            actual.Uri.Should().Be("/");
            actual.Href.Should().Be($"?{key}={value}");
        }
    }
}
