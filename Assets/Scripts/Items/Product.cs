using System;
using Items.Interface;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Items
{
    [Serializable]
    public struct Products
    {
        public Product[] products;
    }

    [Serializable]
    public class Product : ItemBase, IItemBase
    {
        public int product_id;
        public string product_name;
        public string product_description;
        public string product_img;
        public string product_url;
        public string price;

        public Text ProductNameGameObject;
        public Text ProductDescriptionGameObject;
        public Image ProductImgGameObject;
        public Text PriceGameObject;

        public void Create(GameObject container, Transform parent)
        {
            base.Create(container, parent);

            ProductImgGameObject = ContainerGameObject.transform.FindChild("product_image").GetComponent<Image>();
            ProductNameGameObject = ContainerGameObject.transform.FindChild("product_name").GetComponent<Text>();
            ProductDescriptionGameObject = ContainerGameObject.transform.FindChild("product_description").GetComponent<Text>();
            PriceGameObject = ContainerGameObject.transform.FindChild("product_price").GetComponent<Text>();

            //загружаем картинку
            ImageLoadConnection = NetManager.LoadImage(product_img, null, OnLoadImage);
            //Добавляем описание и проч.
            ProductNameGameObject.text = product_name;
            ProductDescriptionGameObject.text = product_description;
            PriceGameObject.text = price + "руб";
        }

        protected override void OnLoadImage(Texture2D texture2D)
        {
            base.OnLoadImage(texture2D);
            ProductImgGameObject.sprite = Sprite.Create(texture2D, new Rect(0,0,texture2D.width,texture2D.height), new Vector2(0.5f,0.5f));
        }

        protected override void OnClick()
        {
            base.OnClick();
            Application.OpenURL(product_url);
        }
    }
}