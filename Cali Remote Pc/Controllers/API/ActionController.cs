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
        public ActionResult Get(Guid id)
        {
            //get find(id) action

            var action = _actionRepository.GetById(id);
            return Ok(action);
        }
        
        [HttpGet("getall/{userId}")]
        public ActionResult GetAll(string userId)
        {
            //get all
            IEnumerable actions = _actionRepository.GetAll(userId);
            return Ok(actions);
        }
        
        [HttpGet("delete/{id}")]
        public ActionResult Delete(Guid id)
        {
            //get action and delete
            _actionRepository.Delete(id);
            return Ok();
        }
        
        [HttpPut("update/{id}")]
        public ActionResult Update(Action newAction)
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
    }
}