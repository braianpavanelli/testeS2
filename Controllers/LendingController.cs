using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteS2IT.Models;

namespace TesteS2IT.Controllers
{
    [Authorize]
    public class LendingController : Controller
    {

        // GET: Lending
        public ActionResult Index()
        {
            return View(Lending.FindAllLending());
        }

        public ActionResult Create()
        {
            ViewBag.Friends = new SelectList(Friend.FindAllFriends().OrderBy(p => p.Name), "ID", "Name");
            ViewBag.Games = new SelectList(Games.FindNoLendedGames().OrderBy(p => p.Name), "ID", "Name");
            return View();
        }


        [HttpPost]
        public ActionResult Create(Lending lending)
        {
            if (ModelState.IsValid)
            {
                Lending.InsertLending(lending);
                return RedirectToAction("Index");
            }
            else
            {
                return View(lending);
            }
        }


        public ActionResult Edit(int id)
        {
            var lending = Lending.FindLending(id);

            if (lending == null)
            {
                return HttpNotFound();
            }

            ViewBag.Friends = new SelectList(Friend.FindAllFriends().OrderBy(p => p.Name), "ID", "Name");
            ViewBag.Games = new SelectList(Games.FindAllGames().OrderBy(p => p.Name), "ID", "Name");
            return View(lending);
        }


        [HttpPost]
        public ActionResult Edit(Lending lending)
        {
            if (ModelState.IsValid)
            {
                Lending.UpdateLendingItem(lending);
                return RedirectToAction("Index");
            }
            else
            {
                return View(lending);
            }
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            Lending.ExcludeLending(id);
            return Json(Lending.FindAllLending());
        }
    }
}
