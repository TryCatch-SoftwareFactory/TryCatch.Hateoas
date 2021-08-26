// <copyright file="StringExtensionsTests.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.UnitTests.Extensions
{
    using FluentAssertions;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class StringExtensionsTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void SetQueryParamAsLast_With_Invalid_QueryParam(string queryParam)
        {
            // Arrange
            var source = "?key1={key1}";

            // Act
            Action actual = () => _ = source.SetQueryParamAsLast(queryParam);

            // Asserts
            actual.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("&key1={key1}&key2={key2}&key3={key3}", "key1", "&key2={key2}&key3={key3}&key1=")]
        [InlineData("&key1={key1}&key2={key2}&key3={key3}", "key5", "&key1={key1}&key2={key2}&key3={key3}")]
        [InlineData("?key1={key1}&key2={key2}&key3={key3}", "key1", "?key2={key2}&key3={key3}&key1=")]
        [InlineData("?key1={key1}&&key2={key2}&&key3={key3}", "key1", "?key2={key2}&key3={key3}&key1=")]
        public void SetQueryParamAsLast_With_Valid_QueryParam(string source, string queryParam, string expected)
        {
            // Arrange

            // Act
            var actual = source.SetQueryParamAsLast(queryParam);

            // Asserts
            actual.Should().Be(expected);
        }

        [Theory]
        [InlineData(null, "value")]
        [InlineData("", "value")]
        [InlineData(" ", "value")]
        [InlineData("key", null)]
        [InlineData("key", "")]
        [InlineData("key", " ")]
        public void ReplaceQueryParam_With_Invalid_Params(string key, string value)
        {
            // Arrange
            var source = "?key1={key1}";

            // Act
            Action actual = () => _ = source.ReplaceQueryParam(key, value);

            // Asserts
            actual.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("?key1={key1}", "key1", "val1", "?key1=val1")]
        [InlineData("?&key1={key1}", "key1", "val1", "?key1=val1")]
        [InlineData("?&&key1={key1}", "key1", "val1", "?key1=val1")]
        public void ReplaceQueryParam_With_Valid_Params(
            string source,
            string key,
            string value,
            string expected)
        {
            // Arrange
            // Act
            var actual = source.ReplaceQueryParam(key, value);

            // Asserts
            actual.Should().Be(expected);
        }

        [Fact]
        public void ReplaceQueryParams_With_Empty_Source()
        {
            // Arrange
            var source = string.Empty;
            var queryParams = new Dictionary<string, string>()
            {
                { "key1", "value1" }
            };

            // Act
            var actual = source.ReplaceQueryParams(queryParams);

            // Asserts
            actual.Should().BeEmpty();
        }

        [Fact]
        public void ReplaceQueryParams_Without_QueryParams()
        {
            // Arrange
            var source = "?&&key1={key1}&&key2={value2}&&key3={key3}";
            Dictionary<string, string> queryParams = null;

            // Act
            Action actual = () => _ = source.ReplaceQueryParams(queryParams);

            // Asserts
            actual.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ReplaceQueryParams_With_Empty_QueryParams()
        {
            // Arrange
            var source = "?&&key1={key1}&&key2={value2}&&key3={key3}";
            var expected = "?key1={key1}&key2={value2}&key3={key3}";

            var queryParams = new Dictionary<string, string>() { };

            // Act
            var actual = source.ReplaceQueryParams(queryParams);

            // Asserts
            actual.Should().Be(expected);
        }

        [Fact]
        public void ReplaceQueryParams_With_NotMatched_QueryParams()
        {
            // Arrange
            var source = "?&&key1={key1}&&key2={value2}&&key3={key3}";
            var expected = "?key1={key1}&key2={value2}&key3={key3}";

            var queryParams = new Dictionary<string, string>()
            {
                { "key5", "value5" }
            };

            // Act
            var actual = source.ReplaceQueryParams(queryParams);

            // Asserts
            actual.Should().Be(expected);
        }

        [Fact]
        public void ReplaceQueryParams_With_Valid_QueryParams()
        {
            // Arrange
            var source = "?&&key1={key1}&&key2={value2}&&key3={key3}";
            var expected = "?key1=value1&key2={value2}&key3={key3}";
            var queryParams = new Dictionary<string, string>()
            {
                { "key1", "value1" }
            };

            // Act
            var actual = source.ReplaceQueryParams(queryParams);

            // Asserts
            actual.Should().Be(expected);
        }

        [Fact]
        public void ReplaceDefaultQueryParams_With_Empty_Source()
        {
            // Arrange
            var source = string.Empty;
            var queryParams = new Dictionary<string, string>()
            {
                { "key1", "value1" }
            };

            // Act
            var actual = source.ReplaceDefaultQueryParams(queryParams);

            // Asserts
            actual.Should().BeEmpty();
        }

        [Fact]
        public void ReplaceDefaultQueryParams_Without_QueryParams()
        {
            // Arrange
            var source = "?&&key1={key1}&&key2={value2}&&key3={key3}";
            Dictionary<string, string> queryParams = null;

            // Act
            Action actual = () => _ = source.ReplaceDefaultQueryParams(queryParams);

            // Asserts
            actual.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ReplaceDefaultQueryParams_With_Empty_QueryParams()
        {
            // Arrange
            var source = "?&&key1={key1}&&key2={value2}&&key3={key3}";
            var expected = "?key1={key1}&key2={value2}&key3={key3}";

            var queryParams = new Dictionary<string, string>() { };

            // Act
            var actual = source.ReplaceDefaultQueryParams(queryParams);

            // Asserts
            actual.Should().Be(expected);
        }

        [Fact]
        public void ReplaceDefaultQueryParams_With_NotMatched_QueryParams()
        {
            // Arrange
            var source = "?&&key1={key1}&&key2={value2}&&key3={key3}";
            var expected = "?key1={key1}&key2={value2}&key3={key3}";

            var queryParams = new Dictionary<string, string>()
            {
                { "key5", "value5" }
            };

            // Act
            var actual = source.ReplaceDefaultQueryParams(queryParams);

            // Asserts
            actual.Should().Be(expected);
        }

        [Fact]
        public void ReplaceDefaultQueryParams_With_Valid_QueryParams()
        {
            // Arrange
            var source = "?&&key1={key1}&&key2={key2}&&key3={key3}&&key4={key4}";
            var expected = "?key4=value4";
            var queryParams = new Dictionary<string, string>()
            {
                { "key1", null },
                { "key2", "" },
                { "key3", " " },
                { "key4", "value4" },
            };

            // Act
            var actual = source.ReplaceDefaultQueryParams(queryParams);

            // Asserts
            actual.Should().Be(expected);
        }

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
