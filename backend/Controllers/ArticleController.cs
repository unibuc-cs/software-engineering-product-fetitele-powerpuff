using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Repositories;
using healthy_lifestyle_web_app.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace healthy_lifestyle_web_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public ArticleController(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

       
        [HttpPost]
        [Authorize(Roles = "Admin")]  
        public async Task<IActionResult> PostArticle([FromBody] CreateArticleDTO articleDTO)
        {
            
            var existingArticle = await _articleRepository.GetByTitleAsync(articleDTO.Title);
            if (existingArticle != null)
            {
                return BadRequest("An article with this title already exists.");
            }

            var article = _mapper.Map<Article>(articleDTO);

           
            bool result = await _articleRepository.AddArticleAsync(article);

            if (result)
            {
                return Ok("Article added successfully");
            }
            return BadRequest("Failed to add the article.");
        }



        [HttpDelete("{title}")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> DeleteArticleByTitle(string title)
        {
           
            var article = await _articleRepository.GetByTitleAsync(title);

            if (article == null)
            {
                return NotFound("Article not found.");
            }

            bool result = await _articleRepository.DeleteArticleAsync(article);

            if (result)
            {
                return Ok("Article deleted successfully.");
            }

            return BadRequest("Failed to delete the article.");
        }

    }
}
