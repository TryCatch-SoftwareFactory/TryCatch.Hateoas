// <copyright file="BooksController.cs" company="TryCatch Software Factory">
// Copyright © TryCatch Software Factory All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
// </copyright>

namespace MyBooksApi.Features.Books
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using MyBooksApi.Features.Books.Services;
    using MyBooksApi.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksMapper mapper;
        private readonly IBooksService service;

        public BooksController(IBooksMapper mapper, IBooksService service)
        {
            this.mapper = mapper;
            this.service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var book = await this.service.GetById(id).ConfigureAwait(false);

            var bookModel = this.mapper.MapTo(book);

            return this.Ok(bookModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetPage([FromQuery] PageFilter filter)
        {
            var page = await this.service.GetBooksPage(filter).ConfigureAwait(false);

            var pageModel = this.mapper.MapTo(page);

            return this.Ok(pageModel);
        }

        [HttpGet("next")]
        public async Task<IActionResult> GetNextPage([FromQuery] PageFilter filter)
        {
            var page = await this.service.GetBooksNextPage(filter).ConfigureAwait(false);

            var pageModel = this.mapper.MapTo(page);

            return this.Ok(pageModel);
        }
    }
}
