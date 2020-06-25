using Repository;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Business
{
    public class UserBDC

    {
        private UserRepository _userRepository;

        public UserBDC()
        {
            _userRepository = new UserRepository();
        }
        public List<UserDTO> GetUser()
        {
            return _userRepository.GetUser();
        }
        public UserDTO login(UserDTO dto)
        {
            return _userRepository.login(dto);
        }
        public UserDTO Register(UserDTO dto)
        {
            return _userRepository.Register(dto);
        }
        public bool AddFollower(int UserID , int FollowerID)
        {
            return _userRepository.AddFollowing(UserID, FollowerID);
        }
        public List<UserDTO> GetFollowers(int UserID)
        {
            return _userRepository.GetFollowers(UserID);
        }

        public List<UserDTO> GetFollowing(int UserID)
        {
            return _userRepository.GetFollowing(UserID);
        }

        public bool Unfollowing(int userID, int followerID)
        {
            return _userRepository.Unfollowing(userID, followerID);
        }

        public List<UserDTO> GetUsersBySearchString(string id)
        {
            return _userRepository.GetUsersBySearchString(id);
        }
    }
}
