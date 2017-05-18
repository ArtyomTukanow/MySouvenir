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
        private DropDownPanel _sexPanel;
        private DropDownPanel _agePanel;
        private DropDownPanel _hollydaysPanel;
        private DropDownPanel _othersPanel;
        private Text _minPriceInput;
        private Text _maxPriceInput;


        private Button _confirmButton;


        private TestLayer()
        {
            _confirmButton = _canvas.transform.FindChild("ConfirmButton").GetComponent<Button>();
            _confirmButton.onClick.AddListener(FindItems);
            _othersPanel = new DropDownPanel(_canvas.transform.FindChild("PanelGameObject"), new Vector2(0, -200),
                "Любой",
                "Романтичный",
                "С юмором",
                "Серьезный",
                "Для дома");
            _hollydaysPanel = new DropDownPanel(_canvas.transform.FindChild("PanelGameObject"), new Vector2(0, -100),
                "Любой",
                "Новый год",
                "Рождество",
                "День рождения",
                "День влюбленных",
                "День защитника Отечества",
                "Международный женский день",
                "Юбилей",
                "Годовщина свадьбы",
                "День матери",
                "День учителя",
                "Хэллоуин");
            _tagPanel = new DropDownPanel(_canvas.transform.FindChild("PanelGameObject"), new Vector2(0, 0),
                "Любой",
                "Мама",
                "Папа",
                "Сестра",
                "Брат",
                "Коллега",
                "Ребенок",
                "Именное");
            _agePanel = new DropDownPanel(_canvas.transform.FindChild("PanelGameObject"), new Vector2(0, 100),
                "Любой",
                "до 5",
                "от 5 до 10",
                "от 11 до 16",
                "от 17 до 25",
                "от 26 до 35",
                "от 36 до 45",
                "от 45");
            _sexPanel = new DropDownPanel(_canvas.transform.FindChild("PanelGameObject"), new Vector2(0, 200),
                "Любой",
                "Парень",
                "Девушка");
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
            urlVariables.Add(_sexPanel.value);
            urlVariables.Add(_agePanel.value);
            urlVariables.Add(_hollydaysPanel.value);
            urlVariables.Add(_othersPanel.value);
            ApplicationManager.ProductManager.ProductsLoad(ProductsLayer.instance.OnProductsLoad, urlVariables);
        }
    }
}