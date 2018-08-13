using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
        /// <summary>
        /// Customer data.
        /// </summary>
    public class Customer
    {        
        public int Id { get; set; }
        /// <summary>
        /// Customers full official name.
        /// </summary>
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }
        /// <summary>
        /// Saldo in euros.
        /// /// </summary>
        /// <remarks>Multicurrency entity will be some day.</remarks> 
        [Range(0, (double)decimal.MaxValue)]
        public decimal Saldo { get; set; }
    }
}
