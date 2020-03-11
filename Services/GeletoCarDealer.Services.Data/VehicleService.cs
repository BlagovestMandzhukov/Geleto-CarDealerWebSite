namespace GeletoCarDealer.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GeletoCarDealer.Data.Common.Repositories;
    using GeletoCarDealer.Data.Models.Models;
    using GeletoCarDealer.Services.Mapping;

    public class VehicleService : IVehicleService
    {
        private readonly IRepository<VehicleMakeSelect> makeRepository;
        private readonly IRepository<VehicleModelSelect> modelRepository;
        private readonly IRepository<VehicleYearSelect> yearsRepository;

        public VehicleService(
            IRepository<VehicleMakeSelect> makeRepository,
            IRepository<VehicleModelSelect> modelRepository,
            IRepository<VehicleYearSelect> yearsRepository)
        {
            this.makeRepository = makeRepository;
            this.modelRepository = modelRepository;
            this.yearsRepository = yearsRepository;
        }

        public string AddVehicle()
        {
            throw new NotImplementedException();
        }

        public string EditVehicle(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetMakes<T>()
        {
            IQueryable<VehicleMakeSelect> makes = this.makeRepository.All().OrderBy(x=>x.Make);
            return makes.To<T>().ToList();
        }

        public IEnumerable<T> GetModels<T>()
        {
            IQueryable<VehicleModelSelect> models = this.modelRepository.All();
            return models.To<T>().ToList();
        }

        public IEnumerable<T> GetYear<T>()
        {
            IQueryable<VehicleYearSelect> years = this.yearsRepository.All();
            return years.To<T>().ToList();
        }
    }
}
