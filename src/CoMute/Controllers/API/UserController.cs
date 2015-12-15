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

        #region User

        [Route("user/add")]
        [HttpPost]
        public HttpResponseMessage RegisterUser(RegistrationRequest registrationRequest)
        {
            try
            {
                //check email does not exist
                if (UserRepo.GetUserByEmail(registrationRequest.EmailAddress) != null)
                    return Request.CreateResponse(HttpStatusCode.PreconditionFailed, string.Format("User with email: {0} already exists.", registrationRequest.EmailAddress));

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

        [Route("user/edit")]
        public HttpResponseMessage EditUser(EditRequest editRequest)
        {
            try
            {
                //check password match
                if (editRequest.Password != editRequest.ConfirmPassword)
                    return Request.CreateResponse(HttpStatusCode.PreconditionFailed, "Passwords do not match.");

                var user = UserRepo.GetUserByID(editRequest.Id);
                if (user == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest);

                user.Name = editRequest.Name;
                user.Surname = editRequest.Surname;
                user.EmailAddress = editRequest.EmailAddress;
                user.PhoneNumber = editRequest.PhoneNumber;
                user.Password = editRequest.Password;

                var id = UserRepo.SaveUser(user);

                return Request.CreateResponse(HttpStatusCode.Created, user);
            }
            catch (Exception ex)
            {
                //log exception
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error occurred. Please contact someone.");
            }
        }

        #endregion

        #region Carpool

        [Route("user/registerCarpool")]
        [HttpPost]
        public HttpResponseMessage RegisterCarpool(RegisterCarPoolRequest registerCarPoolRequest)
        {
            try
            {
                //get carpool opportunities created by user.
                var carpools = UserRepo.GetOwnedCarpools(User.Identity.Name);

                //Check time frames
                if (carpools.Any(cp => cp.DaysAvailable.Any(d => registerCarPoolRequest.DaysAvailable.Equals(d))
                    && registerCarPoolRequest.DepartureTime < cp.ExpectedArrivalTime && registerCarPoolRequest.ExpectedArrivalTime > cp.DepartureTime))
                    return Request.CreateResponse(HttpStatusCode.PreconditionFailed, "Overlapping Times.");

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

        [Route("user/joinCarpool")]
        [HttpPost]
        public HttpResponseMessage JoinCarpool(JoinCarpoolRequest joinCarpoolRequest)
        {
            try
            {
                //get carpool opportunities created by user.
                var carpools = UserRepo.GetJoinedCarpools(User.Identity.Name);

                //Check time frames
                if (carpools.Any(cp => cp.DaysAvailable.Any(d => joinCarpoolRequest.DaysAvailable.Equals(d))
                    && joinCarpoolRequest.DepartureTime < cp.ExpectedArrivalTime && joinCarpoolRequest.ExpectedArrivalTime > cp.DepartureTime))
                    return Request.CreateResponse(HttpStatusCode.PreconditionFailed, "Overlapping Times.");

                var id = UserRepo.JoinCarpool(joinCarpoolRequest.CarpoolId, User.Identity.Name);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("timeframeoverlappingexception"))
                    return Request.CreateResponse(HttpStatusCode.PreconditionFailed, "Overlapping Times.");

                //log exception
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error occurred. Please contact someone.");
            }
        }

        [Route("user/viewJoined")]
        public HttpResponseMessage viewJoined()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, UserRepo.GetJoinedCarpools(User.Identity.Name));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("timeframeoverlappingexception"))
                    return Request.CreateResponse(HttpStatusCode.PreconditionFailed, "Overlapping Times.");

                //log exception
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error occurred. Please contact someone.");
            }
        }

        [Route("user/leaveJoined")]
        public HttpResponseMessage leaveJoined(long carpoolId)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, UserRepo.GetJoinedCarpools(User.Identity.Name));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("timeframeoverlappingexception"))
                    return Request.CreateResponse(HttpStatusCode.PreconditionFailed, "Overlapping Times.");

                //log exception
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error occurred. Please contact someone.");
            }
        }

        [Route("user/search")]
        public HttpResponseMessage leaveJoined(string search)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, UserRepo.SearchCarpool(search));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("timeframeoverlappingexception"))
                    return Request.CreateResponse(HttpStatusCode.PreconditionFailed, "Overlapping Times.");

                //log exception
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Error occurred. Please contact someone.");
            }
        }

        #endregion
    }
}
