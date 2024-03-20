﻿using ElasticSearch.API.Dto;
using ElasticSearch.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace ElasticSearch.API.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductCreateDto request)
        {
            return CreateActionResult(await _productService.SaveAsync(request));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResult(await _productService.GetAllAsync());
        }
    }
}
