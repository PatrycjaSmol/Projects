using AutoMapper;
using FluentValidation;
using ToDoList.Assignees.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ToDoList.Assignees.Controllers;

namespace ToDoListWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssigneeController : ControllerBase
    {
        private readonly IAssigneeService _assigneeService;
        private readonly IMapper _mapper;

        public AssigneeController(IAssigneeService assigneeService, IMapper mapper)
        {
            _assigneeService = assigneeService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<AssigneeDto>>> GetAllAsync()
        {
            var result = await _assigneeService.GetAllAsync();
            return Ok(_mapper.Map<List<AssigneeDto>>(result));
        }
        [HttpGet]
        [Route("byTask{id}")]
        public async Task<ActionResult<List<AssigneeDto>>> GetByTaskAsync([FromRoute] int id)
        {
            var result = await _assigneeService.GetAllByTaskAsync(id);
            return Ok(_mapper.Map<List<AssigneeDto>>(result));
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AssigneeDto>> Get([FromRoute] int id)
        {
            try
            {
                var assignee = await _assigneeService.GetAsync(id);
                var result = _mapper.Map<AssigneeDto>(assignee);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest($"There is no Assignee with id {id}");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _assigneeService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<AssigneeDto>> Update([FromBody] AssigneeDto assigneeDto)
        {
            try
            {
                var assigneeToUpdate = _mapper.Map<AssigneeSv>(assigneeDto);
                await _assigneeService.UpdateAsync(assigneeToUpdate);
                return Ok(assigneeDto);
            }
            catch (ValidationException)
            {
                return BadRequest($"The insert data is not valid");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       

        [HttpPost]
        public async Task<ActionResult<AssigneeDto>> Create([FromBody] AssigneeDto assigneeDto)
        {
            try
            {
                var newAssignee = _mapper.Map<AssigneeSv>(assigneeDto);
                await _assigneeService.CreateAsync(newAssignee);
                return Ok(assigneeDto);
            }
            catch (ValidationException)
            {
                return BadRequest($"The insert data is not valid");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("toTask/{taskId}")]
        public async Task<ActionResult<AssigneeDto>> AddToTaskAsync([FromBody] AssigneeSv assigneeSv, int taskId)
        {
            try
            {
                await _assigneeService.AddToTaskAsync(assigneeSv, taskId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}/fromTask/{taskId}")]
        public async Task<ActionResult> RemoveTask(int assigneeId, int taskId)
        {
            try
            {
                await _assigneeService.RemoveFromTaskAsync(assigneeId, taskId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

