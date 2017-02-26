using Assets.Scripts.Layers.Enum;
using UnityEngine;

namespace Assets.Scripts.Layers
{
    public class ItemLayer : ILayerBase
    {
        private GameObject _canvas = GameObject.Find("itemCanvas");
        private LayerNamesEnum _name = LayerNamesEnum.item;

        public LayerNamesEnum Name { get { return _name; } }
        public GameObject Canvas { get { return _canvas; } }

        public static ItemLayer instance = new ItemLayer();

        private ItemLayer()
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