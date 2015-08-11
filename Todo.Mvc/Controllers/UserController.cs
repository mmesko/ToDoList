using AutoMapper;
using Todo.Model.Common;
using Todo.Service.Common;
using Todo.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Todo.Mvc.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private readonly IUserService userService;
        private UserModel userForValidation;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        #region Methods

        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                ICollection<UserModel> user = Mapper.Map<ICollection<UserModel>>(await userService.GetAsync());
                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Can't find any user");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }        
        }


        [Route("{username}")]
        public async Task<HttpResponseMessage> Get(string username)
        {
            try
            {
                UserModel user = Mapper.Map<UserModel>(await userService.FindAsync(username));
                userForValidation = user;

                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Can't find user with given id");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                }
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("Register")]
        [HttpPost()]
        public async Task<HttpResponseMessage> Register(ChangeUserModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid action.");
                }

                // add password to user
                user.User.PasswordHash = user.Password;

                bool isRegistered = await userService.RegisterUser(Mapper.Map<IUser>(user.User), user.Password);

                if (isRegistered)
                {
                    return Request.CreateResponse(HttpStatusCode.Created, "User registered");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Error while creating new user.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("UpdateUserOrMail/{id}")]
        [Authorize]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdateUsernameOrMail(string id, ChangeUserModel model)
        {
            try
            {
                IUser result = await userService.UpdateEmailOrUsernameAsync(Mapper.Map<IUser>(model.User), model.Password);

                if (model == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Error while validating user. Update failed");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("UpdatePassword/{id}")]
        [Authorize]
        [HttpPut]
        public async Task<HttpResponseMessage> UpdatePassword(string id,ChangeUserPasswordModel model)
        {
            try
            {
                bool result = await userService.UpdatePasswordAsync(id, model.OldPassword, model.NewPassword);

                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Password updated.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Update failed.");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// Keeps user model that holds user and password
        /// </summary>
        public class ChangeUserModel
        {
            public UserModel User { get; set; }
            public string Password { get; set; }
        }

        /// <summary>
        /// Keeps model data for changing user password
        /// </summary>
        public class ChangeUserPasswordModel
        {
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
        }



    }

}
