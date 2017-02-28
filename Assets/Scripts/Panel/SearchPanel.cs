using System;
using UnityEngine.UI;

namespace Panels
{
    public class SearchPanel
    {
        //LOGIC
        public Action OnSearch;

        //GAME OBJECTS
        public InputField SearchInputField;
        public Button SearchItemsButton;
        public Button CancelButton;

        public void InitPanel()
        {
            if(SearchItemsButton != null && OnSearch != null)
                SearchItemsButton.onClick.AddListener(delegate
                {
                    OnSearch.Invoke();
                });
            if(SearchInputField != null && OnSearch != null)
                SearchInputField.onEndEdit.AddListener(delegate
                {
                    OnSearch.Invoke();
                });
        }
    }
}