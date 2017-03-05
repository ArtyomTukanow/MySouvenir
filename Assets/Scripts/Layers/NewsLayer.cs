using Assets.Scripts.Layers.Enum;
using UnityEngine;

namespace Assets.Scripts.Layers
{
    public class NewsLayer : ILayerBase
    {
        private GameObject _canvas = GameObject.Find("newsCanvas");
        private LayerNamesEnum _name = LayerNamesEnum.news;

        public LayerNamesEnum Name { get { return _name; } }

        public GameObject Canvas { get { return _canvas; }}

        public static NewsLayer instance = new NewsLayer();

        private NewsLayer()
        {

        }

        public void OnVisable()
        {

        }

        public void OnDisable()
        {

        }

        public void OnReload()
        {

        }
    }
}