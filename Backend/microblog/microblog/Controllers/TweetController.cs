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

    public class TweetController : ApiController
    {   /// <summary>
        /// This function posts a new tweet.
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("newtweet/{userID}")]
        public IHttpActionResult PostTweet(Tweet tweet, int userID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            try
            {
                //List<KeyValuePair<string, IEnumerable<string>>> headers = Request.Headers.ToList();
                //string token = headers[6].Value.First();

                TweetBDC tweetBDC = new TweetBDC();
                TweetDTO tweetDTO = new TweetDTO();
                var config = new MapperConfiguration(cfg =>
                {

                    cfg.CreateMap<Tweet, TweetDTO>();
                    cfg.CreateMap<TweetDTO, Tweet>();
                });

                IMapper mapper = config.CreateMapper();



                tweetDTO = mapper.Map<Tweet, TweetDTO>(tweet);

                tweetDTO = tweetBDC.PostTweet(tweetDTO, userID);

                return Ok(mapper.Map<TweetDTO, Tweet>(tweetDTO));


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// This function deletes a tweet.
        /// </summary>
        /// <param name="tweetID"></param>
        /// <returns></returns>
        [Route("delete/{tweetID}")]
        [HttpDelete]
        public IHttpActionResult DeleteTweet(int tweetID)
        {
            try
            {
                //List<KeyValuePair<string, IEnumerable<string>>> headers = Request.Headers.ToList();
                //string token = headers[6].Value.First();

                TweetBDC tweetBDC = new TweetBDC();
                tweetBDC.DeleteTweet(tweetID);
            }
            catch (Exception)
            {
                return BadRequest("Tweet does not exist");
            }
            return Ok();
        }


        /// <summary>
        /// This function updates a tweet.
        /// </summary>
        /// <param name="tweetId"></param>
        /// <param name="tweet"></param>
        /// <returns></returns>
        [Route("edit/{tweetID}")]
        [HttpPut]
        public IHttpActionResult UpdateTweet(int tweetID, Tweet tweet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            try
            {
                //List<KeyValuePair<string, IEnumerable<string>>> headers = Request.Headers.ToList();
                //string token = headers[6].Value.First();

                TweetBDC tweetBDC = new TweetBDC();
                TweetDTO tweetDTO = new TweetDTO();
                var config = new MapperConfiguration(cfg =>
                {

                    cfg.CreateMap<Tweet, TweetDTO>();
                    cfg.CreateMap<TweetDTO, Tweet>();
                });

                IMapper mapper = config.CreateMapper();

                tweetDTO = mapper.Map<Tweet, TweetDTO>(tweet);

                tweetDTO = tweetBDC.UpdateTweet(tweetID, tweetDTO);

                return Ok(mapper.Map<TweetDTO, Tweet>(tweetDTO));
            }
            catch (Exception)
            {
                return BadRequest("Invalid");
            }
        }

        /// <summary>
        /// This returns tweet of user and his followings.
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("tweet/{userID}")]
        public IHttpActionResult Tweet(int userID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            try
            {
                TweetBDC tweetBDC = new TweetBDC();
                List<TweetDTO> tweetDTO = new List<TweetDTO>();
                var config = new MapperConfiguration(cfg =>
                {

                    cfg.CreateMap<Tweet, TweetDTO>();
                    cfg.CreateMap<TweetDTO, Tweet>();
                    //cfg.CreateMap<User, UserDTO>();
                    //cfg.CreateMap<UserDTO, User>();

                });

                IMapper mapper = config.CreateMapper();


                tweetDTO = tweetBDC.GetAllTweets(userID);

                List<Tweet> tweets = mapper.Map<List<TweetDTO>, List<Tweet>>(tweetDTO);
                return Ok(tweets);
            }
            catch (Exception)
            {
                return BadRequest("Invalid");
            }
        }




        /// <summary>
        /// This function searches the question based on particular search string.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///  
        [HttpGet]
        [Route("search/{id}")]
        public IHttpActionResult TweetsBySearchString(string id)
        {
            TweetBDC tweetBDC = new TweetBDC();
            List<TweetDTO> tweetDTO =tweetBDC.GetTweetsBySearchString(id);

            List<Tweet>tweets = new List<Tweet>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TweetDTO, Tweet>();
            });

            IMapper mapper = config.CreateMapper();

          tweets = mapper.Map<List<TweetDTO>, List<Tweet>>(tweetDTO);

            return Ok(tweets);

        }

        [HttpGet]
        [Route("date")]
        public IHttpActionResult TweetsByDate()
        {
            TweetBDC tweetBDC = new TweetBDC();
            List<TweetDTO> tweetDTO = tweetBDC.GetAllTweetsByDate();

            List<Tweet> tweets = new List<Tweet>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TweetDTO, Tweet>();
            });

            IMapper mapper = config.CreateMapper();

            tweets = mapper.Map<List<TweetDTO>, List<Tweet>>(tweetDTO);

            return Ok(tweets);

        }

        /// <summary>
        /// This function post upvote or downvote to tweet.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="tweetID"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [Route("tweet/{tweetID}/{userID}/vote/{key}")]
        [HttpPost]
        public IHttpActionResult VoteAnswer(int tweetID, int userID, int key)
        {
            var config = new MapperConfiguration(
                     cfg =>
                     {
                         cfg.CreateMap<Voting, VotingDTO>();
                         cfg.CreateMap<VotingDTO, Voting>();

                     });

            IMapper mapper = config.CreateMapper();
            try
            {
                TweetBDC tweetBDC = new TweetBDC();
                tweetBDC.voteAnswer(tweetID, userID, key);
                List<VotingDTO> votingDTOs = tweetBDC.GetVoting(userID);

                List<Voting> votings = mapper.Map<List<VotingDTO>, List<Voting>>(votingDTOs);
                return Ok(votings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();

        }

        [Route("voting/{userID}")]
        [HttpGet]
        public IHttpActionResult GetVoting(int userID)
        {
            var config = new MapperConfiguration(
                     cfg =>
                     {
                         cfg.CreateMap<Voting, VotingDTO>();
                         cfg.CreateMap<VotingDTO, Voting>();

                     });

            IMapper mapper = config.CreateMapper();
            try
            {
                TweetBDC tweetBDC = new TweetBDC();
                List<VotingDTO> votingDTOs = tweetBDC.GetVoting(userID);

                List<Voting> votings = mapper.Map<List<VotingDTO>, List<Voting>>(votingDTOs);
                return Ok(votings);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }

}
