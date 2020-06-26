using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace MiddleManagerApi.Controllers
{

    public class FallbackController : Controller
    {
        public string Index()
        {
            return "Sorry, something went wrong.";
        }
    }
}