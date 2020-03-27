namespace GeletoCarDealer.Web.ViewModels.UsersArea.Images
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Services.Mapping;

    public class ImagesViewModel : IMapFrom<Image>
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }
    }
}
