using System;
using Items.Interface;
using Net;
using UnityEngine;
using UnityEngine.UI;

namespace Items
{
    [Serializable]
    public struct Products
    {
        public Product[] products;
    }

    [Serializable]
    public struct Product : IItemBase
    {
        public int product_id;
        public string product_name;
        public string product_description;
        public string product_img;
        public string product_url;
        public string price;

        private Sprite LoadedImage;

        public GameObject ContainerGameObject { set; get; }
        public Text ProductNameGameObject;
        public Text ProductDescriptionGameObject;
        public Image ProductImgGameObject;
        public Text PriceGameObject;

        public NetConnection ImageLoadConnection { set; get; }

        public void Create(GameObject container, Transform parent)
        {
            //Удаляем, если в продукте уже есть GameObject
            if(ContainerGameObject != null)
                Loader.Destroy(ContainerGameObject);

            ContainerGameObject = container;
            ContainerGameObject.transform.parent = parent;
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
            ContainerGameObject.GetComponent<Button>().onClick.AddListener(OpenProductUrl);

            ContainerGameObject.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        }

        private void OnLoadImage(Texture2D texture2D)
        {
            ProductImgGameObject.sprite = Sprite.Create(texture2D, new Rect(0,0,texture2D.width,texture2D.height), new Vector2(0.5f,0.5f));
        }

        private void OpenProductUrl()
        {
            Application.OpenURL(product_url);
        }
    }
}