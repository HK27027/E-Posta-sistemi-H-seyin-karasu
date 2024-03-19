


using EMailSending.Data;
using EMailSending.InAndOutModels;
using EMailSending.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EMailSendingApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController:ControllerBase
    {

        readonly IAccount _account;
        public AccountController(IAccount account)
        {
            _account = account;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("MultipleGet")]
        public async Task<IActionResult> MultipleGet([FromForm] MAccount.Form form)
        {
            var CurrentAccount = "";
            var mr = _account.MultipleGet(form, CurrentAccount).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var CurrentAccount = "";
            var mr = _account.SingleGet(id, CurrentAccount).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));

        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromForm] MAccount.Form form)
        {
            var CurrentAccount = "";
            var mr = _account.Login(form, CurrentAccount).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] MAccount.Form form)
        {
            var CurrentAccount = "";
            var mr = _account.Add(form, CurrentAccount).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));

        }
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] MAccount.Form form)
        {
            var CurrentAccount = "";

            var mr = _account.Update(form, CurrentAccount).Result;

            return await Task.FromResult(StatusCode(mr.Status, mr));
        }
     
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var currentUserId = "";
            var mr = _account.Delete(id, currentUserId).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));
        }


        [HttpPost]
        [Route("AccountInfo")]
        public async Task<IActionResult> AccountInfo([FromForm] MAccount.Form form)
        {
          
            var mr = _account.Accountİnfo( form).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));

        }

    }



}

