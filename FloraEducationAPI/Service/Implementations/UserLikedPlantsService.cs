using FloraEducationAPI.Domain.DTO;
using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Domain.Models.Authentication;
using FloraEducationAPI.Domain.Relations;
using FloraEducationAPI.Repository.Interfaces;
using FloraEducationAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Service.Implementations
{
    public class UserLikedPlantsService : IUserLikedPlantsService
    {
        private readonly IUserService userService;
        private readonly IPlantService plantService;
        private readonly IRepository<UserLikedPlants> userLikedPlantsRepository;

        public UserLikedPlantsService(IUserService userService, IPlantService plantService, IRepository<UserLikedPlants> userLikedPlantsRepository)
        {
            this.userService = userService;
            this.plantService = plantService;
            this.userLikedPlantsRepository = userLikedPlantsRepository;
        }

        public UserLikedPlants AddPlantToLikedPlants(UserLikedPlantDTO userLikedPlantsDTO)
        {
            User user = userService.FetchUserByUsername(userLikedPlantsDTO.Username);
            Plant plant = plantService.FetchPlantById(userLikedPlantsDTO.PlantId);

            UserLikedPlants userLikedPlant = new UserLikedPlants
            {
                Username = user.Username,
                User = user,
                PlantId = plant.Id,
                Plant = plant
            };

            return userLikedPlantsRepository.Insert(userLikedPlant);
        }
    }
}
