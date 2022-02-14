// <copyright file="PagingEngineTests.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.UnitTests.Services
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using TryCatch.Hateoas.Models;
    using TryCatch.Hateoas.Services;
    using Xunit;

    public class PagingEngineTests
    {
        private readonly IPagingEngine sut;

        public PagingEngineTests()
        {
            this.sut = new PagingEngine();
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, -1)]
        public void GetLastOffset_with_invalid_arguments(long limit, long total)
        {
            // Arrange

            // Act
            Action act = () => this.sut.GetLastOffset(limit, total);

            // Asserts
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 10, 9)]
        [InlineData(40, 39, 1)]
        [InlineData(40, 100, 81)]
        public void GetLastOffset_ok(long limit, long total, long expected)
        {
            // Arrange

            // Act
            var actual = this.sut.GetLastOffset(limit, total);

            // Asserts
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(0, 1, 1, 1)]
        [InlineData(1, 0, 1, 1)]
        [InlineData(1, 1, -1, 1)]
        public void GetPages_with_invalid_arguments(long offset, long limit, long total, int maxPages)
        {
            // Arrange

            // Act
            Action act = () => this.sut.GetPages(offset, limit, total, maxPages);

            // Asserts
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [MemberData(memberName: nameof(Given.PagesInput), MemberType = typeof(Given))]
        public void GetPages_ok(long offset, long limit, long total, int maxPages, IEnumerable<PageInfo> pages)
        {
            // Arrange

            // Act
            var actual = this.sut.GetPages(offset, limit, total, maxPages);

            // Asserts
            actual.Should().BeEquivalentTo(pages);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        public void GetPrevOffset_with_invalid_arguments(long offset, long limit)
        {
            // Arrange

            // Act
            Action act = () => this.sut.GetPrevOffset(offset, limit);

            // Asserts
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(1, 40, 1)]
        [InlineData(41, 40, 1)]
        [InlineData(41, 100, 1)]
        [InlineData(81, 40, 41)]
        public void GetPrevOffset_Ok(long offset, long limit, long expected)
        {
            // Arrange

            // Act
            var actual = this.sut.GetPrevOffset(offset, limit);

            // Asserts
            actual.Should().Be(expected);
        }
    }
}
