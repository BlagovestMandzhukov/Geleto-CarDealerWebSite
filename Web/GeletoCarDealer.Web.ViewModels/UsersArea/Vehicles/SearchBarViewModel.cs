namespace GeletoCarDealer.Web.ViewModels.UsersArea.Vehicles
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using GeletoCarDealer.Data.Models;

    public class SearchBarViewModel
    {
        public IEnumerable<string> Makes { get; set; }

        public IEnumerable<string> Models { get; set; }

        public IEnumerable<string> Categories { get; set; }
    }
}
