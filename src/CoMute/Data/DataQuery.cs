using CoMute.Web.Data;
using CoMute.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Data
{
	public static class DataQuery
	{
		#region Registration, login, and profile
		public async static Task<AsyncDBResponse> RegisterUser(Models.User u)
		{
            try
            {
                var error = new ObjectParameter("Output", "");
			    var result = await Task.Run(() =>
			    {
				    return Data.DAL.DataEntities.RegisterUser(
					    u.Name,
					    u.Surname,
					    u.Phone,
					    u.EmailAddress,
					    u.Password,
					    error
				    );
			    }).ConfigureAwait(false);

			    return new AsyncDBResponse()
			    {
				    IsSuccess = error.Value.ToString() == String.Empty ? true : false,
				    Error = error.Value.ToString(),
			    };
            }
            catch (Exception ex)
            {
                return new AsyncDBResponse()
                {
                    IsSuccess = false,
                    Error = ex.ToString()
                };
            }			
		}

		public async static Task<AsyncDBResponse<UserLogin_Result>> UserLogin(string email, string password)
		{
            try
            {
			    var error = new ObjectParameter("Output", "");
			    var result = await Task.Run(() =>
			    {
				    return Data.DAL.DataEntities.UserLogin(
					    email,
					    password,
					    error
				    ).FirstOrDefault();
			    }).ConfigureAwait(false);

                return new AsyncDBResponse<UserLogin_Result>()
			    {
				    IsSuccess = error.Value.ToString() == String.Empty ? true : false,
				    Error = error.Value.ToString(),
				    Result = result
			    };
            }
            catch (Exception ex)
            {
                return new AsyncDBResponse<UserLogin_Result>()
                {
                    IsSuccess = false,
                    Error = ex.ToString()
                };
            }
		}

		public async static Task<AsyncDBResponse> UpdateProfile(Models.User u)
		{
            try
            {
			    var error = new ObjectParameter("Output", "");
			    var result = await Task.Run(() =>
			    {
				    return Data.DAL.DataEntities.UpdateProfile(
					    u.Id,
					    u.Name,
					    u.Surname,
					    u.Phone,
					    u.EmailAddress,
					    error
				    );
			    }).ConfigureAwait(false);

			    return new AsyncDBResponse()
			    {
				    IsSuccess = error.Value.ToString() == String.Empty ? true : false,
				    Error = error.Value.ToString(),
			    };
            }
            catch (Exception ex)
            {
                return new AsyncDBResponse()
                {
                    IsSuccess = false,
                    Error = ex.ToString()
                };
            }
		}

        public async static Task<AsyncDBResponse<GetUser_Result>> GetUser(int id)
        {
            try
            {
                var error = new ObjectParameter("Output", "");
                var result = await Task.Run(() =>
                {
                    return Data.DAL.DataEntities.GetUser(
                        id,
                        error
                    ).FirstOrDefault();
                }).ConfigureAwait(false);

                return new AsyncDBResponse<GetUser_Result>()
                {
                    IsSuccess = error.Value.ToString() == String.Empty ? true : false,
                    Error = error.Value.ToString(),
                    Result = result
                };
            }
            catch (Exception ex)
            {
                return new AsyncDBResponse<GetUser_Result>()
                {
                    IsSuccess = false,
                    Error = ex.ToString()
                };
            }
        }

		public async static Task<AsyncDBResponse> UpdatePassword(int userId, string newPassword)
		{
            try
            {
			    var error = new ObjectParameter("Output", "");
			    var result = await Task.Run(() =>
			    {
				    return Data.DAL.DataEntities.UpdatePassword(
					    userId,
					    newPassword,
					    error
				    );
			    }).ConfigureAwait(false);

			    return new AsyncDBResponse()
			    {
				    IsSuccess = error.Value.ToString() == String.Empty ? true : false,
				    Error = error.Value.ToString(),
			    };
            }
            catch (Exception ex)
            {
                return new AsyncDBResponse()
                {
                    IsSuccess = false,
                    Error = ex.ToString()
                };
            }
		}

		#endregion Registration and login

		#region Hosting car pools

		public async static Task<AsyncDBResponse> CreateCarPool(HostedCarPool hcp)
		{
            try
            {
			    var error = new ObjectParameter("Output", "");
			    var result = await Task.Run(() =>
			    {
				    return Data.DAL.DataEntities.CreateCarPool(
					    hcp.HostUserId,
					    hcp.Depart,
					    hcp.Arrive,
					    hcp.Origin,
					    hcp.OriginLat,
					    hcp.OriginLon,
					    hcp.Destination,
					    hcp.DestinationLat,
					    hcp.DestinationLon,
					    hcp.DaysAvailable,
					    hcp.Seats,
					    hcp.Notes,
					    error
				    );
			    }).ConfigureAwait(false);

			    return new AsyncDBResponse()
			    {
				    IsSuccess = error.Value.ToString() == String.Empty ? true : false,
				    Error = error.Value.ToString(),
			    };
            }
            catch (Exception ex)
            {
                return new AsyncDBResponse()
                {
                    IsSuccess = false,
                    Error = ex.ToString()
                };
            }
		}

		public async static Task<AsyncDBResponse> CancelCarPool(int id)
		{
            try
            {
			    var error = new ObjectParameter("Output", "");
			    var result = await Task.Run(() =>
			    {
				    return Data.DAL.DataEntities.CancelCarPool(
					    id,
					    error
				    );
			    }).ConfigureAwait(false);

			    return new AsyncDBResponse()
			    {
				    IsSuccess = error.Value.ToString() == String.Empty ? true : false,
				    Error = error.Value.ToString(),
			    };
            }
            catch (Exception ex)
            {
                return new AsyncDBResponse()
                {
                    IsSuccess = false,
                    Error = ex.ToString()
                };
            }
		}

		public async static Task<AsyncDBResponse<List<GetHostedCarPools_Result>>> GetHostedCarPools(int UserId)
		{
            try
            {
			    var error = new ObjectParameter("Output", "");
			    var result = await Task.Run(() =>
			    {
				    return Data.DAL.DataEntities.GetHostedCarPools(
					    UserId,
					    error
				    );
			    }).ConfigureAwait(false);

			    return new AsyncDBResponse<List<GetHostedCarPools_Result>>()
			    {
				    IsSuccess = error.Value.ToString() == String.Empty ? true : false,
				    Error = error.Value.ToString(),
				    Result = result.ToList()
			    };
            }
            catch (Exception ex)
            {
                return new AsyncDBResponse<List<GetHostedCarPools_Result>>()
                {
                    IsSuccess = false,
                    Error = ex.ToString()
                };
            }
		}

		#endregion Hosting car pools

		#region Participating in car pools

		public async static Task<AsyncDBResponse> JoinCarPool(int UserId, JoinCarPoolRequest jcpr)
		{
            try
            {
			    var error = new ObjectParameter("Output", "");
			    var result = await Task.Run(() =>
			    {
				    return Data.DAL.DataEntities.JoinCarPool(
					    UserId, 
					    jcpr.CarPoolId, 
					    jcpr.SelectedDays,					
					    error
				    );
			    }).ConfigureAwait(false);

			    return new AsyncDBResponse()
			    {
				    IsSuccess = error.Value.ToString() == String.Empty ? true : false,
				    Error = error.Value.ToString(),
			    };
            }
            catch (Exception ex)
            {
                return new AsyncDBResponse()
                {
                    IsSuccess = false,
                    Error = ex.ToString()
                };
            }
		}

		public async static Task<AsyncDBResponse> LeaveCarPool(int UserId, int CarPoolId)
		{
            try
            {
			    var error = new ObjectParameter("Output", "");
			    var result = await Task.Run(() =>
			    {
				    return Data.DAL.DataEntities.LeaveCarPool(
					    UserId,
					    CarPoolId,
					    error
				    );
			    }).ConfigureAwait(false);

			    return new AsyncDBResponse()
			    {
				    IsSuccess = error.Value.ToString() == String.Empty ? true : false,
				    Error = error.Value.ToString(),
			    };
            }
            catch (Exception ex)
            {
                return new AsyncDBResponse()
                {
                    IsSuccess = false,
                    Error = ex.ToString()
                };
            }
		}

		public async static Task<AsyncDBResponse<List<GetJoinedCarPools_Result>>> GetJoinedCarPools(int UserId)
		{
            try
            {
			    var error = new ObjectParameter("Output", "");
			    var result = await Task.Run(() =>
			    {
				    return Data.DAL.DataEntities.GetJoinedCarPools(
					    UserId,
					    error
				    );
			    }).ConfigureAwait(false);

			    return new AsyncDBResponse<List<GetJoinedCarPools_Result>>()
			    {
				    IsSuccess = error.Value.ToString() == String.Empty ? true : false,
				    Error = error.Value.ToString(),
				    Result = result.ToList()
			    };
            }
            catch (Exception ex)
            {
                return new AsyncDBResponse<List<GetJoinedCarPools_Result>>()
                {
                    IsSuccess = false,
                    Error = ex.ToString()
                };
            }
		}

		#endregion Participating in car pools

		#region Finding a car pool

		public async static Task<AsyncDBResponse<List<SearchCarPools_Result>>> SearchCarPools(SearchFilter sf)
		{
            try
            {
			    var error = new ObjectParameter("Output", "");
                var result = await Task.Run(() =>
                {
                    return Data.DAL.DataEntities.SearchCarPools(
                        sf.Depart,
                        sf.Arrive,
                        sf.OriginLat,
                        sf.OriginLon,
                        sf.DestinationLat,
                        sf.DestinationLon,
                        sf.DaysAvailable,
                        error
                    );
                }).ConfigureAwait(false);

			    return new AsyncDBResponse<List<SearchCarPools_Result>>()
			    {
				    IsSuccess = error.Value.ToString() == String.Empty ? true : false,
				    Error = error.Value.ToString(),
				    Result = result.ToList()
			    };
            }
            catch (Exception ex)
            {
                return new AsyncDBResponse<List<SearchCarPools_Result>>()
                {
                    IsSuccess = false,
                    Error = ex.ToString()
                };
            }
		}
		#endregion Finding a car pool
	}
}
