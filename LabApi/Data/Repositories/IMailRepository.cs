using LabApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabApi.Data.Repositories
{
    /// <summary>
    /// Интерфейс класса репозитория для взаимодействия с базой данных
    /// </summary>
    public interface IMailRepository
    {
        void AddMail(MailModel mail, SendResult result, string failedMessage);

        List<MailModel> GetAllMails();
    }
}
