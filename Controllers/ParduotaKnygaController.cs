using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.Models;
using System.Web.UI;
using System.Net;
using System.Net.Mail;



namespace AutoNuoma.Controllers
{
    public class ParduotaKnygaController : Controller
    {
        ParduotaKnygaRepository pknygaRepository = new ParduotaKnygaRepository();
        public ActionResult Index()
        {
            string profileData = this.Session["User"].ToString();
            string user = profileData;
            ModelState.Clear();
            return View(pknygaRepository.GetParduotosKnygos(user));
        }

        [HttpGet]
        public ActionResult Create()
        {
            var fromAddress = new MailAddress("***@gmail.com", "Knygu Shop");
            var toAddress = new MailAddress("*****@gmail.com");
            const string fromPassword = "**************";
            const string subject = "Ataskaita";

            string profileData = this.Session["User"].ToString();
            string user = profileData;

            var knygos = pknygaRepository.GetParduotosKnygos(user);

            var html = string.Empty;

            html += string.Format("<table class='table table - striped datatable'><thead> <tr style='background-color: lightgreen'> ");
            html += string.Format("<th>{0}</th><th>{1}</th><th>{2}</th><th>{3}</th><th>{4}</th></thead><tbody>", "Pavadinimas", "Autorius", "Kiekis", "ISBN", "Pardavėjas");
            foreach (var knyga in knygos)
            {
                html += string.Format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>", knyga.pavadinimas, knyga.autorius, knyga.kiekis, knyga.isbn, knyga.pardavejas);
            }
            html += string.Format("</tbody></table>");
            

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = html
            })
            {
                smtp.Send(message);
            }
            return RedirectToAction("Index");

        }

    }
}