using System.Collections.Generic;
using System.Linq;
using LabApi.Adapters;
using LabApi.Model;
using Microsoft.Extensions.Configuration;

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
            Mail dbMail = MailAdapter.ToData(mail, configuration.GetSection("RecipientsSeparator").Value[0]);
            dbMail.SendResult = result;
            dbMail.FailedMessage = failedMessage;
            context.Mails.Add(dbMail);
            context.SaveChanges();
        }

        /// <summary>
        /// Получение списка сообщений с полями Subject, Body и Recipients
        /// </summary>
        /// <returns></returns>
        public List<Mail> GetAllMails()
        {
            return context.Mails.Select(x => x).ToList();
        }
    }
}