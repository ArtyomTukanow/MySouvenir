using System;
using Assets.Scripts.Layers;
using Net;
using Items;
using UnityEngine;
using Utils;

namespace Managers
{
    public class ProductManager
    {
        public static ProductManager instance = new ProductManager();
        private ProductManager()
        {
        }

        private Products productsList;
        private NetConnection LoadTextConnection;


        public void ProductsLoad(string tagsString)
        {
            //Теги в одну строку. Необходимо распарсить строку, убрать ' ' и ','
            tagsString = tagsString.Trim();
            tagsString = tagsString.Replace(",", "");
            Log.d(tagsString);
            string[] tags = tagsString.Split(' ');
            ProductsLoad(tags);
        }

        public void ProductsLoad(params string[] tags)
        {
            if (LoadTextConnection != null)
            {
                LoadTextConnection.Cancel();
                LoadTextConnection = null;
            }
            LoadTextConnection = NetManager.LoadText(NetManager.MainUrl + "get-products.php", new URLVariables(tags), decodeJSON);
        }

        private void decodeJSON(string jsonData)
        {
            LoadTextConnection = null;
            if (!String.IsNullOrEmpty(jsonData))
            {
                productsList = JsonUtility.FromJson<Products>(jsonData);
                if (productsList.products.Length > 0)
                {
                    ItemsLayer.instance.ScrollPanel.Items = productsList.products;
                    ItemsLayer.instance.ScrollPanel.LoadItems(1); //загружаем первую страницу
                }
            }
        }
    }
}