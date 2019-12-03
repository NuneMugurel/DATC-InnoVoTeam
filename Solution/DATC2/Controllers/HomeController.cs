using Data;
using Data.Model;
using Datc.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DATC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public string DbInsert()
        {
            using (var repository = new RepositoryFactory())
            {
            }
            return "inserted";
        }

        public string DbGet()
        {
            using (var repository = new RepositoryFactory())
            {
                var voter = repository.GetRepository<Voter>().Get(x => x.FirstName == "Mugurel");
                return voter.ToString();
            }
        }
    }
}
