using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class UserController : ApiController
    {
        int rowInserted = 0;
        [Route("user/add")]
        public HttpResponseMessage Post(RegistrationRequest registrationRequest)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PoolConnection"].ConnectionString);
            var user = new User()
            {
                Name = registrationRequest.Name,
                Surname = registrationRequest.Surname,
                EmailAddress = registrationRequest.EmailAddress
            };


        

            try
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tblUsers WHERE Email='" + registrationRequest.EmailAddress + "' AND Name='" + registrationRequest.Name + "' AND  Surname ='" + registrationRequest.Surname + "'", con);//check to see if user has already been registered
               SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                 da.Fill(dt);
            if (dt.Rows[0][0].ToString()!= "0")// user already exists
            {


                return Request.CreateResponse(HttpStatusCode.NotFound); 
                    con.Close();
            }
                else
                {
                    registerUser(registrationRequest.Name, registrationRequest.Surname, registrationRequest.PhoneNumber, registrationRequest.EmailAddress, registrationRequest.Password);
                    if (rowInserted > 0)
                    {

                        return Request.CreateResponse(HttpStatusCode.Accepted);


                    }
                }

               
            }

            catch (Exception ex)
            {


                return Request.CreateResponse(HttpStatusCode.NotFound);

            }

            return Request.CreateResponse(HttpStatusCode.Created,user);
        }



        public void registerUser(string Name, string Surname,string Phone, string Email, string Password)// continue to register user
        {
            SqlConnection insertcon = new SqlConnection(ConfigurationManager.ConnectionStrings["PoolConnection"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO tblUsers (Name,Surname,Phone,Email,Password) Values (@Name,@Surname,@Phone,@Email,@Password)";
            sqlCmd.Connection = insertcon;


            sqlCmd.Parameters.AddWithValue("@Name", Name);
            sqlCmd.Parameters.AddWithValue("@Surname", Surname);
            sqlCmd.Parameters.AddWithValue("@Phone", Phone);
            sqlCmd.Parameters.AddWithValue("@Email", Email);
            sqlCmd.Parameters.AddWithValue("@Password", Password);
            insertcon.Open();
            rowInserted = sqlCmd.ExecuteNonQuery();
           
            insertcon.Close();  
        }
       

    


     

    }
}
