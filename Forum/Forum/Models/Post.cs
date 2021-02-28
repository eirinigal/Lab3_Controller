using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Post
    {
        //Properties of the data model

        [Key]
        public int ID { get; set; } 

        [Required(ErrorMessage ="Please provide Subject of your post!")]
        [MaxLength(25)]
        [MinLength(1)]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please provide the body of your post!")]
        [MaxLength(100)]
        [MinLength(1)]
        public string Message { get; set; }

        public DateTime TimeStamp { get; set; }

        /*
        //Constructor
        public Post( int id, string subject, string message)
        {
            ID = id;
            Subject = subject;
            Message = message;
            TimeStamp = DateTime.Now;

        }
        */

        //Override ToString
        public override string ToString()
        {
            return String.Format("Post Details \nID: {0} \nSubject: {1} \nMessage: {2} \nTime Stamp: {3}", ID, Subject, Message, TimeStamp);
        }

    }
}
