using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabApi.Model
{
    /// <summary>
    /// Класс запроса для метода MailController.PostMethod
    /// </summary>
    public class MailModel
    {
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
        public string[] Recipients { get; set; }

        public MailModel()
        {

        }

        public MailModel(string subject, string body, string[] recipients)
        {
            Subject = subject;
            Body = body;
            Recipients = recipients;
        }

        public bool Validate()
        {
            var isSubjectValid = System.Net.Mail.MailAddress.TryCreate(Subject, out _);
            if (isSubjectValid)
            {
                for (int i = 0; i < Recipients.Length; i++)
                {
                    if (!System.Net.Mail.MailAddress.TryCreate(Recipients[i], out _))
                        return false;
                }
            }
            else
            {
                return false;
            }
            
            return true;
        }
    }
}
