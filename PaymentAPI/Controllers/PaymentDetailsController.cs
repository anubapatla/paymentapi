﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly PaymentDetailContext _context;

        public PaymentDetailsController(PaymentDetailContext context)
        {
            _context = context;
        }

        // GET: api/PaymentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetails>>> GetPaymentDetail()
        {
            return await _context.PaymentDetail.ToListAsync();
        }

        // GET: api/PaymentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetails>> GetPaymentDetails(int id)
        {
            var paymentDetails = await _context.PaymentDetail.FindAsync(id);

            if (paymentDetails == null)
            {
                return NotFound();
            }

            return paymentDetails;
        }

        // PUT: api/PaymentDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentDetails(int id, PaymentDetails paymentDetails)
        {
            if (id != paymentDetails.PaymentDetailsId)
            {
                return BadRequest();
            }

            _context.Entry(paymentDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PaymentDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PaymentDetails>> PostPaymentDetails(PaymentDetails paymentDetails)
        {
            _context.PaymentDetail.Add(paymentDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentDetails", new { id = paymentDetails.PaymentDetailsId }, paymentDetails);
        }

        // DELETE: api/PaymentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentDetails(int id)
        {
            var paymentDetails = await _context.PaymentDetail.FindAsync(id);
            if (paymentDetails == null)
            {
                return NotFound();
            }

            _context.PaymentDetail.Remove(paymentDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentDetailsExists(int id)
        {
            return _context.PaymentDetail.Any(e => e.PaymentDetailsId == id);
        }
    }
}
