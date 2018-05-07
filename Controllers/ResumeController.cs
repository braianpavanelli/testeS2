using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TesteS2IT.Controllers
{
    [Authorize]
    public class ResumeController : Controller
    {
        // GET: Resume
        public ActionResult Index()
        {
            return View();
        }
    }
}