using Items.Interface;
using UnityEngine;

namespace Panels
{
    public class PanelBase <T> : IPanelItems<T>  where T : IItemBase
    {
        public GameObject ItemsContainer { set; get; }

        protected string _itemsResourceName;

        private T[] _items;
        public virtual T[] Items
        {
            set
            {
                DestroyOldItems();
                _items = value;
            }
            get { return _items; }
        }

        public virtual void DestroyOldItems()
        {
            if(Items == null || Items.Length == 0)
                return;

            foreach (T item in Items)
                item.Remove();
        }

        /// <summary>
        /// Новя панель
        /// </summary>
        /// <param name="itemsResourceName">GameObject, нахоящийся в папке GameObjects</param>
        /// <param name="panel"></param>
        /// <param name="parent"></param>
        public PanelBase(string itemsResourceName, GameObject panel, GameObject parent = null)
        {
            _itemsResourceName = itemsResourceName;
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
                item.Create(Loader.Instantiate(Resources.Load<GameObject>("GameObjects/"+_itemsResourceName)), ItemsContainer.transform);

                Vector2 sizeContainer = Items[i].ContainerGameObject.GetComponent<RectTransform>().sizeDelta;
                ItemsContainer.GetComponent<RectTransform>().sizeDelta += new Vector2(0, sizeContainer.y);
                item.ContainerGameObject.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(0, -sizeContainer.x / 2 - i * sizeContainer.y);
                i++;
            }
        }
    }
}