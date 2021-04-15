using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrsServer.Models {
	public class Product {


		public int Id { get; set; }

		[StringLength(30), Required]
		public string PartNbr { get; set; }

		[StringLength(300), Required]
		public string Name { get; set; }  

		[Column(TypeName = "decimal(9,2)"), Required]
		public decimal Price { get; set; }

		[StringLength(30), Required]
		public string Unit { get; set; }

		[StringLength(255)]
		public string PhotoPath { get; set; }

		public int VendorId { get; set; }

		//public virtual IEnumerable<Vendor> Vendor { get; set; }
		public virtual Vendor Vendor { get; set; }



	}
}
