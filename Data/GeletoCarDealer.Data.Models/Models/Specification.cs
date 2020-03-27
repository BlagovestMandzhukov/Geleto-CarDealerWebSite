namespace GeletoCarDealer.Data.Models.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Specification
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int VehicleId { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
