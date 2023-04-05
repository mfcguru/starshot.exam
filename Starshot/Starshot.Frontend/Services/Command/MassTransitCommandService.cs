using MassTransit;
using Starshot.Frontend.Services.Command.Models;
using System.Threading;
using System;

namespace Starshot.Frontend.Services.Command
{
    public class MassTransitCommandService : ICommandService
    {
        private readonly IPublishEndpoint publishEndpoint;
        public MassTransitCommandService(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task<ServiceResult> AddUser(string firstName, string lastName, DateTime timeIn, DateTime timeOut)
        {
            try
            {
                await publishEndpoint.Publish<AddUserModel>(new AddUserModel
                {
                    FirstName = firstName,
                    LastName = lastName,
                    TimeIn = timeIn,
                    timeOut = timeOut
                });

                return new ServiceResult
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                };
            }
            catch(Exception e)
            {
                return new ServiceResult
                {
                    ErrorMessage = e.ToString(),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Success = false
                };
            }
        }

        public async Task<ServiceResult> DeleteUser(int userId)
        {
            try
            {
                await publishEndpoint.Publish<DeleteUserModel>(new DeleteUserModel
                {
                    UserId = userId
                });

                return new ServiceResult
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResult
                {
                    ErrorMessage = e.ToString(),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Success = false
                };
            }
        }

        public async Task<ServiceResult> EditUser(int userId, string firstName, string lastName, DateTime timeIn, DateTime timeOut, bool active)
        {
            try
            {
                await publishEndpoint.Publish<EditUserModel>(new EditUserModel
                {
                    UserId = userId,
                    FirstName = firstName,
                    LastName = lastName,
                    TimeIn = timeIn,
                    timeOut = timeOut,
                    Active = active
                });

                return new ServiceResult
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Success = true
                };
            }
            catch (Exception e)
            {
                return new ServiceResult
                {
                    ErrorMessage = e.ToString(),
                    StatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Success = false
                };
            }
        }
    }
}
