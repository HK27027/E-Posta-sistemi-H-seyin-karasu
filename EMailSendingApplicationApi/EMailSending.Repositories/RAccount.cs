using EMailSending.Data;
using EMailSending.InAndOutModels;
using EMailSending.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Net.Mail;

namespace EMailSending.Repositories

{
    public class RAccount : IAccount
    {
        readonly MongoContext _mc;
        public RAccount(IOptions<AppSettings> options)
        {
            _mc = new MongoContext(options);
        }
        public async Task<MethodResponse<MAccount.Response>> Add(MAccount.Form form, string CurrentAccount)
        {

            try
            {
                MethodResponse<MAccount.Response> mr = new();
                var mailControl = _mc.Account.Find(k => k.Email == form.Email).Any();
                if (mailControl)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("E-mail adresi kullanılıyor!");
                }
                if (form.Name==null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("İsim boş geçilemez!");
                }
                if (form.Email == null && !MailAddress.TryCreate(form.Email, out MailAddress mail))
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("E-Mail boş geçilemez!");
                }
               
                if (form.Password == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Şifre boş geçilemez!");
                }
                else
                {
                    if (form.Password.Length<4)
                    {
                        mr.Status = 400;
                        mr.StatusTexts.Add("Şifre en az 4 karakter olmalı!");
                    }
                }
                if (mr.Status != 200)
                {
                    return await Task.FromResult(mr);
                }
                Account account = new()
                {
                    Email = form.Email,
                    Name = form.Name,
                    Password = form.Password,
                };
                await _mc.Account.InsertOneAsync(account);
                mr.Item= SingleGet(account.Id, CurrentAccount).Result.Item;
                mr.StatusTexts.Add("Hesap oluşturuldu");
                return await Task.FromResult(mr);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            throw new NotImplementedException();
        }

        public Task<MethodResponse<string>> Delete(string id, string CurrentAccount)
        {
            throw new NotImplementedException();
        }

        public Task<MethodResponse<List<MAccount.Response>>> MultipleGet(MAccount.Form form, string CurrentAccount)
        {
            throw new NotImplementedException();
        }

        public async Task<MethodResponse<MAccount.Response>> SingleGet(string id, string CurrentAccount)
        {
            try
            {
                MethodResponse<MAccount.Response> mr = new();
                var account = _mc.Account.Find(k => k.Id == id).FirstOrDefault();
                if (account==null)
                {
                    return await Task.FromResult(mr);
                }
                MAccount.Response response = new()
                {
                    Id = account.Id,
                    Email = account.Email,
                    Name = account.Name,

                };

                mr.Item = response;


                return await Task.FromResult(mr);
            }
            catch (Exception ex)
            {
                throw await Task.FromResult(ex);
            }
        }

        public Task<MethodResponse<MAccount.Response>> Update(MAccount.Form form, string CurrentAccount)
        {
            throw new NotImplementedException();
        }

        public async Task<MethodResponse<MAccount.Response>> Login(MAccount.Form form, string CurrentAccount)
        {

            try
            {
                MethodResponse<MAccount.Response> mr = new();
                var account=_mc.Account.Find(k=>k.Email==form.Email &&k.Password==form.Password).FirstOrDefault();
                if (account==null) {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Hatalı email/şifre!");
                    return await Task.FromResult(mr);

                }
                MAccount.Response response = new()
                {
                   Id = account.Id,
                   Email=account.Email,
                   Name= account.Name,
                   
                };

                mr.Item = response;


                return await Task.FromResult(mr);
            }
            catch (Exception ex)
            {
                throw await Task.FromResult(ex);
            }
        }

        public async Task<MethodResponse<MAccount.Info>> Accountİnfo(MAccount.Form form)
        {
            try
            {
                MethodResponse<MAccount.Info> mr = new();
                var user = _mc.Users.Find(k => k.AccountId == form.AccountId&& k.IsDeleted==false).ToList().Count;
                var email = _mc.Emails.Find(k => k.AccountId == form.AccountId==k.IsDeleted==false).ToList().Count;
                var sent = _mc.EmailSent.Find(k => k.AccountId == form.AccountId&&k.IsDeleted==false).ToList().Count;

                MAccount.Info response = new()
                {
                    UserCount = user,
                    EmailCount = email,
                    EmailSentCount = sent,

                };

                mr.Item = response;


                return await Task.FromResult(mr);
            }
            catch (Exception ex)
            {
                throw await Task.FromResult(ex);
            }
        }
    }
}
