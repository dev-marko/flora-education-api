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
        private readonly IRepository<Plant> plantRepository;

        public PlantService(IRepository<Plant> plantRepository)
        {
            this.plantRepository = plantRepository;
        }

        public Plant CreatePlant(Plant entity)
        {
            return plantRepository.Insert(entity);
        }

        public Plant DeletePlant(Plant entity)
        {
            return plantRepository.Delete(entity);
        }

        public List<Plant> FetchAllPlants()
        {
            return plantRepository.FetchAll().ToList();
        }

        public List<Plant> FetchAllPlantsByType(PlantType plantType)
        {
            return plantRepository.FetchAll().Where(e => e.Type.Equals(plantType)).ToList();
        }

        public Plant FetchPlantById(Guid id)
        {
            return plantRepository.FetchById(id);
        }

        public Plant FetchPlantByName(string plantName)
        {
            return plantRepository.FetchAll().Where(e => e.Equals(plantName)).SingleOrDefault();
        }

        public Plant UpdatePlant(Plant entity)
        {
            return plantRepository.Update(entity);
        }
    }
}
