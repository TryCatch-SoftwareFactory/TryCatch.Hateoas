// <copyright file="BooksQueriesTests.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.FunctionalTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc.Testing;
    using MyBooksApi;
    using Newtonsoft.Json;
    using TryCatch.Hateoas.FunctionalTests.Models;
    using Xunit;

    public class BooksQueriesTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private const string BaseUri = "/api/books";

        private readonly WebApplicationFactory<Startup> factory;

        public BooksQueriesTests(WebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Theory]
        [MemberData(memberName: nameof(Given.PageQueriesInput), MemberType = typeof(Given))]
        public async Task GetPage_ok(IDictionary<string, string> queryParams, BooksPage expected)
        {
            // Arrange
            var queryString = string.Join("&", queryParams.Select(x => $"{x.Key}={x.Value}"));
            var uri = new Uri($"{BaseUri}?{queryString}", UriKind.RelativeOrAbsolute);
            var client = this.factory.CreateClient();

            // Act
            var response = await client.GetAsync(uri).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var actual = JsonConvert.DeserializeObject<BooksPage>(content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().NotBeNull();
            actual.Limit.Should().Be(expected.Limit);
            actual.Offset.Should().Be(expected.Offset);
            actual.Matched.Should().Be(expected.Matched);
            actual.Count.Should().Be(expected.Count);
            actual.Items.Should().BeEquivalentTo(expected.Items);
            actual.Links.OrderBy(x => x.Rel).Should().BeEquivalentTo(expected.Links.OrderBy(x => x.Rel));
        }

        [Theory]
        [MemberData(memberName: nameof(Given.PageNextQueriesInput), MemberType = typeof(Given))]
        public async Task GetNextPage_ok(IDictionary<string, string> queryParams, BooksNextPage expected)
        {
            // Arrange
            var queryString = string.Join("&", queryParams.Select(x => $"{x.Key}={x.Value}"));
            var uri = new Uri($"{BaseUri}/next?{queryString}", UriKind.RelativeOrAbsolute);
            var client = this.factory.CreateClient();

            // Act
            var response = await client.GetAsync(uri).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var actual = JsonConvert.DeserializeObject<BooksNextPage>(content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().NotBeNull();
            actual.Limit.Should().Be(expected.Limit);
            actual.Offset.Should().Be(expected.Offset);
            actual.Items.Should().BeEquivalentTo(expected.Items);
            actual.Links.OrderBy(x => x.Rel).Should().BeEquivalentTo(expected.Links.OrderBy(x => x.Rel));
        }

        [Theory]
        [MemberData(memberName: nameof(Given.BookInput), MemberType = typeof(Given))]
        public async Task GetBook_ok(int bookId, Book expected)
        {
            // Arrange
            var uri = new Uri($"{BaseUri}/{bookId}", UriKind.RelativeOrAbsolute);
            var client = this.factory.CreateClient();

            // Act
            var response = await client.GetAsync(uri).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var actual = JsonConvert.DeserializeObject<Book>(content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            actual.Should().NotBeNull();
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
