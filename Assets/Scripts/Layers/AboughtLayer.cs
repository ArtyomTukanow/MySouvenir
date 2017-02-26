using UnityEngine;
using Assets.Scripts.Layers.Enum;

namespace Assets.Scripts.Layers
{
    public class AboughtLayer : ILayerBase
    {
        private GameObject _canvas = GameObject.Find("aboughtCanvas");
        private LayerNamesEnum _name = LayerNamesEnum.abought;

        public LayerNamesEnum Name { get { return _name; } }

        public GameObject Canvas { get { return _canvas; }}

        public static AboughtLayer instance = new AboughtLayer();

        private AboughtLayer()
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