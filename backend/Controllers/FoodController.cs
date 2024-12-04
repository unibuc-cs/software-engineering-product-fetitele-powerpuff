﻿using healthy_lifestyle_web_app.Entities;
using healthy_lifestyle_web_app.Repositories;
using healthy_lifestyle_web_app.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;

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

        // POST pentru a adăuga un articol nou
        [HttpPost]
        public async Task<IActionResult> PostArticle([FromBody] CreateArticleDTO articleDTO)
        {
            // Verifică dacă există deja un articol cu același titlu
            var existingArticle = await _articleRepository.GetByTitleAsync(articleDTO.Title);
            if (existingArticle != null)
            {
                return BadRequest("An article with this title already exists.");
            }

            // Creează articolul pe baza DTO-ului
            var article = _mapper.Map<Article>(articleDTO);

            // Adaugă articolul în baza de date
            bool result = await _articleRepository.AddArticleAsync(article);

            if (result)
            {
                return Ok("Article added successfully");
            }
            return BadRequest("Failed to add the article.");
        }
    }
}
