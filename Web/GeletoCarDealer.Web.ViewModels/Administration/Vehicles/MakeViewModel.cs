using System;
using System.Collections.Generic;
using System.Text;

namespace GeletoCarDealer.Web.ViewModels.Administration.Vehicles
{
    public class MakeViewModel
    {
        public int Id { get; set; }

        public ICollection<string> Makes { get; set; }
    }
}
