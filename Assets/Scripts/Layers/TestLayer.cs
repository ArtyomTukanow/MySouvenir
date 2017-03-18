using Assets.Scripts.Layers.Enum;
using Panels;
using UnityEngine;

namespace Assets.Scripts.Layers
{
    public class TestLayer : ILayerBase
    {
        private GameObject _canvas = GameObject.Find("testCanvas");
        private LayerNamesEnum _name = LayerNamesEnum.test;

        public LayerNamesEnum Name { get { return _name; } }

        public GameObject Canvas { get { return _canvas; }}

        public static TestLayer instance = new TestLayer();

        private TestLayer()
        {
            DropDownPanel panel = new DropDownPanel(_canvas.transform, new Vector2(100, 100), "text 1", "text 2", "text 3");
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