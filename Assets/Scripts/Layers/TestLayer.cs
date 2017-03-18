using Assets.Scripts.Layers.Enum;
using Managers;
using Net;
using Panels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Layers
{
    public class TestLayer : ILayerBase
    {
        private GameObject _canvas = GameObject.Find("testCanvas");
        private LayerNamesEnum _name = LayerNamesEnum.test;

        public LayerNamesEnum Name { get { return _name; } }

        public GameObject Canvas { get { return _canvas; }}

        public static TestLayer instance = new TestLayer();


        private DropDownPanel _tagPanel;
        private Button _confirmButton;
        private Text _minPriceInput;
        private Text _maxPriceInput;




        private TestLayer()
        {
            _confirmButton = _canvas.transform.FindChild("ConfirmButton").GetComponent<Button>();
            _confirmButton.onClick.AddListener(FindItems);
            _tagPanel = new DropDownPanel(_canvas.transform.FindChild("PanelGameObject"), new Vector2(0, 0),
                "Парень",
                "Девушка",
                "Мама",
                "Сестра",
                "Коллега",
                "Дом",
                "Романтика",
                "Ребенок",
                "Маленький");
            _minPriceInput = _canvas.transform.FindChild("minPriceInput").FindChild("text").GetComponent<Text>();
            _maxPriceInput = _canvas.transform.FindChild("maxPriceInput").FindChild("text").GetComponent<Text>();
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

        private void FindItems()
        {
            ApplicationManager.UiManager.Layer = LayerNamesEnum.items;
            URLVariables urlVariables = new URLVariables();
            if(_minPriceInput.text != "")
                urlVariables.Add(URLVariables.MIN_PRICE, _minPriceInput.text);
            if(_maxPriceInput.text != "")
                urlVariables.Add(URLVariables.MAX_PRICE, _maxPriceInput.text);
            urlVariables.Add(_tagPanel.value);
            ApplicationManager.ProductManager.ProductsLoad(ProductsLayer.instance.OnProductsLoad, urlVariables);
        }
    }
}