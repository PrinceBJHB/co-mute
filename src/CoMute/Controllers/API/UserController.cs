﻿using CoMute.Domain.Inteface;
using CoMute.Domain.Repo;
using CoMute.Model.Models;
using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class UserController : ApiController
    {
        IUserRepo UserRepo;

        public UserController()
        {
            UserRepo = new UserRepo();
        }

        public HttpResponseMessage Post(RegistrationRequest registrationRequest)
        {
            //check password match
            if (registrationRequest.Password != registrationRequest.ConfirmPassword)
                return Request.CreateResponse(HttpStatusCode.PreconditionFailed, "Passwords do not match.");

            //Uncomment if unique email is required
            //if (UserRepo.GetUserByEmail(registrationRequest.EmailAddress) != null)
            //    return Request.CreateResponse(HttpStatusCode.Conflict, string.Format("User with email: {0} already exists.", registrationRequest.EmailAddress));
            
            var user = new User()
            {
                Name = registrationRequest.Name,
                Surname = registrationRequest.Surname,
                EmailAddress = registrationRequest.EmailAddress,
                PhoneNumber = registrationRequest.PhoneNumber,
                Password = registrationRequest.Password
            };

            user = (User)UserRepo.SaveUser(user);

            return Request.CreateResponse(HttpStatusCode.Created, user);
        }
    }
}
