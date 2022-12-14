using FloraEducationAPI.Domain.Enumerations;
using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Repository.Interfaces;
using FloraEducationAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Service.Implementations
{
    public class PlantService : IPlantService
    {
        private readonly IRepository<Plant> plantRepositoryGeneric;
        private readonly IPlantRepository plantRepository;

        public PlantService(IRepository<Plant> plantRepositoryGeneric, IPlantRepository plantRepository)
        {
            this.plantRepositoryGeneric = plantRepositoryGeneric;
            this.plantRepository = plantRepository;
        }

        public Plant CreatePlant(Plant entity)
        {
            return plantRepositoryGeneric.Insert(entity);
        }

        public Plant DeletePlant(Plant entity)
        {
            return plantRepositoryGeneric.Delete(entity);
        }

        public List<Plant> FetchAllPlants()
        {
            return plantRepositoryGeneric.FetchAll().ToList();
        }

        public List<Plant> FetchAllPlantsByType(PlantType plantType)
        {
            return plantRepositoryGeneric.FetchAll().Where(e => e.Type.Equals(plantType)).ToList();
        }

        public Plant FetchPlantById(Guid id)
        {
            return plantRepository.FetchPlantById(id);
        }

        public Plant FetchPlantByName(string plantName)
        {
            return plantRepositoryGeneric.FetchAll().Where(e => e.Equals(plantName)).SingleOrDefault();
        }

        public bool PlantExists(Guid id)
        {
            return FetchPlantById(id) != null;
        }

        public Plant UpdatePlant(Plant entity)
        {
            return plantRepositoryGeneric.Update(entity);
        }
    }
}
