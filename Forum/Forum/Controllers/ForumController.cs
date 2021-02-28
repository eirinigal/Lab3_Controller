using Forum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        //Private static field which keeps an in-memory list of posts.
        private static List<Post> postList = new List<Post>()
        {
            new Post(){ID=1, TimeStamp=DateTime.Now, Subject="First!", Message="This is the first post"},
            new Post(){ID=2, TimeStamp=DateTime.Now, Subject="Second!", Message="This is the second post"},
            new Post(){ID=3, TimeStamp=DateTime.Now, Subject="Third!", Message="This is the third post"}

        };


        /*Instead of the above I had the following in order for the constructor to be called and the timestamp was set in the constructor. GET requests had no issue to called
         
         private static List<Post> postList = new List<Post>()
        {
            new Post(ID=1, Subject="First!", Message="This is the first post"),
            new Post(ID=2, Subject="Second!", Message="This is the second post"),
            new Post(ID=3, Subject="Third!", Message="This is the third post")

        };  */



        //GET, POST, PUT, DELETE Methods

        //Return all posts in the forum
        // GET: api/<ForumController>
        [HttpGet]
        public List<Post> Get()
        {
            return postList;
        }

        //Return on post - the request will specify the ID for the post
        // GET api/<ForumController>/5
        [HttpGet("{id}")]
        public Post Get(int id)
        {
            return postList.SingleOrDefault(p=> p.ID == id);
           
        }


        //Third post -- we need to specify the route
        //Return the last specified number of posts in the forum
        // GET api/<ForumController>/GetAnother/2
        [HttpGet("GetAnother/{number}")]
        public List<Post> GetAnother(int number)
        {
            return postList.OrderByDescending(p=>p.ID).Take(number).ToList();

        }


        //Add a new post

        // POST api/<ForumController>
        [HttpPost]
        public string Post([FromBody] Post newPost)
        {
            try
            {
                if (ModelState.IsValid) //validation that the new post meets the data annotations we specified
                {
                   // declaring the ID value of the new post
                    int id;
                    if (postList.Count == 0) // if there is not posts in the list, it will get assigned the ID = 0
                    {
                        id = 0;
                    }
                    else
                    {
                        id = postList[postList.Count - 1].ID + 1; //otherwise it will be the next available
                    }

                    // if newEntry was not using an anonymous class and was trying to call the contructor of the Post.cs... the POST request could not even be called... is it that the POST cannt access the constructor of another class?
                    Post newEntry = new Post() { ID = id, Subject = newPost.Subject, Message = newPost.Message, TimeStamp = DateTime.Now }; 
                    postList.Add(newEntry);
                    return "Post was added!";

                }
                else
                {
                    return "Post was not added :(";
                }

            }
            catch (Exception e) 
            {
                return e.Message.ToString();
            }

        }


        //Update the subject of a specified post eg. post with id 3

        // PUT api/<ForumController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] Post newSubject)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //check if the ID is the same with the post we are about to update
                    if(id == newSubject.ID)
                    {
                        //find the existing post in the post list
                        var findPost = postList.SingleOrDefault(p=>p.ID == id);
                        if(findPost == null)
                        {
                            return "Post not found :(";
                        }
                        else
                        {
                            findPost.Subject = newSubject.Subject;
                            return "Subject was updated";
                        }

                    }
                    else
                    {
                        return "Invalid ID";
                    }


                }
                else
                {
                    return "Post subject could not get updated! " + Response.StatusCode;

                }
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
           

        }



        //Delete a specified post

        // DELETE api/<ForumController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            try
            {
                //First we need to find the post using its ID value
                var findPost = postList.SingleOrDefault(p => p.ID == id);
                if(findPost == null)
                {
                    return "Post with id: " + id + " could not be delete :(";
                }
                else
                {
                    postList.Remove(findPost);
                    return "Post is now deleted!";
                }

            }
            catch(Exception e)
            {
                return e.Message.ToString();
            }
           

        }
    }
}
