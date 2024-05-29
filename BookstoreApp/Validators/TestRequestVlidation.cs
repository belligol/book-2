using BookStore_Models.Models.Requests;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace BookstoreApp.Validators
{
    public class TestRequestValidation : AbstractValidator<GetAllBookByAuthorRequest>
    {

        public TestRequestValidation()
        {
            RuleFor(x => x.AuthorId)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0).WithMessage("> 0");

            RuleFor(x => x.AfterDate)
                .NotEmpty()
                .NotNull();

        }
    }
}