using AutoMapper;
using FluentValidation;
using ToDoList.Buckets.Controllers;
using ToDoList.Buckets.Services;
using ToDoList.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoListWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BucketController : ControllerBase
    {
        private readonly IBucketService _bucketService;
        private readonly IMapper _mapper;

        public BucketController(IBucketService bucketService, IMapper mapper)
        {
            _bucketService = bucketService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<BucketDto>>> GetAllAsync()
        {
            try
            {
                var buckets = await _bucketService.GetAllAsync();
                var listBuckets = _mapper.Map<List<BucketDto>>(buckets);
                return Ok(listBuckets);
            }
            catch (DataNotFoundException)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<BucketSv>> GetAsync([FromRoute] int id)
        {
            try
            {
                var bucket = await _bucketService.GetAsync(id);
                return Ok(bucket);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpPost]
        public async Task<ActionResult<BucketDto>> CreateAsync([FromBody] BucketDto bucketDto)
        {
            try
            {
                var bucket = _mapper.Map<BucketSv>(bucketDto);
                await _bucketService.CreateAsync(bucket);
                return Ok(bucket);
            }
            catch (ValidationException)
            {
                return BadRequest($"The insert data of bucket is not valid.");
            }
        }


        [HttpPut]
        public async Task<ActionResult<BucketDto>> UpdateAsync([FromBody] BucketDto bucketDto)
        {
            try
            {
                var bucketToUpdate = _mapper.Map<BucketSv>(bucketDto);
                await _bucketService.UpdateAsync(bucketToUpdate);
                return Ok(bucketToUpdate);
            }
            catch (ValidationException)
            {
                return BadRequest($"The insert data of bucket is not valid.");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            await _bucketService.DeleteAsync(id);
            return Ok();
        }
    }
}
