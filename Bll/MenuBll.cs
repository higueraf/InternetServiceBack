using InternetServiceBack.Dtos;
using InternetServiceBack.Dtos.Common;
using InternetServiceBack.Dtos.Menu;
using InternetServiceBack.Helpers;
using InternetServiceBack.Models;
using InternetServiceBack.Repository;

namespace InternetServiceBack.Bll
{
    public class MenuBll
    {
        private readonly DatabaseInternetServiceContext _context;
        private readonly MenuRepository _menuRepository;

        public MenuBll(DatabaseInternetServiceContext context)
        {
            _context = context;
            _menuRepository = new MenuRepository();
        }

        public GenericResponseDto<List<MenuDto>> GetMenus()
        {
            try
            {
                var menus = _menuRepository.GetMenus(_context);
                return new GenericResponseDto<List<MenuDto>>
                {
                    statusCode = 200,
                    data = menus,
                    message = "",
                };
            }
            catch (Exception ex)
            {
                return new GenericResponseDto<List<MenuDto>>
                {
                    statusCode = 500,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<MenuDto> GetMenuById(Guid menuId)
        {
            try
            {
                var menu = _menuRepository.GetMenuById(_context, menuId);
                if (menu != null)
                {
                    return new GenericResponseDto<MenuDto>
                    {
                        statusCode = 200,
                        data = menu,
                        message = "Menu found",
                    };
                }
                else
                {
                    return new GenericResponseDto<MenuDto>
                    {
                        statusCode = 404,
                        message = "Menu not found",
                    };
                }
            }
            catch (Exception ex)
            {
                return new GenericResponseDto<MenuDto>
                {
                    statusCode = 500,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<MenuDto> CreateMenu(MenuDto menuDto)
        {
            _context.Database.BeginTransaction();
            try
            {
                Menu existingMenu = _menuRepository.FindMenuByMenuName(_context, menuDto);
                if (existingMenu != null)
                {
                    return new GenericResponseDto<MenuDto>
                    {
                        statusCode = 500,
                        data = null,
                        message = MessageHelper.RegisterMenuErrorExisteMenu,
                    };
                }
                var menuDtoSaved = _menuRepository.CreateMenu(_context, menuDto);
                _context.Database.CommitTransaction();
                return new GenericResponseDto<MenuDto>
                {
                    statusCode = 200,
                    data = menuDtoSaved,
                    message = "",
                };
            }
            catch (Exception)
            {
                _context.Database.RollbackTransaction();
                return new GenericResponseDto<MenuDto>
                {
                    statusCode = 500,
                    data = null,
                    message = MessageHelper.RegisterMenuErrorEx,
                };
            }
        }
        public GenericResponseDto<MenuDto> UpdateMenu(MenuDto menuDto)
        {
            _context.Database.BeginTransaction();
            try
            {
                menuDto = _menuRepository.UpdateMenu(_context, menuDto);
                _context.Database.CommitTransaction();
                if (menuDto==null)
                {
                    return new GenericResponseDto<MenuDto>
                    {
                        statusCode = 404,
                        data = null,
                        message = "Menu not found",
                    };
                }

                return new GenericResponseDto<MenuDto>
                {
                    statusCode = 200,
                    data = menuDto,
                    message = "Menu updated successfully",
                };
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                return new GenericResponseDto<MenuDto>
                {
                    statusCode = 500,
                    data = null,
                    message = ex.Message,
                };
            }
        }

        public GenericResponseDto<bool> DeleteMenu(Guid menuId)
        {
            try
            {
                var success = _menuRepository.DeleteMenu(_context, menuId);
                if (!success)
                {
                    return new GenericResponseDto<bool>
                    {
                        statusCode = 404,
                        data = false,
                        message = "Menu not found",
                    };
                }

                return new GenericResponseDto<bool>
                {
                    statusCode = 200,
                    data = true,
                    message = "Menu deleted successfully",
                };
            }
            catch (Exception ex)
            {
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
