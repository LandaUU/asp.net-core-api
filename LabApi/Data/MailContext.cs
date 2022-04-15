using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabApi.Data
{
    /// <summary>
    /// Класс EF, в котором описываются таблицы в БД
    /// </summary>
    public class MailContext : DbContext
    {
        /// <summary>
        /// Таблица сообщений
        /// </summary>
        public DbSet<Mail> Mails { get; set; }
        
        public DbSet<Recipient> Recipients { get; set; }


        /// <summary>
        /// Конструктор класса, в котором считываются настройки, задаваемые в Startup.cs
        /// </summary>
        /// <param name="options"></param>
        public MailContext(DbContextOptions<MailContext> options)
    : base(options)
        {

        }
    }
}
