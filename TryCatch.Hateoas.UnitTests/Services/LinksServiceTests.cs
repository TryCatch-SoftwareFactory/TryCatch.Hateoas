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
            var host = HostString.FromUriComponent(new Uri("https://localhost"));
            this.httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            this.httpContext = Substitute.For<HttpContext>();
            this.httpRequest = Substitute.For<HttpRequest>();            
            this.httpRequest.Host.Returns(host);
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
