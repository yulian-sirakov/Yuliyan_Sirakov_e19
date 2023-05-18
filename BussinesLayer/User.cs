using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BussinesLayer
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        [MaxLength(20)]
        [Required]
        public string Lastname { get; set; }
        [Range(10, 80)]
        public int Age { get; set; }
        [MaxLength(20)]
        [Required]
        public string Username { get; set; }
        [MaxLength(70)]
        [Required]
        public string Pass { get; set; }
        [MaxLength(20)]
        [Required]
        public string Email { get; set; }
        public List<User> Friends { get; set; }
        public List<Game> Games { get; set; }
        public Genre Favourite_Genre { get; set; }
        public User(string name, string lastname, int age, string username, string pass, string email)
        {
            Name = name;
            Lastname = lastname;
            Age = age;
            Username = username;
            Pass = pass;
            Email = email;
            Friends = new List<User>();
            Games = new List<Game>();
        }

        private User()
        {
            Friends = new List<User>();
            Games = new List<Game>();
        }
    }
}
