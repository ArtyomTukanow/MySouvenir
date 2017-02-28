using Items.Interface;
using UnityEngine;

namespace Panels
{
    public class PanelBase <T> : IPanel<T>  where T : IItemBase
    {
        public GameObject ItemsContainer { set; get; }

        private T[] _items;
        public T[] Items
        {
            set
            {
                destroyOldItems();
                _items = value;
            }
            get { return _items; }
        }

        protected void destroyOldItems()
        {
            if(Items == null || Items.Length == 0)
                return;

            foreach (T item in Items)
                item.Remove();
        }

        public virtual void InitPanel(GameObject panel, GameObject parent = null)
        {
            ItemsContainer = panel;
            if(parent != null)
                ItemsContainer.transform.parent = parent.transform;
        }

        public virtual void ReloadPanel()
        {

        }

        public virtual void LoadItems()
        {
            int i = 0;
            foreach (T item in Items)
            {
                item.Create(Loader.Instantiate(Resources.Load<GameObject>("productItem")), ItemsContainer.transform);

                Vector2 sizeContainer = Items[i].ContainerGameObject.GetComponent<RectTransform>().sizeDelta;
                ItemsContainer.GetComponent<RectTransform>().sizeDelta += new Vector2(0, sizeContainer.y);
                item.ContainerGameObject.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(0, -sizeContainer.x / 2 - i * sizeContainer.y);
                i++;
            }
        }
    }
}