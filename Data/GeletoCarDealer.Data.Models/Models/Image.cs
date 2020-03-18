namespace GeletoCarDealer.Data.Models.Models
{
    using GeletoCarDealer.Data.Common.Models;

    public class Image : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public byte[] ImageData { get; set; }

        public int VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
