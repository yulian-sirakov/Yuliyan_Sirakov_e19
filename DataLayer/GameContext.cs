using BussinesLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class GameContext : IDb<Game, int>
    {
        private readonly YuliyanDBContext dbContext;

        public GameContext(YuliyanDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Create(Game item)
        {
            try
            {
                dbContext.Games.Add(item);
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
                List<Game> games = dbContext.Games.ToList();
                Game item = games.FirstOrDefault(x => x.ID == key);
                dbContext.Games.Remove(item);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Game Read(int key, bool useNavigationalProperties = false)
        {
            try
            {
                if (useNavigationalProperties)
                {
                    return dbContext.Games.Include(x => x.Genre).Include(x => x.Users).FirstOrDefault(x => x.ID == key);
                }
                return dbContext.Games.FirstOrDefault(x => x.ID == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Game> ReadAll(bool useNavigationalProperties = false)
        {
            try
            {
                var games = dbContext.Games;
                if (useNavigationalProperties)
                {
                    games.Include(x => x.Genre).Include(x => x.Users);
                }
                return games.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Game item, bool useNavigationalProperties = false)
        {
            try
            {
                Game game = this.Read(item.ID, useNavigationalProperties);

                if (game == null)
                {
                    Create(item);
                }

                game.Name = item.Name;
                if (useNavigationalProperties)
                {
                    List<User> users = new List<User>();
                    foreach (User u in item.Users)
                    {
                        User userfromdb = dbContext.Users.Find(u.ID);
                        if (userfromdb != null)
                        {
                            users.Add(userfromdb);
                        }
                        else
                        {
                            users.Add(u);
                        }
                    }
                    game.Users = users;

                    Genre g = dbContext.Genres.Find(item.Genre.ID);
                    if (g != null)
                    {
                        game.Genre = g;
                    }
                    else
                    {
                        game.Genre = item.Genre;
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
