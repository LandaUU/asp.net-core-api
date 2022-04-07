using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using LabApi.Data;
using LabApi.Data.Repositories;
using LabApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LabApi.Controllers
{
    /// <summary>
    /// Класс контроллера, принимающий запросы
    /// </summary>
    [Route("api/mails")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private IConfiguration _configuration;
        private ILogger<MailController> _logger;
        private IMailRepository _repository;

        /// <summary>
        /// Конструктор с параметрами для Dependency injection
        /// </summary>
        /// <param name="repository">экземпляр класса репозитория для взаимодействия с данными в базе данных</param>
        /// <param name="configuration">экземпляр класса для считывания данных из appsettings.json</param>
        /// <param name="logger">экземпляр класса для логгирования успешных и неуспешных операций</param>
        public MailController(IMailRepository repository, IConfiguration configuration, ILogger<MailController> logger)
        {
            _repository = repository;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// POST-запрос, отправляющий сообщение на mail и сохраняющий его в БД
        /// </summary>
        /// <param name="mail">Тело запроса, включающее отправителя, тело сообщения и адресатов</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult PostMethod(MailModel mail)
        {
            _logger.LogInformation(
                @$"POST-request, Subject: {mail.Subject}, Recipients : {string.Join(",", mail.Recipients)}");
            _logger.LogDebug($@"Body of message:\n{mail.Body}");
            string failedMessage = "";
            try
            {
                if (SendMail(mail.Subject, mail.Body, mail.Recipients, ref failedMessage))
                {
                    _logger.LogInformation("E-Mail Message succesfully sent, OK");
                    _repository.AddMail(mail, SendResult.OK, failedMessage);
                }
                else
                {
                    _repository.AddMail(mail, SendResult.Failed, failedMessage);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Received an error while writing message to database: {ex.Message}");
                return StatusCode(500);
            }
        }

        /// <summary>
        /// GET-запрос, возвращающий список сообщений в JSON формате
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Data.Mail>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult GetMethod()
        {
            try
            {
                var mails = _repository.GetAllMails();
                _logger.LogInformation(@$"GET-request, getting {mails.Count} messages");
                return Ok(mails);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Received an error while getting messages: {ex.Message}");
                return StatusCode(500);
            }
        }


        /// <summary>
        /// Метод, отправляющий сообщение по SMTP протоколу.
        /// Параметры SMTP клиента задаются в appsettings.json
        /// </summary>
        /// <param name="subject">Отправитель</param>
        /// <param name="body">Тело сообщения</param>
        /// <param name="recipients">Получатели</param>
        /// <param name="failedMessage">Текст ошибки</param>
        /// <returns></returns>
        [NonAction]
        public bool SendMail(string subject, string body, string[] recipients, ref string failedMessage)
        {
            try
            {
                using (SmtpClient client =
                       new SmtpClient(_configuration.GetSection("SMTP").GetSection("SmtpHost").Value,
                           int.Parse(_configuration.GetSection("SMTP").GetSection("Port").Value)))
                {
                    client.Credentials = new NetworkCredential()
                    {
                        UserName = _configuration.GetSection("SMTP").GetSection("Username").Value,
                        Password = _configuration.GetSection("SMTP").GetSection("Password").Value
                    };
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = true;
                    client.Send(_configuration.GetSection("SMTP").GetSection("Username").Value,
                        string.Join(",", recipients), subject, body);
                    return true;
                }
            }
            catch (Exception ex)
            {
                failedMessage = ex.Message;
                _logger.LogError($"Received an error while sending a message : {ex.Message}");
                return false;
            }
        }
    }
}