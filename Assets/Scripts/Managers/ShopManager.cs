using System;
using Items;
using Net;
using UnityEngine;
using Utils;

namespace Managers
{
    public class ShopManager
    {
        public static ShopManager instance = new ShopManager();
        private ShopManager()
        {
        }

        private Action<Shops> _currentCallback;

        public void ShopsLoad(Action<Shops> callback)
        {
            _currentCallback = callback;
            NetManager.LoadText(NetManager.MainUrl + "get-shops.php", null, onLoadShopsText);
            LoadingUtil.Show();
        }

        private void onLoadShopsText(string jsonData)
        {
            _currentCallback(JsonUtility.FromJson<Shops>(jsonData));
        }


        private Action<ShopFull> _currentShopFullCallback;

        public void ShopLoad(Action<ShopFull> callback, string id)
        {
            _currentShopFullCallback = callback;
            URLVariables variables = new URLVariables();
            variables.Add("id", id);
            NetManager.LoadText(NetManager.MainUrl + "get-shop-by-id.php", variables, onLoadShopText);
            LoadingUtil.Show();
        }

        private void onLoadShopText(string jsonData)
        {
            _currentShopFullCallback(JsonUtility.FromJson<ShopFull>(jsonData));
        }
    }
}