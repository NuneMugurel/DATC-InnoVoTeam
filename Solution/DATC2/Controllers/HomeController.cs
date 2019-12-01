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
                var candidate = repository.GetRepository<Candidate>().Get(x => x.FirstName == "Barna").FirstOrDefault();

                var voter = repository.GetRepository<Voter>().Get(x => x.FirstName == "Sorin").FirstOrDefault();
                voter.Candidate = candidate;
                repository.GetRepository<Voter>().Update(voter);
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
