using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;

namespace CoMute.Web.Controllers.API
{
    public class UserController : ApiController
    {

        private string connectionString =
            "Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\Prince\\Documents\\Code\\co-mute\\src\\CoMute\\App_Data\\CarPoolDatabase.mdf;Integrated Security=True";


        //[Route("user/profile")]
        public HttpResponseMessage profile(UserProfileRequest profileRequest)
        {
            if (!doesUserExist(profileRequest.Email))
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.OK, getUser(profileRequest.Email));
                
        }

        //[Route("user/add")]
        public HttpResponseMessage add(RegistrationRequest registrationRequest)
        {
            var user = new User()
            {
                Name = registrationRequest.Name,
                Surname = registrationRequest.Surname,
                EmailAddress = registrationRequest.EmailAddress,
                Password = registrationRequest.Password
            };

            if (!doesUserExist(registrationRequest.EmailAddress))
            {
                if(user.Name.IsEmpty() || user.Surname.IsEmpty() || user.EmailAddress.IsEmpty() || user.Password.IsEmpty())
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                addUser(user);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }


        }

        private void addUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO users (Email, Name, Surname, Password) VALUES (@Email, @Name, @Surname, @Password)");
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.EmailAddress);
                cmd.Parameters.AddWithValue("@Surname", user.Surname);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private User getUser(string email)
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
                                if (reader["Email"].ToString().Equals(email))
                                {
                                    var user = new User()
                                    {
                                        Name = reader["Name"].ToString(),
                                        Surname = reader["Surname"].ToString(),
                                        EmailAddress = reader["Email"].ToString()                                        
                                    };
                                    return user;
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                }
                return null;
            }

        }

        private Boolean doesUserExist(string email)
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
                                if (reader["Email"].ToString().Equals(email))
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
