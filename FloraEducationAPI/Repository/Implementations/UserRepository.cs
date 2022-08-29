using FloraEducationAPI.Context;
using FloraEducationAPI.Domain.Models.Authentication;
using FloraEducationAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using BC=BCrypt.Net.BCrypt;

namespace FloraEducationAPI.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        public FloraEducationDbContext context { get; set; }
        public DbSet<User> entities { get; set; }

        public UserRepository(FloraEducationDbContext context)
        {
            this.context = context;
            this.entities = context.Set<User>();
        }

        public void Delete(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("User object is null");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public IEnumerable<User> FetchAllUsers()
        {
            return entities.AsEnumerable();
        }

        public User FetchUserByEmail(string email)
        {
            return entities.SingleOrDefault(e => e.Email == email);
        }

        public User FetchUserByUsername(string username)
        {
            return entities.Include(e => e.Badges).Include("Badges.Badge").SingleOrDefault(e => e.Username == username);
        }

        public User VerifyUserCredentials(string username, string password)
        {
            User user = FetchUserByUsername(username);
            
            if (user == null || !BC.Verify(password, user.Password))
            {
                return null;
            }

            return user;
        }

        public void Insert(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("User object is null");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("User object is null");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public bool UserExists(string username, string email)
        {
            return (FetchUserByUsername(username) != null) && (FetchUserByEmail(email) != null);
        }
    }
}
