using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNET_Core_2_1.DBLAYER;
using ASPNET_Core_2_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ASPNET_Core_2_1.Controllers
{
    public class FormsController : Controller
    {
        public static IConfiguration conf;
        public IActionResult BasicFroms()
        {
            return View();
        }

        public IActionResult YeniForma(string idn)
        {
            if (idn == null)
            {
                DBClass db = new DBClass(conf);
                ListIsci li = db.LIDb();

                return View(li);
            }
            else
            {
                DBClass db = new DBClass(conf);
                ListIsci li = db.LIDb();
                Isci isc = db.GetIsci(idn);
                li.ISC = isc;

                return View(li);
            }
        }
        [HttpPost]
        public IActionResult YeniForma(IFormCollection Collection)
        {
            string ad = Collection["ad"];
            string soyad = Collection["soyad"];
            string yas = Collection["yas"];
            string IDN = Collection["idn"];
            DBClass db = new DBClass(conf);
            if (ad != null && soyad != null && yas != null)
            {
                if (Collection["prs"] == "insert")
                {
                    db.set_datadb(ad, soyad, yas);
                }
                if (Collection["prs"] == "update")
                {
                    db.setup_datadb(IDN, ad, soyad, yas);
                }
                if (Collection["prs"] == "delete")
                {
                    db.setdel_datadb(IDN);
                }
                if (Collection["prs"] == "refresh")
                {}
            }
            ListIsci li = db.LIDb();
            return View(li);
        }

        public IActionResult Advanced()
        {

            return View();
        }

        public IActionResult Wizard()
        {
            return View();
        }

        public IActionResult FileUpload()
        {
            return View();
        }


        public IActionResult TextEditor()
        {
            return View();
        }

        public IActionResult Markdown()
        {
            return View();
        }

        public IActionResult Autocomplete()
        {
            return View();
        }
    }
}