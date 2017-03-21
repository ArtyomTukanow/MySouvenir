using System.Collections.Generic;
using System.Linq;
using Assets.Engine.Core.Util.SITween;
using Assets.Scripts.Event;
using Assets.Scripts.Layers;
using Assets.Scripts.Layers.Enum;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Managers
{
    public class UIManager
    {
        public static UIManager instance = new UIManager();

        private UIManager()
        {
        }

        public IEnumerable<ILayerBase> Layers = new List<ILayerBase>(new ILayerBase[]
        {
            AboughtLayer.instance,
            HowItsWorkLayer.instance,
            ProductLayer.instance,
            ProductsLayer.instance,
            NewsLayer.instance,
            ShopsLayer.instance,
            ShopLayer.instance,
            TestLayer.instance
        });

        private GameObject topBarCanvas = GameObject.Find("topBarCanvas");
        private GameObject menuCanvas = GameObject.Find("menuCanvas");

        //Images
        private GameObject menuAlphaBack = GameObject.Find("menuAlphaBack");
        private GameObject menuWindow = GameObject.Find("menuWindow");

        //Buttons
        private Button newsButton = GameObject.Find("newsButton").GetComponent<Button>();
        private Button testButton = GameObject.Find("testButton").GetComponent<Button>();
        private Button chooseGiftButton = GameObject.Find("chooseGiftButton").GetComponent<Button>();
        private Button shopsButton = GameObject.Find("shopsButton").GetComponent<Button>();
        private Button howItWorksButton = GameObject.Find("howItWorksButton").GetComponent<Button>();
        private Button aboughtButton = GameObject.Find("aboughtButton").GetComponent<Button>();
        private Button exitButton = GameObject.Find("exitButton").GetComponent<Button>();

        private Button topBarOpenMenuButton = GameObject.Find("topBarOpenMenuButton").GetComponent<Button>();



        private List<LayerNamesEnum> _lastLayers = new List<LayerNamesEnum>();
        private LayerNamesEnum _layer = LayerNamesEnum.none;

        public List<LayerNamesEnum> LastLayers
        {get { return _lastLayers; }
        }

        /// <summary>
        /// Текущий слой. Все остальные слои скрываются, кроме данного.
        /// </summary>
        public LayerNamesEnum Layer {
            set
            {
                if(MenuIsOpened)
                    closeMenuAnimate();

                if(value == _layer) return;

                foreach (ILayerBase layer in Layers) //disable old layers
                {
                    if (layer.Name == _layer)
                    {
                        layer.OnDisable();
                    }
                }
                if(_lastLayers.Count == 0 || _layer != _lastLayers[_lastLayers.Count-1])
                    _lastLayers.Add(_layer);
                _layer = value;
                foreach (ILayerBase layer in Layers) //enable new layers
                {
                    if (layer.Name == _layer)
                    {
                        layer.OnVisable();
                        layer.Canvas.SetActive(true);
                    }
                    else
                    {
                        layer.Canvas.SetActive(false);
                    }
                }
            }
            get
            {
                return _layer;
            }
        }

        public void GoBackToLastLayer()
        {
            Layer = _lastLayers[_lastLayers.Count-1];
            _lastLayers.RemoveAt(_lastLayers.Count-1);
        }


        public void Start()
        {
            newsButton.onClick.AddListener(delegate
            {
                if (_hasTouchedMenu) return;

                Layer = LayerNamesEnum.news;
                _lastLayers = new List<LayerNamesEnum>();
            });
            testButton.onClick.AddListener(delegate
            {
                if (_hasTouchedMenu) return;
                Layer = LayerNamesEnum.test;
                _lastLayers = new List<LayerNamesEnum>();
            });
            chooseGiftButton.onClick.AddListener(delegate
            {
                if (_hasTouchedMenu) return;
                Layer = LayerNamesEnum.items;
                _lastLayers = new List<LayerNamesEnum>();
            });
            shopsButton.onClick.AddListener(delegate
            {
                if (_hasTouchedMenu) return;
                Layer = LayerNamesEnum.shops;
                _lastLayers = new List<LayerNamesEnum>();
            });
            howItWorksButton.onClick.AddListener(delegate
            {
                if (_hasTouchedMenu) return;
                Layer = LayerNamesEnum.howItsWork;
                _lastLayers = new List<LayerNamesEnum>();
            });
            aboughtButton.onClick.AddListener(delegate
            {
                if (_hasTouchedMenu) return;
                Layer = LayerNamesEnum.abought;
                _lastLayers = new List<LayerNamesEnum>();
            });
            exitButton.onClick.AddListener(delegate
            {
                if (_hasTouchedMenu) return;
                Application.Quit();
            });

            menuAlphaBack.GetComponent<Button>().onClick.AddListener(closeMenuAnimate);

            topBarOpenMenuButton.onClick.AddListener(openMenuAnimate);

            EventDispatcher.addOnTouchDown(OnTouchDown);
            EventDispatcher.addOnTouchUp(OnTouchUp);
            EventDispatcher.addOnTouch(OnTouch);

            //DEFAULT PARAMS
            closeMenu();
            Layer = LayerNamesEnum.news;
        }

        private bool _hasTouchedMenu;
        private void OnTouchDown(float x, float y)
        {
            if (x < 100 || MenuIsOpened)
            {
                if (x > 600) x = 600;
                _hasTouchedMenu = true;
                menuWindow.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(x, 0);
                menuAlphaBack.GetComponent<Image>().color = new Color(0,0,0,(100-x)/100-0.5f);
            }
        }
        private void OnTouch(float x, float y)
        {
            if (_hasTouchedMenu)
            {
                if (x > 600) x = 600;
                menuWindow.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(x, 0);
                menuAlphaBack.GetComponent<Image>().color = new Color(0,0,0,(x-100)/1000);
                menuWindow.SetActive(true);
                menuAlphaBack.SetActive(true);
            }
        }
        private void OnTouchUp()
        {
            if (_hasTouchedMenu)
            {
                if (menuWindow.transform.position.x > 300)
                {
                    openMenuAnimate();
                }
                else
                {
                    closeMenuAnimate();
                }
            }
            _hasTouchedMenu = false;
        }

        public bool MenuIsOpened {get { return menuWindow.activeSelf; }}

        public void openMenu()
        {
            menuWindow.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(600, 0);
            menuAlphaBack.GetComponent<Image>().color = new Color(0,0,0,0.5f);
            menuWindow.SetActive(true);
            menuAlphaBack.SetActive(true);
        }

        public void closeMenu()
        {
            menuWindow.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0);
            menuAlphaBack.GetComponent<Image>().color = new Color(0,0,0,0.5f);
            menuWindow.SetActive(false);
            menuAlphaBack.SetActive(false);
        }






        private void openMenuAnimate()
        {
            _startPosX = menuWindow.GetComponent<RectTransform>().anchoredPosition.x;
            SITween.to(menuWindow, onUpdateOpen, 0.5f, openMenu);
            menuWindow.SetActive(true);
            menuAlphaBack.SetActive(true);
        }

        private float _startPosX;
        private void onUpdateOpen(object obj, SITween tween)
        {
            float x = tween.getTweenValue(_startPosX, 600, SITween.EaseType.linear);
            menuWindow.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, 0);
        }

        private void closeMenuAnimate()
        {
            _startPosX = menuWindow.GetComponent<RectTransform>().anchoredPosition.x;
            SITween.to(menuWindow, onUpdateClose, 0.5f, closeMenu);
        }

        private void onUpdateClose(object obj, SITween tween)
        {
            float x = tween.getTweenValue(_startPosX, 0, SITween.EaseType.linear);
            menuWindow.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, 0);
        }
    }
}