using Farkas_Zoltán_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Farkas_Zoltán_backend.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet("feladat10")]
        public IActionResult Get()
        {
            using (var context = new LibrarydbContext())
            {
                try
                {
                    return Ok(context.Books.Select(book => new
                    {
                        book.BookId,
                        book.Title,
                        book.PublishDate,
                        book.AuthorId,
                        book
                    .CategoryId
                    }).ToList());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpPost("feladat13")]
        public IActionResult Get5(string uid, Book book)
        {
            string userid = "FKB3F4FEA09CE43C";
            using (var context = new LibrarydbContext())
            {
                try
                {
                    if (userid == uid)
                    {
                        var result = context.Books.Add(book);
                        context.SaveChanges();

                        CreatedAtAction(nameof(AuthorsController.Get), new
                        {
                            id = book.BookId
                        }, book);

                        return StatusCode(201, "Könyv hozzáadása sikeresen megtörtént.");
                    }
                    else
                    {
                        return StatusCode(401, "Nincs jogosultsága új könyv felvételéhez!");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        [HttpGet("feladat20")]
        public IActionResult GetAllData()
        {
            using (var context = new LibrarydbContext())
            {
                try
                {
                    var result = context.Authors
                    .Include(book => book.Books)
                    .ThenInclude(category => category.Category)
                    .Select(bookDatas => new
                    {
                        Name = bookDatas.AuthorName,
                        Books = bookDatas.Books.Select(book => new
                        {
                            BookTitle = book.Title,
                            BookCategory = book.Category.CategoryName,
                            BookPublish = book.PublishDate
                        }).ToList()
                    })
                        .ToList();
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
