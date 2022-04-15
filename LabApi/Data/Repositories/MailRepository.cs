using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LabApi.Adapters;
using LabApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabApi.Data.Repositories
{
    /// <summary>
    /// Класс для взаимодействия с данными из базы данных
    /// </summary>
    public class MailRepository : IMailRepository
    {
        private readonly MailContext context;
        private IConfiguration configuration;

        public MailRepository(MailContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        /// <summary>
        /// Добавление записи сообщения в базу данных
        /// </summary>
        /// <param name="mail">Запрос, отправляемый пользователем</param>
        /// <param name="result">Результат добавления в базу данных</param>
        /// <param name="failedMessage">Текст ошибки</param>
        public void AddMail(MailModel mail, SendResult result, string failedMessage)
        {
            Mail dbMail = MailAdapter.ToData(mail, true);
            dbMail.SendResult = result;
            dbMail.FailedMessage = failedMessage;
            context.Mails.Add(dbMail);
            context.SaveChanges();
            foreach (string recipient in mail.Recipients)
            {
                AddRecipient(recipient, dbMail.MailId);
            }
            context.SaveChanges();
        }

        /// <summary>
        /// Получение списка сообщений с полями Subject, Body и Recipients
        /// </summary>
        /// <returns></returns>
        public List<MailModel> GetAllMails()
        {
            var dataList = context.Mails.Select(x => x).Include(x => x.Recipients);
            return dataList.Select(x => MailAdapter.ToModel(x)).ToList();
        }

        public bool AddRecipient(string requestRecipient, int mailId)
        {
            Recipient recipient = new Recipient()
            {
                RecipientName = requestRecipient,
                MailId = mailId
            };

            context.Recipients.Add(recipient);
            return context.SaveChanges() > 0;
        }
    }
}
