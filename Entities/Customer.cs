using System;

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
        public string Name { get; set; }
        /// <summary>
        /// Saldo in euros.
        /// /// </summary>
        /// <remarks>Multicurrency entity will be some day.</remarks> 
        public decimal Saldo { get; set; }
    }
}
