﻿namespace GeletoCarDealer.Web.ViewModels.UsersArea.Vehicles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using GeletoCarDealer.Data.Models;
    using GeletoCarDealer.Services.Mapping;

    public class SideBarVehiclesViewModel : IMapFrom<Vehicle>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Milage { get; set; }

        public decimal Price { get; set; }

        public int Year { get; set; }

        public string ImageUrl { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Vehicle, SideBarVehiclesViewModel>()
                .ForMember(x => x.ImageUrl, cfg => cfg.MapFrom(x => x.Images.Any() ? x.Images.FirstOrDefault().ImageUrl : "/img/no-image.png"));
        }
    }
}
