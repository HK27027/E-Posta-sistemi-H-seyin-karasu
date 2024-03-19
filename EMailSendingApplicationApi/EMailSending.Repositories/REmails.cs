using EMailSending.Data;
using EMailSending.InAndOutModels;
using EMailSending.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace EMailSending.Repositories

{
    public class REmails : IEmails
    {
        readonly MongoContext _mc;
        public REmails(IOptions<AppSettings> options)
        {
            _mc = new MongoContext(options);
        }
        public async Task<MethodResponse<MEmails.Response>> Add(MEmails.Form form, string CurrentAccount)
        {

            try
            {
                MethodResponse<MEmails.Response> mr = new();
                if (form.Username == null || form.Username == "undefined")
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Kullanıcı adı geçersiz");
                    return await Task.FromResult(mr);
                }
                if (form.ServerName == null || form.ServerName == "undefined")
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("ServerName geçersiz ");
                    return await Task.FromResult(mr);
                }
                if (form.PortNumber == null || form.PortNumber == "undefined")
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Port numarası boş geçilemez");
                    return await Task.FromResult(mr);
                }

                if (form.Password == null || form.Password == "undefined")
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Şifre boş olamaz");
                    return await Task.FromResult(mr);
                }


                if (mr.Status != 200)
                {
                    return await Task.FromResult(mr);
                }
                Emails email = new()
                {
                  AccountId = CurrentAccount,
                    Password = form.Password,
                    PortNumber = form.PortNumber,
                    ServerName = form.ServerName,
                    Username = form.Username,
                  
                };
                await _mc.Emails.InsertOneAsync(email);
                mr.StatusTexts.Add(" E-Mail adresi eklendi");
                return await Task.FromResult(mr);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            throw new NotImplementedException();
        }

        public async Task<MethodResponse<string>> Delete(string id, string CurrentAccount)
        {
            try
            {
                MethodResponse<string> mr = new();


                var user = _mc.Emails.Find(x => x.Id == id).FirstOrDefault();
                if (user == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Kişi bulunamadı");
                    return await Task.FromResult(mr);
                }

                #region Mongo
                var builder = Builders<Emails>.Update;
                var update = builder.Set(x => x.IsDeleted, true);
                await _mc.Emails.UpdateOneAsync(x => x.Id == id, update);
                #endregion Mongo


                mr.Status = 200;

                mr.StatusTexts.Add("Silindi");
                return await Task.FromResult(mr);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<MethodResponse<List<MEmails.Response>>> MultipleGet(MEmails.Form form, string CurrentAccount)
        {
            try
            {
                MethodResponse<List<MEmails.Response>> mr = new();


                var jb = Builders<Emails>.Filter;
                var jq = jb.Empty;
                jq &= jb.Eq(x => x.IsDeleted, false);
                jq &= jb.Eq(x => x.AccountId, CurrentAccount);
                if (!string.IsNullOrWhiteSpace(form.Search))
                {
                    var search = BsonRegularExpression.Create(new Regex(form.Search, RegexOptions.IgnoreCase));


                    jq = (jb.Regex(k => k.Username, search) |
                        jb.Regex(k => k.ServerName, search) 
                        );
                }
                mr.Item = _mc.Emails.Find(jq).ToList().Select(x => new MEmails.Response
                {
                    Id = x.Id,
                    PortNumber = x.PortNumber,
                    ServerName = x.ServerName,
                    AccountId = x.AccountId,
                    Username = x.Username,
                    Password=x.Password,
                 
                    
                }).ToList();
                mr.Count = mr.Item.Count;
                return await Task.FromResult(mr);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<MethodResponse<MEmails.Response>> SingleGet(string id, string CurrentAccount)
        {
            try
            {
                MethodResponse<MEmails.Response> mr = new();




                #region Mongo
                var j = _mc.Emails.Find(k => k.Id == id && k.IsDeleted == false).FirstOrDefault();
                #endregion Mongo
                MEmails.Response response = new()
                {
                    Id = j.Id,
                    PortNumber = j.PortNumber,
                    ServerName = j.ServerName,
                    AccountId = j.AccountId,
                    Username = j.Username,
                    Password = j.Password,
                
                };
                mr.Item = response;

                return await Task.FromResult(mr);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<MethodResponse<MEmails.Response>> Update(MEmails.Form form, string CurrentAccount)
        {
            try
            {
                MethodResponse<MEmails.Response> mr = new();

                if (form.Username == null || form.Username == "undefined")
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Kullanıcı adı geçersiz veya boş olamaz");
                    return await Task.FromResult(mr);
                }
                if (form.ServerName == null || form.ServerName == "undefined")
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("ServerName geçersiz veya boş olamaz");
                    return await Task.FromResult(mr);
                }
                if (form.PortNumber == null || form.PortNumber == "undefined")
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Port numarası boş geçilemez");
                    return await Task.FromResult(mr);
                }

                if (form.Password == null || form.Password == "undefined")
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Şifre boş olamaz");
                    return await Task.FromResult(mr);
                }




                #region Mongo
                var builder = Builders<Emails>.Update;
                var update = builder

                    .Set(x => x.Username, form.Username)
                    .Set(x => x.ServerName, form.ServerName)
                    .Set(x => x.PortNumber, form.PortNumber)
                    .Set(x => x.Password, form.Password);


                await
                _mc.Emails.UpdateOneAsync(x => x.Id == form.Id, update);
                #endregion Mongo

                mr = SingleGet(form.Id, CurrentAccount).Result;
                mr.Status = 200;
                mr.StatusTexts.Add("Güncellendi");
                return await Task.FromResult(mr);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
