using Items.Interface;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Items
{
    public class ItemBase : IItemBase
    {
        public NetConnection ImageLoadConnection { set; get; }

        public GameObject ContainerGameObject { set; get; }

        public virtual void Create(GameObject container, Transform parent)
        {
            //Удаляем, если в продукте уже есть GameObject
            if(ContainerGameObject != null)
                Loader.Destroy(ContainerGameObject);

            ContainerGameObject = container;
            ContainerGameObject.transform.parent = parent;

            ContainerGameObject.GetComponent<Button>().onClick.AddListener(OnClick);
            ContainerGameObject.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        }

        public virtual void Remove()
        {
            if (ContainerGameObject != null)
                Loader.Destroy(ContainerGameObject);
            if (ImageLoadConnection != null)
                ImageLoadConnection.Cancel();
        }

        protected virtual void OnClick()
        {

        }

        protected virtual void OnLoadImage(Texture2D texture2D)
        {
            if (ImageLoadConnection == null)
                return;
        }
    }
}