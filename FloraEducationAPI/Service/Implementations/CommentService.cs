using FloraEducationAPI.Domain.DTO;
using FloraEducationAPI.Domain.Models;
using FloraEducationAPI.Domain.Models.Authentication;
using FloraEducationAPI.Repository.Interfaces;
using FloraEducationAPI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Service.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> commentRepository;
        private readonly IPlantService plantService;
        private readonly IUserService userService;

        public CommentService(IRepository<Comment> commentRepository, IPlantService plantService, IUserService userService)
        {
            this.commentRepository = commentRepository;
            this.plantService = plantService;
            this.userService = userService;
        }

        public Comment AddCommentToPlant(CommentDTO commentDTO)
        {
            Plant plant = plantService.FetchPlantById(commentDTO.PlantId);
            User user = userService.FetchUserByUsername(commentDTO.Username);

            Comment comment = new Comment
            {
                Plant = plant,
                Author = user,
                Content = commentDTO.Content
            };

            return commentRepository.Insert(comment);
        }
    }
}
