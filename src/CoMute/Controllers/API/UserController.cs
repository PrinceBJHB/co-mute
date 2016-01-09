using CoMute.Web.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoMute.Web.Data;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace CoMute.Web.Controllers.API
{
	[RoutePrefix("api/User")]
    public class UserController : ApiController
    {
		[Route("Authenticate")]
		[HttpGet, HttpPost]
		public HttpResponseMessage Login(string email, string password)
		{
			email = email.ToLower();
			password = CalculateMD5Hash(password);
			var result =  DataQuery.UserLogin(email, password).Result;

			if (result.IsSuccess)
			{
				Models.User user = new Models.User()
				{
					Id = result.Result.Id,
					Name = result.Result.Name,
					Surname = result.Result.Surname,
					EmailAddress = result.Result.Email,
					Phone = result.Result.Phone,
					Password = result.Result.Password,
					ConfirmPassword = result.Result.Password
				};
				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
				response.Content = new ObjectContent(typeof(Models.User), user, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
				return response;
			}
			else
			{
				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
				response.Content = new ObjectContent(typeof(string), result.Error, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
				return response;
			}
		}

        [Route("add")]
        public HttpResponseMessage Register(Models.User user)
        {
			user.Password = CalculateMD5Hash(user.Password);
			user.ConfirmPassword = user.Password;
			user.EmailAddress = user.EmailAddress.ToLower();
			var result = DataQuery.RegisterUser(user).Result;
			if (result.IsSuccess)
				return new HttpResponseMessage(HttpStatusCode.OK);
			else
			{
				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
				response.Content = new ObjectContent(typeof(string), result.Error, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
				return response;
			}	
        }

		[Route("update")]
        public HttpResponseMessage UpdateProfile(Models.User user)
        {
			var result = DataQuery.UpdateProfile(user).Result;
			if (result.IsSuccess)
				return new HttpResponseMessage(HttpStatusCode.OK);
			else
			{
				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
				response.Content = new ObjectContent(typeof(string), result.Error, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
				return response;
			}	
        }

        [Route("get")]
        public HttpResponseMessage GetUser(int id)
        {
            var result = DataQuery.GetUser(id).Result;
            if (result.IsSuccess)
            {
                Models.User user = new Models.User()
                {
                    Id = result.Result.Id,
                    Name = result.Result.Name,
                    Surname = result.Result.Surname,
                    EmailAddress = result.Result.Email,
                    Phone = result.Result.Phone,
                    Password = result.Result.Password,
                    ConfirmPassword = result.Result.Password
                };
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ObjectContent(typeof(Models.User), user, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new ObjectContent(typeof(string), result.Error, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return response;
            }
        }

		[Route("updatepw")]
		public HttpResponseMessage UpdatePassword(int userId, string newPassword)
		{
			newPassword = CalculateMD5Hash(newPassword);
			var result = DataQuery.UpdatePassword(userId, newPassword).Result;
			if (result.IsSuccess)
				return new HttpResponseMessage(HttpStatusCode.OK);
			else
			{
				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
				response.Content = new ObjectContent(typeof(string), result.Error, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
				return response;
			}
		}

		public string CalculateMD5Hash(string input)
		{
			// step 1, calculate MD5 hash from input
			MD5 md5 = System.Security.Cryptography.MD5.Create();
			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
			byte[] hash = md5.ComputeHash(inputBytes);

			// step 2, convert byte array to hex string
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hash.Length; i++)
			{
				sb.Append(hash[i].ToString("X2"));
			}
			return sb.ToString();
		}
    }
}
