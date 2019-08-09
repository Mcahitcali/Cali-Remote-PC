using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cali_Remote_Pc.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Cali_Remote_Pc.Controllers.API
{
    [Route("/api/v1/user")]
    [ApiController]
    public class UserController : Controller
    {
        [Route("/add")]
        [HttpPost]
        public ActionResult Add(User newUser)
        {
            //new user

            return Ok(User);
        }

        [Route("get/{id}")]
        [HttpGet]
        public ActionResult Get(int id)
        {
            //get find(id) user
            return Ok(User);
        }

        [Route("getall")]
        [HttpGet]
        public ActionResult GetAll()
        {

            //get all
            return Ok();
        }

        [Route("delete/{id}")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            //get user and delete
            return Ok();
        }

        [Route("update/{id}")]
        [HttpPut]
        public ActionResult Update(User newUser)
        {
            return Ok(User);
        }
    }
}