using System;
using Items.Interface;
using Managers;
using Net;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    [Serializable]
    public class ProductFull : ItemBase, IItemBase
    {
        //////////
        // MODEL
        ////////

        public int product_id;
        public string product_name;
        public string product_description;
        public string product_img;
        public string product_url;
        public string price;
        public string shop_id;
        public string shop_name;
        public string shop_img;
        public string shop_adress;
        public string shop_url;


        //////////
        // VIEW
        ////////

        public Text ProductName;
        public Text ProductDescription;
        public Image ProductImage;
        public Button ProductImageButton;
        public Text PriceGameObject;
        public Button GoBackButton;

        public Button ShopInfoButton;
        public Button ShopUrlButton;
        public Button ItemUrlButton;
        public Image ShopImage;
        public Button ShopImageButton;
        public Text ShopAdress;

        public void Create(GameObject container, Transform parent)
        {
            base.Create(container, parent);

            ProductImage = ContainerGameObject.transform.FindChild("product_image").GetComponent<Image>();
            ProductImage.preserveAspect = true;
            ProductImage.type = Image.Type.Simple;
            ProductName = ProductImage.transform.FindChild("product_name").GetComponent<Text>();
            ProductDescription = ContainerGameObject.transform.FindChild("product_description").GetComponent<Text>();
            PriceGameObject = ProductImage.transform.FindChild("product_price").GetComponent<Text>();
            GoBackButton = ContainerGameObject.transform.FindChild("goBackButton").GetComponent<Button>();

            ShopInfoButton = ContainerGameObject.transform.FindChild("shop_info_button").GetComponent<Button>();
            ShopUrlButton = ContainerGameObject.transform.FindChild("shop_url_button").GetComponent<Button>();
            ItemUrlButton = ContainerGameObject.transform.FindChild("item_url_button").GetComponent<Button>();
            ShopImage = ProductImage.transform.FindChild("shop_image").GetComponent<Image>();
            ShopAdress = ContainerGameObject.transform.FindChild("shop_adress").GetComponent<Text>();

            ProductImageButton = ContainerGameObject.transform.FindChild("product_image").GetComponent<Button>();
            ShopImageButton = ProductImage.transform.FindChild("shop_image").GetComponent<Button>();

            GoBackButton.onClick.AddListener(ApplicationManager.UiManager.GoBackToLastLayer);
            ProductImageButton.onClick.AddListener(delegate
            {
                Application.OpenURL(product_url);
            });
            ItemUrlButton.onClick.AddListener(delegate
            {
                Application.OpenURL(product_url);
            });
            ShopImageButton.onClick.AddListener(delegate
            {
                Application.OpenURL(shop_url);
            });
            ShopInfoButton.onClick.AddListener(delegate
            {
                //fixme HERE MUST ME OPEN SHOP PAGE
            });
            ShopUrlButton.onClick.AddListener(delegate
            {
                Application.OpenURL(shop_url);
            });


            //загружаем картинку
            ImageLoadConnection = NetManager.LoadImage(product_img, null, OnLoadImage);
            //Добавляем описание и проч.
            ProductName.text = product_name;
            PriceGameObject.text = price + "руб";
            if (!String.IsNullOrEmpty(product_description.Trim()))
                ProductDescription.text = product_description;
            if (!String.IsNullOrEmpty(shop_adress.Trim()))
                ShopAdress.text = shop_adress;
            ShopInfoButton.GetComponentInChildren<Text>().text = "Подробнее о \"" + shop_name + "\"";
            ShopUrlButton.GetComponentInChildren<Text>().text = "Перейти на сайт \"" + shop_name + "\"";
        }

        protected override void OnLoadImage(Texture2D texture2D)
        {
            base.OnLoadImage(texture2D);
            ProductImage.sprite = Sprite.Create(texture2D, new Rect(0,0,texture2D.width,texture2D.height), new Vector2(0.5f,0.5f));
            ImageLoadConnection = NetManager.LoadImage(shop_img, null, OnLoadShopImg);
        }

        private void OnLoadShopImg(Texture2D texture2D)
        {
            base.OnLoadImage(texture2D);
            ShopImage.sprite = Sprite.Create(texture2D, new Rect(0,0,texture2D.width,texture2D.height), new Vector2(0.5f,0.5f));
        }
    }
}