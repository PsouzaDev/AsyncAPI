﻿using Books.API.Context;
using Books.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Books.API.Services
{
    public class BooksRepository : IBooksRepository, IDisposable
    {
        private BooksContext _context;

        public BooksRepository(BooksContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Book> GetBookAsync(Guid id)
        {
            return await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id); 
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            await _context.Database.ExecuteSqlCommandAsync("WAITFOR DELAY '00:00:02';");
            return await _context.Books.Include(b => b.Author).ToListAsync(); 
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        public IEnumerable<Book> GetBooks()
        {
            _context.Database.ExecuteSqlCommand("WAITFOR DELAY '00:00:02';");
            return _context.Books.Include(b => b.Author).ToList();
        }

        public Book GetBook(Guid id)
        {
            return _context.Books.Include(b => b.Author).FirstOrDefault(b => b.Id == id);
        }
    }
}
