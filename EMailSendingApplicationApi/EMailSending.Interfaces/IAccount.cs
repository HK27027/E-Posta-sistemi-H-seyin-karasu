
using EMailSending.Data;
using EMailSending.InAndOutModels;
using News.Interfaces;

namespace EMailSending.Interfaces
{
    public interface IAccount:IBase<MAccount.Response,MAccount.Form>
    {
        Task<MethodResponse<MAccount.Response>> Login(MAccount.Form form, string CurrentAccount);
        Task<MethodResponse<MAccount.Info>> Accountİnfo(MAccount.Form form);
    }
}
