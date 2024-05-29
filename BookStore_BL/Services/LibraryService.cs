
using BookStore_BL.Services;
using BookStore_BL.Interfaces;
using BookStore_Models.Models;

public class LibraryService : ILibraryService
{
    private readonly IBookService _bookService;
    private readonly  IAuthorService _authorService;
    public LibraryService(IAuthorService authorService, IBookService bookService)
    {
        _authorService = authorService;
        _bookService = bookService;
    }

    public async Task<GetAllBookByAuthorResponse> GetAllBookByAuthorAfterDate(GetAllBookByAuthorRequest request)
    {
        var result = new GetAllBookByAuthorResponse();
        result.Author = await _authorService.GetById(request.AuthorId);
        result.Books = await _bookService.GetAllByAuthorId(request.AuthorId);
        return result;
    }

    public async Task<int> CheckAuthorCount(int input)
    {
        var authors = await _authorService.GetAll();
        var authorCount = authors.Count;

        return input + authorCount;
    }

}
