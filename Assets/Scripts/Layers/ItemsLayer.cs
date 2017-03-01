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

        private ItemsLayer()
        {
            itemsSearchPanel = new SearchPanel(GameObject.Find("itemsSearchPanel"));
            itemsSearchPanel.OnSearch = delegate { ApplicationManager.productManager.ProductsLoad(itemsSearchPanel.SearchInputField.text); };

            itemsScrollPanel = new ScrollPanelBase<Product>("productsItems", GameObject.Find("itemsContent"));
            itemsScrollPanel.DefaultText = GameObject.Find("defaultText").GetComponent<Text>();
            itemsScrollPanel.NextArrow = GameObject.Find("nextItemsPageButton").GetComponent<Button>();
            itemsScrollPanel.PrevArrow = GameObject.Find("prevItemsPageButton").GetComponent<Button>();
            itemsScrollPanel.BottomPanel = GameObject.Find("bottomItemsBar");

            itemsScrollPanel.ReloadPanel();
            itemsSearchPanel.ReloadPanel();
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