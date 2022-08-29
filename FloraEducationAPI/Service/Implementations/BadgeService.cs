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
    public class BadgeService : IBadgeService
    {
        private readonly IRepository<Badge> badgeRepository;
        private readonly IRepository<UserBadges> userBadgesRepository;
        private readonly IUserService userService;

        public BadgeService(IRepository<Badge> badgeRepository, IRepository<UserBadges> userBadgesRepository, IUserService userService)
        {
            this.badgeRepository = badgeRepository;
            this.userBadgesRepository = userBadgesRepository;
            this.userService = userService;
        }

        public Badge AddBadge(string name)
        {
            Badge b = new Badge
            {
                Name = name
            };

            return badgeRepository.Insert(b);
        }

        public UserBadges AddBadgeToUser(UserBadgeDTO userBadgeDTO)
        {
            User user = userService.FetchUserByUsername(userBadgeDTO.Username);
            Badge badge = FetchBadgeByName(userBadgeDTO.BadgeName);

            UserBadges userBadge = new UserBadges
            {
                Username = user.Username,
                User = user,
                BadgeId = badge.Id,
                Badge = badge
            };

            return userBadgesRepository.Insert(userBadge);
        }

        public Badge FetchBadgeByName(string name)
        {
            return badgeRepository.FetchAll().SingleOrDefault(e => e.Name.Equals(name));
        }
    }
}
