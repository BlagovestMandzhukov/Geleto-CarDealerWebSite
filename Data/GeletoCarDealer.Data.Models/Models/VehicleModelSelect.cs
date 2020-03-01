using System;
using System.Collections.Generic;
using System.Text;

namespace GeletoCarDealer.Data.Models.Models
{
    public class VehicleModelSelect
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public int MakeId { get; set; }

        public virtual VehicleMakeSelect VehicleMakeSelect { get; set; }
    }
}
