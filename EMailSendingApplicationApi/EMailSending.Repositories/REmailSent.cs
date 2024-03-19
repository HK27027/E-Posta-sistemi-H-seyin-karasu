using EMailSending.Data;
using EMailSending.InAndOutModels;
using EMailSending.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace EMailSending.Repositories

{
    public class REmailSent : IEmailSent
    {
        readonly MongoContext _mc;
        public REmailSent(IOptions<AppSettings> options)
        {
            _mc = new MongoContext(options);
        }
        public async Task<MethodResponse<MEmailSent.Response>> Add(MEmailSent.Form form, string CurrentAccount)
        {

            try
            {
                MethodResponse<MEmailSent.Response> mr = new();
                if (form.EMailId==null || form.EMailId == "undefined")
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Mail seçilmedi");
                    return await Task.FromResult(mr);
                }
                if (form.UserIds==null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("En az bir kişi seçilmeli");
                    return await Task.FromResult(mr);
                }
                if (form.Title==null || form.Title == "undefined")
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("Başlık boş geçilemez");
                    return await Task.FromResult(mr);
                }
                if (form.Content == null || form.Content == "undefined")
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("İçerik boş geçilemez");
                    return await Task.FromResult(mr);
                }
              
                var mail = _mc.Emails.Find(k => k.Id == form.EMailId && k.IsDeleted == false).FirstOrDefault();

                if (mail != null)
                {
                    foreach (var item in form.UserIds)
                    {
                        var user = _mc.Users.Find(k => k.Id==item.Id&&k.IsDeleted==false).FirstOrDefault();

                        // E-posta adresi ve şifrenizi buraya ekleyin
                        string smtpUsername = mail.Username;
                        string smtpPassword = mail.Password;

                        // SMTP ayarları
                        SmtpClient smtpClient = new SmtpClient(mail.ServerName)
                        {
                            Port =Convert.ToInt32(mail.PortNumber),
                            Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                            EnableSsl = true,
                        };


                       var body = form.Content;
                        body = body.Replace("#isim", user.Name);
                        // E-posta oluştur
                        MailMessage mailMessage = new MailMessage
                        {
                            From = new MailAddress(smtpUsername),
                            Subject = form.Title,
                            Body = body,
                        };

                        // Alıcı adresi ekleyin
                        mailMessage.To.Add(user.Email);

                       
                            // E-postayı gönder
                            smtpClient.Send(mailMessage);

                        
                       
                        EmailSent send = new()
                            {
                                Content = body,
                                EMailId = form.EMailId,
                                Title = form.Title,
                                UserId = item.Id,
                                AccountId = CurrentAccount,
                            };
                            await _mc.EmailSent.InsertOneAsync(send);
                        
                     
                    }

                    mr.StatusTexts.Add("Mail Gönderildi");
                }
             

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


                var user = _mc.EmailSent.Find(x => x.Id == id).FirstOrDefault();
                if (user == null)
                {
                    mr.Status = 400;
                    mr.StatusTexts.Add("mail bulunamadı");
                    return await Task.FromResult(mr);
                }

                #region Mongo
                var builder = Builders<EmailSent>.Update;
                var update = builder.Set(x => x.IsDeleted, true);
                await _mc.EmailSent.UpdateOneAsync(x => x.Id == id, update);
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
    

        public async Task<MethodResponse<List<MEmailSent.Response>>> MultipleGet(MEmailSent.Form form, string CurrentAccount)
        {
            try
            {
                MethodResponse<List<MEmailSent.Response>> mr = new();


                var jb = Builders<EmailSent>.Filter;
                var jq = jb.Empty;
                jq &= jb.Eq(x => x.IsDeleted, false);
                jq &= jb.Eq(x => x.AccountId, CurrentAccount);
                if (!string.IsNullOrWhiteSpace(form.Search))
                {
                    var search = BsonRegularExpression.Create(new Regex(form.Search, RegexOptions.IgnoreCase));


                    jq = (jb.Regex(k => k.Content, search) |
                        jb.Regex(k => k.Title, search)
                        );
                }
                mr.Item = _mc.EmailSent.Find(jq).ToList().Select(x => new MEmailSent.Response
                {
                    Id = x.Id,
                    AccountId = x.AccountId,
                    Title = x.Title,
                    Content = x.Content,
                    EMailId = x.EMailId,
                    UserId = x.UserId   ,
                    EmailName=x.EMailId!=null?_mc.Emails.Find(k=>k.Id==x.EMailId&&k.IsDeleted==false)?.FirstOrDefault()?.Username:null,
                    UserName=x.UserId!=null?_mc.Users.Find(k=>k.Id==x.UserId&&k.IsDeleted==false)?.FirstOrDefault()?.Name:null,
                    

                }).ToList();
                mr.Count = mr.Item.Count;
                return await Task.FromResult(mr);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<MethodResponse<MEmailSent.Response>> SingleGet(string id, string CurrentAccount)
        {
            try
            {
                MethodResponse<MEmailSent.Response> mr = new();




                #region Mongo
                var j = _mc.EmailSent.Find(k => k.Id == id && k.IsDeleted == false).FirstOrDefault();
                #endregion Mongo
                MEmailSent.Response response = new()
                {
                    Id = j.Id,
                AccountId= j.AccountId,
               UserId= j.UserId,
               EMailId= j.EMailId,
               Content = j.Content,
               Title = j.Title
               
                };
                mr.Item = response;

                return await Task.FromResult(mr);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<MethodResponse<MEmailSent.Response>> Update(MEmailSent.Form form, string CurrentAccount)
        {
            throw new NotImplementedException();
        }
    }
}
