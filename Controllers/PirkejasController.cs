﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoNuoma.Repos;
using AutoNuoma.ViewModels;
using AutoNuoma.Models;


namespace AutoNuoma.Controllers
{
    public class PirkejasController : Controller
    {
        NaudotojasRepository naudotojasRepository = new NaudotojasRepository();
        LytisRepository lytisRepository = new LytisRepository();
        PirkejasRepository pirkejasRepository = new PirkejasRepository();
        PrekiuKrepselisRepository krepselisRepository = new PrekiuKrepselisRepository();
        // GET: Pirkejas
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            NaudotojasRegisterViewModel pirkejas = new NaudotojasRegisterViewModel();
            PopulateSelections(pirkejas);
            return View();
            //return RedirectToAction("Meniu", "Pirkejas");

        }
        [HttpPost]
        public ActionResult Register(NaudotojasRegisterViewModel collection)
        {
            try
            {
                NaudotojasRegisterViewModel naudotojas = naudotojasRepository.getNaudotojas(collection.prisijungimo_vardas);
                Console.WriteLine(collection.prisijungimo_vardas);
                if(naudotojas.prisijungimo_vardas != null)
                {
                    ModelState.AddModelError("prisijungimo_vardas", "Naudotojas su tokiu prisijungimo vardu jau egzistuoja duomenų bazėje.");
                    return View(collection);
                }
                else
                {
                    collection.role = 0;
                    naudotojasRepository.addNaudotojas(collection);
                    krepselisRepository.addKrepselis(collection.prisijungimo_vardas);
                    PopulateSelections(collection);
                    return RedirectToAction("Meniu", "Pirkejas");
                }
                

            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }
        public void PopulateSelections(NaudotojasRegisterViewModel naudotojasRegisterView)
        {
            var lytys = lytisRepository.GetLytis();

            List<SelectListItem> selectListLytis = new List<SelectListItem>();


            foreach (var item in lytys)
            {
                selectListLytis.Add(new SelectListItem() { Value = Convert.ToString(item.id_lytis), Text = item.pavadinimas });
            }


            naudotojasRegisterView.lytisList = selectListLytis;

        }
        public ActionResult Meniu()
        {
            return View("Meniu");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Naudotojas naudotojas)
        {
                string password = pirkejasRepository.getPassword(naudotojas.prisijungimo_vardas);
                if (password != null)
                {
                    if (password == naudotojas.slaptazodis)
                    {
                    var profileData = new UserProfileSessionDataController { Username = naudotojas.prisijungimo_vardas };
                        this.Session["User"] = profileData.Username;
                        return RedirectToAction("Meniu", "Pirkejas");
                    }
                    else
                    {
                        ModelState.AddModelError("", "invalid Username or Password");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "invalid Username or Password");
                    return View();
                }

            return View(naudotojas);
        }

    }
}