using Repository;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class TweetBDC
    {
        private TweetRepository _tweetRepository;

        public TweetBDC()
        {
            _tweetRepository = new TweetRepository();
        }

        public TweetDTO GetTweetByTweetID(int id)
        {
            return _tweetRepository.GetTweetByTweetID(id);
        }


        public List<TweetDTO> GetAllTweets(int UserID)
        {
            return _tweetRepository.GetAllTweets(UserID);
        }

        public TweetDTO PostTweet(TweetDTO tweetDTO,int userID)
        {
            return _tweetRepository.PostTweet(tweetDTO,userID);
        }

        public TweetDTO UpdateTweet(int id, TweetDTO tweetDTO)
        {
            return _tweetRepository.UpdateTweet(id, tweetDTO);
        }

      

        public List<TweetDTO> GetTweetsByAuthorID(int id)
        {
            return _tweetRepository.GetTweetsByAuthorID(id);

        }

        public void DeleteTweet(int tweetID)
        {
             _tweetRepository.DeleteTweet(tweetID);
        }

        public List<TweetDTO> GetTweetsBySearchString(string id)
        {
            return _tweetRepository.GetTweetsBySearchString(id);
        }

        public void voteAnswer(int tweetID, int userID, int key)
        {
            _tweetRepository.voteTweet(tweetID, userID, key);
        }

        public List<VotingDTO> GetVoting(int userID)
        {
            return _tweetRepository.GetVoting(userID);
        }
       public List<TweetDTO> GetAllTweetsByDate()
        {
            return _tweetRepository.GetAllTweetsByDate();
        }
    }
}
