using FloraEducationAPI.Domain.DTO;
using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Repository.Interfaces;
using FloraEducationAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Service.Implementations
{
    public class MiniQuizService : IMiniQuizService
    {
        private readonly IRepository<MiniQuiz> miniQuizRepositoryGeneric;
        private readonly IRepository<QuizQuestion> questionsRepository;
        private readonly IMiniQuizRepository miniQuizRepository;
        private readonly IPlantService plantService;

        public MiniQuizService(IRepository<MiniQuiz> miniQuizRepositoryGeneric, IRepository<QuizQuestion> questionsRepository, IMiniQuizRepository miniQuizRepository, IPlantService plantService)
        {
            this.miniQuizRepositoryGeneric = miniQuizRepositoryGeneric;
            this.questionsRepository = questionsRepository;
            this.miniQuizRepository = miniQuizRepository;
            this.plantService = plantService;
        }

        public QuizQuestion AddQuestionToQuiz(QuizQuestionDTO quizQuestionDTO)
        {
            var miniQuiz = miniQuizRepositoryGeneric.FetchById(quizQuestionDTO.QuizId);

            List<string> answers = new List<string>()
            {
                quizQuestionDTO.Answer1,
                quizQuestionDTO.Answer2,
                quizQuestionDTO.Answer3,
                quizQuestionDTO.Answer4,
            };

            var quizQuestions = new QuizQuestion
            {
                Quiz = miniQuiz,
                Question = quizQuestionDTO.Question,
                Answers = answers,
                CorrectAnswerIndex = quizQuestionDTO.CorrectAnswerIndex
            };

            return questionsRepository.Insert(quizQuestions);
        }

        public MiniQuiz CreateMiniQuiz(MiniQuizDTO miniQuizDTO)
        {
            var plant = plantService.FetchPlantById(miniQuizDTO.PlantId);

            var miniQuiz = new MiniQuiz
            {
                PlantId = plant.Id,
                Plant = plant,
                Title = miniQuizDTO.Title
            };

            return miniQuizRepositoryGeneric.Insert(miniQuiz);
        }

        public MiniQuiz DeleteMiniQuiz(MiniQuiz miniQuiz)
        {
            return miniQuizRepositoryGeneric.Delete(miniQuiz);
        }

        public List<MiniQuiz> FetchAllMiniQuizes()
        {
            return miniQuizRepositoryGeneric.FetchAll().ToList();
        }

        public MiniQuiz FetchMiniQuizById(Guid id)
        {
            return miniQuizRepositoryGeneric.FetchById(id);
        }

        public MiniQuiz FetchMiniQuizByPlantId(Guid plantId)
        {
            return miniQuizRepository.FetchMiniQuizByPlantId(plantId);
        }

        public MiniQuiz UpdateMiniQuiz(MiniQuiz miniQuiz)
        {
            return miniQuizRepositoryGeneric.Update(miniQuiz);
        }
    }
}
