using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrsServer.Data;
using PrsServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrsServer.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class RequestLinesController : ControllerBase {
		private readonly PrsDbContext _context;

		public RequestLinesController(PrsDbContext context) {
			_context = context;
		}


		#region Calc Total
		private async Task<IActionResult> CalcSubtotal(int id) {
			var order = await _context.Request.FindAsync(id);
			if (order == null) {
				return NotFound();
			}
			order.Total = _context.RequestLine.Where(li => li.RequestId == id)
																			.Sum(li => li.Quantity * li.Product.Price);

			var rowsAffected = await _context.SaveChangesAsync();
			if (rowsAffected != 1) {
				throw new Exception("Failed to Update Request Total");
			}
			return Ok();
		}

		#endregion


		#region Get ALL Requestlines
		// GET: api/RequestLines
		[HttpGet]
		public async Task<ActionResult<IEnumerable<RequestLine>>> GetRequestLine() {
			return await _context.RequestLine.Include(r => r.Request).Include(p => p.Product).ToListAsync();
		}
		#endregion


		#region Get Single Requestline
		// GET: api/RequestLines/5
		[HttpGet("{id}")]
		public async Task<ActionResult<RequestLine>> GetRequestLine(int id) {
			var requestLine = await _context.RequestLine.Include(r => r.Request).Include(p => p.Product).SingleOrDefaultAsync(rl => rl.Id == id);

			if (requestLine == null) {
				return NotFound();
			}

			return requestLine;
		}
		#endregion


		#region PUT (calc tot) (varify Qty)
		// PUT: api/RequestLines/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPut("{id}")]
		public async Task<IActionResult> PutRequestLine(int id, RequestLine requestLine) {
			if (id != requestLine.Id) {
				return BadRequest();
			}
			
			_context.Entry(requestLine).State = EntityState.Modified;
			if (requestLine.Quantity>0) {
				try {
					await _context.SaveChangesAsync();
					await CalcSubtotal(requestLine.RequestId);
				}
				catch (DbUpdateConcurrencyException) {
					if (!RequestLineExists(id)) {
						return NotFound();
					}
					else {
						throw;
					}
				}
			}
			else { 
				return StatusCode(405); /////// CHECKBACK
			}
				

			return NoContent();
		}

		#endregion


		#region Post (Calc)
		// POST: api/RequestLines/"{reqId}"
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		// [HttpPost("{reqId}")]
		[HttpPost]
		// public async Task<ActionResult<RequestLine>> PostRequestLine(RequestLine requestLine, int reqId) {
		public async Task<ActionResult<RequestLine>> PostRequestLine(RequestLine requestLine, int reqId) {

				//requestLine.RequestId = reqId;

				if (requestLine.Quantity <= 0) {
				return StatusCode(405); /////// CHECKBACK
			}

			_context.RequestLine.Add(requestLine);
			await _context.SaveChangesAsync();
			await CalcSubtotal(requestLine.RequestId);

			return CreatedAtAction("GetRequestLine", new { id = requestLine.Id }, requestLine);
		}
		#endregion

		#region Delete
		// DELETE: api/RequestLines/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<RequestLine>> DeleteRequestLine(int id) {
			var requestLine = await _context.RequestLine.FindAsync(id);
			if (requestLine == null) {
				return NotFound();
			}

			_context.RequestLine.Remove(requestLine);
			await _context.SaveChangesAsync();
			await CalcSubtotal(requestLine.RequestId);

			return requestLine;
		}
		#endregion


		private bool RequestLineExists(int id) {
			return _context.RequestLine.Any(e => e.Id == id);
		}


	}
}
