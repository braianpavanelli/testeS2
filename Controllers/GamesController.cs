using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using TesteS2IT.Models;

namespace TesteS2IT.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
        // GET: Games
                public ActionResult Index()
        {
            return View(Games.FindAllGames());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Games game, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {

                //if (Request.Files.Count > 0)
                //{
                //    var file = Request.Files[0];

                //    if (file != null && file.ContentLength > 0)
                //    {
                //        var fileName = "game_" + Path.GetFileName(game.Name) + game.Year + ".jpg";
                //        var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                //        file.SaveAs(path);
                //        game.UrlImage = path;
                //    }
                //}

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = "game_" + Regex.Replace(game.Name, @"\s", "_") + game.Year + ".jpg";
                    var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    file.SaveAs(path);
                    game.UrlImage = "~/Images/" + fileName;
                }

                Games.InsertGame(game);
                return RedirectToAction("Details", new { id = game.ID });
            }
            else
            {
                return View(game);
            }
        }

        public ActionResult Details(int id)
        {
            var game = Games.FindGame(id);

            if (game == null)
            {
                return HttpNotFound();
            }

            return View(game);
        }

        public ActionResult Edit(int id)
        {
            var game = Games.FindGame(id);

            if (game == null)
            {
                return HttpNotFound();
            }

            return View(game);
        }


        [HttpPost]
        public ActionResult Edit(Games game, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    if (!string.IsNullOrEmpty(game.UrlImage))
                    {
                        var pathDelete = Path.Combine(Server.MapPath("~"), game.UrlImage);
                        if (System.IO.File.Exists(pathDelete)) { System.IO.File.Delete(pathDelete); };
                    }
                    var fileName = "game_" + Regex.Replace(game.Name, @"\s", "_") + game.Year + ".jpg";
                    var path = Path.Combine(Server.MapPath("~/Images/"), fileName);

                    file.SaveAs(path);
                    game.UrlImage = "~/Images/" + fileName;
                }

                Games.UpdateGames(game);
                return RedirectToAction("Index");
            }
            else
            {
                return View(game);
            }
        }

        [HttpPost]
        public ActionResult DeleteImage(Games game)
        {
            if (ModelState.IsValid)
            {
                game = Games.FindGame(game.ID);
                game.UrlImage = string.Empty;
                Games.UpdateGames(game);
            }
            //return Json(Games.FindGame(game.ID));
            return Json(new { Url = game.ID });
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            Games.ExcludeGame(id);
            return Json(Games.FindAllGames());
        }
    }
}