using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using SharedClassLibrary.Models;

namespace Middle_Manager_API.Data
{
    public class Seed
    {
        public static void SeedUsers(DataContext context, UserManager<User> userManager)
        {
            if (!context.AppUsers.Any())
            {
                var UserData = System.IO.File.ReadAllText("Data/SeedJsonData/UserSeedData.json");
                

                var users = JsonConvert.DeserializeObject<List<User>>(UserData);
                foreach (var user in users)
                {
                    user.UserName = user.UserName.ToLower();
                    var result = userManager.CreateAsync(user, "password").Result;
                    //context.Users.Add(user);
                }
                context.SaveChanges();
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static void SeedOrganisations(DataContext context)
        {
            if (!context.Organisations.Any())
            {
                var organisationData = System.IO.File.ReadAllText("Data/SeedJsonData/OrganisationSeedData.json");
                var organisations = JsonConvert.DeserializeObject<List<Organisation>>(organisationData);
                foreach (var org in organisations)
                {
                    org.DateCreated = DateTime.UtcNow;
                    org.CreatedBy = "Seed";
                    org.DateUpdated = DateTime.UtcNow;
                    org.UpdatedBy = "Seed";
                    context.Organisations.Add(org);
                }
                context.SaveChanges();
            }
        }


    }
}