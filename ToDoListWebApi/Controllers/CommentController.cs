using AutoMapper;
using FluentValidation;
using ToDoList.Comments.Controllers;
using ToDoList.Comments.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoListWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<CommentDto>>> GetAllAsync()
        {
            try
            {
                var comments = await _commentService.GetAllAsync();
                var result = _mapper.Map<List<CommentDto>>(comments);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
        }

        [HttpGet]
        [Route("ByTask/{taskId}")]
        public async Task<ActionResult<List<CommentDto>>> GetAllByTaskAsync([FromRoute] int taskId)
        {
            try
            {
                var comment = await _commentService.GetAllByTaskAsync(taskId);
                var result = _mapper.Map<List<CommentDto>>(comment);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CommentDto>> GetAsync([FromRoute] int id)
        {
            try
            {
                var comment = await _commentService.GetAsync(id);
                return Ok(_mapper.Map<CommentDto>(comment));
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            try
            {
                await _commentService.DeleteAsync(id);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<CommentDto>> UpdateAsync([FromBody] CommentDto commentDto)
        {
            try
            {
                var comment = _mapper.Map<CommentSv>(commentDto);
                await _commentService.UpdateAsync(comment);
                return Ok(comment);
            }
            catch (ValidationException)
            {
                return BadRequest($"The insert data is not valid. Can't update comment with id: {commentDto.Id}");
            }
        }


        [HttpPost]
        public async Task<ActionResult<CommentDto>> CreateAsync([FromBody] CommentDto commentDto)
        {
            try
            {
                var comment = _mapper.Map<CommentSv>(commentDto);
                await _commentService.CreateAsync(comment);
                return Ok(comment);
            }
            catch (ValidationException)
            {
                return BadRequest("The insert data is not valid.");
            }
        }
    }
}

