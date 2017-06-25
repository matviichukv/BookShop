using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;
using System.Windows.Controls;

namespace BLL.Abstract
{
    public interface IReviewProvider
    {
        void AddReview(string message, int bookId, int userId);
        List<ReviewViewModel> GetUserReviews(int userId);
        List<ReviewViewModel> GetBookReviews(int bookId);
    }
}
