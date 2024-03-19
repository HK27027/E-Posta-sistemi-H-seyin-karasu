


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
    public class EmailSentController:ControllerBase
    {

        readonly IEmailSent _emailsent;
        public EmailSentController(IEmailSent emailsent)
        {
            _emailsent = emailsent;
        }
        [HttpPost]
        [Route("MultipleGet")]
        public async Task<IActionResult> MultipleGet([FromForm] MEmailSent.Form form)
        {
            var CurrentAccount = form.AccountId;
            var mr = _emailsent.MultipleGet(form, CurrentAccount).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));
        }
      
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var CurrentAccount = "";
            var mr = _emailsent.SingleGet(id, CurrentAccount).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));

        }
       
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] MEmailSent.Form form)
        {
            var CurrentAccount = form.AccountId;
            var mr = _emailsent.Add(form, CurrentAccount).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));

        }
  
        //[HttpPut]
        //public async Task<IActionResult> Put([FromForm] MEmailSent.Form form)
        //{
        //    var CurrentAccount = "";

        //    var mr = _emailsent.Update(form, CurrentAccount).Result;

        //    return await Task.FromResult(StatusCode(mr.Status, mr));
        //}
    
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var currentUserId = "";
            var mr = _emailsent.Delete(id, currentUserId).Result;
            return await Task.FromResult(StatusCode(mr.Status, mr));
        }
      
    }



}

