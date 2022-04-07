namespace LabApi.Model
{
    /// <summary>
    /// Класс запроса для метода MailController.PostMethod
    /// </summary>
    public class MailModel
    {
        public MailModel()
        {
        }

        public MailModel(string subject, string body, string[] recipients)
        {
            Subject = subject;
            Body = body;
            Recipients = recipients;
        }

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
    }
}