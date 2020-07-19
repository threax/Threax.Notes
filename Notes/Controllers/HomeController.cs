using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Threax.AspNetCore.Mvc.CacheUi;

namespace Notes.Controllers
{
    [Authorize(AuthenticationSchemes = AuthCoreSchemes.Cookies)]
    public partial class HomeController : CacheUiController
    {
        public HomeController(ICacheUiBuilder builder)
          : base(builder)
        {

        }

        public Task<IActionResult> Index()
        {
            return CacheUiView();
        }

        public Task<IActionResult> Header()
        {
            return CacheUiView();
        }

        public Task<IActionResult> Footer()
        {
            return CacheUiView();
        }

        [AllowAnonymous]
        public IActionResult Gate()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AppStart()
        {
            return View();
        }

        //The other view action methods are in the additional partial classes for HomeController, expand the node for
        //this class to see them.
    }
}
