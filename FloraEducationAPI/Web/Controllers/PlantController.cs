using FloraEducationAPI.Domain.DTO;
using FloraEducationAPI.Domain.Enumerations;
using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly IPlantService plantService;
        private readonly IMiniQuizService miniQuizService;

        public PlantController(IPlantService plantService, IMiniQuizService miniQuizService)
        {
            this.plantService = plantService;
            this.miniQuizService = miniQuizService;
        }

        [HttpGet("categories")]
        public IActionResult GetAllPlantTypes()
        {
            return Ok(JsonConvert.SerializeObject(new { categories = Enum.GetNames(typeof(PlantType))}));
        }

        [HttpGet]
        public IActionResult GetAllPlantsByType([FromQuery] int query)
        {
            PlantType plantType = (PlantType)query;

            return Ok(plantService.FetchAllPlantsByType(plantType));
        }

        [HttpPost]
        public IActionResult AddPlant([FromBody] Plant plant)
        {
            return Ok(plantService.CreatePlant(plant));
        }

        [HttpGet("{plantId}/mini-quiz")]
        public IActionResult GetMiniQuizForPlant(Guid plantId)
        {
            var miniQuiz = miniQuizService.FetchMiniQuizByPlantId(plantId);

            if (miniQuiz != null)
            {
                return Ok(miniQuiz);
            }

            return NotFound(JsonConvert.SerializeObject(new { error = "Mini quiz not found" }));
        }

        [HttpPost("{plantId}/mini-quiz")]
        public IActionResult AddMiniQuizForPlant(Guid plantId, [FromBody] MiniQuizDTO miniQuizDTO)
        {
            miniQuizDTO.PlantId = plantId;
            return Ok(miniQuizService.CreateMiniQuiz(miniQuizDTO));
        }

        [HttpPost("{plantId}/mini-quiz")]
        public IActionResult CheckMiniQuizAnswers(Guid plantId, [FromBody] MiniQuiz miniQuiz)
        {
            // TODO: Check mini-quiz answers
            // if passed, generate Badge and add to user profile
            // if failed, send failed message
            throw new NotImplementedException();
        }

        [HttpPost("{plantId}/mini-quiz/question")]
        public IActionResult AddQuestionToMiniQuiz(Guid plantId, [FromBody] QuizQuestionDTO quizQuestionDTO)
        {
            var miniQuiz = miniQuizService.FetchMiniQuizByPlantId(plantId);
            quizQuestionDTO.QuizId = miniQuiz.Id;
            return Ok(miniQuizService.AddQuestionToQuiz(quizQuestionDTO));
        }
    }
}
