using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BussinesLayer
{
    public class Game
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Genre Genre { get; set; }

        public Game(string name)
        {
            Name = name;
            Users = new List<User>();
        }
        private Game()
        {
            Users = new List<User>();
        }
    }
}