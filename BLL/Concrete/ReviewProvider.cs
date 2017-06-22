using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using BLL.Abstract;
using DAL.Concrete;
using DAL.Entity;
using DAL.Abstract;

namespace BLL.Concrete
{
    public class ReviewProvider : IReviewProvider
    {
        IReviewRepository reviewRepository = new ReviewRepository();
        IUserRepository userRepository = new UserRepository();

        public void AddReview(string message, int bookId, int userId)
        {
            Review review = new Review
            {
                Message = message,
                Date = DateTime.Now,
                BookId = bookId,
                UserId = userId,
            };

            reviewRepository.AddReview(review);
        }

        public List<ReviewViewModel> GetBookReviews(int bookId)
        {
            return reviewRepository.GetBookReviews(bookId).Select(r => new ReviewViewModel()
            {
                Message = r.Message,
                Date = r.Date,
                Likes = r.ReviewLikes.Count,
                UserName = r.User.UserName,
                BookName = r.Book.BookName
            }).ToList();
        }

        public List<ReviewViewModel> GetUserReviews(int userId)
        {
            return reviewRepository.GetUserReviews(userId).Select(r => new ReviewViewModel()
            {
                Message = r.Message,
                Date = r.Date,
                Likes = r.ReviewLikes.Count,
                UserName = r.User.UserName,
                BookName = r.Book.BookName
            }).ToList();
        }

        public void PressLikeButton(string userEmail, int reviewId)
        {
            if (reviewRepository.GetReviewById(reviewId).ReviewLikes.Select(l => l.User.UserEmail).Contains(userEmail))
            {
                reviewRepository.RemoveLike(reviewRepository.GetReviewById(reviewId).ReviewLikes.FirstOrDefault(l => l.User.UserEmail == userEmail));
            }
            else
            {
                reviewRepository.AddLike(new Like()
                {
                    ReviewId = reviewId,
                    UserId = userRepository.GetUserByEmail(userEmail).UserId
                });
            }
        }
    }
}
