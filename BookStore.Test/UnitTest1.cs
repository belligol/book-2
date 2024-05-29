using BookStore_BL.Interfaces;
using BookStore_BL.Services;
using BookStore_DL.Interfaces;
using BookStore_DL.Repositories;
using BookStore_Models.Models;
using Microsoft.AspNetCore.Components.Forms;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Xunit;

namespace BookStore.Test
{
    public class LibraryServiceTests
    {
        public static List<Book> BookData = new List<Book>()
        {
            new Book()
            {
                Id = new Guid(),
                Title = "Book 1",
                AuthorId = 2
            },
            new Book()
            {
                Id = new Guid(),
                Title = "Book 2",
                AuthorId = 3
            }
        };

        public static List<Author> AuthorData = new List<Author>()
        {
            new Author()
            {
                Id = 1,
                Name = "Author 1",
                BirthDay = DateTime.Now
            },
              new Author()
            {
                Id = 2,
                Name = "Author 2",
                BirthDay = DateTime.Now
            },
                new Author()
            {
                Id = 3,
                Name = "Author 3",
                BirthDay = DateTime.Now
            }
        };

        [Fact]
        public async Task CheckBookCount_OK()
        {
            // setup
            var request = new GetAllBookByAuthorRequest
            {
                AuthorId = 2,
                AfterDate = DateTime.Now,
            };

            var expectedCount = 1;

            var mockedAuthorService = new Mock<IAuthorService>();
            var mockedBookService = new Mock<IBookService>();

            mockedAuthorService.Setup(x => x.GetById(request.AuthorId))
                               .ReturnsAsync(AuthorData.FirstOrDefault(a => a.Id == request.AuthorId));

            mockedBookService.Setup(x => x.GetAllByAuthorId(request.AuthorId))
                             .ReturnsAsync(BookData.Where(b => b.AuthorId == request.AuthorId).ToList());

            var service = new LibraryService(mockedAuthorService.Object, mockedBookService.Object);

            // act
            var result = await service.GetAllBookByAuthorAfterDate(request);

            // assert
            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Books.Count);
            Assert.Equal(request.AuthorId, result.Author.Id);
        }


        [Fact]
        public async Task CheckAuthorCount_OKAsync()
        {
            // Setup
            var input = 10;
            var expectedCount = 13;

            var mockedAuthorRepository = new Mock<IAuthorRepository>();
            mockedAuthorRepository.Setup(x => x.GetAll()).Returns(Task.FromResult(AuthorData));

            var bookService = new BookService(new Mock<IBookRepository>().Object); // Inject a mock IBookRepository
            var authorService = new AuthorService(mockedAuthorRepository.Object);
            var service = new LibraryService(authorService, bookService);

            // Act
            // Act
            var resultTask = service.CheckAuthorCount(input);
            var result = await resultTask;

            // Assert
            Assert.Equal(expectedCount, result);
        }

        [Fact]
        public async Task GetAllBookByAuthorAfterDate_OK()
        {
            //setup
            var request = new GetAllBookByAuthorRequest
            {
                AuthorId = 2,
                AfterDate = DateTime.Now,
            };

            var expectedCount = 1;

            var mockedAuthorRepository = new Mock<IAuthorRepository>();
            var mockedBookRepository = new Mock<IBookRepository>();

            mockedAuthorRepository.Setup(
                x => x.GetById(request.AuthorId))
                .ReturnsAsync(AuthorData!.FirstOrDefault(a => a.Id == request.AuthorId));

            mockedBookRepository.Setup(
                x => x.GetAllByAuthorId(request.AuthorId))
                .ReturnsAsync(BookData
                    .Where(b => b.AuthorId == request.AuthorId)
                    .ToList());

            //inject
            var bookService = new BookService(mockedBookRepository.Object);
            var authorService = new AuthorService(mockedAuthorRepository.Object);
            var service = new LibraryService(authorService, bookService);

            //act
            var result = await service.GetAllBookByAuthorAfterDate(request);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Books.Count);
            Assert.Equal(request.AuthorId, result.Author.Id);
        }

    }
}