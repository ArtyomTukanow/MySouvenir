using System;
using Assets.Scripts.Layers;
using Items.Interface;
using UnityEngine;
using UnityEngine.UI;

namespace Panels
{
    /// <summary>
    /// Скролл панель из объектов типа T
    /// </summary>
    /// <typeparam name="T">Тип отоброжаемых объектов. Обязательно наследуемый от IItemBase</typeparam>
    public class ScrollPanelBase <T> : PanelBase<T> where T : IItemBase
    {
        public int Page = 1;
        public int ItemsPerPage = 10;


        //GAME OBJECTS
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

        public override void LoadItems()
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
            ItemsContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            for (int i = startProductId; i < endProductId; i++)
            {
                Items[i].Create(Loader.Instantiate(Resources.Load<GameObject>("productItem")), ItemsContainer.transform);

                Vector2 sizeContainer = Items[i].ContainerGameObject.GetComponent<RectTransform>().sizeDelta;
                ItemsContainer.GetComponent<RectTransform>().sizeDelta += new Vector2(0, sizeContainer.y);
                Items[i].ContainerGameObject.GetComponent<RectTransform>().anchoredPosition =
                    new Vector2(0, -sizeContainer.x / 2 - (i - startProductId) * sizeContainer.y);
            }
            ItemsLayer.instance.MoveBottomPanelToBottom(); //FIXME!!!!!!!
        }

        public override void ReloadPanel()
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