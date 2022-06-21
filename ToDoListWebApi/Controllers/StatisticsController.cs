using ToDoList.Common.Enum;
using ToDoList.Statistics.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ToDoListWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpGet]
        public async Task<ActionResult<Dictionary<TaskState, int>>> GetAllStatistics()
        {
            try
            {
                var result = await _statisticService.GetAllStatistic();
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest($"Something went wrong.");
            }
        }

        [HttpGet]
        [Route("ByBucket/{bucketId}")]
        public async Task<ActionResult<Dictionary<TaskState, int>>> GetStatisticByBucket([FromRoute] int bucketId)
        {
            try
            {
                var result = await _statisticService.GetStatisticByBucket(bucketId);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest($"Something went wrong.");
            }
        }
    }
}
