using BestPracticeApi.Exceptions.ClientExceptions;
using BestPracticeApi.Exceptions.ServerExceptions;
using BestPracticeApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BestPracticeApi.Controllers
{
    [ApiController]
    public class CRUDController : ControllerBase
    {
        private readonly IAppUserManager _appUserManager;
        public CRUDController(IAppUserManager appUserManager) 
        {
            _appUserManager = appUserManager;
        }

        [HttpGet("/CRUD/AlreadyExistException")]
        public async Task<ActionResult<string>> GetAlrExistExc()
        {
            var user = await _appUserManager.GetUserAsync();

            if (user.Age < 31)
                throw new AlreadyExistException(nameof(user), new { Id = Guid.NewGuid(), user.Name });
            else
                return new JsonResult(user);
        }

        [HttpGet("/CRUD/InvalidModelException")]
        public async Task<ActionResult<string>> GetInvModelExc()
        {
            var user = await _appUserManager.GetUserAsync();

            if (user.Age < 31)
                throw new InvalidModelException("Model failed validation!");
            else
                return new JsonResult(user);
        }

        [HttpGet("/CRUD/MissingParametersException")]
        public async Task<ActionResult<string>> GetMissParamExc()
        {
            var user = await _appUserManager.GetUserAsync();

            if (user.Age < 31)
                throw new MissingParametersException(new List<string> { "Name", "UserName" });
            else
                return new JsonResult(user);
        }

        [HttpGet("/CRUD/NotExistException")]
        public async Task<ActionResult<string>> GetNotExistExc()
        {
            var user = await _appUserManager.GetUserAsync();

            if (user.Age < 31)
                throw new NotExistsException("This user does not exist!");
            else
                return new JsonResult(user);
        }

        [HttpGet("/CRUD/CriticalException")]
        public async Task<ActionResult<string>> GetCritExc()
        {
            var user = await _appUserManager.GetUserAsync();

            if (user.Age < 31)
                throw new CriticalServerException("Uncaught server exception!", new Exception());
            else
                return new JsonResult(user);
        }
    }
}
