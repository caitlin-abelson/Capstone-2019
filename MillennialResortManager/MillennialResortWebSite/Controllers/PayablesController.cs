using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LogicLayer;
using DataObjects;
using MillennialResortWebSite.Models;

namespace MillennialResortWebSite.Controllers
{
    public class PayablesController : ApiController
    {
        ISupplierOrderManager _supplierOrderManager = new SupplierOrderManager();
        IReceivingManager _receivingManager = new ReceivingTicketManager();
        List<ReceivingTicket> _receivingTickets = null;
        List<SupplierOrder> _supplierOrders = null;
        List<SupplierOrderViewModel> _supplierOrdersVM = new List<SupplierOrderViewModel>();

        public PayablesController()
        {
            try
            {

                _receivingTickets = _receivingManager.retrieveAllReceivingTickets();
                _supplierOrders = _supplierOrderManager.RetrieveAllSupplierOrders();
                foreach (var order in _supplierOrders)
                {
                    _supplierOrdersVM.Add(new SupplierOrderViewModel
                    {
                        SupplierOrderID = order.SupplierOrderID,
                        SupplierID = order.SupplierID,
                        SupplierName = order.SupplierName,
                        EmployeeID = order.EmployeeID,
                        FirstName = order.FirstName,
                        LastName = order.LastName,
                        Description = order.Description,
                        DateOrdered = order.DateOrdered,
                        Lines = _supplierOrderManager.RetrieveAllSupplierOrderLinesBySupplierOrderID(order.SupplierOrderID)
                    });
                }
                foreach (var ticket in _receivingTickets)
                {
                    _supplierOrdersVM.Find(x => x.SupplierOrderID == ticket.SupplierOrderID).Exceptions = ticket.ReceivingTicketExceptions;

                    _supplierOrdersVM.Find(x => x.SupplierOrderID == ticket.SupplierOrderID).DateReceived = ticket.ReceivingTicketCreationDate;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }





        // GET: api/Payables/5
        public SupplierOrderViewModel GetSupplierOrder(int id)
        {
            try
            {
                return _supplierOrdersVM.Find(s => s.SupplierOrderID == id);
            }
            catch (Exception)
            {
                throw;
            }

        }

        // Get
        public IEnumerable<SupplierOrderViewModel> GetAllSupplierOrders()
        {
            return _supplierOrdersVM;
        }
        /*
        public IEnumerable<SupplierOrderViewModel> GetSupplierOrdersSince(DateTime d)
        {
            var orders = (from r in _supplierOrdersVM
                          where r.DateOrdered >= d
                          select r).OrderBy(r => r.DateOrdered);
            
            return orders;
        }
        */

        // GET: api/Payables/5
        public ReceivingTicket GetReceivingTicket(int id)
        {
            try
            {
                return _receivingTickets.Find(r => r.ReceivingTicketID == id);
            }
            catch (Exception)
            {
                throw;
            }

        }


        //// POST: api/Payables
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Payables/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/Payables/5
        //public void Delete(int id)
        //{
        //}
    }
}
