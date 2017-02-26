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
        private NetConnection LoadProductsConnection;


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
            if (LoadProductsConnection != null)
            {
                LoadProductsConnection.Cancel();
                LoadProductsConnection = null;
            }
            LoadProductsConnection = NetManager.LoadText(NetManager.MainUrl + "get-products.php", new URLVariables(tags), DecodeJsonProducts);
        }

        private void DecodeJsonProducts(string jsonData)
        {
            LoadProductsConnection = null;
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



        private Product product;
        private NetConnection LoadProductConnection;

        public void ProductLoad(int id)
        {
            if (LoadProductConnection != null)
            {
                LoadProductConnection.Cancel();
                LoadProductConnection = null;
            }
            URLVariables variables = new URLVariables();
            variables.Names = new[] {"id"};
            variables.Params = new[] {id.ToString()};
            LoadProductConnection = NetManager.LoadText(NetManager.MainUrl + "get-product-by-id.php", variables, DecodeJsonProduct);
        }

        private void DecodeJsonProduct(string jsonData)
        {
            LoadProductConnection = null;
            if (!String.IsNullOrEmpty(jsonData))
            {
                product = JsonUtility.FromJson<Product>(jsonData);
                if (product.product_id >= 0)
                {
//                    ItemsLayer.instance.ScrollPanel.Items = productsList.products;
//                    ItemsLayer.instance.ScrollPanel.LoadItems(1); //загружаем первую страницу
                }
            }
        }
    }
}