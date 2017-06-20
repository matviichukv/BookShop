using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entity;

namespace DAL.Abstract
{
    public interface IReviewRepository
    {
        void AddReview(Review review);
        List<Review> GetUserReviews(int userId);
        List<Review> GetBookReviews(int bookId);
    }
}
