
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMedia.Api.Responses;
using SocialMedia.Core.CustonEntities;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService; // agregada para manejo url en paginacion

        public PostController(IPostService postService, IMapper mapper, IUriService uriService)
        {
            _postService = postService;
            _mapper = mapper;
            _uriService = uriService;
        }

        // acontinuacion es documentacion se agrega con ///

        /// <summary>
        /// Retrieve all posts
        /// </summary>
        /// <param name="filters">Filters to apply</param>
        /// <returns></returns>/// 

        [HttpGet(Name = "GetPosts")] //en el video lo puso (Name = nameof(GetPosts)) ami me dava error
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<PostDTO>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public IActionResult Consultas([FromQuery]PostQueryFilter filters)
        {
            var posts = _postService.GetPosts(filters);
            var postsDTO = _mapper.Map<IEnumerable<PostDTO>>(posts);  /*IEnumerable<PostDTO> es cambiado por PagedList<PostDTO>
                                                                    * para implementar la paginacion o tambien se puede dejar normalmente*/


            var metadata = new Metadata
            {
                TotalCount = posts.TotalCount,
                PageSize = posts.PageSize,
                CurrentPage = posts.CurrentPage,
                TotalPage = posts.TotalPages,
                HasNextPage = posts.HasNextPage,
                HasPreviousPage = posts.HasPreviousPage,
                HasNextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl("GetPosts")).ToString(),
                HasPreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl("GetPosts")).ToString()
            };
            var response = new ApiResponse<IEnumerable<PostDTO>>(postsDTO) 
            {
                Meta = metadata
            };// objeto metadata agregado en paginacion con objeto tipado metadata

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }


        [HttpGet ("{id}")]
        public async Task<IActionResult> Consulta(int id)
        {
            
            var post = await _postService.GetPost(id);
            var postDTO = _mapper.Map<PostDTO>(post);

            var response = new ApiResponse<PostDTO>(postDTO);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Agregar(PostDTO postDTO)
        {
            var post = _mapper.Map<Post>(postDTO);

            await _postService.PostCrear(post);

            postDTO = _mapper.Map<PostDTO>(post);
            var response = new ApiResponse<PostDTO>(postDTO);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar(int id, PostDTO postDTO)
        {
            var post = _mapper.Map<Post>(postDTO);
            post.Id = id;
            var result = await _postService.PostUpdate(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            
            var result = await _postService.PostDelete(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
