using AutoMapper;
using FluentValidation;
using ToDoList.Common.Exceptions;
using ToDoList.Tasks.Controllers;
using ToDoList.Tasks.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoListWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper;

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("ByBucket/{bucketId}")]
        public async Task<ActionResult<List<TaskDto>>> GetAllByBucketAsync([FromRoute] int bucketId)
        {
            try
            {
                var tasks = await _taskService.GetAllByBucketAsync(bucketId);
                var result =_mapper.Map<List<TaskDto>>(tasks);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TaskDto>> GetAsync([FromRoute] int id)
        {
            try
            {
                var task = await _taskService.GetAsync(id);
                var result = _mapper.Map<TaskDto>(task);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskDto>>> GetAll()
        {
            try
            {
                var tasks = await _taskService.GetAll();
                var result = _mapper.Map<List<TaskDto>>(tasks);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _taskService.Delete(id);
            }
            catch (DataNotFoundException)
            {
                throw new DataNotFoundException($"The insert task with id : {id} doesn't exist.");
            }

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<TaskDto>> Update([FromBody] TaskDto taskDto)
        {
            try
            {
                var taskToUpdate = _mapper.Map<TaskSv>(taskDto);
                await _taskService.Update(taskToUpdate);
                var result = _mapper.Map<TaskDto>(taskToUpdate);
            }
            catch (ValidationException)
            {
                return BadRequest($"Couldn't update task with id: {taskDto.Id} - it's not valid.");
            }
            catch (Exception)
            {
                return BadRequest($"Something went wrong.");
            }

            return Ok(taskDto);
        }


        [HttpPost]
        [Route("AddToBucket{id}")]
        public async Task<ActionResult<TaskDto>> Create([FromBody] TaskDto taskDto, [FromRoute] int id)
        {
            try
            {
                var newTask = _mapper.Map<TaskSv>(taskDto);
                await _taskService.Create(newTask);
                var result = _mapper.Map<TaskDto>(newTask);
            }
            catch (ValidationException)
            {
                return BadRequest($"Couldn't create a task with id: {taskDto.Id} - it's not valid.");
            }
            catch (Exception)
            {
                return BadRequest($"Something went wrong.");
            }

            return Ok(taskDto);
        }

    }
}

