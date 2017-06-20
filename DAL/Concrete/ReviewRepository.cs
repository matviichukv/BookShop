using DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entity;

namespace DAL.Concrete
{
    public class ReviewRepository : IReviewRepository
    {
        BookShopContext ctx = new BookShopContext();

        public void AddReview(Review review)
        {
            ctx.Reviews.Add(review);
            ctx.SaveChanges();
        }

        public List<Review> GetBookReviews(int bookId)
        {
            return ctx.Reviews
                .Include(r => r.User).Include(i => i.ReviewLikes).Where(i => i.BookId == bookId).ToList();
        }

        public List<Review> GetUserReviews(int userId)
        {
            return ctx.Reviews
                .Include(r => r.User)
                .Where(i => i.UserId == userId).ToList();
        }
    }
}
