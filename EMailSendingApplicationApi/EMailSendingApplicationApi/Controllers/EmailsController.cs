


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
    public class EmailController:ControllerBase
    {

        readonly IEmails _email;
        public EmailController(IEmails email)
        {
            _email = email;
        }
        [HttpPost]
        [Route("MultipleGet")]
        public async Task<IActionResult> MultipleGet([FromForm] MEmails.Form form)
        {
            var CurrentAccount = form.AccountId;
            var mr = _email.MultipleGet(form, CurrentAccount).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var CurrentAccount = "";
            var mr = _email.SingleGet(id, CurrentAccount).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));

        }
       
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] MEmails.Form form)
        {
            var CurrentAccount = form.AccountId;
            var mr = _email.Add(form, CurrentAccount).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));

        }
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] MEmails.Form form)
        {
            var CurrentAccount = form.AccountId;

            var mr = _email.Update(form, CurrentAccount).Result;

            return await Task.FromResult(StatusCode(mr.Status, mr));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var currentUserId = "";
            var mr = _email.Delete(id, currentUserId).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));
        }
      
    }



}

