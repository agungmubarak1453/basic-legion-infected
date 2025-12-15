using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using BasicLegionInfected.Core;

namespace BasicLegionInfected.View
{
    public class MenuManager : ASingleton<MenuManager>
    {
        private List<Menu> _menus = new();

        public void ShowMenu(string menuCode)
        {
            Menu menu = GetMenu(menuCode);
            menu.Show();
        }

        public void HideMenu(string menuCode)
        {
            Menu menu = GetMenu(menuCode);
            menu.Hide();
        }

        public Menu GetMenu(string menuCode)
        {
            return _menus.FirstOrDefault(menu => menu.Code.Equals(menuCode));
        }

        public void AddMenu(Menu menu)
        {
            _menus.Add(menu);
        }

        public void RemoveMenu(Menu menu)
        {
            _menus.Remove(menu);
        }
    }
}
