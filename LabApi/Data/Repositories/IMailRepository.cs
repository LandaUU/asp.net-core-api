using System.Collections.Generic;
using LabApi.Model;

namespace LabApi.Data.Repositories
{
    /// <summary>
    /// Интерфейс класса репозитория для взаимодействия с базой данных
    /// </summary>
    public interface IMailRepository
    {
        void AddMail(MailModel mail, SendResult result, string failedMessage);

        List<Mail> GetAllMails();
    }
}