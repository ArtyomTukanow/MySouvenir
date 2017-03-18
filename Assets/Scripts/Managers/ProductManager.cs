using System;
using Assets.Scripts.Layers;
using Assets.Scripts.Layers.Enum;
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
        private Action<Products> _currentCallback;


        public void ProductsLoad(Action<Products> callback, string tagsString)
        {
            //Теги в одну строку. Необходимо распарсить строку, убрать ' ' и ','
            tagsString = tagsString.Trim();
            tagsString = tagsString.Replace(",", "");
            Log.d(tagsString);
            string[] tags = tagsString.Split(' ');
            ProductsLoad(callback, new URLVariables(tags));
        }

        public void ProductsLoad(Action<Products> callback, params string[] tags)
        {
            ProductsLoad(callback, new URLVariables(tags));
        }

        public void ProductsLoad(Action<Products> callback, URLVariables tags)
        {
            _currentCallback = callback;
            NetManager.LoadText(NetManager.MainUrl + "get-products.php", tags, OnLoadProductsText);
        }

        private void OnLoadProductsText(string jsonData)
        {
            if (!String.IsNullOrEmpty(jsonData))
            {
                productsList = JsonUtility.FromJson<Products>(jsonData);
                _currentCallback(productsList);
            }
        }



        private ProductFull product;

        public void ProductLoad(int id)
        {
            URLVariables variables = new URLVariables();
            variables.Add("id", id.ToString());
            NetManager.LoadText(NetManager.MainUrl + "get-product-by-id.php", variables, OnLoadProductText);
        }

        private void OnLoadProductText(string jsonData)
        {
            if (!String.IsNullOrEmpty(jsonData))
            {
                product = JsonUtility.FromJson<ProductFull>(jsonData);
                if (product.product_id >= 0)
                {
                    ProductLayer.instance.itemScrollPanel.Items = new []{product};
                    ProductLayer.instance.OnReload();
                }
            }
        }
    }
}