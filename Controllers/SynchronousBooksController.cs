﻿using Books.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.API.Controllers
{
    [Route("api/synchronousbooks")]
    public class SynchronousBooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;

        public SynchronousBooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
        }

        public IActionResult GetBooks()
        {
            var bookEntities = _booksRepository.GetBooks();
            return Ok(bookEntities);
        }
    }
}
