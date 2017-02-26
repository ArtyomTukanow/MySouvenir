using System;
using Assets.Scripts.Layers;
using Items.Interface;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Panels
{
    /// <summary>
    /// Скролл панель из объектов типа T
    /// </summary>
    /// <typeparam name="T">Тип отоброжаемых объектов. Обязательно наследуемый от IItemBase</typeparam>
    public class ScrollPanelBase <T> : IPanel where T : IItemBase
    {
        //LOGIC
        public bool IsVerticalPanel = false;

        public int Page = 1;
        public int ItemsPerPage = 10;
        public T[] Items;


        //GAME OBJECTS
        public GameObject ItemsContainer;
        public Button NextArrow;
        public Button PrevArrow;

        public void LoadNextPage()
        {
            if(Items == null || Items.Length == 0)
                throw new Exception("Items are null or empty");
            Page++;
            LoadItems();
        }

        public void LoadPrevPage()
        {
            if(Items == null || Items.Length == 0)
                throw new Exception("Items are null or empty");
            Page--;
            LoadItems();
        }

        public void LoadItems(int page)
        {
            Page = page;
            LoadItems();
        }

        public void LoadItems()
        {
            if(Items == null || Items.Length == 0)
                throw new Exception("Items are null or empty");

            destroyOldItems();
            if (Page < 1) Page = 1;

            int pagesCount = Items.Length % ItemsPerPage > 0
                ? Items.Length / ItemsPerPage + 1
                : Items.Length / ItemsPerPage;
            Page = pagesCount >= Page ? Page : pagesCount;

            int startProductId = (Page - 1) * ItemsPerPage;
            int endProductId = Page == pagesCount ? Items.Length : Page * ItemsPerPage;
            Log.d("Loading Items " + startProductId + " to " + endProductId);
            ItemsContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            for (int i = startProductId; i < endProductId; i++)
            {
                Items[i]
                    .Create(Loader.Instantiate(Resources.Load<GameObject>("productItem")),
                        ItemsContainer.transform);

                Vector2 sizeContainer = Items[i].ContainerGameObject.GetComponent<RectTransform>().sizeDelta;
                ItemsContainer.GetComponent<RectTransform>().sizeDelta += new Vector2(0, sizeContainer.y);
                Items[i].ContainerGameObject.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(0, -140 - (i - startProductId) * sizeContainer.y);
            }
            ItemsLayer.instance.MoveBottomPanelToBottom();
        }

        private void destroyOldItems()
        {
            if(Items == null || Items.Length == 0)
                throw new Exception("Items are null or empty");

            foreach (T item in Items)
                if (item.ContainerGameObject != null)
                {
                    item.ImageLoadConnection.Cancel();
                    Loader.Destroy(item.ContainerGameObject);
                }
        }

        public void InitPanel()
        {
            if(NextArrow != null)
                NextArrow.onClick.AddListener(delegate
                {
                    ItemsContainer.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    LoadNextPage();
                });
            if(PrevArrow != null)
                PrevArrow.onClick.AddListener(delegate
                {
                    ItemsContainer.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    LoadPrevPage();
                });
        }

    }
}