using Assets.Scripts.Layers.Enum;
using Items;
using Managers;
using Panels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Layers
{
    public class ProductsLayer : ILayerBase
    {
        private GameObject _canvas = GameObject.Find("productsCanvas");
        private LayerNamesEnum _name = LayerNamesEnum.items;

        public ScrollPanelBase<Product> itemsScrollPanel;
        public SearchPanel itemsSearchPanel;

        public LayerNamesEnum Name
        {
            get { return _name; }
        }

        public GameObject Canvas
        {
            get { return _canvas; }
        }

        private ProductsLayer()
        {
            itemsSearchPanel = new SearchPanel(GameObject.Find("productsSearchPanel"));
            itemsSearchPanel.OnSearch = delegate { ApplicationManager.productManager.ProductsLoad(itemsSearchPanel.SearchInputField.text); };

            itemsScrollPanel = new ScrollPanelBase<Product>("productsItems", GameObject.Find("productsContent"));
            itemsScrollPanel.DefaultText = GameObject.Find("productsText").GetComponent<Text>();
            itemsScrollPanel.NextArrow = GameObject.Find("nextItemsPageButton").GetComponent<Button>();
            itemsScrollPanel.PrevArrow = GameObject.Find("prevItemsPageButton").GetComponent<Button>();
            //itemsScrollPanel.BottomPanel = GameObject.Find("bottomItemsBar");

            itemsScrollPanel.ReloadPanel();
            itemsSearchPanel.ReloadPanel();
        }

        public static ProductsLayer instance = new ProductsLayer();



        public void OnVisable()
        {

        }

        public void OnDisable()
        {

        }
    }
}