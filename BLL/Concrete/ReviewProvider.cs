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

        public void AddReview(string message, int bookId, int userId)
        {
            Review review = new Review
            {
                Message = message,
                Date = DateTime.Now,
                ThumbUp = 0,
                ThumbDown = 0,
                BookId = bookId,
                UserId = userId
            };

            reviewRepository.AddReview(review);
        }
    }
}
