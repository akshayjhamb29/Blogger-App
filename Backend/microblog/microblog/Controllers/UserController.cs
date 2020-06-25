using AutoMapper;
using Business;
using microblog.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace microblog.Controllers
{
    public class UserController : ApiController
    {
        
         /// <summary>
         /// This function registers a new user.
         /// </summary>
         /// <param name="register"></param>
         /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginModel login)
        {
            User user = new User();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ModelState.IsValid)
            {
                UserBDC userBDC = new UserBDC();
                UserDTO userDTO = new UserDTO();

                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<LoginModel, UserDTO>();
                    cfg.CreateMap<UserDTO, User>();
                });

                IMapper mapper = config.CreateMapper();

                userDTO = mapper.Map<LoginModel, UserDTO>(login);



                UserDTO dbUser = userBDC.login(userDTO);

                user = mapper.Map<UserDTO, User>(dbUser);
                return Ok(user);
            }
            return Ok(user);
        }
        /// <summary>
        /// This function registers a new user.
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public IHttpActionResult PostRegister(RegisterModel register)
        {
            User user = new User();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (ModelState.IsValid)
            {
                UserBDC userBDC = new UserBDC();
                UserDTO userDTO = new UserDTO();

                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<RegisterModel, UserDTO>();
                    cfg.CreateMap<UserDTO, User>();
                });

                IMapper mapper = config.CreateMapper();

                userDTO = mapper.Map<RegisterModel, UserDTO>(register);


                UserDTO dbUser = userBDC.Register(userDTO);

                user = mapper.Map<UserDTO, User>(dbUser);

                return Ok(user);
            }
            return Ok(user);
        }
        /// <summary>
        /// This function follow a user
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="FollowerID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("following/{userID}/{FollowerID}")]
        public IHttpActionResult AddFollowing(int userID , int FollowerID)
        {
            UserBDC userBDC = new UserBDC();
            bool flag = userBDC.AddFollower(userID, FollowerID);

            return Ok();
        }
        /// <summary>
        /// This function unfollow a user
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="FollowerID"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("unfollowing/{userID}/{FollowerID}")]
        public IHttpActionResult Unfollowing(int userID, int FollowerID)
        {
            UserBDC userBDC = new UserBDC();
            bool flag = userBDC.Unfollowing(userID, FollowerID);

            return Ok();
        }

        /// <summary>
        /// This function gets a list of followers of user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("followers/{id}")]
        public IHttpActionResult GetFollowers(int id)
        {
            UserBDC userBDC = new UserBDC();
            List<UserDTO> userDTO = userBDC.GetFollowers(id);

            List<User> users = new List<User>();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserDTO, User>();
            });

            IMapper mapper = config.CreateMapper();

            users = mapper.Map<List<UserDTO>, List<User>>(userDTO);
            return Ok(users);
        }

        /// <summary>
        /// This function gets a list of following of user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("following/{id}")]
        public IHttpActionResult GetFollowing(int id)
        {
            UserBDC userBDC = new UserBDC();
            List<UserDTO> userDTO = userBDC.GetFollowing(id);

            List<User> users = new List<User>();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserDTO, User>();
            });

            IMapper mapper = config.CreateMapper();

            users = mapper.Map<List<UserDTO>, List<User>>(userDTO);
            return Ok(users);
        }

        /// <summary>
        /// This function searches the user based on particular search string.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///  
        [HttpGet]
        [Route("searchuser/{id}")]
        public IHttpActionResult UsersBySearchString(string id)
        {
            UserBDC userBDC = new UserBDC();
            List<UserDTO> userDTOs = userBDC.GetUsersBySearchString(id);

            List<User> users = new List<User>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserDTO, User>();
            });

            IMapper mapper = config.CreateMapper();

            users = mapper.Map<List<UserDTO>, List<User>>(userDTOs);

            return Ok(users);

        }


    }
}
