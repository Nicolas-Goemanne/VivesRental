using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivesRental.Enums;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class ArticleSdk
    {
        private readonly IArticleService _articleService;

        public ArticleSdk(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public Task<ArticleResult?> Get(Guid id)
        {
            return _articleService.Get(id);
        }

        public Task<List<ArticleResult>> Find(ArticleFilter? filter)
        {
            return _articleService.Find(filter);
        }

        public Task<ArticleResult?> Create(ArticleRequest entity)
        {
            return _articleService.Create(entity);
        }

        public Task<bool> UpdateStatus(Guid articleId, ArticleStatus status)
        {
            return _articleService.UpdateStatus(articleId, status);
        }

        public Task<bool> Remove(Guid id)
        {
            return _articleService.Remove(id);
        }
    }
}
