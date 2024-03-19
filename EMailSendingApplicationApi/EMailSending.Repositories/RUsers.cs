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
    public class RUser : IUSers
    {
        readonly MongoContext _mc;
        public RUser(IOptions<AppSettings> options)
        {
            _mc = new MongoContext(options);
        }
        public async Task<MethodResponse<MUsers.Response>> Add(MUsers.Form form, string CurrentAccount)
        {

            try
            {
                MethodResponse<MUsers.Response> mr = new();

                if (form.Name==null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("İsim boş geçilemez");
                    return await Task.FromResult(mr);
                }
                if (form.Email == null && !MailAddress.TryCreate(form.Email, out MailAddress mail))
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("E-Posta boş geçilemez");
                    return await Task.FromResult(mr);
                }
             


                if (mr.Status != 200)
                {
                    return await Task.FromResult(mr);
                }
                Users user = new()
                {
                    Email = form.Email,
                    Name = form.Name,
                    Title = form.Title,
               
                    CompanyName = form.CompanyName,
                 
                    PhoneNumber = form.PhoneNumber, 
                    AccountId=CurrentAccount,
                };
                await _mc.Users.InsertOneAsync(user);
                mr.StatusTexts.Add("Kişi Eklendi");
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

               
                var user = _mc.Users.Find(x => x.Id == id ).FirstOrDefault();
                if (user == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Kişi bulunamadı");
                    return await Task.FromResult(mr);
                }
               
                #region Mongo
                var builder = Builders<Users>.Update;
                var update = builder.Set(x => x.IsDeleted, true);
                await _mc.Users.UpdateOneAsync(x => x.Id == id, update);
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

        public async Task<MethodResponse<List<MUsers.Response>>> MultipleGet(MUsers.Form form, string CurrentAccount)
        {
            try
            {
                MethodResponse<List<MUsers.Response>> mr = new();
             

                var jb = Builders<Users>.Filter;
                var jq = jb.Empty;
                jq &= jb.Eq(x => x.IsDeleted, false);
                jq &= jb.Eq(x => x.AccountId, CurrentAccount);
                if (!string.IsNullOrWhiteSpace(form.Search))
                {
                    var search = BsonRegularExpression.Create(new Regex(form.Search, RegexOptions.IgnoreCase));


                    jq = (jb.Regex(k => k.Name, search) |
                        jb.Regex(k => k.CompanyName, search) |
                        jb.Regex(k => k.Email, search) |
                        jb.Regex(k => k.PhoneNumber, search) |
                        jb.Regex(k => k.Title, search) 
                        );
                }
                mr.Item = _mc.Users.Find(jq).ToList().Select(x => new MUsers.Response
                {
                  Id = x.Id,

                  CompanyName = x.CompanyName,
                  Email = x.Email,
              
                  Name = x.Name,
                  PhoneNumber = x.PhoneNumber,
                  Title = x.Title   
                }).ToList();
                mr.Count = mr.Item.Count;
                return await Task.FromResult(mr);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public  async Task<MethodResponse<MUsers.Response>> SingleGet(string id, string CurrentAccount)
        {
            try
            {
                MethodResponse<MUsers.Response> mr = new();
             
            


                #region Mongo
                var j = _mc.Users.Find(k => k.Id == id && k.IsDeleted == false).FirstOrDefault();
                #endregion Mongo
                MUsers.Response response = new()
                {
                 Id = j.Id,
                 Title = j.Title,
                 PhoneNumber= j.PhoneNumber,
               
                 Name = j.Name,
               
                 Email = j.Email,
                 CompanyName = j.CompanyName    
                };
                mr.Item = response;

                return await Task.FromResult(mr);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<MethodResponse<MUsers.Response>> Update(MUsers.Form form, string CurrentAccount)
        {
            try
            {
                MethodResponse<MUsers.Response> mr = new();

                if (form.Name == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("İsim boş geçilemez");
                    return await Task.FromResult(mr);
                }
                if (form.Email == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("E-Posta boş geçilemez");
                    return await Task.FromResult(mr);
                }




                #region Mongo
                var builder = Builders<Users>.Update;
                var update = builder
              
                    .Set(x => x.Email, form.Email)
                    .Set(x => x.Name, form.Name)
                 
                    .Set(x => x.Title, form.Title)
                    .Set(x => x.CompanyName, form.CompanyName)
                    .Set(x => x.PhoneNumber, form.PhoneNumber);


                await
                _mc.Users.UpdateOneAsync(x => x.Id == form.Id, update);
                #endregion Mongo

                mr = SingleGet(form.Id,CurrentAccount).Result;
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
