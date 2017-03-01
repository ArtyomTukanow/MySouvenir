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
            NetManager.LoadText(NetManager.MainUrl + "get-products.php", new URLVariables(tags), DecodeJsonProducts);
        }

        private void DecodeJsonProducts(string jsonData)
        {
            if (!String.IsNullOrEmpty(jsonData))
            {
                productsList = JsonUtility.FromJson<Products>(jsonData);
                ProductsLayer.instance.itemsScrollPanel.Items = productsList.products;
                ProductsLayer.instance.itemsScrollPanel.LoadItems(0); //загружаем первую страницу
            }
        }



        private Product product;

        public void ProductLoad(int id)
        {
            URLVariables variables = new URLVariables();
            variables.Names = new[] {"id"};
            variables.Params = new[] {id.ToString()};
            NetManager.LoadText(NetManager.MainUrl + "get-product-by-id.php", variables, DecodeJsonProduct);
        }

        private void DecodeJsonProduct(string jsonData)
        {
            if (!String.IsNullOrEmpty(jsonData))
            {
                product = JsonUtility.FromJson<Product>(jsonData);
                if (product.product_id >= 0)
                {
//                    ProductsLayer.instance.itemsScrollPanel.Items = productsList.products;
//                    ProductsLayer.instance.itemsScrollPanel.LoadItems(0); //загружаем первую страницу
                }
            }
        }
    }
}