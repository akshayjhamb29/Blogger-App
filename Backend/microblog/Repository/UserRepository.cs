using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Shared;
using AutoMapper;
using Entity.Models;

namespace Repository
{
    public class UserRepository
    {

        public List<UserDTO> GetUser()
        {
            List<UserDTO> userDTO = new List<UserDTO>();
            try
            {
                //var config = new MapperConfiguration(
                //    cfg =>
                //    {
                //        cfg.CreateMap<User, UserDTO>();

                //    });

                //IMapper mapper = config.CreateMapper();
                //using (var dbContext = new DatasetContext())
                //{
                //    List<User> userdb = dbContext.Users.ToList();

                //    userDTO = mapper.Map<List<User>, List<UserDTO>>(userdb);

                //}


            }

            catch (Exception e)
            {
                return null;
            }
            return userDTO;

        }

        public UserDTO login(UserDTO userinput)
        {
            try
            {
                var config = new MapperConfiguration(
                   cfg =>
                   {
                       cfg.CreateMap<User, UserDTO>();

                   });

                IMapper mapper = config.CreateMapper();
                using (var dbContext = new DatasetContext())
                {
                    UserDTO userDTO = new UserDTO();
                    var userDb = dbContext.Users.SingleOrDefault(x => x.EmailID == userinput.EmailID);
                    if (userDb.Password == userinput.Password)
                    {

                        userDTO = mapper.Map<User, UserDTO>(userDb);

                        return userDTO;
                    }
                    else
                    {
                        throw new ArgumentException("please enter correct credentials");
                    }
                }
            }


            catch (System.Data.SqlClient.SqlException ea)
            {
                throw new Exception("It will take some time to process your request.The server seems slow");
            }

            catch (NullReferenceException eb)
            {
                throw new Exception("Please register before logging in");
            }

            catch (ArgumentException ec)
            {
                throw new Exception("Incorrect Password entered.");
            }

            catch (Exception ex)
            {
                throw new Exception("Some error has occured");
            }

        }
        /// <summary>
        /// This function searchs a user based on search input.
        /// </summary>
        /// <param name="userinput"></param>
        /// <returns></returns>
        public List<UserDTO> GetUsersBySearchString(string id)
        {
            List<UserDTO> userDTOs = new List<UserDTO>();
            try
            {
                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<User, UserDTO>();

                    });

                IMapper mapper = config.CreateMapper();
                using (var dbContext = new DatasetContext())
                {
                    List<User> userdb = dbContext.Users.Where(s =>
                                                s.FirstName.ToLower().Contains(id.ToLower()) || s.LastName.ToLower().Contains(id.ToLower())
                                                                                 || s.EmailID.ToLower().Contains(id.ToLower())).ToList();

                    userDTOs = mapper.Map<List<User>, List<UserDTO>>(userdb);

                }
                return userDTOs;


            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// This function registers a new user.
        /// </summary>
        /// <param name="userinput"></param>
        /// <returns></returns>
        public UserDTO Register(UserDTO userinput)
        {


            var config = new MapperConfiguration(
                  cfg =>
                  {
                      cfg.CreateMap<UserDTO, User>();
                      cfg.CreateMap<User, UserDTO>();

                  });

            IMapper mapper = config.CreateMapper();
            using (var dbContext = new DatasetContext())
            {
                User user = new User();

                user = mapper.Map<UserDTO, User>(userinput);
                user.Country = "india";
                //user.ProfileImage = "/a.jpg";
                //user.Tweets = new List<Tweet>();
                user.Followings = new List<Following>();
                dbContext.Users.Add(user);
                dbContext.SaveChanges();


                User newuser = dbContext.Users.SingleOrDefault(x => x.EmailID == userinput.EmailID);

                UserDTO userDTO = new UserDTO();
                userDTO = mapper.Map<User, UserDTO>(newuser);
                return userDTO;

            }




        }

        /// <summary>
        /// Add a folower in user list
        /// </summary>
        /// <param name="UserID"></param> Logged in user 
        /// <param name="FollowingID"></param> to be followed user 
        /// <returns></returns>
        public bool AddFollowing (int LoggedUserID , int ToBeFollowedID)
        {
            using (var dbContext = new DatasetContext())
            {     
                Following person = new Following();
                person.UserID = LoggedUserID;
                person.FollowingID = ToBeFollowedID;
                person.Follow = dbContext.Users.SingleOrDefault(x => x.UserID == ToBeFollowedID);
                var userDb = dbContext.Users.SingleOrDefault(x => x.UserID == LoggedUserID);
                person.User = userDb;
                userDb.Followings.Add(person);
                dbContext.SaveChanges();
            }
            return true;
        }

        public bool Unfollowing(int userID, int followerID)
        {
            using (var dbContext = new DatasetContext())
            {

    
                Following following = dbContext.Followings.SingleOrDefault(x => x.User.UserID == userID && x.Follow.UserID == followerID);
                dbContext.Followings.Remove(following);
                dbContext.SaveChanges();
            }
            return true;
        }


        /// <summary>
        /// This function returns the list of followers
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>

        public List <UserDTO> GetFollowers(int LoggedUserID)
        {
          
            List<UserDTO> userDTO = new List<UserDTO>();
            try
            {
                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<User, UserDTO>();

                    });

                IMapper mapper = config.CreateMapper();
                using (var dbContext = new DatasetContext())
                {
                    //List<User> users = dbContext.Users.Include("Followings").Include("Followings.Follow").SingleOrDefault(x => x.UserID == LoggedUserID).Followings.Select(y => y.Follow).ToList();
                    List<User> users = dbContext.Followings.Include("user").Where(x => x.Follow.UserID == LoggedUserID).Select(x => x.User).ToList();
                    userDTO = mapper.Map<List<User>, List<UserDTO>>(users);

                }


            }

            catch (Exception e)
            {
                return null;
            }
            return userDTO;

        }



        /// <summary>
        /// This function returns the list of followingUser
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>

        public List<UserDTO> GetFollowing(int LoggedUserID)
        {

            List<UserDTO> userDTO = new List<UserDTO>();
            try
            {
                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<User, UserDTO>();

                    });

                IMapper mapper = config.CreateMapper();
                using (var dbContext = new DatasetContext())
                {
                    List<User> users = dbContext.Users.Include("Followings").Include("Followings.Follow").SingleOrDefault(x => x.UserID == LoggedUserID).Followings.Select(y => y.Follow).ToList();
                    userDTO = mapper.Map<List<User>, List<UserDTO>>(users);

                }


            }

            catch (Exception e)
            {
                return null;
            }
            return userDTO;

        }

    }
}
