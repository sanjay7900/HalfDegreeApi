using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BihariJe_WebApp.Controllers
{
    public class DeliveryController : Controller
    {
        // GET: DeliveryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DeliveryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DeliveryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeliveryController/Create
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

        // GET: DeliveryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DeliveryController/Edit/5
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

        // GET: DeliveryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DeliveryController/Delete/5
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
