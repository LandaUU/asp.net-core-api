namespace LabApi.Data
{
    /// <summary>
    /// Класс получателя
    /// </summary>
    public class Recipient
    {
        /// <summary>
        /// ID получателя
        /// </summary>
        public int RecipientId { get; set; }
        /// <summary>
        /// Имя получателя (email)
        /// </summary>
        public string RecipientName { get; set; }

        /// <summary>
        /// ID сообщения
        /// </summary>
        public int MailId { get; set; }
        /// <summary>
        /// Сообщение
        /// </summary>
        public Mail Mail { get; set; }
    }
}