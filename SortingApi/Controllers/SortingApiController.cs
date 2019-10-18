using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SortingApi.Model;
using SortingApi.Repository;

namespace SortingApi.Controllers
{
    // Routes are per the requirements of the exercise
    [Route("mergesort")]
    [ApiController]
    public class SortingApiController : ControllerBase
    {
        // Normally the repository would be a DB and we would pass it through dependency injection to the constructor
        // However, I am coding for the purpose of this specific exercise, so I took this approach
        // More details in the readme
        private static readonly IRepository<Execution> Repository = new ExecutionRepository(".\\Execution");

        // GET https://localhost:44327/mergesort/executions
        [HttpGet("executions")]
        public ActionResult<IEnumerable<string>> Get()
        {
            List<Execution> executions = Repository.GetAll();
            if (executions != null && executions.Count > 0)
            {
                var jsonSettings = new JsonSerializerSettings();
                jsonSettings.DateFormatString = "ddMMyyyyHHmmss"; // Format demanded by exercise


                var executionsAsString = new List<string>();
                foreach (var execution in executions)
                {
                    executionsAsString.Add(JsonConvert.SerializeObject(execution, jsonSettings));
                }

                return executionsAsString;
            }

            return new string[0];
        }

        // GET https://localhost:44327/mergesort/executions/5
        [HttpGet("executions/{id}")]
        public ActionResult<string> Get(int id)
        {
            Execution execution = Repository.Get(id);
            if (execution != null)
            {
                var jsonSettings = new JsonSerializerSettings();
                jsonSettings.DateFormatString = "ddMMyyyyHHmmss"; // Format demanded by exercise
                return Ok(JsonConvert.SerializeObject(execution, jsonSettings));
            }

            return string.Empty;
        }

        // POST https://localhost:44327/mergesort
        [HttpPost]
        public IActionResult Post([FromBody] string[] input)
        {
            try
            {
                List<int> inputArray = input.Select(x => Convert.ToInt32(x)).ToList();
                int newId = Repository.GetNewExecutionId();
                var newExecution = new Execution(newId, inputArray);
                bool inserted = Repository.Store(newExecution);
                if (inserted)
                {
                    // This format is requested by the exercise
                    return Ok("{\"id\":\"" + newId + "\", \"status\":" + newExecution.Status + "\"}");
                }
                return Conflict("Post Request was not received.");
            }
            catch (Exception e)
            {
                return BadRequest($"Exception parsing input: {e.Message}");
            }

        }
    }
}
