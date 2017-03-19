using Assets.Scripts.Layers.Enum;
using Items;
using Managers;
using Panels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Layers
{
    public class ShopsLayer : ILayerBase
    {
        private GameObject _canvas = GameObject.Find("shopsCanvas");
        private LayerNamesEnum _name = LayerNamesEnum.shops;

        public LayerNamesEnum Name { get { return _name; } }

        public GameObject Canvas { get { return _canvas; }}

        public static ShopsLayer instance = new ShopsLayer();

        private ScrollPanelBase<Shop> itemsScrollPanel;

        private ShopsLayer()
        {
            itemsScrollPanel = new ScrollPanelBase<Shop>("shopsItems", GameObject.Find("shopsContent"));
            itemsScrollPanel.DefaultText = GameObject.Find("shopsText").GetComponent<Text>();
            itemsScrollPanel.NextArrow = GameObject.Find("nextShopsPageButton").GetComponent<Button>();
            itemsScrollPanel.PrevArrow = GameObject.Find("prevShopsPageButton").GetComponent<Button>();

            itemsScrollPanel.ReloadPanel();
        }

        public void OnVisable()
        {
            ApplicationManager.UiManager.Layer = LayerNamesEnum.shops;
            ApplicationManager.ShopManager.ShopsLoad(OnShopsLoad);
        }

        private void OnShopsLoad(Shops shops)
        {
            itemsScrollPanel.Items = shops.shops;
            OnReload();
        }

        public void OnDisable()
        {

        }

        public void OnReload()
        {
            itemsScrollPanel.LoadItems(0);
        }
    }
}