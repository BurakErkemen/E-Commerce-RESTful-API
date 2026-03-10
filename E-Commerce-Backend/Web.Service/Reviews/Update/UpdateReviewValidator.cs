using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Service.Users.Update;

namespace Web.Service.Reviews.Update
{
    public class UpdateReviewValidator : AbstractValidator<UpdateReviewRequest>
    {
        public UpdateReviewValidator() {

            RuleFor(x => x.ReviewRating)
                .NotEmpty().WithMessage("Rating is required!");

            RuleFor(x => x.ReviewComment)
                .NotEmpty().WithMessage("Comment is required!");
        }
    }
}
