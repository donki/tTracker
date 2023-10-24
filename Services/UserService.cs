using System.IO;
using System.Text.Json;
using tTrackerWeb.Database;
using tTrackerWeb.Model;

namespace tTrackerWeb.Services
{
    public class UserService
    {
        private static object usersLock = new object();
        private SqliteDbContext dbContext;

        public UserService(SqliteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<User> GetUsers()
        {
            lock (usersLock)
            {
                return this.dbContext.Users.ToList();
            }
        }

        public User AddUser(User newUser)
        {
            lock (usersLock)
            {
                newUser.Id = dbContext.Users.Count() + 1;
                dbContext.Users.Add(newUser);
                // Guardamos los cambios en la base de datos.
                dbContext.SaveChanges();

                // El nuevo usuario ahora tendrá un ID asignado después de la inserción en la base de datos.
                return newUser;
            }
        }

        public User GetUserByUsername(string username)
        {
            return GetUsers().FirstOrDefault(u => u.Username == username);
        }


        public User GetUserById(int id)
        {
            lock (usersLock)
            {
                var user = dbContext.Users.FirstOrDefault(u => u.Id == id);

                return user;
            }
        }

        public void UpdateUser(User updatedUser)
        {
            lock (usersLock)
            {
                var existingUser = dbContext.Users.FirstOrDefault(u => u.Id == updatedUser.Id);

                if (existingUser != null)
                {
                    existingUser.Username = updatedUser.Username;
                    existingUser.Password = updatedUser.Password;

                    dbContext.SaveChanges();
                }
            }
        }


    }
}
