// <copyright file="IDictionaryExtensionsTests.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.UnitTests.Extensions
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public class IDictionaryExtensionsTests
    {
        [Fact]
        public void AddRange_With_Empty_Source()
        {
            // Arrange
            var expected = new Dictionary<string, string>()
            {
                { "key1", "value1" },
                { "key2", "value1" },
                { "key3", "value1" },
                { "key4", "value1" },
            };
            var source = new Dictionary<string, string>();

            // Act
            var actual = source.AddRange(expected);

            // Asserts
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void AddRange_Without_KeysToAdd()
        {
            // Arrange
            Dictionary<string, string> keysToAdd = null;
            var source = new Dictionary<string, string>()
            {
                { "key1", "value1" },
                { "key2", "value1" },
                { "key3", "value1" },
                { "key4", "value1" },
            };

            // Act
            Action actual = () => _ = source.AddRange(keysToAdd);

            // Asserts
            actual.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddRange_With_Empty_KeysToAdd()
        {
            // Arrange
            var keysToAdd = new Dictionary<string, string>();
            var expected = new Dictionary<string, string>()
            {
                { "key1", "value1" },
                { "key2", "value1" },
                { "key3", "value1" },
                { "key4", "value1" },
            };

            // Act
            var actual = expected.AddRange(keysToAdd);

            // Asserts
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void AddRange_With_Valid_KeysToAdd()
        {
            // Arrange
            var keysToAdd = new Dictionary<string, string>()
            {
                { "key1", "value1" },
                { "key2", "value1" },
                { "key3", "value1" },
                { "key4", "value1" },
            };
            var source = new Dictionary<string, string>()
            {
                { "key12", "value1" },
                { "key13", "value1" },
                { "key14", "value1" },
                { "key15", "value1" },
            };
            var expected = new Dictionary<string, string>()
            {
                { "key1", "value1" },
                { "key2", "value1" },
                { "key3", "value1" },
                { "key4", "value1" },
                { "key12", "value1" },
                { "key13", "value1" },
                { "key14", "value1" },
                { "key15", "value1" },
            };

            // Act
            var actual = source.AddRange(keysToAdd);

            // Asserts
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void NormalizedKeys_With_Empty_Source()
        {
            // Arrange
            var expected = new Dictionary<string, string>()
            {
                { "key1", "value1" },
                { "key2", "value1" },
                { "key3", "value1" },
                { "key4", "value1" },
            };
            var source = new Dictionary<string, string>();

            // Act
            var actual = source.NormalizedKeys(expected);

            // Asserts
            actual.Should().BeEmpty();
        }

        [Fact]
        public void NormalizedKeys_Without_Keys()
        {
            // Arrange
            Dictionary<string, string> keys = null;
            var source = new Dictionary<string, string>()
            {
                { "key1", "value1" },
                { "key2", "value1" },
                { "key3", "value1" },
                { "key4", "value1" },
            };

            // Act
            Action actual = () => _ = source.NormalizedKeys(keys);

            // Asserts
            actual.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void NormalizedKeys_With_Empty_Keys()
        {
            // Arrange
            var keys = new Dictionary<string, string>();
            var source = new Dictionary<string, string>()
            {
                { "key1", "value1" },
                { "key2", "value1" },
                { "key3", "value1" },
                { "key4", "value1" },
            };

            // Act
            var actual = source.NormalizedKeys(keys);

            // Asserts
            actual.Should().BeEquivalentTo(source);
        }

        [Fact]
        public void NormalizedKeys_With_Valid_Keys()
        {
            // Arrange
            var keys = new Dictionary<string, string>()
            {
                { "key1", "value1" },
                { "key2", "value1" },
                { "key3", "value1" },
                { "key4", "value1" },
            };
            var source = new Dictionary<string, string>()
            {
                { "kEy1", "value1" },
                { "kEy2", "value1" },
                { "kEy3", "value1" },
                { "kEy4", "value1" },
            };

            // Act
            var actual = source.NormalizedKeys(keys);

            // Asserts
            actual.Keys.Should().BeEquivalentTo(keys.Keys);
        }

        [Fact]
        public void FilterKeys_With_Empty_Source()
        {
            // Arrange
            var expected = new Dictionary<string, string>();
            var keysToFilter = new[] { "key1", "key2" };

            // Act
            var actual = expected.FilterKeys(keysToFilter);

            // Asserts
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void FilterKeys_Without_Keys()
        {
            // Arrange
            var expected = new Dictionary<string, string>()
            {
                { "key1", "value1" },
                { "key2", "value2" },
                { "key3", "value3" },
                { "key4", "value4" },
            };
            string[] keysToFilter = null;

            // Act
            Action actual = () => _ = expected.FilterKeys(keysToFilter);

            // Asserts
            Assert.Throws<ArgumentNullException>(actual);
        }

        [Fact]
        public void FilterKeys_With_Empty_Keys()
        {
            // Arrange
            var expected = new Dictionary<string, string>()
            {
                { "key1", "value1" },
                { "key2", "value2" },
                { "key3", "value3" },
                { "key4", "value4" },
            };
            var keysToFilter = new string[] { };

            // Act
            var actual = expected.FilterKeys(keysToFilter);

            // Asserts
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void FilterKeys_With_Valid_Keys()
        {
            // Arrange
            var sources = new Dictionary<string, string>()
            {
                { "key1", "value1" },
                { "key2", "value2" },
                { "key3", "value3" },
                { "key4", "value4" },
            };
            var keysToFilter = new[] { "key1", "key4", "key2" };
            var expected = new Dictionary<string, string>()
            {
                { "key3", "value3" },
            };

            // Act
            var actual = sources.FilterKeys(keysToFilter);

            // Asserts
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Parse_AsQueryString_With_Empty_Collection()
        {
            // Arrange
            var expected = string.Empty;
            var sut = new Dictionary<string, string>();

            // Act
            var actual = sut.AsQueryString();

            // Asserts
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Parse_AsQueryString_Ok()
        {
            // Arrange
            var expected = "&key1=value1&key2=value2&key3=value3";
            var sut = new Dictionary<string, string>()
            {
                { "key1", "value1" },
                { "key2", "value2" },
                { "key3", "value3" },
            };

            // Act
            var actual = sut.AsQueryString();

            // Asserts
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
