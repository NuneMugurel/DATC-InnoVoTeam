using Data.Model;
using Datc.Data.Repository;
using DATC2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace DATC2.Controllers
{
    public class CandidateController : Controller
    {
        // GET: Candidate
        public ActionResult Index()
        {
            IEnumerable<CandidateViewModel> students = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:61178/api/");
                //HTTP GET
                var responseTask = client.GetAsync("CandidatesApi");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CandidateViewModel>>();
                    readTask.Wait();


                    students = from g in readTask.Result
                               select new CandidateViewModel
                               {
                                   Id = g.Id,
                                   FirstName = g.FirstName,
                                   LastName = g.LastName,
                                   PartyName = g.PartyName
                               };
                }
                else //web api sent error response 
                {
                    //log response status here..

                    students = Enumerable.Empty<CandidateViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(students);
        }

        public ActionResult Delete(int Id)
        {
            return View(new CandidatesApiController().DeleteCandidate(Id));
        }

        public string insertTest()
        {
            using (var repository = new RepositoryFactory())
            {
                var candidate = new Candidate { FirstName = "Miai", LastName = "Coada", PartyName = "TAT" };
                repository.GetRepository<Candidate>().Insert(candidate);
            }
            return "inserted";
        }
    }
}