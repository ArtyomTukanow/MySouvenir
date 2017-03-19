using System;
using Assets.Scripts.Layers;
using Assets.Scripts.Layers.Enum;
using Items.Interface;
using Managers;
using Net;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    [Serializable]
    public class Shop : ItemBase, IItemBase
    {
        //////////
        // MODEL
        ////////

        public string shop_img;
        public string shop_description;
        public string shop_name;
        public string shop_id;

        //////////
        // VIEW
        ////////

        public Text ShopNameGameObject;
        public Image ShopImgGameObject;
        public Text ShopDescriptionGameObject;

        public void Create(GameObject container, Transform parent)
        {
            base.Create(container, parent);

            ShopNameGameObject = ContainerGameObject.transform.FindChild("shop_name").GetComponent<Text>();
            ShopNameGameObject.text = shop_name;
            ShopImgGameObject = ContainerGameObject.transform.FindChild("shop_image").GetComponent<Image>();

            ShopDescriptionGameObject = ContainerGameObject.transform.FindChild("shop_description").GetComponent<Text>();
            if (!String.IsNullOrEmpty(shop_description))
            {
                ShopDescriptionGameObject.text = shop_description;
            }

            ImageLoadConnection = NetManager.LoadImage(shop_img, null, OnLoadImage);
        }

        protected override void OnLoadImage(Texture2D texture2D)
        {
            base.OnLoadImage(texture2D);
            ShopImgGameObject.sprite = Sprite.Create(texture2D, new Rect(0,0,texture2D.width,texture2D.height), new Vector2(0.5f,0.5f));
        }

        protected override void OnClick()
        {
            base.OnClick();
            ApplicationManager.UiManager.Layer = LayerNamesEnum.shop;
            ApplicationManager.ShopManager.ShopLoad(ShopLayer.instance.OnShopLoad, shop_id);
        }
    }
}