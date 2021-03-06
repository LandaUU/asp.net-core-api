using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabApi.Data
{
    /// <summary>
    /// Класс сообщения для хранения в БД
    /// </summary>
    public class Mail
    {
        /// <summary>
        /// ID сообщения
        /// </summary>
        public int MailId { get; set; }

        /// <summary>
        /// Отправитель
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Тело сообщения
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// Получатели
        /// </summary>
        public List<Recipient> Recipients { get; set; }
        /// <summary>
        /// Дата создания записи сообщения в БД
        /// </summary>
        public DateTime DateCreate { get; set; }
        /// <summary>
        /// Результат отправки сообщения (OK, Failed)
        /// </summary>
        public SendResult SendResult { get; set; }
        /// <summary>
        /// Описание ошибки при отправлении сообщения
        /// </summary>
        public string FailedMessage { get; set; }
    }
}
