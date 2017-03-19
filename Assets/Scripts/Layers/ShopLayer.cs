using Assets.Scripts.Layers.Enum;
using Items;
using Panels;
using UnityEngine;

namespace Assets.Scripts.Layers
{
    public class ShopLayer : ILayerBase
    {
        public LayerNamesEnum Name { get { return LayerNamesEnum.shop; } }

        private GameObject _canvas = GameObject.Find("shopCanvas");
        public GameObject Canvas { get { return _canvas; } }

        public ScrollPanelBase<ShopFull> itemScrollPanel;

        public static ShopLayer instance = new ShopLayer();
        private ShopLayer()
        {
            itemScrollPanel = new ScrollPanelBase<ShopFull>("shopItems", GameObject.Find("shopContent"));
            itemScrollPanel.ReloadPanel();
        }

        public void OnVisable()
        {

        }

        public void OnDisable()
        {
            itemScrollPanel.DestroyOldItems();
        }

        public void OnReload()
        {
            itemScrollPanel.LoadItems();
        }

        public void OnShopLoad(ShopFull shop)
        {
            itemScrollPanel.Items = new []{shop};
            OnReload();
        }
    }
}