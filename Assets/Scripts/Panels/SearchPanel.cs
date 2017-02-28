using System;
using UnityEngine;
using UnityEngine.UI;

namespace Panels
{
    public class SearchPanel : IPanel
    {
        //LOGIC
        public Action OnSearch;

        //GAME OBJECTS
        public InputField SearchInputField;
        public Button SearchButton;

        public GameObject ItemsContainer { set; get; }

        public void ReloadPanel()
        {
            if(SearchButton != null && OnSearch != null)
                SearchButton.onClick.AddListener(delegate
                {
                    OnSearch.Invoke();
                });
            if(SearchInputField != null && OnSearch != null)
                SearchInputField.onEndEdit.AddListener(delegate
                {
                    OnSearch.Invoke();
                });
        }

        public SearchPanel(GameObject panel, GameObject parent = null)
        {
            ItemsContainer = panel;
            if(parent != null)
                ItemsContainer.transform.parent = parent.transform;

            SearchInputField = ItemsContainer.transform.FindChild("searchInput").GetComponent<InputField>();
            SearchButton = ItemsContainer.transform.FindChild("searchButton").GetComponent<Button>();
        }
    }
}