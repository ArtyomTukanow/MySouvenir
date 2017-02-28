using Assets.Scripts.Layers.Enum;
using Items;
using Managers;
using Panels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Layers
{
    public class ItemsLayer : ILayerBase
    {
        private GameObject _canvas = GameObject.Find("itemsCanvas");
        private LayerNamesEnum _name = LayerNamesEnum.items;

        public ScrollPanelBase<Product> ScrollPanel;
        public SearchPanel SearchPanel;

        public LayerNamesEnum Name
        {
            get { return _name; }
        }

        public GameObject Canvas
        {
            get { return _canvas; }
        }

        private ItemsLayer()
        {
            SearchPanel = new SearchPanel(GameObject.Find("itemsSearchBar"));
            SearchPanel.OnSearch = delegate { ApplicationManager.productManager.ProductsLoad(SearchPanel.SearchInputField.text); };

            ScrollPanel = new ScrollPanelBase<Product>(GameObject.Find("itemsContent"));
            ScrollPanel.NextArrow = GameObject.Find("nextItemsPageButton").GetComponent<Button>();
            ScrollPanel.PrevArrow = GameObject.Find("prevItemsPageButton").GetComponent<Button>();
            ScrollPanel.BottomPanel = GameObject.Find("bottomItemsBar");

            ScrollPanel.ReloadPanel();
            SearchPanel.ReloadPanel();
        }

        public static ItemsLayer instance = new ItemsLayer();



        public void OnVisable()
        {

        }

        public void OnDisable()
        {

        }
    }
}