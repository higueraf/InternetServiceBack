using InternetServiceBack.Dtos.Common;
using InternetServiceBack.Dtos.User;
using InternetServiceBack.Helpers;
using InternetServiceBack.Models;
using InternetServiceBack.Repository;

namespace InternetServiceBack.Bll
{
    public class UserBll
    {
        private readonly DatabaseInternetServiceContext _context;
        private readonly UserRepository _userRepository;

        public UserBll(DatabaseInternetServiceContext context)
        {
            _context = context;
            _userRepository = new UserRepository();
        }

        public GenericResponseDto<List<UserDto>> GetUsers()
        {
            try
            {
                var users = _userRepository.GetUsers(_context);
                return new GenericResponseDto<List<UserDto>>
                {
                    statusCode = 200,
                    data = users,
                    message = "",
                };
            }
            catch (Exception ex)
            {
                return new GenericResponseDto<List<UserDto>>
                {
                    statusCode = 500,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<UserDto> GetUserById(Guid userId)
        {
            try
            {
                var user = _userRepository.GetUserById(_context, userId);
                if (user != null)
                {
                    return new GenericResponseDto<UserDto>
                    {
                        statusCode = 200,
                        data = user,
                        message = "User found",
                    };
                }
                else
                {
                    return new GenericResponseDto<UserDto>
                    {
                        statusCode = 404,
                        message = "User not found",
                    };
                }
            }
            catch (Exception ex)
            {
                return new GenericResponseDto<UserDto>
                {
                    statusCode = 500,
                    message = ex.Message,
                };
            }
        }


    public GenericResponseDto<UserDto> CreateUser(UserDto userDto)
        {
            _context.Database.BeginTransaction();
            try
            {
                User existingUser = _userRepository.FindUserByUserName(_context, userDto.userName);
                if (existingUser != null)
                {
                    return new GenericResponseDto<UserDto>
                    {
                        statusCode = 500,
                        data = null,
                        message = MessageHelper.RegisterUserErrorExisteUser,
                    };
                }
                userDto.password = (new MethodsEncryptHelper()).EncryptPassword(userDto.password);
                var userDtoSaved = _userRepository.CreateUser(_context, userDto);
                _context.Database.CommitTransaction();
                return new GenericResponseDto<UserDto>
                {
                    statusCode = 200,
                    data = userDtoSaved,
                    message = "",
                };
            }
            catch (Exception)
            {
                _context.Database.RollbackTransaction();
                return new GenericResponseDto<UserDto>
                {
                    statusCode = 500,
                    data = null,
                    message = MessageHelper.RegisterUserErrorEx,
                };
            }
        }

        public GenericResponseDto<UserDto> UpdateUser(UserDto userDto)
        {
            _context.Database.BeginTransaction();
            try
            {
                UserDto userUpdatedDto = _userRepository.UpdateUser(_context, userDto);
                _context.Database.CommitTransaction();
                if (userUpdatedDto == null)
                {
                    return new GenericResponseDto<UserDto>
                    {
                        statusCode = 404,
                        data = null,
                        message = "User not found",
                    };
                }

                return new GenericResponseDto<UserDto>
                {
                    statusCode = 200,
                    data = userUpdatedDto,
                    message = "User updated successfully",
                };
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new GenericResponseDto<UserDto>
                {
                    statusCode = 500,
                    data = null,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<bool> DeleteUser(Guid userId)
        {
            _context.Database.BeginTransaction();
            try
            {
                var success = _userRepository.DeleteUser(_context, userId);
                if (!success)
                {
                    return new GenericResponseDto<bool>
                    {
                        statusCode = 404,
                        data = false,
                        message = "User not found",
                    };
                }
                _context.Database.CommitTransaction();
                return new GenericResponseDto<bool>
                {
                    statusCode = 200,
                    data = true,
                    message = "User deleted successfully",
                };
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new GenericResponseDto<bool>
                {
                    statusCode = 500,
                    data = false,
                    message = ex.Message,
                };
            }
        }


    }
}
