using System;
using Items.Interface;
using Managers;
using Net;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    [Serializable]

    public class ShopFull : ItemBase, IItemBase
    {
        public string shop_name;
        public string shop_url;
        public string shop_id;
        public string shop_adress;
        public string shop_description;
        public string shop_img;
        public string shop_map_img;
        public string shop_map_url;

        public Text ShopName;
        public Image ShopImage;
        public Text ShopDescription;
        public Text ShopAdress;
        public Button goBackButton;
        public Image MapImage;
        public Button MapButton;
        public Button ShopButton;


        public void Create(GameObject container, Transform parent)
        {
            base.Create(container, parent);

            ShopName = ContainerGameObject.transform.FindChild("shop_name").GetComponent<Text>();
            ShopName.text = shop_name;
            ShopImage = ContainerGameObject.transform.FindChild("shop_image").GetComponent<Image>();
            ShopDescription = ContainerGameObject.transform.FindChild("shop_description").GetComponent<Text>();
            ShopDescription.text = shop_description.Replace("<br />", "\n");;
            ShopAdress = ContainerGameObject.transform.FindChild("shop_adress").GetComponent<Text>();
            ShopAdress.text = shop_adress.Replace("<br />", "\n");;
            goBackButton = ContainerGameObject.transform.FindChild("goBackButton").GetComponent<Button>();
            goBackButton.onClick.AddListener(UIManager.instance.GoBackToLastLayer);
            MapImage = ContainerGameObject.transform.FindChild("map_image").GetComponent<Image>();
            MapButton = ContainerGameObject.transform.FindChild("map_image").GetComponent<Button>();
            MapButton.onClick.AddListener(delegate
            {
                Application.OpenURL(shop_map_url);
            });
            ShopButton = ContainerGameObject.transform.FindChild("shop_button").GetComponent<Button>();
            ShopButton.onClick.AddListener(delegate
            {
                Application.OpenURL(shop_url);
            });

            ImageLoadConnection = NetManager.LoadImage(shop_img, null, OnLoadImage);
        }

        protected override void OnLoadImage(Texture2D texture2D)
        {
            base.OnLoadImage(texture2D);
            ShopImage.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height),
                new Vector2(0.5f, 0.5f));
            ImageLoadConnection = NetManager.LoadImage(shop_map_img, null, OnLoadMapImg);
        }

        private void OnLoadMapImg(Texture2D texture2D)
        {
            base.OnLoadImage(texture2D);
            MapImage.sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height),
                new Vector2(0.5f, 0.5f));
        }
    }
}