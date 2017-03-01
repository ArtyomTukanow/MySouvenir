using Items.Interface;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Panels
{
    /// <summary>
    /// Скролл панель из объектов типа T
    /// </summary>
    /// <typeparam name="T">Тип отоброжаемых объектов. Обязательно наследуемый от IItemBase</typeparam>
    public class ScrollPanelBase <T> : PanelBase<T> where T : IItemBase
    {
        private int _pagesCount;
        private int _page = 1;

        private int _itemsPerPage;
        public int ItemsPerPage
        {
            set
            {
                if (Items != null)
                {
                    _pagesCount = Items.Length % value > 0
                        ? Items.Length / value + 1
                        : Items.Length / value;
                }
                else
                {
                    _pagesCount = 0;
                }

                _itemsPerPage = value;
            }
            get { return _itemsPerPage; }
        }

        public override T[] Items
        {
            get { return base.Items; }
            set
            {
                _pagesCount = value.Length % _itemsPerPage > 0
                    ? value.Length / _itemsPerPage + 1
                    : value.Length / _itemsPerPage;
                base.Items = value;
            }
        }

        //GAME OBJECTS
        public Button NextArrow;
        public Button PrevArrow;
        private string _firstDefaultText;
        private Text _defaultText;
        public Text DefaultText
        {
            set
            {
                _firstDefaultText = value.text;
                _defaultText = value;
            }
            get { return _defaultText; }
        }

        public ScrollPanelBase(string itemsResourceName, GameObject panel, GameObject parent = null)
            : base (itemsResourceName, panel, parent)
        {
            ItemsPerPage = 10;
        }

        public void LoadNextPage()
        {
            _page++;
            LoadItems();
        }

        public void LoadPrevPage()
        {
            _page--;
            LoadItems();
        }

        public void LoadItems(int page)
        {
            _page = page;
            LoadItems();
        }

        public override void LoadItems()
        {
            DestroyOldItems();
            _page = _pagesCount > _page ? _page : _pagesCount - 1;
            if (_page < 0) _page = 0;

            int startProductId = _page * ItemsPerPage;
            int endProductId = _page == _pagesCount - 1 || _pagesCount == 0 ? Items.Length : (_page + 1) * ItemsPerPage;
            ItemsContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
            int itemsPerPageCount = endProductId - startProductId;
            if (itemsPerPageCount > 0)
            {
                for (int i = startProductId; i < endProductId; i++)
                {
                    Items[i]
                        .Create(Loader.Instantiate(Resources.Load<GameObject>("GameObjects/"+_itemsResourceName)),
                            ItemsContainer.transform);

                    Vector2 sizeContainer = Items[i].ContainerGameObject.GetComponent<RectTransform>().sizeDelta;
                    ItemsContainer.GetComponent<RectTransform>().sizeDelta += new Vector2(0, sizeContainer.y);
                    Items[i].ContainerGameObject.GetComponent<RectTransform>().anchoredPosition =
                        new Vector2(0, -sizeContainer.y / 2 - (i - startProductId) * sizeContainer.y);
                }
            }
            else if (DefaultText != null)
            {
                DefaultText.text = "Поиск не дал результатов...";
            }
        }

        public override void ReloadPanel()
        {
            if(NextArrow != null)
                NextArrow.onClick.AddListener(delegate
                {
                    ItemsContainer.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    LoadNextPage();
                });
            if(PrevArrow != null)
                PrevArrow.onClick.AddListener(delegate
                {
                    ItemsContainer.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    LoadPrevPage();
                });
        }

    }
}