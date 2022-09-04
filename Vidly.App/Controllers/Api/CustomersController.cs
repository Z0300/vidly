using AutoMapper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Vidly.App.Dtos;
using Vidly.App.Models;

namespace Vidly.App.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/customers
        public IHttpActionResult GetCustomers(string query = null)
        {

            var customersQuery = _context.Customers.Include(a => a.MembershipType);

            var Request = HttpContext.Current.Request.Params;
            int totalRecord = customersQuery.Count();
            int filterRecord = 0;

            var draw = Request["draw"];
            var limit = Convert.ToInt32(Request["limit"] ?? "0");
            var requestSearchValue = Request["search[value]"] ?? "";
            var extJSSearchValue = Request["filter"] ?? "";
            int pageSize = Convert.ToInt32(Request["length"] ?? "10");


            if (requestSearchValue != "")
                query = requestSearchValue;

            int skip = Convert.ToInt32(Request["start"] ?? "0");


            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(a => a.Name.Contains(query));
            filterRecord = customersQuery.Count();


            //pagination
            //if (skip > 0)
            customersQuery = customersQuery
                .OrderBy(a => a.Name)
                .Skip(skip)
                .Take(limit > 0 ? limit : pageSize);


            var customerDto = customersQuery
                 .ToList();

            var obj = new
            {
                draw = draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = customerDto
            };


            return Ok(obj);
        }



        // GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}
