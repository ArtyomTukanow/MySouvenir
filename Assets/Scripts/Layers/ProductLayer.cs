using Assets.Scripts.Layers.Enum;
using UnityEngine;

namespace Assets.Scripts.Layers
{
    public class ProductLayer : ILayerBase
    {
        private GameObject _canvas = GameObject.Find("productCanvas");
        private LayerNamesEnum _name = LayerNamesEnum.item;

        public LayerNamesEnum Name { get { return _name; } }
        public GameObject Canvas { get { return _canvas; } }

        public static ProductLayer instance = new ProductLayer();

        private ProductLayer()
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