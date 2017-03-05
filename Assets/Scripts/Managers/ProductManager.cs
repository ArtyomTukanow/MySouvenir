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
            ApplicationManager.uiManager.Layer = LayerNamesEnum.items;
            NetManager.LoadText(NetManager.MainUrl + "get-products.php", new URLVariables(tags), OnLoadProductsText);
        }

        private void OnLoadProductsText(string jsonData)
        {
            if (!String.IsNullOrEmpty(jsonData))
            {
                productsList = JsonUtility.FromJson<Products>(jsonData);
                ProductsLayer.instance.itemsScrollPanel.Items = productsList.products;
                ProductsLayer.instance.OnReload();
            }
        }



        private ProductFull product;

        public void ProductLoad(int id)
        {
            ApplicationManager.uiManager.Layer = LayerNamesEnum.item;
            URLVariables variables = new URLVariables();
            variables.Names = new[] {"id"};
            variables.Params = new[] {id.ToString()};
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