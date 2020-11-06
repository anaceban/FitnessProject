using ApplicationFitness.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;

namespace WebApi.Services
{
    public interface IUserReviewService
    {
        IList<Review> GetReviews();

        IList<Review> GetReviewsByUserId(int id);

        Review AddNewReview(ReviewDto dto);

        Review UpdateReview(int id, ReviewDto dto);

        bool RemoveReview(int id);
    }
}
