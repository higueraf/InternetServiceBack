using InternetServiceBack.Models;
using InternetServiceBack.Dtos.Menu;

namespace InternetServiceBack.Repository
{
    public class MenuRepository
    {
        public List<MenuDto> GetMenus(DatabaseInternetServiceContext contextDB)
        {
            List<MenuDto> menuDtos = new List<MenuDto>();
            List<Menu> menus = null;
            try
            {
                menus = contextDB.Menu.ToList();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            foreach (var menu in menus)
            {
                menuDtos.Add(new MenuDto
                {
                    MenuID = menu.MenuID,
                    Name = menu.Name,
                    Url = menu.Url,
                    Icon = menu.Icon,
                });
            }
            return menuDtos;
        }

        public MenuDto CreateMenu(DatabaseInternetServiceContext contextDB, MenuDto menuDto)
        {
            if (menuDto == null) return null;

            Menu menu = new Menu();
            menu.Name = menuDto.Name;
            menu.Url = menuDto.Url;
            menu.Icon = menuDto.Icon;
            try
            {
                contextDB.Menu.Add(menu);
                contextDB.SaveChanges();
                MenuDto createdMenuDto = GetMenuById(contextDB, menu.MenuID);
                return createdMenuDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Menu: {ex.Message}");
                return null;
            }
        }

        public MenuDto UpdateMenu(DatabaseInternetServiceContext contextDB, MenuDto menuDto)
        {
            var menu = contextDB.Menu.FirstOrDefault(x => x.MenuID == menuDto.MenuID);
            if (menu == null)
                return null;

            menu.Name = menuDto.Name;
            menu.Url = menuDto.Url;
            menu.Icon = menuDto.Icon;
            try
            {
                contextDB.SaveChanges();
                return menuDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Menu: {ex.Message}");
                return null;
            }
        }

        public bool DeleteMenu(DatabaseInternetServiceContext contextDB, Guid menuId)
        {
            var menu = contextDB.Menu.FirstOrDefault(x => x.MenuID == menuId);
            if (menu == null)
                return false;

            try
            {
                contextDB.Menu.Remove(menu);
                contextDB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting Menu: {ex.Message}");
                return false;
            }
        }

        public Menu? FindMenuByMenuName(DatabaseInternetServiceContext contextDB, MenuDto menuDto)
        {
            var menuFind = contextDB.Menu.SingleOrDefault(x => x.Name == menuDto.Name);
            return menuFind;
        }


        public MenuDto GetMenuById(DatabaseInternetServiceContext contextDB, Guid menuID)
        {
            Menu menu = contextDB.Menu.SingleOrDefault(x => x.MenuID == menuID);
            MenuDto menuDto = new MenuDto();
            if (menu != null)
            {
                menuDto.MenuID = menu.MenuID;
                menuDto.Name = menu.Name;
                menuDto.Url = menu.Url;
                menuDto.Icon = menu.Icon;
            }
            return menuDto;
        }
    }
}
