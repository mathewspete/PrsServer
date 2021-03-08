using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrsServer.Models {
	public class Vendor {


		public int Id { get; set; }

		[StringLength(30), Required]
		public string Code { get; set; }

		[StringLength(30), Required]
		public string Name { get; set; }

		[StringLength(30), Required]
		public string Address { get; set; }

		[StringLength(30), Required]
		public string City { get; set; }

		[StringLength(2), Required]
		public string State { get; set; }

		[StringLength(5), Required]
		public string Zip { get; set; }

		[StringLength(12)]
		public string Phone { get; set; }

		[StringLength(255)]
		public string Email { get; set; } 




	}
}
