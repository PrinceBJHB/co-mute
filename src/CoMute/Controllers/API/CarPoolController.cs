using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class CarPoolController : ApiController
    {
      
        int rowInserted = 0;
        [Route("api/carpool")]
        public HttpResponseMessage Post(CarPool registerCarPool)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["PoolConnection"].ConnectionString);
            var CarPool = new CarPool()
            {
                Notes = registerCarPool.Notes,
                DateCreated= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                DepartTime= registerCarPool.DepartTime,
                ArriveTime = registerCarPool.ArriveTime,
                Origin = registerCarPool.Origin,
                DaysAvailable=registerCarPool.DaysAvailable, 
                Destination = registerCarPool.Destination,
                SeatsAvailable= registerCarPool.SeatsAvailable,
                Email = registerCarPool.Email
            };
              try
            {
                if (registerCarPool.DepartTime != null)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from tblPools where Email =@Email and DepartTime=@departTime and ArriveTime=@arriveTime and DaysAvailable=@daysavail", con);//check if   two car-pool opportunities with overlapping time-frames.
                    cmd.Parameters.AddWithValue("@Email", registerCarPool.Email);
                    cmd.Parameters.AddWithValue("@departTime", registerCarPool.DepartTime);
                    cmd.Parameters.AddWithValue("@arriveTime", registerCarPool.ArriveTime);
                    cmd.Parameters.AddWithValue("@daysavail", registerCarPool.DaysAvailable);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)// if there are registrations overlapping then do not  create
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound);

                    }

                }
                else // create the record 
                {
                    creatPoolingOpportunity(registerCarPool.Email, registerCarPool.DepartTime, registerCarPool.ArriveTime, registerCarPool.DaysAvailable, registerCarPool.Destination, registerCarPool.Origin, registerCarPool.Notes, registerCarPool.SeatsAvailable, registerCarPool.DateCreated);
                    if (rowInserted > 0)
                    {

                        return Request.CreateResponse(HttpStatusCode.Accepted);


                    }
                }


            }

            catch(Exception ex)
            {


                return Request.CreateResponse(HttpStatusCode.NotFound);

            }

            con.Close();
          return Request.CreateResponse(HttpStatusCode.Created, CarPool);
        }


        public void creatPoolingOpportunity(string Email, string DepartTime, string ArriveTime, string DaysAvailable, string Destination, string Origin, string Notes, string SeatsAvailable, string DateCreated)// continue to create opportunity
        {
            SqlConnection insertcon = new SqlConnection(ConfigurationManager.ConnectionStrings["PoolConnection"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO tblPools (Email,DepartTime,ArriveTime,DaysAvailable,Destination,Origin,SeatsAvailable,DateCreated) Values (@Email,@DepartTime,@ArriveTime,@DaysAvailable,@Destination,@Origin,@Notes,@SeatsAvailable,@DateCreated)";
            sqlCmd.Connection = insertcon;


            sqlCmd.Parameters.AddWithValue("@Email",Email);
            sqlCmd.Parameters.AddWithValue("@DepartTime", DepartTime);
            sqlCmd.Parameters.AddWithValue("@ArriveTime", ArriveTime);
            sqlCmd.Parameters.AddWithValue("@DaysAvailable", DaysAvailable);
            sqlCmd.Parameters.AddWithValue("@Destination", Destination);
            sqlCmd.Parameters.AddWithValue("@Origin", Origin);
            sqlCmd.Parameters.AddWithValue("@Notes", Notes);
            sqlCmd.Parameters.AddWithValue("@SeatsAvailable", SeatsAvailable);
            sqlCmd.Parameters.AddWithValue("@DateCreated", DateCreated);
            insertcon.Open();
            rowInserted = sqlCmd.ExecuteNonQuery();
           
            insertcon.Close();  
        }
       
    }
}
