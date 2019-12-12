using Business.Api;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class VotersController : Controller
    {
        // GET: Voters
        public ActionResult Index()
        {
            var voters = ApiConsumer<Voter>.ConsumeGet("Voters");
            var votersViewModelList = new List<VoterViewModel>();
            foreach(var voter in voters)
            {
                var voterViewModel = new VoterViewModel
                {
                    FirstName = voter.FirstName,
                    Id = voter.Id,
                    LastName = voter.LastName,
                    Cnp = voter.Cnp
                };
                try
                {
                    voterViewModel.SecretQuestionCounter = voter.SecretQuestions.Count;
                }
                catch
                {
                    voterViewModel.SecretQuestionCounter = 0;
                }
                votersViewModelList.Add(voterViewModel);
            }
            return View(votersViewModelList);
        }

        // GET: Voters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Voters/Create
        [HttpPost]
        public ActionResult Create(VoterViewModel voterViewModel)
        {
            try
            {
                var voter = new Voter
                {
                    FirstName = voterViewModel.FirstName,
                    Id = voterViewModel.Id,
                    LastName = voterViewModel.LastName,
                    Cnp = voterViewModel.Cnp
                };
                ApiConsumer<Voter>.ConsumePost("Voters", voter);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Voters/Edit/5
        public ActionResult Edit(int id)
        {
            var voter = ApiConsumer<Voter>.ConsumeGet("Voters", id);
            var voterViewModel = new VoterViewModel
            {
                FirstName = voter.FirstName,
                Id = voter.Id,
                LastName = voter.LastName,
                Cnp = voter.Cnp
            };
            return View(voterViewModel);
        }

        // POST: Voters/Edit/5
        [HttpPost]
        public ActionResult Edit(VoterViewModel voterViewModel)
        {
            try
            {
                var voter = new Voter
                {
                    FirstName = voterViewModel.FirstName,
                    Id = voterViewModel.Id,
                    LastName = voterViewModel.LastName,
                    Cnp = voterViewModel.Cnp
                };
                ApiConsumer<Voter>.ConsumePut("Voters", voter);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Voters/Delete/5
        public ActionResult Delete(int id)
        {
            var voter = ApiConsumer<Voter>.ConsumeGet("Voters", id);
            var voterViewModel = new VoterViewModel
            {
                FirstName = voter.FirstName,
                Id = voter.Id,
                LastName = voter.LastName,
                Cnp = voter.Cnp
            };
            return View(voterViewModel);
        }

        // POST: Voters/Delete/5
        [HttpPost]
        public ActionResult Delete(VoterViewModel voterViewModel)
        {
            try
            {
                ApiConsumer<object>.ConsumeDelete("Voters", voterViewModel.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
