using ApplicationFitness.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Repositories;

namespace WebApi.Services
{
    public class UserReviewService : IUserReviewService
    {
        private IRepository<Review> _repositoryReview;
        public UserReviewService(IRepository<Review> repository) 
        {
            _repositoryReview = repository;
        }

        public Review AddNewReview(ReviewDto dto)
        {
            var review = new Review
            {
                UserId = dto.UserId,
                Comment = dto.Comment
            };
            _repositoryReview.Add(review);
            _repositoryReview.Save();
            return review;
        }

        public IList<Review> GetReviews()
        {
            return _repositoryReview.GetAll().ToList();
        }

        public IList<Review> GetReviewsByUserId(int UserId)
        {
            var reviews = _repositoryReview.GetAll().Where(r => r.UserId == UserId);
            return reviews.ToList();
        }

        public bool RemoveReview(int id)
        {
            var review = _repositoryReview.Find(id);
            if (review == null)
            {
                return false;
            }
            else
            {
                _repositoryReview.Delete(review);
                _repositoryReview.Save();
                return true;
            }
        }

        public Review UpdateReview(int id, ReviewDto dto)
        {
            var review = _repositoryReview.Find(id);
            if(review == null)
            {
                return null;
            }
            review.Comment = dto.Comment;
            _repositoryReview.Save();
            return review;
        }
    }
}
