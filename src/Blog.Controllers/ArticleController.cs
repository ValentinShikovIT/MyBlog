using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Controllers.Infrastructure;
using MyBlog.Controllers.Models.Articles;
using MyBlog.Services;
using MyBlog.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBlog.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly IArticleService articlesService;
        private readonly IMapper mapper;

        public ArticlesController(IArticleService articlesService, IMapper mapper)
        {
            this.articlesService = articlesService;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await articlesService.All(1);

            return this.View(mapper.Map<IEnumerable<ArticleListingServiceModel>, IEnumerable<ArticleDisplayModel>>(articles));
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
            => View();

        [HttpPost]
        [Authorize(Roles = "Admin,Regular")]
        public async Task<IActionResult> Create(ArticleFormModel model)
        {
            if (ModelState.IsValid)
            {
                var articleId = await this.articlesService.CreateAsync(
                    model.Title,
                    model.Description,
                    this.User.GetUserId());

                return RedirectToAction(nameof(Index));
            }

            return this.View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            if (!await this.articlesService.Exists(id, this.User.GetUserId()))
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, ArticleFormModel model)
        {
            if (!await this.articlesService.Exists(id, this.User.GetUserId()))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await this.articlesService.Edit(id, model.Title, model.Description);

                return RedirectToAction(nameof(Details), new { id });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var article = await this.articlesService.Details(id);

            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteById(int id)
        {
            if (!await this.articlesService.Exists(id, this.User.GetUserId()))
            {
                return NotFound();
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            if (!await this.articlesService.Exists(id, this.User.GetUserId()))
            {
                return NotFound();
            }

            await this.articlesService.Remove(id);

            return Redirect(nameof(Index));
        }
    }
}
