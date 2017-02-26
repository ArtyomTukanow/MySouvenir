using Assets.Scripts.Layers.Enum;
using UnityEngine;

namespace Assets.Scripts.Layers
{
    public class ShopLayer : ILayerBase
    {
        private GameObject _canvas = GameObject.Find("shopCanvas");
        private LayerNamesEnum _name = LayerNamesEnum.shops;

        public LayerNamesEnum Name { get { return _name; } }

        public GameObject Canvas { get { return _canvas; }}

        public static ShopLayer instance = new ShopLayer();

        private ShopLayer()
        {

        }

        public void OnVisable()
        {

        }

        public void OnDisable()
        {

        }
    }
}