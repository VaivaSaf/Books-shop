using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.ViewModels;
using AutoNuoma.Models;
using System.Diagnostics;

namespace AutoNuoma.Controllers
{
    public class KnygaController :Controller
    {
        KnygaRepository knygaRepository = new KnygaRepository();
        ZanraiRepository zandraiRepository = new ZanraiRepository();
        KalbosRepository kalbosRepository = new KalbosRepository();
       KrepselioKnygaRepository krepselioKnygaRepository = new KrepselioKnygaRepository();
        PrekiuKrepselisRepository krepselisRepository = new PrekiuKrepselisRepository();
       NaujasKnyguUzsakymasPardavejasRepository uzsakymasRepository = new NaujasKnyguUzsakymasPardavejasRepository();

        public ActionResult Index()
        {
            string profileData = this.Session["User"].ToString();
            string user = profileData;
            ModelState.Clear();
            return View(knygaRepository.GetKnygaP(user));
        }
        public ActionResult IndexA()
        {
            ModelState.Clear();
            return View(knygaRepository.GetKnyga1());
        }

        public ActionResult Create()
        {
            KnygaEditViewModel knygasEditViewModel = new KnygaEditViewModel();
            PopulateSelections(knygasEditViewModel);
            return View(knygasEditViewModel);
        }
        public ActionResult CreateP()
        {
            KnygaEditViewModel uzsakymas = new KnygaEditViewModel();
            PopulateSelections(uzsakymas);
            return View(uzsakymas);
        }
        [HttpPost]
        public ActionResult CreateP(KnygaEditViewModel collection,int id, int kiekis, int kalba, int zanras )
        {
            try
            {
                // Patikrinama ar klientas su tokiu asmens kodu jau egzistuoja
                Nauju_knygu_uzsakymas uzsak = uzsakymasRepository.getUzsakymas(id);
                KnygaEditViewModel knyga = knygaRepository.GetKnyga(collection.ISBN);


                if (knyga.ISBN != null)
                {
                    // ModelState.AddModelError("Kodas", "Knyga su tokiu kodu jau užregistruota");
                    return View(collection);
                }
                //Jei nera sukuria nauja klienta
                else
                {
                    string profileData = this.Session["User"].ToString();
                    string user = profileData;
                    knygaRepository.addKnygaP(collection, kiekis, kalba, zanras,user);
                    uzsakymasRepository.deleteUsakymas(id);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        [HttpPost]
        public ActionResult Create(KnygaEditViewModel collection)
        {
            try
            {
                KnygaEditViewModel knyga = knygaRepository.GetKnyga(collection.ISBN);

                if (knyga.ISBN != null)
                {
                  //  ModelState.AddModelError("tabelio_nr", "Gydytojas su tokiu tabelio numeriu jau egzistuoja duomenų bazėje.");
                    return View(collection);
                }
                if (ModelState.IsValid)
                {
                    string profileData = this.Session["User"].ToString();
                    string user = profileData;
                    knygaRepository.addKnyga1(collection,user);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }
        public ActionResult CreateA()
        {
            KnygaEditViewModel knygasEditViewModel = new KnygaEditViewModel();
            PopulateSelections(knygasEditViewModel);
            return View(knygasEditViewModel);
        }

        [HttpPost]
        public ActionResult CreateA(KnygaEditViewModel collection)
        {
            try
            {
                KnygaEditViewModel knyga = knygaRepository.GetKnyga(collection.ISBN);

                if (knyga.ISBN != null)
                {
                    ModelState.AddModelError("ISBN", "Knyga su tokiu ISBN numeriu jau egzistuoja duomenų bazėje.");
                    return View(collection);
                }
                if (ModelState.IsValid)
                {
                    Debug.WriteLine("*");
                    knygaRepository.addKnyga(collection);
                    return RedirectToAction("IndexA");
                }
                return View(collection);

            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        public ActionResult Edit(string id)
        {
            KnygaEditViewModel knygaEditViewModel = knygaRepository.GetKnyga(id);
            PopulateSelections(knygaEditViewModel);
            return View(knygaEditViewModel);
        }

        [HttpPost]
        public ActionResult Edit(string id, KnygaEditViewModel collection)
        {
            try
            {
                string profileData = this.Session["User"].ToString();
                string user = profileData;
                knygaRepository.updateKnygaP(collection,user);
                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }
        public ActionResult EditA(string id)
        {
            KnygaEditViewModel knygaEditViewModel = knygaRepository.GetKnyga(id);
            PopulateSelections(knygaEditViewModel);
            return View(knygaEditViewModel);
        }

        [HttpPost]
        public ActionResult EditA(string id, KnygaEditViewModel collection)
        {
            try
            {
                knygaRepository.updateKnyga(collection);
                return RedirectToAction("IndexA");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        public ActionResult Delete(string id)
        {
            KnygaEditViewModel knygaEditViewModel = knygaRepository.GetKnyga(id);
            return View(knygaEditViewModel);
        }

        [HttpPost]
        public ActionResult Delete(string id, FormCollection collection)
        {
            try
            {
                bool naudojama = false;


                if (!naudojama)
                {
                    knygaRepository.deleteKnyga(id);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DeleteA(string id)
        {
            KnygaEditViewModel knygaEditViewModel = knygaRepository.GetKnyga(id);
            return View(knygaEditViewModel);
        }

        [HttpPost]
        public ActionResult DeleteA(string id, FormCollection collection)
        {
            try
            {
                bool naudojama = false;


                if (!naudojama)
                {
                    knygaRepository.deleteKnyga(id);
                }

                return RedirectToAction("IndexA");
            }
            catch
            {
                return View();
            }
        }
        public void PopulateSelections(KnygaEditViewModel knygaEditViewModel)
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
            knygaEditViewModel.zanrasList = selectListItemsZ;
            knygaEditViewModel.kalbaList = selectListItemsK;




        }
        public ActionResult Info(string id)
        {
            KnygaEditViewModel knygaEditViewModel = knygaRepository.GetKnyga(id);
            return View(knygaEditViewModel);
        }

        [HttpPost]
        public ActionResult Info(string id, FormCollection collection, string delete, string up)
        {
            
            try
            {

                if (!string.IsNullOrEmpty(delete))
                {
                    knygaRepository.deleteKnyga(id);
                    return RedirectToAction("IndexA");
                }
                if (!string.IsNullOrEmpty(up))
                {
                    knygaRepository.upKnyga(id, 1);
                    return RedirectToAction("IndexA");
                }

                return RedirectToAction("IndexA");
            }
            
            catch
            {
                return View();
            }
        }

        public ActionResult IndexPirkejui()
        {
            var profileData = this.Session["User"];
            ModelState.Clear();
            return View(knygaRepository.GetKnyga1()); 

        }
       /* public ActionResult AddTo()
        {
            //KnygaEditViewModel knygasEditViewModel = new KnygaEditViewModel();
            //PopulateSelections(knygasEditViewModel);
            return RedirectToAction("IndexPirkejui");
        }*/

        //[HttpPost]
        /*public ActionResult AddTo(string id)
        {
            try
            {
                KnygaEditViewModel knygaView = knygaRepository.GetKnyga(id);
                KrepselioKnyga collection = new KrepselioKnyga();
                collection.fk_KnygaISBN = id;
                string profileData = this.Session["User"].ToString();
                string user = profileData;
                int krepselioID = krepselisRepository.getID(user);
                KrepselioKnyga knyga = krepselioKnygaRepository.getKnyga(collection.fk_KnygaISBN);
                if (knyga.fk_KnygaISBN != null)
                {
                    krepselisRepository.updateKrepselis((decimal)knygaView.kaina, user);
                    krepselioKnygaRepository.updateKnygosKieki(knyga);
                    //  ModelState.AddModelError("tabelio_nr", "Gydytojas su tokiu tabelio numeriu jau egzistuoja duomenų bazėje.");
                    return RedirectToAction("IndexPirkejui");
                }
                collection.fk_Prekiu_krepselis = krepselioID;
                krepselisRepository.updateKrepselis((decimal)knygaView.kaina, user);
                krepselioKnygaRepository.addKnyga(collection);

                return RedirectToAction("IndexPirkejui");
            }
            catch
            {
                //PopulateSelections(collection);
                return RedirectToAction("IndexPirkejui");
            }
        }*/

    }
}