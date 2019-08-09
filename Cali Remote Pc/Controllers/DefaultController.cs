using System;
using Cali_Remote_Pc.Services;
using Cali_Remote_Pc.Services.JsonRequest;
using Cali_Remote_Pc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using System.Collections.Generic;
using Cali_Remote_Pc.Entity;
using Microsoft.AspNetCore.Identity;
using Action = Cali_Remote_Pc.Entity.Action;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Cali_Remote_Pc.Controllers
{
    [Authorize]
    public class DefaultController : Controller
    {
        private readonly UserManager<User> _userManager;

        private readonly string GET_URL = "https://shutdown-remote-api.herokuapp.com/api/getactions";
        private readonly string POST_URL = "https://shutdown-remote-api.herokuapp.com/api/postactions";
        private readonly string _userId;
        private readonly User _user;

        private Client _CurrentClient;
        private string Message;

        public DefaultController(UserManager<User> userManager, IHttpContextAccessor accessor)
        {
            _userManager = userManager;
            _user = _userManager.GetUserAsync(accessor.HttpContext.User).Result;
            _userId = _userManager.GetUserId(accessor.HttpContext.User);
            _CurrentClient = new Client();
        }

        [HttpGet]
        public IActionResult Index()
        {
            IndexModel model = new IndexModel()
            {
                viewActions = GetActions(_userId),
                viewClients = GetClients(_userId)
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string ActionValue, string Command)
        {
            if (string.IsNullOrEmpty(ActionValue))
            {
                Actions.Instance = new Actions(0, 0, 0, 0, Command, DateTime.Now);
            }
            else
            {
                switch (ActionValue)
                {
                    case "Lock": Actions.Instance = new Actions(1, 0, 0, 0, string.Empty, DateTime.Now); break;
                    case "Sign Out": Actions.Instance = new Actions(0, 1, 0, 0, string.Empty, DateTime.Now); break;
                    case "Restart": Actions.Instance = new Actions(0, 0, 1, 0, string.Empty, DateTime.Now); break;
                    case "Shutdown": Actions.Instance = new Actions(0, 0, 0, 1, string.Empty, DateTime.Now); break;
                    case "Default": Actions.Instance = new Actions(0, 0, 0, 0, string.Empty, DateTime.Now); break;
                }
            }
            IndexModel model = new IndexModel()
            {
                viewActions = GetActions(_userId),
                viewClients = GetClients(_userId)
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult GetClients(IndexModel model)
        {
            return PartialView("_GetClienstPartialView", model.viewClients);
        }

        [HttpGet]
        public IActionResult GetActions(IndexModel model)
        {
            return PartialView("_GetActionsPartialView", model.viewActions);
        }

        [HttpPost]
        public IActionResult GetCurrentClient(Client model)
        {
            _CurrentClient = model;
            return PartialView("_GetCurrentClientPartialView", model);
        }

        [HttpGet]
        public IActionResult AddClient()
        {
            return PartialView("_PostClientPartialView");
        }

        [HttpPost]
        public IActionResult AddClient(Client model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = _userId;
                IndexModel indexModel = new IndexModel()
                {
                    viewActions = GetActions(_userId),
                    viewClients = PostClient(model)
                };
                return PartialView("_GetClienstPartialView", indexModel.viewClients);
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult AddAction()
        {
            return PartialView("_AddActionPartialView");
        }

        [HttpPost]
        public IActionResult AddAction(Action model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = _userId;
                IndexModel indexModel = new IndexModel()
                {
                    viewActions = PostAction(model),
                    viewClients = GetClients(_userId)
                };
                return PartialView("_GetActionsPartialView", indexModel.viewActions);
            }

            return Ok("Error Actions!");
        }

        [HttpGet]
        public async Task<IActionResult> Download()
        {
            string filename = "client_" + _userId + ".exe";

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot\\client", "StartTrayActions.exe");

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/vnd.microsoft.portable-executable", filename);
        }





        //------ API REQUEST AREA ----- \\


        private IEnumerable<Action> GetActions(string userId)
        {
            try
            {
                var request = new Request("https://localhost:44346/api/action/getall/" + userId, "GET");
                var responseClient = (IEnumerable<Action>)request.Execute<IEnumerable<Action>>();

                return responseClient;
            }
            catch (Exception err)
            {
                return new List<Action>();
            }

        }

        private void AttachAction()
        {
            try
            {
                var request = new Request();
                var response = (Actions)request.Execute<Actions>(POST_URL, Actions.Instance, "POST");
            }
            catch (Exception e)
            {
                Message = e.Message;
            }

        }

        private IEnumerable<Action> PostAction(Action action)
        {
            try
            {
                var request = new Request();
                var response = request.Execute<Action>("https://localhost:44346/api/action/add", action, "POST");

                return GetActions(_userId);
            }
            catch (Exception e)
            {
                Message = e.Message;
                return GetActions(_userId);
            }
        }

        private IEnumerable<Client> GetClients(string userId)
        {
            try
            {
                var request = new Request("https://localhost:44346/api/client/getall/" + userId, "GET");
                var responseClient = (IEnumerable<Client>)request.Execute<IEnumerable<Client>>();

                return responseClient;
            }
            catch (Exception err)
            {
                return new List<Client>();
            }

        }

        private IEnumerable<Client> PostClient(Client client)
        {
            try
            {

                var request = new Request();
                var responseClient = request.Execute<Client>("https://localhost:44346/api/client/add", client, "POST");

                return GetClients(_userId);
            }
            catch (Exception e)
            {
                Message = e.Message;
                return GetClients(_userId);
            }
        }
    }
}