using BLL.IServices;
using DTO;
using DTO.Order;
using Entities;
using Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderManagement : IOrderManagement
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderManagement()
        {
            _dbContext = new ApplicationDbContext();
        }
        public void Create(CreateOrderDTO input)
        {
            if (_dbContext.Orders.FirstOrDefault(u => u.Id == input.Id) != null)
            {
                throw new Exception($"Mã đơn hàng \"{input.Id}\"đã tồn tại");
            }
            using (var trans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var order = new OrderEntities()
                    {
                        Id = input.Id,
                        Order_no = input.Order_no,
                        Customer_name = input.Customer_name,
                        Receive_address = input.Receive_address,
                        Total_amount = input.Total_amount,
                        Note = input.Note
                    };
                    _dbContext.Orders.Add(order);
                    var Result = _dbContext.SaveChanges() > 0;
                    if (Result)
                    {
                        if (input.OrderDetails != null && input.OrderDetails.Any())
                        {
                            var orderDetails = new List<OrderDetailEntities>();
                            foreach (var item in input.OrderDetails)
                            {
                                orderDetails.Add(new OrderDetailEntities()
                                {
                                    Id = item.Id,
                                    Order_no_id = order.Id,
                                    Items = item.Items,
                                    Quantity = item.Quantity
                                });
                            }
                            _dbContext.OrderDetails.AddRange(orderDetails);
                        }
                        _dbContext.SaveChanges();
                        trans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception($"Tạo đơn hàng \"{input.Id}\"không thành công");
                }
                finally
                {
                    trans.Dispose();
                }
            }            
        }

        public void Delete(string id)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                throw new Exception("Không tìm thấy đơn hàng");
            }
            var orderDetails = _dbContext.OrderDetails.Where(c => c.Order_no_id == id);
            if (orderDetails != null && orderDetails.Any())
            {
                _dbContext.OrderDetails.RemoveRange(orderDetails);
            }
            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();
        }

        public PaginationModels<List<OrderDTO>> Get(string search)
        {
            var orders = _dbContext.Orders.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                orders = orders.Where(w => w.Order_no.ToLower().Contains(search.Trim().ToLower()));
            }

            return new PaginationModels<List<OrderDTO>>
            {
                TotalItem = orders.Count(),
                Items = orders.Select(s => new OrderDTO() {
                    Id = s.Id,
                    Order_no = s.Order_no,
                    Customer_name = s.Customer_name,
                    Receive_address = s.Receive_address,
                    Total_amount = s.Total_amount,
                    Note = s.Note
                }).ToList()
            };
        }

        public OrderDTO GetById(string id)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                throw new Exception("Không tìm thấy đơn hàng");
            }
            var result = new OrderDTO()
            {
                Id = order.Id,
                Order_no = order.Order_no,
                Customer_name = order.Customer_name,
                Receive_address = order.Receive_address,
                Total_amount = order.Total_amount,
                Note = order.Note
            };
            result.OrderDetails = _dbContext.OrderDetails.Where(w => w.Order_no_id == id).Select(s => new OrderDetailDTO()
            {
                Id = s.Id,
                Order_no_id = s.Order_no_id,
                Items = s.Items,
                Quantity = s.Quantity
            }).ToList();
            return result;
        }

        public void Update(UpdateOrderDTO input, string id)
        {
            var order = _dbContext.Orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                throw new Exception("Không tìm thấy đơn hàng");
            }
            using (var trans = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    order.Order_no = input.Order_no;
                    order.Customer_name = input.Customer_name;
                    order.Receive_address = input.Receive_address;
                    order.Total_amount = input.Total_amount;
                    order.Note = input.Note;
                    var Result = _dbContext.SaveChanges() > 0;
                    if (Result)
                    {
                        var orderDetailDelete = _dbContext.OrderDetails.Where(z => z.Order_no_id == id);
                        _dbContext.OrderDetails.RemoveRange(orderDetailDelete);
                        _dbContext.SaveChanges();
                        if (input.OrderDetails != null && input.OrderDetails.Any())
                        {
                            var orderDetails = new List<OrderDetailEntities>();
                            foreach (var item in input.OrderDetails)
                            {
                                orderDetails.Add(new OrderDetailEntities()
                                {
                                    Id = item.Id,
                                    Order_no_id = order.Id,
                                    Items = item.Items,
                                    Quantity = item.Quantity
                                });
                            }
                            _dbContext.OrderDetails.AddRange(orderDetails);
                        }
                        _dbContext.SaveChanges();
                        trans.Commit();
                    }
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception($"Tạo đơn hàng \"{input.Id}\"không thành công");
                }
                finally
                {
                    trans.Dispose();
                }
            }
            
        }
    }
}
