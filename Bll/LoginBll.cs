using InternetServiceBack.Helpers;
using InternetServiceBack.Repository;
using InternetServiceBack.Dtos.LoginProcess;
using InternetServiceBack.Dtos.Common;
using InternetServiceBack.Models;

namespace InternetServiceBack.Bll
{
    public class LoginBll
    {
        private readonly DatabaseInternetServiceContext _context;
        private readonly UserRepository _userRepository;
        public LoginBll(DatabaseInternetServiceContext context)
        {
            _context = context;
            _userRepository = new UserRepository();
        }
        public GenericResponseDto<LoginResponseDto> GetLoginUSer(LoginRequestDto loginRequestDto)
        {
            try
            {
                User user = _userRepository.FindUserByUserName(_context, loginRequestDto.UserName);

                if (user != null)
                {
                    string passFindDecrypt = (new MethodsEncryptHelper()).DencryptPassword(user.Password);
                    if (passFindDecrypt == loginRequestDto.Password)
                    {

                        return new GenericResponseDto<LoginResponseDto>
                        {
                            statusCode = 200,
                            data = new LoginResponseDto
                            {
                                UserName = user.UserName,
                                Token = (new MethodsHelper()).CreateTokenSesion(user.UserID),
                            },
                        };
                    }
                    else
                    {
                        return new GenericResponseDto<LoginResponseDto>
                        {
                            statusCode = 500,
                            message = MessageHelper.LoginErrorNotActived,
                        };
                    }
                }
                else
                {
                    return new GenericResponseDto<LoginResponseDto>
                    {
                        statusCode = 500,
                        message = MessageHelper.LoginErrorPassword,
                    };
                }
            
            }
            catch (Exception ex)
            {
                return new GenericResponseDto<LoginResponseDto>
                {
                    statusCode = 500,
                    message = "Error.",
                };
            }
        }
    }
}


