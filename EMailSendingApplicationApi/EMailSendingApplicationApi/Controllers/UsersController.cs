


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
    public class UserController:ControllerBase
    {

        readonly IUSers _user;
        public UserController(IUSers user)
        {
            _user = user;
        }
        [HttpPost]
        [Route("MultipleGet")]
        public async Task<IActionResult> MultipleGet([FromForm] MUsers.Form form)
        {
            var CurrentAccount = form.AccountId;
            var mr = _user.MultipleGet(form, CurrentAccount).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));
        }
     
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var CurrentAccount = "";
            var mr = _user.SingleGet(id, CurrentAccount).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));

        }
       
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] MUsers.Form form)
        {
            var CurrentAccount = form.AccountId;
            var mr = _user.Add(form, CurrentAccount).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));

        }

        [HttpPut]
        public async Task<IActionResult> Put([FromForm] MUsers.Form form)
        {
            var CurrentAccount = "";

            var mr = _user.Update(form, CurrentAccount).Result;

            return await Task.FromResult(StatusCode(mr.Status, mr));
        }
    
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var CurrentAccount = "";
            var mr = _user.Delete(id, CurrentAccount).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));
        }
      
    }



}

