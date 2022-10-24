using BLL.IServices;
using DTO.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NHLogistic.API.Controllers
{
    //[Route("api/[controller]")]
    public class OrderManagementController : ApiController
    {
        private readonly IOrderManagement _orderService;

        public OrderManagementController(IOrderManagement orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        //[Route("find/{id}")]
        public IHttpActionResult GetById(string id)
        {
            try
            {
                var order = _orderService.GetById(id);

                return Ok(order);
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpGet]
        //[Route("get")]
        public IHttpActionResult Get(string search)
        {
            try
            {
                var orders = _orderService.Get(search);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }


        [HttpPost]
        [Route("add")]
        public IHttpActionResult Create([FromBody] CreateOrderDTO order)
        {
            try
            {
                _orderService.Create(order);
                return Ok();
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }


        [HttpPut]
        [Route("update")]
        public IHttpActionResult Update(string id, [FromBody] UpdateOrderDTO order)
        {
            try
            {
                _orderService.Update(order, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                _orderService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return (IHttpActionResult)Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }
    }
}
