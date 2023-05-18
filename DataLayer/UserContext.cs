using BussinesLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{

    public class UserContext : IDb<User, int>
    {
        private readonly YuliyanDBContext dbContext;

        public UserContext(YuliyanDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Create(User item)
        {
            try
            {

                dbContext.Users.Add(item);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int key)
        {
            try
            {
                List<User> users = dbContext.Users.ToList();
                User item = users.FirstOrDefault(x => x.ID == key);
                dbContext.Users.Remove(item);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User Read(int key, bool useNavigationalProperties = false)
        {
            try
            {
                if (useNavigationalProperties)
                {
                    return dbContext.Users.Include(x => x.Friends).Include(x => x.Games).Include(x => x.Favourite_Genre).FirstOrDefault(x => x.ID == key);
                }
                return dbContext.Users.FirstOrDefault(x => x.ID == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<User> ReadAll(bool useNavigationalProperties = false)
        {
            try
            {
                var users = dbContext.Users;
                if (useNavigationalProperties)
                {
                    users.Include(x => x.Friends).Include(x => x.Games).Include(x => x.Favourite_Genre);
                }
                return users.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(User item, bool useNavigationalProperties = false)
        {
            try
            {
                User user = this.Read(item.ID, useNavigationalProperties);

                if (user == null)
                {
                    Create(item);
                }
                user = this.Read(item.ID, useNavigationalProperties);


                user.Name = item.Name;
                user.Lastname = item.Lastname;
                user.Age = item.Age;
                user.Username = item.Username;
                user.Pass = item.Pass;
                user.Email = item.Email;
                if (useNavigationalProperties)
                {
                    List<User> friends = new List<User>();
                    foreach (User u in item.Friends)
                    {
                        User userfromdb = dbContext.Users.Find(u.ID);
                        if (userfromdb != null)
                        {
                            friends.Add(userfromdb);
                        }
                        else
                        {
                            friends.Add(u);
                        }
                    }
                    user.Friends = friends;


                    List<Game> games = new List<Game>();
                    foreach (Game game in item.Games)
                    {
                        Game gamefromdb = dbContext.Games.Find(game.ID);
                        if (gamefromdb != null)
                        {
                            games.Add(gamefromdb);
                        }
                        else
                        {
                            games.Add(game);
                        }
                    }
                    user.Games = games;

                    Genre g = dbContext.Genres.Find(item.Favourite_Genre.ID);
                    if (g != null)
                    {
                        user.Favourite_Genre = g;
                    }
                    else
                    {
                        user.Favourite_Genre = item.Favourite_Genre;
                    }
                }
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
