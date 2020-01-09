﻿using Business.Api;
using Data.Model;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using Web.Models;
using System.Linq;

namespace Web.Controllers
{
    public class VotingSessionController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SessionStats()
        {
            var votingSessions = ApiConsumer<VotingSession>.ConsumeGet("VotingSessions");
            if (votingSessions.Count() > 0)
            {
                var latestDate = votingSessions.Max(s => s.EndDate);
                var latestSession = votingSessions.Where(s => s.EndDate == latestDate).FirstOrDefault();
                if (latestSession != null && DateTime.Compare(DateTime.Now, latestSession.EndDate) < 0)
                {
                    //TODO: take stuff from queue and put it into db if necessary
                    var votes = ApiConsumer<Vote>.ConsumeGet("Votes");
                    var sessionVotesList = ((List<Vote>)votes).FindAll(v => v.VotingSession.Name == latestSession.Name);
                    var candidatesList = new List<CandidatePerSessionViewModel>();
                    var totalCandidates = latestSession.Candidates.Count;
                    foreach (var candidate in latestSession.Candidates)
                    {
                        var candidateVotes = 0;
                        var candidatePerSession = new CandidatePerSessionViewModel
                        {
                            FirstName = candidate.FirstName,
                            LastName = candidate.LastName,
                            PartyName = candidate.PartyName
                        };
                        foreach (var vote in votes)
                            if (vote.Candidate.FirstName == candidate.FirstName && vote.Candidate.LastName == candidate.LastName)
                                candidateVotes++;
                        candidatePerSession.VotePercentage = (candidateVotes / totalCandidates).ToString() + "%";
                        candidatesList.Add(candidatePerSession);
                    }
                    ViewBag.SessionName = latestSession.Name;
                    ViewBag.SessionId = latestSession.Id;
                    return PartialView("SessionStats", candidatesList);
                }
            }
            var latestSessionViewModel = new VotingSessionViewModel();
            return PartialView("Create", latestSessionViewModel);
        }

        [HttpPost]
        public ActionResult Create(VotingSessionViewModel votingSessionViewModel)
        {
            var votingSession = new VotingSession { StartDate = DateTime.Now, EndDate = votingSessionViewModel.EndDate, Name = votingSessionViewModel.Name };
            var candidates = ApiConsumer<Candidate>.ConsumeGet("Candidates");
            votingSession.Candidates = (ICollection<Candidate>)candidates;
            //foreach(var candidate in candidates)
            //{
            //    candidate.VotingSessions.Add(votingSession);
            //}
            //foreach(var candidate in candidates)
            //{
            //    foreach (var sss in candidate.VotingSessions)
            //        sss.Candidates = null;
            //    var response = ApiConsumer<Candidate>.ConsumePut("Candidates", candidate);
            //}
            var response = ApiConsumer<VotingSession>.ConsumePost("VotingSessions", votingSession);
            return RedirectToAction("Index");
        }

        public ActionResult StopSession(int id)
        {
            var runningSession = ApiConsumer<VotingSession>.ConsumeGet("VotingSessions", id);
            runningSession.EndDate = DateTime.Now;
            ApiConsumer<VotingSession>.ConsumePut("VotingSessions", runningSession);
            return RedirectToAction("Index");
        }
    }
}