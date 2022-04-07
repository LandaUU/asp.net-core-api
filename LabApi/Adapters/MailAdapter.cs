using System;
using LabApi.Data;
using LabApi.Model;

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
        public static Data.Mail ToData(MailModel request, char separator)
        {
            Data.Mail dbMail = new Mail
            {
                Subject = request.Subject,
                Body = request.Body,
                DateCreate = DateTime.Now,
            };
            dbMail.Recipients = string.Join(separator, request.Recipients);

            return dbMail;
        }

        /// <summary>
        /// Преобразование в данные, возвращаемые в ответ на запрос
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public static MailModel ToModel(Mail mail, char separator)
        {
            MailModel response = new MailModel()
            {
                Subject = mail.Subject,
                Body = mail.Body,
                Recipients = mail.Recipients.Split(separator)
            };
            return response;
        }
    }
}