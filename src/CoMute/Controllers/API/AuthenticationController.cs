using System;
using System.Data.SqlClient;
using System.Linq.Expressions;
using CoMute.Web.Models.Dto;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class AuthenticationController : ApiController
    {

        private string connectionString =
            "Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\Prince\\Documents\\Code\\co-mute\\src\\CoMute\\App_Data\\CarPoolDatabase.mdf;Integrated Security=True";

        /// <summary>
        /// Logs a user into the application.
        /// </summary>
        /// <param name="loginRequest">The user's login details</param>
        /// <returns></returns>
        public HttpResponseMessage Post(LoginRequest loginRequest)
        {
            if (areCredentialsCorrect(loginRequest.Email, loginRequest.Password))
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound);
        }


        private Boolean areCredentialsCorrect(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("select * from users", connection))
            {

                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader != null)
                    {
                        using (reader)
                        {

                            while (reader.Read())
                            {
                                if (reader["Email"].ToString().Equals(email) &&
                                    reader["Password"].ToString().Equals(password))
                                {
                                    return true;
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                }
                return false;
            }
        }
    }
}
