using Assets.Scripts.Layers.Enum;
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

        }

        public void OnVisable()
        {

        }

        public void OnDisable()
        {

        }
    }
}