using Assets.Scripts.Layers.Enum;
using UnityEngine;

namespace Assets.Scripts.Layers
{
    public class HowItsWorkLayer : ILayerBase

    {
        private GameObject _canvas = GameObject.Find("howItsWorkCanvas");
        private LayerNamesEnum _name = LayerNamesEnum.howItsWork;

        public LayerNamesEnum Name
        {
            get { return _name; }
        }

        public GameObject Canvas
        {
            get { return _canvas; }
        }

        public static HowItsWorkLayer instance = new HowItsWorkLayer();

        private HowItsWorkLayer()
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