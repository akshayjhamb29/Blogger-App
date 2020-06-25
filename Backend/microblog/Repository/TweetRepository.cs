using AutoMapper;
using Entity;
using Entity.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class TweetRepository
    {  /// <summary>
       /// This returns tweet of user and his followings.
       /// </summary>
       /// <param name="UserID"></param>
       /// <returns></returns>
        public List<TweetDTO> GetAllTweets(int UserID)
        {
            List<TweetDTO> tweetDTO = new List<TweetDTO>();
            try
            {
                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<Tweet, TweetDTO>();
                        //cfg.CreateMap<User, UserDTO>();
                    });

                IMapper mapper = config.CreateMapper();
                using (var dbContext = new DatasetContext())
                {
                    List<User> users = dbContext.Users.Include("Followings").Include("Followings.Follow").SingleOrDefault(x => x.UserID == UserID).Followings.Select(y => y.Follow).ToList();
                    List<Tweet> tweets = new List<Tweet>();
                    List<Tweet> tweet = dbContext.Tweets.Where(x => x.UserID == UserID).ToList();
                    for (int j = 0; j < tweet.Count; j++)
                    {
                        tweets.Add(tweet.ElementAt(j));
                    }

                    for (int i = 0; i < users.Count; i++)
                    {
                        User user = users.ElementAt(i);
                        List<Tweet> anothertweet = dbContext.Tweets.Where(x => x.UserID == user.UserID).ToList();

                        for (int j = 0; j < anothertweet.Count; j++)
                        {
                            tweets.Add(anothertweet.ElementAt(j));
                        }
                        
                    }

                    tweets = tweets.OrderByDescending(x => x.UpdatedDate).ToList();

                    List<TweetDTO> tweetDTOs = mapper.Map<List<Tweet>, List<TweetDTO>>(tweets);
                    return tweetDTOs;

                }
               

            }

            catch (Exception e)
            {
                return null;
            }


        }

     
        public TweetDTO GetTweetByTweetID(int id)
        {
            TweetDTO tweetDTO = new TweetDTO();
            try
            {
                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<Tweet, TweetDTO>();

                    });

                IMapper mapper = config.CreateMapper();
                using (var dbContext = new DatasetContext())
                {
                    Tweet tweetdb = dbContext.Tweets.SingleOrDefault(x => x.TweetID == id);

                    tweetDTO = mapper.Map<Tweet, TweetDTO>(tweetdb);

                }
                return tweetDTO;

            }

            catch (Exception e)
            {
                return null;
            }

        }



        /// <summary>
        /// This function returns the search results.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<TweetDTO> GetTweetsBySearchString(string id)
        {
            List<TweetDTO> tweetDTO = new List<TweetDTO>();
            try
            {
                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<Tweet, TweetDTO>();

                    });

                IMapper mapper = config.CreateMapper();
                using (var dbContext = new DatasetContext())
                {
                    List<Tweet> tweetdb = dbContext.Tweets.Where(s => 
                                                s.Description.ToLower().Contains("#"+id.ToLower())).ToList();

                    tweetDTO = mapper.Map<List<Tweet>, List<TweetDTO>>(tweetdb);

                }
                return tweetDTO;


            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// This function deletes a tweet.
        /// </summary>
        /// <param name="tweetID"></param>
        /// <returns></returns>
      
        public void DeleteTweet(int tweetID)
        {
            try
            {
                using (var dbContext = new DatasetContext())
                {

                    var tweet = dbContext.Tweets.SingleOrDefault(x => x.TweetID == tweetID);
                    dbContext.Tweets.Remove(tweet);
                    dbContext.SaveChanges();

                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
        
        public List<TweetDTO> GetTweetsByAuthorID(int id)
        {
            List<TweetDTO> tweetDTO = new List<TweetDTO>();
            try
            {
                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<Tweet, TweetDTO>();

                    });

                IMapper mapper = config.CreateMapper();
                using (var dbContext = new DatasetContext())
                {
                    //List<Tweet> tweetdb = dbContext.Tweets.Where(x => x.Author_UserID == id).ToList();

                    //tweetDTO = mapper.Map<List<Tweet>, List<TweetDTO>>(tweetdb);

                }
                return tweetDTO;

            }

            catch (Exception e)
            {
                return null;
            }


        }
        /// <summary>
        /// This function updates a tweet.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tweetDTO"></param>
        /// <returns></returns>
        public TweetDTO UpdateTweet(int id, TweetDTO tweetDTO)
        {
            var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<Tweet, TweetDTO>();
                        cfg.CreateMap<TweetDTO, Tweet>();

                    });

            IMapper mapper = config.CreateMapper();
            using (var dbContext = new DatasetContext())
            {
                Tweet tweet = new Tweet();
                tweet = dbContext.Tweets.SingleOrDefault(x => x.TweetID == id);

             
                tweet.Description = tweetDTO.Description;
            

                tweet.UpdatedDate = DateTime.Now;
                tweet.TweetID = id;
             
                //dbContext.Entry(tweet).State = EntityState.Modified;

                 dbContext.SaveChanges();

                tweetDTO = mapper.Map<Tweet, TweetDTO>(tweet);

                return tweetDTO;

            }
        }
        /// <summary>
        /// This function posts a new tweet.
        /// </summary>
        /// <param name="tweetDTO"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public TweetDTO PostTweet(TweetDTO tweetDTO,int userID)
        {
            var config = new MapperConfiguration(
                     cfg =>
                     {
                         cfg.CreateMap<Tweet, TweetDTO>();
                         cfg.CreateMap<TweetDTO, Tweet>();

                     });

            IMapper mapper = config.CreateMapper();
            using (var dbContext = new DatasetContext())
            {
                Tweet tweet = new Tweet();

                tweet = mapper.Map<TweetDTO, Tweet>(tweetDTO);
          
                tweet.UpdatedDate = DateTime.Now;
                tweet.CreatedDate = DateTime.Now.Date;
                tweet.User = dbContext.Users.SingleOrDefault(x => x.UserID == userID);
                tweet.Address = tweet.User.FirstName + " "  + tweet.User.LastName;
                tweet.UserID = tweet.User.UserID;

                dbContext.Tweets.Add(tweet);

                dbContext.SaveChanges();

                tweetDTO = mapper.Map<Tweet, TweetDTO>(tweet);

                return tweetDTO;

            }


        }

        /// <summary>
        /// This function post upvote or downvote to tweet.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="tweetID"></param>
        /// <param name="key"></param>
        /// <returns></returns>

        public void voteTweet(int tweetID, int userID, int key)
        {
            try
            {
                using (var db = new DatasetContext())
                {
                    Voting vote = db.Votings.SingleOrDefault(x => x.UserID == userID && x.TweetID == tweetID);

                    bool Key = (key == 0) ? false : true;
                    if (vote != null)
                    {
                        if (vote.Status == Key)
                        {
                            db.Votings.Remove(vote);
                        }
                        else
                        {
                            vote.Status = Key;
                        }
                    }
                    else
                    {
                        Voting newVote = new Voting();
                        newVote.UserID = userID;
                        newVote.TweetID = tweetID;
                        newVote.Status = Key;
                        db.Votings.Add(newVote);
                    }

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<TweetDTO> GetAllTweetsByDate()
        {

            List<TweetDTO> tweetDTO = new List<TweetDTO>();
            try
            {
                var config = new MapperConfiguration(
                    cfg =>
                    {
                        cfg.CreateMap<Tweet, TweetDTO>();
                        //cfg.CreateMap<User, UserDTO>();
                    });

                IMapper mapper = config.CreateMapper();
                using (var dbContext = new DatasetContext())
                {
                    DateTime dateTime = DateTime.Now.Date;
                    
                    List<Tweet> tweets = dbContext.Tweets.Where(x => x.CreatedDate==dateTime).ToList();

                   




                    List<TweetDTO> tweetDTOs = mapper.Map<List<Tweet>, List<TweetDTO>>(tweets);

                    return tweetDTOs;
                }

               
            }

            catch (Exception e)
            {
                return null;
            }


        }


        /// <summary>
        /// This function gets voting of a user on tweets.
        /// /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<VotingDTO> GetVoting(int userID)
        {
            try
            {
                var config = new MapperConfiguration(
                     cfg =>
                     {
                         cfg.CreateMap<Voting, VotingDTO>();
                         cfg.CreateMap<VotingDTO, Voting>();

                     });

                IMapper mapper = config.CreateMapper();
                using (var db = new DatasetContext())
                {
                    List<Voting> votings = db.Votings.Where(x => x.UserID == userID).ToList();

                    List<VotingDTO> votingDTOs = mapper.Map<List<Voting>, List<VotingDTO>>(votings);
                    return votingDTOs;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
