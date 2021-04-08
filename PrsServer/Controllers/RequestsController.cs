using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrsServer.Data;
using PrsServer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrsServer.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class RequestsController : ControllerBase {
		private readonly PrsDbContext _context;

		public RequestsController(PrsDbContext context) {
			_context = context;
		}

		#region SetStatusNew

		public async Task<IActionResult> SetToNew(int id) {
			var request = await _context.Request.FindAsync(id);
			if (request == null) {
				return NotFound();
			}
			request.Status = "NEW";
			return await PutRequest(request.Id, request);
		}

		#endregion

		#region SetStatusReview

		// PUT: api/Requests/Review/5
		// 
		[HttpPut("review/{id}")]
		public async Task<IActionResult> SetToReview(int id) {
			var request = await _context.Request.FindAsync(id);
			if (request == null) {
				return NotFound();
			}
			request.Status = (request.Total > 50 && request.Total !=0)?"REVIEW":"APPROVE";
			return await PutRequest(request.Id, request);
		}

		#endregion

		#region SetStatusApprove
		// PUT: api/Requests/Approve/5
		// 
		[HttpPut("approve/{id}")]
		public async Task<IActionResult> SetToApprove(int id) {
			var request = await _context.Request.FindAsync(id);
			if (request == null) {
				return NotFound();
			}
			request.Status = "APPROVE";
			return await PutRequest(request.Id, request);
		}

		#endregion

		#region SetStatusReject
		// PUT: api/Requests/Reject/5
		// 
		[HttpPut("reject/{id}")]
		//public async Task<IActionResult> SetToReject(int id) {
		//	var request = await _context.Request.FindAsync(id);
		//	if (request == null) {
		//		return NotFound();
		//	}
		//	request.Status = "REJECT";
		//	if (request.RejectionReason == null) {
		//		return StatusCode(406); /////// CHECKBACK
		//	}
		//	return await PutRequest(request.Id, request);
		//}


		public async Task<IActionResult> SetToReject(int id, Request request) {
			if (id != request.Id) {
				//return BadRequest();
				return StatusCode(412);
			}
			if (request.RejectionReason == null) {
				return StatusCode(406); /////// CHECKBACK
			}
			request.Status = "REJECT";

			_context.Entry(request).State = EntityState.Modified;

			try {
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException) {
				if (!RequestExists(id)) {
					return NotFound();
				}
				else {
					throw;
				}
			}

			return NoContent();
		}

		#endregion


		#region GetPending
		// GET: api/Requests/pending
		[HttpGet("pending")]
		public async Task<ActionResult<IEnumerable<Request>>> GetPending() {
			return await _context.Request
				.Include(u => u.User)
				.Where(r => r.Status == "REVIEW")
				//.Include(c => c.Customer) // include is used to join customer to order
				// .Include(s => s.UserId) // include is used to join salesperson to order
				.ToListAsync();
		}
		#endregion

		#region GetPendingNotUsers
		// GET: api/Requests/pending/uid
		[HttpGet("pending/{userid}")]
		public async Task<ActionResult<IEnumerable<Request>>> GetReview(int userid) {
			return await _context.Request
				.Include(u => u.User)
				.Where(r => r.Status == "REVIEW" && r.UserId != userid)
				//.Include(c => c.Customer) // include is used to join customer to order
				// .Include(s => s.UserId) // include is used to join salesperson to order
				.ToListAsync();
		}
		#endregion


		#region // GET: api/Requests/User/id
		[HttpGet("user/{userid}")]
		public async Task<ActionResult<IEnumerable<Request>>> GetUsersRequest(int id) {
			return await _context.Request
								.Where(r => r.UserId == id)
								.Include(u => u.User)
								.ToListAsync();
		}

		#endregion

		#region // GET: api/Requests
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Request>>> GetRequest() {
			return await _context.Request
								.Include(u => u.User)
								.ToListAsync();
		}

		#endregion


		#region // GET: api/Requests/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Request>> GetRequest(int id) {
			var request = await _context.Request
							.Include(u => u.User)
							.Include(rl => rl.RequestLine)
							.ThenInclude(p =>p.Product)
							.SingleOrDefaultAsync(r=>r.Id == id);

			if (request == null) {
				return NotFound();
			}

			return request;
		}
		#endregion


		#region // PUT: api/Requests/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPut("{id}")]
		public async Task<IActionResult> PutRequest(int id, Request request) {
			if (id != request.Id) {
				return BadRequest();
			}

			_context.Entry(request).State = EntityState.Modified;

			try {
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException) {
				if (!RequestExists(id)) {
					return NotFound();
				}
				else {
					throw;
				}
			}

			return NoContent();
		}
		#endregion

		#region // POST: api/Requests
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost]
		public async Task<ActionResult<Request>> PostRequest(Request request) {
			request.Status = "NEW";
			_context.Request.Add(request);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetRequest", new { id = request.Id }, request);
		}

		// DELETE: api/Requests/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Request>> DeleteRequest(int id) {
			var request = await _context.Request.FindAsync(id);
			if (request == null) {
				return NotFound();
			}

			_context.Request.Remove(request);
			await _context.SaveChangesAsync();

			return request;
		}

		private bool RequestExists(int id) {
			return _context.Request.Any(e => e.Id == id);
		}

		#endregion

	}
}
