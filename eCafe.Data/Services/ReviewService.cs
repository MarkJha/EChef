using System.Collections.Generic;
using System.Threading.Tasks;
using eCafe.Infrastructure.Models;
using eCafe.Infrastructure.Services.Abstract;
using eCafe.Infrastructure.Repository.Interface;
using eCafe.Infrastructure.Mapper;
using eCafe.Core.Entities;
using System.Linq;

namespace eCafe.Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<ReviewDto> CreateReviewAsync(ReviewDto model)
        {
            var entity = await _reviewRepository
                        .InsertAsync(model.ToEntity());

            return entity.ToViewModel();
        }

        public async Task<ReviewDto> DeleteReviewAsync(int id)
        {
            var entity = await _reviewRepository
                       .DeleteAsync(new Review { Id = id });

            return entity.ToViewModel();
        }

        public async Task<IEnumerable<ReviewDto>> GetReviewAsync(int pageSize = 10, int pageNumber = 1, string name = null)
        {
            var model = await _reviewRepository
                                .AllIncludingAsync(r => r.SubMenuDetail);

            return model
                        .OrderByDescending(a => a.Id)
                        .Skip(pageNumber - 1 * pageSize)
                        .Take(pageSize)
                        .Select(item => item.ToViewModel())
                        .ToList();
        }
    }
}