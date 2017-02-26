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
            SearchPanel = new SearchPanel();
            SearchPanel.SearchInputField = GameObject.Find("searchItemsInput").GetComponent<InputField>();
            SearchPanel.SearchItemsButton = GameObject.Find("searchItemsButton").GetComponent<Button>();

            ScrollPanel = new ScrollPanelBase<Product>();
            ScrollPanel.ItemsContainer = GameObject.Find("itemsContent");
            ScrollPanel.NextArrow = GameObject.Find("nextItemsPageButton").GetComponent<Button>();
            ScrollPanel.PrevArrow = GameObject.Find("prevItemsPageButton").GetComponent<Button>();
            SearchPanel.OnSearch = delegate { ApplicationManager.productManager.ProductsLoad(SearchPanel.SearchInputField.text); };

            ScrollPanel.InitPanel();
            SearchPanel.InitPanel();
        }

        public static ItemsLayer instance = new ItemsLayer();


        private GameObject bottomItemsBar = GameObject.Find("bottomItemsBar");



        public void OnVisable()
        {

        }

        public void OnDisable()
        {

        }

        public void MoveBottomPanelToBottom()
        {
            ScrollPanel.ItemsContainer.GetComponent<RectTransform>().sizeDelta += new Vector2(0, bottomItemsBar.GetComponent<RectTransform>().sizeDelta.y);
            bottomItemsBar.transform.parent = ScrollPanel.ItemsContainer.transform;
            bottomItemsBar.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}