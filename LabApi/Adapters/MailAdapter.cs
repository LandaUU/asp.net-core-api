using Microsoft.Extensions.Configuration;
using LabApi.Data;
using LabApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LabApi.Adapters
{
    /// <summary>
    /// Статический класс, конвертирующий данные, подаваемые в запрос в данные для базы данных и обратно
    /// </summary>
    public static class MailAdapter
    {

        /// <summary>
        /// Преобразование в класс данных для базы данных
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Data.Mail ToData(MailModel request)
        {
            Data.Mail dbMail = new Mail
            {
                Subject = request.Subject,
                Body = request.Body,
                DateCreate = DateTime.Now,
            };
            
            for (int i = 0; i < request.Recipients.Length; i++)
            {
                dbMail.Recipients.Add(new Recipient()
                {
                    RecipientName = request.Recipients[i],
                    Mail = dbMail
                });
            }
            return dbMail;
        }
        
        public static Data.Mail ToData(MailModel request, bool isRecipientsNull)
        {
            Data.Mail dbMail = new Mail
            {
                Subject = request.Subject,
                Body = request.Body,
                DateCreate = DateTime.Now,
            };

            if (!isRecipientsNull)
            {
                for (int i = 0; i < request.Recipients.Length; i++)
                {
                    dbMail.Recipients.Add(new Recipient()
                    {
                        RecipientName = request.Recipients[i],
                        Mail = dbMail
                    });
                }
            }

            return dbMail;
        }

        /// <summary>
        /// Преобразование в данные, возвращаемые в ответ на запрос
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public static MailModel ToModel(Mail mail)
        {
            MailModel response = new MailModel()
            {
                Subject = mail.Subject,
                Body = mail.Body,
                Recipients = mail.Recipients.Select(x => x.RecipientName).ToArray()
            };
            return response;
        }
    }
}
