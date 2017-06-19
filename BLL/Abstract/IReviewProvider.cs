using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Abstract
{
    public interface IReviewProvider
    {
        void AddReview(string message, int bookId, int userId);
    }
}
