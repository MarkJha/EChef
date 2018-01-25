using eCafe.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eCafe.Infrastructure.Services.Abstract
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetReviewAsync(int pageSize = 10, int pageNumber = 1, string name = null);

        Task<ReviewDto> CreateReviewAsync(ReviewDto model);

        Task<ReviewDto> DeleteReviewAsync(int id);

    }
}
