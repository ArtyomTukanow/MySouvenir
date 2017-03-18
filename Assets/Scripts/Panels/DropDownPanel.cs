using UnityEngine;
using UnityEngine.UI;

namespace Panels
{
    public class DropDownPanel
    {
        public string value { set; get; }

        public GameObject root { get { return _panel; } }
        private GameObject _panel;
        private Transform _dropDown;
        private Button _buttonDrop;
//        public List<Button> Elements;


        public DropDownPanel(Transform parent, Vector2 position, params string[] elements)
        {
            _panel = Loader.Instantiate(Resources.Load<GameObject>("GameObjects/panels/dropDownPanel"));
            _panel.transform.parent = parent;
            _panel.transform.localPosition = position;
            _panel.transform.localScale = Vector3.one;

            _dropDown = _panel.transform.FindChild("DropDown").transform;
            _buttonDrop = _panel.transform.FindChild("ButtonDrop").GetComponent<Button>();
            _buttonDrop.onClick.AddListener(delegate
            {
                _dropDown.gameObject.SetActive(!_dropDown.gameObject.activeSelf);
            });


            _buttonDrop.GetComponentInChildren<Text>().text = elements[0];
            for (int i = 0; i < elements.Length; i++)
            {
                GameObject dropElement = Loader.Instantiate(Resources.Load<GameObject>("GameObjects/panels/dropElement"), _dropDown);
                dropElement.GetComponentInChildren<Text>().text = elements[i];
                dropElement.GetComponent<Button>().onClick.AddListener(delegate
                {
                    _dropDown.gameObject.SetActive(false);
                    _buttonDrop.GetComponentInChildren<Text>().text = dropElement.GetComponentInChildren<Text>().text;
                    value = dropElement.GetComponentInChildren<Text>().text;
                });
            }

            value = elements[0];
            _dropDown.gameObject.SetActive(false);
        }
    }
}