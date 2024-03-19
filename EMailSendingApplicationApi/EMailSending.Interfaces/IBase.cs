using EMailSending.Data;
using EMailSending.InAndOutModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Interfaces
{
    public partial interface IBase<Response, Form>
    {
        Task<MethodResponse<Response>> Add(Form form, string CurrentAccount);
        Task<MethodResponse<List<Response>>> MultipleGet(Form form, string CurrentAccount);
        Task<MethodResponse<Response>> SingleGet(string id, string CurrentAccount);
        Task<MethodResponse<Response>> Update(Form form, string CurrentAccount);
        Task<MethodResponse<string>> Delete(string id, string CurrentAccount);
    }
}
