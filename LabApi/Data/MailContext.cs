using Microsoft.EntityFrameworkCore;

namespace LabApi.Data
{
    /// <summary>
    /// Класс EF, в котором описываются таблицы в БД
    /// </summary>
    public class MailContext : DbContext
    {
        /// <summary>
        /// Конструктор класса, в котором считываются настройки, задаваемые в Startup.cs
        /// </summary>
        /// <param name="options"></param>
        public MailContext(DbContextOptions<MailContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Таблица сообщений
        /// </summary>
        public DbSet<Mail> Mails { get; set; }
    }
}