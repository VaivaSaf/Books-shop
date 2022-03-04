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
    public class Nauju_knygu_uzsakymasController : Controller
    {
        Nauju_knygu_uzsakymasRepository uzsakymasRepository = new Nauju_knygu_uzsakymasRepository();
        ZanraiRepository zandraiRepository = new ZanraiRepository();
        KalbosRepository kalbosRepository = new KalbosRepository();

        public ActionResult Index()
        {
            ModelState.Clear();
            return View(uzsakymasRepository.getUzsakymai());
        }

        public ActionResult Create()
        {
            Nauju_knygu_uzsakymas uzsakymas = new Nauju_knygu_uzsakymas();
            return View(uzsakymas);
        }

        // POST: Klientas/Create
        [HttpPost]
        public ActionResult Create(Nauju_knygu_uzsakymas collection)
        {
            try
            {
                // Patikrinama ar klientas su tokiu asmens kodu jau egzistuoja
                Nauju_knygu_uzsakymas uzsak = uzsakymasRepository.getUzsakymas(collection.id);
                
                    uzsakymasRepository.addUzsakymas(collection);
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }
        public ActionResult Edit(int id)
        {
            Nauju_knygu_uzsakymas uzsakymas = uzsakymasRepository.getUzsakymas(id);
            PopulateSelections(uzsakymas);
            return View(uzsakymas);
        }

        [HttpPost]
        public ActionResult Edit(int id, Nauju_knygu_uzsakymas collection)
        {
            try
            {
               uzsakymasRepository.updateKnyga(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                //PopulateSelections(collection);
                return View(collection);
            }
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

        public void PopulateSelections(Nauju_knygu_uzsakymas uzsakymasEditViewModel)
        {
            var kalbos = kalbosRepository.GetKalba();
            var zandrai = zandraiRepository.GetZanrai();
            List<SelectListItem> selectListItemsK = new List<SelectListItem>();
            List<SelectListItem> selectListItemsZ = new List<SelectListItem>();

            foreach (var item in kalbos)
            {
                selectListItemsK.Add(new SelectListItem() { Value = Convert.ToString(item.id_kalbos), Text = item.pavadinimas });
            }
            foreach (var item in zandrai)
            {
                selectListItemsZ.Add(new SelectListItem() { Value = Convert.ToString(item.id_zanrai), Text = item.pavadinimas });
            }
           // uzsakymasEditViewModel.zanrasList = selectListItemsZ;
            //uzsakymasEditViewModel.kalbaList = selectListItemsK;




        }
    }
}