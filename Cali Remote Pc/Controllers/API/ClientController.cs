using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cali_Remote_Pc.DTO;
using Cali_Remote_Pc.Entity;
using Cali_Remote_Pc.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cali_Remote_Pc.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpPost("add")]
        public IActionResult Add(Client newClient)
        {
            try
            {
                newClient.Id = new Guid();
                _clientRepository.Add(newClient);
                _clientRepository.Save();
                return Ok(newClient);
            }
            catch (System.Exception)
            {

                return Ok("Failed Added Client!");
            }
        }

        [HttpGet("get/{id}")]
        public ActionResult Get(Guid id)
        {
            //get find(id) Client

            var Client = _clientRepository.GetById(id);
            return Ok(Client);
        }

        [HttpGet("getall/{userId}")]
        public ActionResult GetAll(string userId)
        {
            //get all
            IEnumerable Clients = _clientRepository.GetAll(userId);
            return Ok(Clients);
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            //get Client and delete
            _clientRepository.Delete(id);
            _clientRepository.Save();
            return Ok();
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(Guid id, Client newClient)
        {
            try
            {
                if(!id.Equals(newClient.Id))
                {
                    return Ok("Failed Update Client!");
                }
                _clientRepository.Update(newClient);
                _clientRepository.Save();
                return Ok("Successfully Update Client!");

            }
            catch (System.Exception)
            {
                return Ok("Failed Update Client!");
            }
        }
    }
}