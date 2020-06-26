using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Middle_Manager_API.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedClassLibrary.Dtos;
using static SharedClassLibrary.Enums;

namespace Middle_Manager_API.Controllers
{
    public class BaseController : ControllerBase
    {

        public BaseController()
        {

        }

        private UserDetails _UserDetails;

        public UserDetails UserDetails
        {
            get
            {
                if(_UserDetails == null)
                {
                    _UserDetails = GetUserDetails();
                    return _UserDetails;
                }
                return _UserDetails;
            }
        }

        public UserDetails GetUserDetails()
        {
            var userDetails = this.HttpContext.Request.Headers["Authorization"];
            var httpToken = userDetails.First().Split(' ').Last();

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(httpToken);
            var token = handler.ReadToken(httpToken) as JwtSecurityToken;
            try
            {
                var confirmedUserId = int.Parse(token.Claims.First(claim => claim.Type == "nameid").Value);
                var userName = token.Claims.First(claim => claim.Type == "unique_name").Value;
                var email = token.Claims.First(claim => claim.Type == "email").Value;
                var organisationId = int.Parse(token.Claims.First(claim => claim.Type == "primarygroupsid").Value);
                return new UserDetails()
                {
                    Id = confirmedUserId,
                    Name = userName,
                    Email = email,
                    OrganisationId = organisationId
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
