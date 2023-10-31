using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prospera.Models;

namespace Prospera.Controllers
{
    public class MenuUsuarioController : Controller
    {
        // GET: MenuUsuarioController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MenuUsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MenuUsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenuUsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuUsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenuUsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenuUsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenuUsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
            
        



    }
}
