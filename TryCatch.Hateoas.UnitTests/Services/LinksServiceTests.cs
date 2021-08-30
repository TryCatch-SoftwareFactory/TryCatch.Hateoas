// <copyright file="LinksServiceTests.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace TryCatch.Hateoas.UnitTests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FluentAssertions;
    using Microsoft.AspNetCore.Http;
    using NSubstitute;
    using TryCatch.Hateoas.Models;
    using TryCatch.Hateoas.Services;
    using Xunit;

    public class LinksServiceTests
    {
        private readonly HttpContext httpContext;

        private readonly HttpRequest httpRequest;

        private readonly IPagingEngine pagingEngine;

        private readonly IHttpContextAccessor httpContextAccessor;

        private readonly ILinksService sut;

        public LinksServiceTests()
        {
            var uri = new Uri("https://localhost/api", UriKind.Absolute);
            var host = HostString.FromUriComponent(uri);
            var path = PathString.FromUriComponent(uri);
            this.httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            this.httpContext = Substitute.For<HttpContext>();
            this.httpRequest = Substitute.For<HttpRequest>();
            this.httpRequest.Host.Returns(host);
            this.httpRequest.Path.Returns(path);
            this.httpRequest.Scheme.Returns("https");
            this.httpContext.Request.Returns(this.httpRequest);
            this.httpContextAccessor.HttpContext.Returns(this.httpContext);            
            this.pagingEngine = new PagingEngine();            
            this.sut = new LinksService(this.pagingEngine, this.httpContextAccessor);
        }

        [Fact]
        public void Construct_without_pagingEngine()
        {
            // Arrange
            IPagingEngine pagingEngine = null;

            // Act
            Action actual = () => _ = new LinksService(pagingEngine, this.httpContextAccessor);

            // Asserts
            actual.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Construct_without_httpContextAccessor()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = null;

            // Act
            Action actual = () => _ = new LinksService(this.pagingEngine, httpContextAccessor);

            // Asserts
            actual.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Construct_without_request()
        {
            // Arrange
            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            var httpContext = Substitute.For<HttpContext>();
            httpContext.Request.Returns(null as  HttpRequest);
            httpContextAccessor.HttpContext.Returns(httpContext);

            // Act
            Action actual = () => _ = new LinksService(this.pagingEngine, httpContextAccessor);

            // Asserts
            actual.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Construct_without_host_value()
        {
            // Arrange            
            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            var httpContext = Substitute.For<HttpContext>();
            var httpRequest = Substitute.For<HttpRequest>();            
            httpContext.Request.Returns(httpRequest);
            httpContextAccessor.HttpContext.Returns(httpContext);

            // Act
            Action actual = () => _ = new LinksService(this.pagingEngine, httpContextAccessor);

            // Asserts
            actual.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Construct_with_port()
        {
            // Arrange
            var host = HostString.FromUriComponent(new Uri("https://localhost:5001"));
            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            var httpContext = Substitute.For<HttpContext>();
            var httpRequest = Substitute.For<HttpRequest>();
            httpRequest.Host.Returns(host);
            httpRequest.Scheme.Returns("https");
            httpContext.Request.Returns(httpRequest);            
            httpContextAccessor.HttpContext.Returns(httpContext);            
            var services = new LinksService(this.pagingEngine, httpContextAccessor);
            var identity = Guid.NewGuid().ToString();
            var templates = new HashSet<LinkInfo>()
            {
                new LinkInfo("self", "GET")
            };

            // Act
            var links = services.GetEntityLinks(templates, identity);

            // Asserts
            links.Should().HaveCount(1);
            links.First().Href.Should().Be($"https://localhost:5001/{identity}");
        }

        [Fact]
        public void Construct_without_port()
        {
            // Arrange
            var host = HostString.FromUriComponent(new Uri("http://localhost"));
            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            var httpContext = Substitute.For<HttpContext>();
            var httpRequest = Substitute.For<HttpRequest>();
            httpRequest.Host.Returns(host);
            httpRequest.Scheme.Returns("http");
            httpContext.Request.Returns(httpRequest);
            httpContextAccessor.HttpContext.Returns(httpContext);
            var services = new LinksService(this.pagingEngine, httpContextAccessor);
            var identity = Guid.NewGuid().ToString();
            var templates = new HashSet<LinkInfo>()
            {
                new LinkInfo("self", "GET")
            };

            // Act
            var links = services.GetEntityLinks(templates, identity);

            // Asserts
            links.Should().HaveCount(1);
            links.First().Href.Should().Be($"http://localhost/{identity}");
        }

        [Fact]
        public void Construct_with_path()
        {
            // Arrange
            var uri = new Uri("https://localhost:5001/api/items");
            var host = HostString.FromUriComponent(uri);
            var path = PathString.FromUriComponent(uri);
            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            var httpContext = Substitute.For<HttpContext>();
            var httpRequest = Substitute.For<HttpRequest>();
            httpRequest.Host.Returns(host);
            httpRequest.Path.Returns(path);
            httpRequest.Scheme.Returns("https");
            httpContext.Request.Returns(httpRequest);
            httpContextAccessor.HttpContext.Returns(httpContext);
            var services = new LinksService(this.pagingEngine, httpContextAccessor);
            var identity = Guid.NewGuid().ToString();
            var templates = new HashSet<LinkInfo>()
            {
                new LinkInfo("self", "GET")
            };

            // Act
            var links = services.GetEntityLinks(templates, identity);

            // Asserts
            links.Should().HaveCount(1);
            links.First().Href.Should().Be($"https://localhost:5001/api/items/{identity}");
        }

        [Fact]
        public void GetEntityLinks_Without_Templates()
        {
            // Arrange
            IEnumerable<LinkInfo> templates = null;
            var identity = Guid.NewGuid().ToString();

            // Act
            Action actual = () => _ = this.sut.GetEntityLinks(templates, identity);

            // Asserts
            actual.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [MemberData(memberName: nameof(Given.EntityLinksInput), MemberType = typeof(Given))]
        public void GetEntityLinks_Ok(IEnumerable<LinkInfo> templates, string identity, IEnumerable<Link> expected)
        {
            // Arrange

            // Act
            var actual = this.sut.GetEntityLinks(templates, identity);

            // Asserts
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(0, 1, 0)]
        [InlineData(1, 0, 0)]
        [InlineData(1, 1, -1)]
        public void GetPageLinks_with_invalid_arguments(int offset, int limit, long total)
        {
            // Arrange

            // Act
            Action actual = () => _ = this.sut.GetPageLinks(offset, limit, total);

            // Asserts
            actual.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [MemberData(memberName: nameof(Given.PageLinksInputWithDefaultValues), MemberType = typeof(Given))]
        public void GetPageLinks_with_defaultValues(int offset, int limit, long total, IEnumerable<Link> expected)
        {
            // Arrange            

            // Act
            var actual = this.sut.GetPageLinks(offset, limit, total).OrderBy(x => x.Rel);

            // Asserts
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(memberName: nameof(Given.PageLinksInput), MemberType = typeof(Given))]
        public void GetPageLinks_with_customValues(
            int offset,
            int limit,
            long total,
            IDictionary<string, string> defaultQueryParams,
            IEnumerable<LinkInfo> templates, 
            IEnumerable<Link> expected)
        {
            // Arrange            

            // Act
            var actual = this.sut.GetPageLinks(offset, limit, total, defaultQueryParams, templates).OrderBy(x => x.Rel);

            // Asserts
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        public void GetNextPageLinks_with_invalid_arguments(int offset, int limit)
        {
            // Arrange

            // Act
            Action actual = () => _ = this.sut.GetNextPageLinks(offset, limit);

            // Asserts
            actual.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [MemberData(memberName: nameof(Given.NextPageLinksInputWithDefaultValues), MemberType = typeof(Given))]
        public void GetNextPageLinks_with_defaultValues(int offset, int limit, IEnumerable<Link> expected)
        {
            // Arrange            

            // Act
            var actual = this.sut.GetNextPageLinks(offset, limit).OrderBy(x => x.Rel);

            // Asserts
            actual.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [MemberData(memberName: nameof(Given.NextPageLinksInput), MemberType = typeof(Given))]
        public void GetNextPageLinks_with_customValues(
            int offset,
            int limit,
            IDictionary<string, string> defaultQueryParams,
            IEnumerable<LinkInfo> templates,
            IEnumerable<Link> expected)
        {
            // Arrange            

            // Act
            var actual = this.sut.GetNextPageLinks(offset, limit, defaultQueryParams, templates).OrderBy(x => x.Rel);

            // Asserts
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
