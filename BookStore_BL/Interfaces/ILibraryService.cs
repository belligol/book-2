
namespace BookStore_BL.Interfaces
{
    public interface ILibraryService
    {
        public Task<GetAllBookByAuthorResponse>
            GetAllBookByAuthorAfterDate(GetAllBookByAuthorRequest request);
    }

}

