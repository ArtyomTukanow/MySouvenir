using System.Collections.Generic;
using Assets.Scripts.Layers;
using Assets.Scripts.Layers.Enum;
using UnityEngine;
using UnityEngine.UI;

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
            ItemLayer.instance,
            ItemsLayer.instance,
            NewsLayer.instance,
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



        private LayerNamesEnum _layer = LayerNamesEnum.none;

        /// <summary>
        /// Текущий слой. Все остальные слои скрываются, кроме данного.
        /// </summary>
        public LayerNamesEnum Layer {
            set
            {
                if(value == _layer) return;

                foreach (ILayerBase layer in Layers) //disable old layers
                {
                    if (layer.Name == _layer)
                    {
                        layer.OnDisable();
                    }
                }
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
                closeMenu();
            }
            get
            {
                return _layer;
            }
        }


        public void Start()
        {
            newsButton.onClick.AddListener(delegate { Layer = LayerNamesEnum.news; });
            testButton.onClick.AddListener(delegate { Layer = LayerNamesEnum.test; });
            chooseGiftButton.onClick.AddListener(delegate {
                Layer = LayerNamesEnum.items;
            });
            shopsButton.onClick.AddListener(delegate { Layer = LayerNamesEnum.shops; });
            howItWorksButton.onClick.AddListener(delegate { Layer = LayerNamesEnum.howItsWork; });
            aboughtButton.onClick.AddListener(delegate { Layer = LayerNamesEnum.abought; });
            exitButton.onClick.AddListener(Application.Quit);

            menuAlphaBack.GetComponent<Button>().onClick.AddListener(closeMenu);

            topBarOpenMenuButton.onClick.AddListener(openMenu);

            //DEFAULT PARAMS
            closeMenu();
            Layer = LayerNamesEnum.news;
        }

        private void openMenu()
        {
            if(menuWindow.activeSelf)
                return;

            menuWindow.SetActive(true);
            menuAlphaBack.SetActive(true);
        }

        private void closeMenu()
        {
            if(!menuWindow.activeSelf)
                return;

            menuWindow.SetActive(false);
            menuAlphaBack.SetActive(false);
        }

    }
}