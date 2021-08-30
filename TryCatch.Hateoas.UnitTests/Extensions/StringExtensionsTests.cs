// <copyright file="StringExtensionsTests.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.UnitTests.Extensions
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("//api//?&key1=2", "/api?key1=2")]
        [InlineData("https://localhost//api//?&key1=2", "https://localhost/api?key1=2")]
        [InlineData("https://localhost:5001//api//?&key1=2", "https://localhost:5001/api?key1=2")]
        [InlineData("http://localhost//api//?&key1=2", "http://localhost/api?key1=2")]
        [InlineData("http://localhost:5001//api//?&key1=2", "http://localhost:5001/api?key1=2")]
        [InlineData("https://localhost//api?&key1=2", "https://localhost/api?key1=2")]
        [InlineData("https://localhost:5001/api?&key1=2", "https://localhost:5001/api?key1=2")]
        [InlineData("http://localhost//api?&key1=2", "http://localhost/api?key1=2")]
        [InlineData("http://localhost:5001//api?&key1=2", "http://localhost:5001/api?key1=2")]
        [InlineData("https://localhost//api?key1=2", "https://localhost/api?key1=2")]
        [InlineData("https://localhost:5001/api?key1=2", "https://localhost:5001/api?key1=2")]
        [InlineData("http://localhost//api?key1=2", "http://localhost/api?key1=2")]
        [InlineData("http://localhost:5001//api?key1=2", "http://localhost:5001/api?key1=2")]
        [InlineData("https://localhost//api??=key1=2&&k2==2", "https://localhost/api?key1=2&k2=2")]
        [InlineData("https://localhost:5001/api?key1=2&&k2==2", "https://localhost:5001/api?key1=2&k2=2")]
        [InlineData("http://localhost//api??key1=2&&k2==2", "http://localhost/api?key1=2&k2=2")]
        [InlineData("http://localhost:5001//api??key1=2&&k2==2", "http://localhost:5001/api?key1=2&k2=2")]
        public void CleanUri_ok(string query, string expected)
        {
            // Arrange

            // Act
            var actual = query.CleanUri();

            // Asserts
            actual.Should().Be(expected);
        }
    }
}
