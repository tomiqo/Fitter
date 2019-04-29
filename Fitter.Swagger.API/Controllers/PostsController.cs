using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitter.BL.Factories;
using Fitter.BL.Mapper;
using Fitter.BL.Mapper.Interface;
using Fitter.BL.Model;
using Fitter.BL.Repositories;
using Fitter.BL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Swashbuckle.AspNetCore.Annotations;

namespace Fitter.Swagger.API.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly IPostsRepository _postsRepository;

        public PostsController()
        {
            _dbContextFactory = new DbContextFactory();
            _mapper = new Mapper();
            _postsRepository = new PostsRepository(_dbContextFactory, _mapper);
        }

        [HttpPost]
        [Route("create")]
        [SwaggerOperation(OperationId = "CreatePost")]
        public ActionResult Create(PostModel post)
        {
            _postsRepository.Create(post);
            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        [SwaggerOperation(OperationId = "DeletePost")]
        public ActionResult Delete(Guid id)
        {
            _postsRepository.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("getPostsForTeam")]
        [SwaggerOperation(OperationId = "GetPostsForTeam")]
        public ActionResult<IList<PostModel>> GetPostsForTeam(Guid id)
        {
            return _postsRepository.GetPostsForTeam(id).ToList();
        }
    }
}