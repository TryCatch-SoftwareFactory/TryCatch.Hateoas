// <copyright file="LinksServiceTests.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.UnitTests.Services
{
    using FluentAssertions;
    using System;
    using System.Linq;
    using TryCatch.Hateoas.Services;
    using TryCatch.Hateoas.UnitTests.Mocks;
    using Xunit;

    public class LinksServiceTests
    {
        ////private readonly ILinksService sut;

        ////public LinksServiceTests()
        ////{
        ////    // this.sut = new LinksService();
        ////}

        ////[Fact]
        ////public void GetEntityLinks_Without_Entity()
        ////{
        ////    // Arrange
        ////    FakeEntity entity = null;

        ////    // Act
        ////    Action actual = () => _ = this.sut.GetEntityLinks(entity);

        ////    // Asserts
        ////    actual.Should().Throw<ArgumentNullException>();
        ////}

        ////[Fact]
        ////public void GetEntityLinks_With_Invalid_Entity()
        ////{
        ////    // Arrange
        ////    string entity = null;

        ////    // Act
        ////    Action actual = () => _ = this.sut.GetEntityLinks(entity);

        ////    // Asserts
        ////    actual.Should().Throw<ArgumentException>();
        ////}

        ////[Fact]
        ////public void GetEntityLinks_With_Valid_Entity()
        ////{
        ////    // Arrange
        ////    var entity = Given.ValidEntity();
        ////    var expected = Given.ExpectedLinksForEntity();

        ////    // Act
        ////    var actual = this.sut.GetEntityLinks(entity);

        ////    // Asserts
        ////    actual.Should().BeEquivalentTo(expected);
        ////}        

        ////[Theory]
        ////[InlineData(0, 10)]
        ////[InlineData(1, 0)]
        ////public void GetPageResultLinks_With_Invalid_Args(int offset, int limit)
        ////{
        ////    // Arrange
        ////    var total = 100;

        ////    // Act
        ////    Action actual = () => _ = this.sut.GetPageResultLinks(offset, limit, total);

        ////    // Asserts
        ////    actual.Should().Throw<ArgumentOutOfRangeException>();
        ////}

        ////[Theory]
        ////[InlineData(1, 1000, 1, new[] { 1 })]
        ////[InlineData(1, 100, 1, new[] { 1 })]
        ////[InlineData(1, 10, 91, new []{ 1, 11, 21, 31, 41 })]
        ////[InlineData(11, 10, 91, new[] { 11, 21, 31, 41, 51 })]
        ////public void GetPageResultLinks_With_Valid_Args(int offset, int limit, int lastPage, int[] offsets)
        ////{
        ////    // Arrange
        ////    var total = 100;
        ////    var expected = Given
        ////        .ExpectedLinksForPageResult(offset, limit, lastPage, offsets)
        ////        .OrderBy(x => x.Rel);

        ////    this.sut.SetFakeQueryParams(offset, limit);

        ////    // Act
        ////    var actual = this.sut.GetPageResultLinks(offset, limit, total).OrderBy(x => x.Rel);

        ////    // Asserts
        ////    actual.Should().BeEquivalentTo(expected);
        ////}
    }
}
