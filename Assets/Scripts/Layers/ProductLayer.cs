using Assets.Scripts.Layers.Enum;
using Items;
using Panels;
using UnityEngine;

namespace Assets.Scripts.Layers
{
    public class ProductLayer : ILayerBase
    {
        private GameObject _canvas = GameObject.Find("productCanvas");
        private LayerNamesEnum _name = LayerNamesEnum.item;

        public ScrollPanelBase<ProductFull> itemScrollPanel;

        public LayerNamesEnum Name
        {
            get { return _name; }
        }

        public GameObject Canvas
        {
            get { return _canvas; }
        }

        public static ProductLayer instance = new ProductLayer();


        private ProductLayer()
        {
            itemScrollPanel = new ScrollPanelBase<ProductFull>("productItems", GameObject.Find("productContent"));
            itemScrollPanel.ReloadPanel();
        }

        public void OnVisable()
        {

        }

        public void OnDisable()
        {

        }

        public void OnReload()
        {
            itemScrollPanel.LoadItems();
        }
    }
}