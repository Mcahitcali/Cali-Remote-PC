using Cali_Remote_Pc.DTO;
using Cali_Remote_Pc.Entity;
using Cali_Remote_Pc.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using Action = Cali_Remote_Pc.Entity.Action;

namespace Cali_Remote_Pc.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {
        private readonly IActionRepository _actionRepository;
        public ActionController(IActionRepository actionRepository)
        {
            _actionRepository = actionRepository;
        }

        [HttpPost("add")]
        public IActionResult Add(Action newAction)
        {
            try
            {
                newAction.Id = new Guid();
                _actionRepository.Add(newAction);
                _actionRepository.Save();
                return Ok("Successfully Added Action!");
            }
            catch (System.Exception)
            {

                return Ok("Failed Added Action!");
            }
        }
        
        [HttpGet("get/{id}")]
        public IActionResult Get(Guid id)
        {
            //get find(id) action

            var action = _actionRepository.GetById(id);
            return Ok(action);
        }
        
        [HttpGet("getall/{userId}")]
        public IActionResult GetAll(string userId)
        {
            //get all
            IEnumerable actions = _actionRepository.GetAll(userId);
            return Ok(actions);
        }
        
        [HttpGet("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            //get action and delete
            _actionRepository.Delete(id);
            return Ok();
        }
        
        [HttpPut("update/{id}")]
        public IActionResult Update(Action newAction)
        {
            try
            {
                _actionRepository.Update(newAction);
                return Ok("Successfully Update Action!");

            }
            catch (System.Exception)
            {
                return Ok("Failed Update Action!");
            }
        }

        [HttpPost("attach")]
        public IActionResult Attach(Action activeaction)
        {
            return Ok();
        }

        [HttpGet("getactiveaction")]
        public IActionResult GetActiveAction()
        {
            return Ok();
        }
    }
}