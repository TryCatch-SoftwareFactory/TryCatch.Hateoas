// <copyright file="PageModelTests.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.UnitTests.Models
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using TryCatch.Hateoas.Models;
    using TryCatch.Hateoas.UnitTests.Mocks;
    using Xunit;

    public class PageModelTests
    {
        [Fact]
        public void Construct_with_default_values()
        {
            // Arrange

            // Act
            var actual = new PageModel<FakeEntity>();

            // Asserts
            actual.Links.Should().BeEmpty();
            actual.Items.Should().BeEmpty();
            actual.Count.Should().Be(0L);
            actual.Matched.Should().Be(0L);
            actual.Offset.Should().Be(1);
            actual.Limit.Should().Be(1);
        }

        [Theory]
        [InlineData(0, 1, 0, 0)]
        [InlineData(1, 0, 0, 0)]
        [InlineData(1, 1, -1, 0)]
        [InlineData(1, 1, 0, -1)]
        public void Construct_with_invalid_values(int offset, int limit, long matched, long count)
        {
            // Arrange
            var items = Array.Empty<FakeEntity>();
            var links = Array.Empty<Link>();

            // Act
            Action act = () => _ = new PageModel<FakeEntity>(items, links, offset, limit, matched, count);

            // Asserts
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Construct_with_invalid_items()
        {
            // Arrange
            IEnumerable<FakeEntity> items = null;
            var links = Array.Empty<Link>();
            var offset = 1;
            var limit = 40;
            var matched = 10L;
            var count = 1000L;

            // Act
            Action act = () => _ = new PageModel<FakeEntity>(items, links, offset, limit, matched, count);

            // Asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Construct_with_invalid_links()
        {
            // Arrange
            IEnumerable<FakeEntity> items = Array.Empty<FakeEntity>();
            IEnumerable<Link> links = null;
            var offset = 1;
            var limit = 40;
            var matched = 10L;
            var count = 1000L;

            // Act
            Action act = () => _ = new PageModel<FakeEntity>(items, links, offset, limit, matched, count);

            // Asserts
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Construct_with_custom_values()
        {
            // Arrange
            var items = Array.Empty<FakeEntity>();
            var links = Array.Empty<Link>();

            // Act
            var actual = new PageModel<FakeEntity>(items, links, 41, 40, 60, 1000);

            // Asserts
            actual.Links.Should().BeEmpty();
            actual.Items.Should().BeEmpty();
            actual.Count.Should().Be(1000);
            actual.Matched.Should().Be(60);
            actual.Offset.Should().Be(41);
            actual.Limit.Should().Be(40);
        }
    }
}
