﻿using System;
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
using Swashbuckle.AspNetCore.Annotations;

namespace Fitter.Swagger.API.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ICommentsRepository _commentsRepository;

        public CommentsController()
        {
            _dbContextFactory = new DbContextFactory();
            _mapper = new Mapper();
            _commentsRepository = new CommentsRepository(_dbContextFactory, _mapper);
        }

        [HttpPost]
        [Route("create")]
        [SwaggerOperation(OperationId = "CreateComment")]
        public ActionResult Create(CommentModel comment)
        {
            _commentsRepository.Create(comment);
            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        [SwaggerOperation(OperationId = "DeleteComment")]
        public ActionResult Delete(Guid id)
        {
            _commentsRepository.Delete(id);
            return Ok();
        }

        [HttpPost]
        [Route("tagUsers")]
        [SwaggerOperation(OperationId = "CommentTagUsers")]
        public ActionResult TagUsers(List<UserDetailModel> users, Guid id)
        {
            _commentsRepository.TagUsers(users, id);
            return Ok();
        }

        [HttpGet]
        [Route("getCommentsForPost")]
        [SwaggerOperation(OperationId = "GetCommentsForPost")]
        public ActionResult<IList<CommentModel>> GetCommentsForPost(Guid id)
        {
            return _commentsRepository.GetCommentsForPost(id).ToList();
        }

        [HttpGet]
        [Route("getTagsForComment")]
        [SwaggerOperation(OperationId = "GetTagsForComment")]
        public ActionResult<IList<UserListModel>> GetTagsForComment(Guid id)
        {
            return _commentsRepository.GetTagsForComment(id).ToList();
        }
    }
}