using CoMute.Domain.Inteface;
using CoMute.Domain.Repo;
using CoMute.Model.Models;
using CoMute.Web.Models.Dto;
using System;
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

        [Route("user/add")]
        public HttpResponseMessage RegisterUser(RegistrationRequest registrationRequest)
        {
            try
            {
                //check password match
                if (registrationRequest.Password != registrationRequest.ConfirmPassword)
                    return Request.CreateResponse(HttpStatusCode.PreconditionFailed, "Passwords do not match.");

                var user = new User()
                {
                    Name = registrationRequest.Name,
                    Surname = registrationRequest.Surname,
                    EmailAddress = registrationRequest.EmailAddress,
                    PhoneNumber = registrationRequest.PhoneNumber,
                    Password = registrationRequest.Password
                };

                var id = UserRepo.SaveUser(user);

                return Request.CreateResponse(HttpStatusCode.Created, user);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("emailalreadyexistsexception"))
                    return Request.CreateResponse(HttpStatusCode.Conflict, string.Format("User with email: {0} already exists.", registrationRequest.EmailAddress));
                //log exception
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error occurred. Please contact someone.");
            }
        }

        [Route("user/registerCarpool")]
        public HttpResponseMessage RegisterCarpool(RegisterCarPoolRequest registerCarPoolRequest)
        {
            try
            {
                var carpool = new CarPool()
                {
                    AvailableSeats = registerCarPoolRequest.AvailableSeats,
                    //DaysAvailable = registerCarPoolRequest.DaysAvailable,
                    ExpectedArrivalTime = registerCarPoolRequest.ExpectedArrivalTime,
                    DepartureTime = registerCarPoolRequest.DepartureTime,
                    Destination = registerCarPoolRequest.Destination,
                    Notes = registerCarPoolRequest.Notes,
                    Origin = registerCarPoolRequest.Origin
                };

                var id = UserRepo.CreateCarpool(carpool, User.Identity.Name);

                return Request.CreateResponse(HttpStatusCode.Created, carpool);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("timeframeoverlappingexception"))
                    return Request.CreateResponse(HttpStatusCode.PreconditionFailed, "Overlapping Times.");

                //log exception
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error occurred. Please contact someone.");
            }
        }
    }
}
