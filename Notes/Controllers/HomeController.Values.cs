using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Notes.Controllers
{
    public partial class HomeController
    {
        [Authorize(Roles = Roles.EditValues)]
        public IActionResult Values()
        {
            return View();
        }
    }
}