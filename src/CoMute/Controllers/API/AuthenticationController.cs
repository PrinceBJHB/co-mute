using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class AuthenticationController : ApiController
    {
        /// <summary>
        /// Logs a user into the application.
        /// </summary>
        /// <param name="loginRequest">The user's login details</param>
        /// <returns></returns>
        /// 
        Dictionary<string,string> myuser= new Dictionary<string,string>();

        public HttpResponseMessage Post(LoginRequest loginRequest)
        {
             SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PoolConnection"].ConnectionString);

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblUsers where Email =@email and Password=@password", con);
                cmd.Parameters.AddWithValue("@email", loginRequest.Email);
                cmd.Parameters.AddWithValue("@password", loginRequest.Password);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                  
                   myuser.Add("email",loginRequest.Email);// returns email when user login

                   return  Request.CreateResponse(HttpStatusCode.Accepted, myuser);

                }
                else
                {
                   return Request.CreateResponse(HttpStatusCode.NotFound);
                }


            }

            catch(Exception ex)
            {


                return Request.CreateResponse(HttpStatusCode.NotFound);

            }

            con.Close();
        }



            
        }
    }

