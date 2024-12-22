using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class ArticleReservationSdk
    {
        private readonly IArticleReservationService _articleReservationService;

        public ArticleReservationSdk(IArticleReservationService articleReservationService)
        {
            _articleReservationService = articleReservationService;
        }

        public Task<ArticleReservationResult?> Get(Guid id)
        {
            return _articleReservationService.Get(id);
        }

        public Task<List<ArticleReservationResult>> Find(ArticleReservationFilter? filter)
        {
            return _articleReservationService.Find(filter);
        }

        public Task<ArticleReservationResult?> Create(ArticleReservationRequest request)
        {
            return _articleReservationService.Create(request);
        }

        public Task<bool> Remove(Guid id)
        {
            return _articleReservationService.Remove(id);
        }
    }
}
