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
        private readonly IRepository<MiniQuiz> miniQuizRepository;
        private readonly IRepository<QuizQuestion> questionsRepository;
        private readonly IPlantService plantService;

        public MiniQuizService(IRepository<MiniQuiz> miniQuizRepository, IRepository<QuizQuestion> questionsRepository, IPlantService plantService)
        {
            this.miniQuizRepository = miniQuizRepository;
            this.questionsRepository = questionsRepository;
            this.plantService = plantService;
        }

        public QuizQuestion AddQuestionToQuiz(QuizQuestionDTO quizQuestionDTO)
        {
            var miniQuiz = miniQuizRepository.FetchById(quizQuestionDTO.QuizId);

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
                Plant = plant,
                Title = miniQuizDTO.Title
            };

            return miniQuizRepository.Insert(miniQuiz);
        }

        public MiniQuiz DeleteMiniQuiz(MiniQuiz miniQuiz)
        {
            return miniQuizRepository.Delete(miniQuiz);
        }

        public List<MiniQuiz> FetchAllMiniQuizes()
        {
            return miniQuizRepository.FetchAll().ToList();
        }

        public MiniQuiz FetchMiniQuizById(Guid id)
        {
            return miniQuizRepository.FetchById(id);
        }

        public MiniQuiz FetchMiniQuizByPlantId(Guid plantId)
        {
            return miniQuizRepository.FetchAll().SingleOrDefault(e => e.Plant.Id.Equals(plantId));
        }

        public MiniQuiz UpdateMiniQuiz(MiniQuiz miniQuiz)
        {
            return miniQuizRepository.Update(miniQuiz);
        }
    }
}
