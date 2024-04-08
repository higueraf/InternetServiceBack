using InternetServiceBack.Models;
using InternetServiceBack.Dtos.User;

namespace InternetServiceBack.Repository
{
    public class UserRepository
    {
        public List<UserDto> GetUsers(DatabaseInternetServiceContext contextDB)
        {
            List<UserDto> userDtos = new List<UserDto>();
            List<User> users = null;
            try
            {
                users = contextDB.User.ToList();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            foreach (var user in users)
            {
                userDtos.Add(new UserDto
                {
                    userID = user.UserID,
                    userName = user.UserName,
                    email = user.Email,
                    userApproval= user.UserApproval,
                    approvalDate = user.ApprovalDate,
                    creationDate = user.CreationDate

,
                });
            }
            return userDtos;
        }

        public UserDto CreateUser(DatabaseInternetServiceContext contextDB, UserDto userDto)
        {
            if (userDto == null) return null;

            User user = new User();
            user.UserName = userDto.userName;
            user.Password = userDto.password;            
            user.Email = userDto.email;
            user.CreationDate = DateTime.Now;
            user.UserID = Guid.NewGuid();
            try
            {
                contextDB.User.Add(user);
                contextDB.SaveChanges();
                UserDto createdUserDto = GetUserById(contextDB, user.UserID);
                return createdUserDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating User: {ex.Message}");
                return null;
            }
        }

        public UserDto? UpdateUser(DatabaseInternetServiceContext contextDB, UserDto userDto)
        {
            var user = contextDB.User.FirstOrDefault(x => x.UserID == userDto.userID);
            if (user == null)
                return null;

            user.UserName = userDto.userName;
            user.Email = userDto.email;

            try
            {
                contextDB.SaveChanges();
                return userDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating User: {ex.Message}");
                return null;
            }
        }

        public bool DeleteUser(DatabaseInternetServiceContext contextDB, Guid userId)
        {
            var user = contextDB.User.FirstOrDefault(x => x.UserID == userId);
            if (user == null)
                return false;

            try
            {
                contextDB.User.Remove(user);
                contextDB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting User: {ex.Message}");
                return false;
            }
        }

        public User? FindUserByUserName(DatabaseInternetServiceContext contextDB, string userName)
        {
            var userFind = contextDB.User.SingleOrDefault(x => x.UserName == userName);
            return userFind;
        }


        public UserDto GetUserById(DatabaseInternetServiceContext contextDB, Guid userID)
        {
            User user = contextDB.User.SingleOrDefault(x => x.UserID == userID);
            UserDto userDto = new UserDto();
            if (user != null)
            {
                userDto.userID = user.UserID;
                userDto.userName = user.UserName;
                userDto.email = user.Email;
            }
            return userDto;
        }


        public void ChangePassword(DatabaseInternetServiceContext contextDB, Guid userId, string password)
        {
            User user= contextDB.User.Single(x => x.UserID == userId);
            user.Password = password;
            contextDB.SaveChanges();
        }
    }
}
