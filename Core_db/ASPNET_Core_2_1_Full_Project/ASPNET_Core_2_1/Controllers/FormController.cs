using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ASPNET_Core_2_1.DBLAYER;
using Microsoft.Extensions.Configuration;
using ASPNET_Core_2_1.Models;

namespace ASPNET_Core_2_1.Controllers
{
    public class FormController : Controller
    {
        public static IConfiguration config;

        [HttpGet]
        public IActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Giris(IFormCollection forma)
        {
            DBClass db = new DBClass(config);
            bool t;
            t = db.GetQeydiyyat(forma["Name"], forma["Email"], forma["Password"]);

            if (t == true)
            {
                return RedirectToAction();
            }
            else
            {
                return RedirectToAction("ParoluUnutmaq");
            }
        }
        [HttpGet]
        public IActionResult Qeydiyyat()
        {
            //DBClass db = new DBClass(config);
            //ListCins lc = db.LIC();
            //ViewBag.ss = lc;
            return View();
        }
        [HttpPost]
        public IActionResult Qeydiyyat(IFormCollection Collection)
        {
            DBClass db = new DBClass(config);

            string ad = Collection["ad"];
            string soyad = Collection["soyad"];
            string ataadi = Collection["ataadi"];
            string yas = Collection["yas"];
            string dogumtarixi = Collection["dogumtarixi"];
            string email = Collection["email"];
            string parol = Collection["parol"];
            string elaqenom = Collection["qeydiyyatkodu"];

            if (ad != null && soyad != null && ataadi != null && yas != null && dogumtarixi != null && email != null && parol != null && elaqenom != null)
            {
                if (Collection["prs"] == "add")
                {
                    db.setAdd_datadb(ad, soyad, ataadi, dogumtarixi, email, parol, elaqenom, yas);
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult ParoluUnutmaq()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ParoluUnutmaq(IFormCollection IForm)
        {
            DBClass db = new DBClass(config);
            bool t;
            t = db.setCheck_datadb(IForm["Email"]);

            if (t == true)
            {
                ViewBag.Success = "Parolu Muveffeqiyyetle elde etdiniz";
                return RedirectToAction("QeydiyyatParolu");

            }
            else
            {
                return RedirectToAction("ParoluUnutmaq");
            }

        }
        [HttpGet]
        public IActionResult QeydiyyatParolu()
        {
            return View();
        }
        [HttpPost]
        public IActionResult QeydiyyatParolu(IFormCollection IForm)
        {
            DBClass db = new DBClass(config);
            ListQeydiyyat listQeydiyyat = new ListQeydiyyat();
            Qeydiyyat qg = db.GetParol(IForm["code"]);
            listQeydiyyat.GQ = qg;
            if (listQeydiyyat.GQ.parol != "")
            {
                return View("ParoluGoster", listQeydiyyat);
            }
            else
            { return View(); }
        }
        [HttpGet]
        public IActionResult ParoluGoster()
        {
            
            return View();
        }
    }
}