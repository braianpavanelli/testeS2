using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteS2IT.Models;

namespace TesteS2IT.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        // GET: Friends
        public ActionResult Index()
        {
            return View(Friend.FindAllFriends());
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Friend friend)
        {
            if (ModelState.IsValid)
            {
                Friend.InsertFriend(friend);
                return RedirectToAction("Index");
            }
            else
            {
                return View(friend);
            }
        }


        public ActionResult Edit(int id)
        {
            var friend = Friend.FindFriend(id);

            if (friend == null)
            {
                return HttpNotFound();
            }

            return View(friend);
        }


        [HttpPost]
        public ActionResult Edit(Friend friend)
        {
            if (ModelState.IsValid)
            {
                Friend.UpdateFriend(friend);
                return RedirectToAction("Index");
            }
            else
            {
                return View(friend);
            }
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            Friend.ExcludeFriend(id);
            return Json(Friend.FindAllFriends());
        }
    }
}