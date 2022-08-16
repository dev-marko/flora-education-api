using FloraEducationAPI.Domain.DTO.Authentication;
using FloraEducationAPI.Domain.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Service.Interfaces
{
    public interface IJWTManagerService
    {
        Tokens GenerateToken(User user);
    }
}
