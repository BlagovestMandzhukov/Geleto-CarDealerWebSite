namespace GeletoCarDealer.Data.Models.Models
{
    using System.Collections.Generic;

    public class Category
    { 
        public Category()
        {
            this.Vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
