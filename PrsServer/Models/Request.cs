using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrsServer.Models {
	public class Request {

		public int Id { get; set; }

		[StringLength(80), Required]
		public string Description { get; set; }

		[StringLength(80), Required]
		public string Justification { get; set; }

		[StringLength(80)]
		public string RejectionReason { get; set; }

		[StringLength(20), Required]
		public string DeliveryMode { get; set; }

		[StringLength(10), Required]
		public string Status { get; set; }

		[Column(TypeName = "decimal(11,2)"), Required]
		public decimal Total { get; set; } 

		public int UserId { get; set; }
		public virtual User User{ get; set; }

		
		public virtual IEnumerable<RequestLine> RequestLine { get; set; }

	}
}
