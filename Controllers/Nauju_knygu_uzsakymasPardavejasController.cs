using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.Models;
using AutoNuoma.ViewModels;
using System.Net;
using System.Configuration;
using System.Diagnostics;

namespace AutoNuoma.Controllers
{
    public class Nauju_knygu_uzsakymasPardavejasController : Controller
    {
        NaujasKnyguUzsakymasPardavejasRepository uzsakymasRepository = new NaujasKnyguUzsakymasPardavejasRepository();
        ZanraiRepository zandraiRepository = new ZanraiRepository();
        KalbosRepository kalbosRepository = new KalbosRepository();

        public ActionResult Index()
        {
            ModelState.Clear();
            return View(uzsakymasRepository.getUzsakymai());
        }

        public ActionResult Delete(int id)
        {
            Nauju_knygu_uzsakymas uzsakymasEditViewModel = uzsakymasRepository.getUzsakymas(id);
            return View(uzsakymasEditViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                bool naudojama = false;


                if (!naudojama)
                {
                    uzsakymasRepository.deleteUsakymas(id);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


    }
}