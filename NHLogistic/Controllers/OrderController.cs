using BLL.IServices;
using DTO.Order;
using NHLogistic.RestAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NHLogistic.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        private readonly IRestApiServices _restApi;
        public OrderController(IRestApiServices restApi)
        {
            _restApi = restApi;
        }
        public async Task<ActionResult> Index(string search)
        {
            var models = new List<OrderDTO>();
            var result = await _restApi.GetAsJson<OrderDTO>("Order/get", search);
            if (result.Success)
            {
                if (result.Data != null)
                    models.AddRange(result.Data);
            }
            return View(models);
        }
        public ActionResult New()
        {
            var model = new OrderDTO();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> New(OrderDTO model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            var result = await _restApi.PostAsJson<OrderDTO>("Order/Create", model);
            if (result.Success)
            {
                TempData["Successful"] = result.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IntervalServer"] = result.Message;
            }
            return View(model);
        }

        public async Task<ActionResult> Edit(string Id)
        {
            var model = new OrderDTO();
            var result = await _restApi.GetAsJson<OrderDTO>(string.Format("{0}/{1}", "Order/GetById", Id));
            if (result.Success)
            {
                model = result.Data;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(OrderDTO model)
        {
            if (ModelState.IsValid == false)
            {
                return View(model);
            }
            var result = await _restApi.PostAsJson<OrderDTO>("Order/Update", model);
            if (result.Success)
            {
                TempData["Successful"] = result.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IntervalServer"] = result.Message;
            }
            return View(model);
        }

        public async Task<ActionResult> Destroy(string Id)
        {
            var result = await _restApi.DeleteJson<OrderDTO>(string.Format("{0}/{1}", "Order/Delete", Id));
            if (result.Success)
            {
                TempData["Successful"] = result.Message;
            }
            else
            {
                TempData["IntervalServer"] = result.Message;
            }
            return RedirectToAction("Index");
        }
    }
}