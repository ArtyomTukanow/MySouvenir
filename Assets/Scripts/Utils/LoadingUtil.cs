using UnityEngine;

namespace Utils
{
    public class LoadingUtil
    {
        public static GameObject LoadindBaseGameObject = GameObject.Find("LoadindBaseGameObject");
        public static GameObject LoadingCanvas = GameObject.Find("LoadingCanvas");

        public static void Start()
        {
            LoadingCanvas.SetActive(false);
        }

        private static float z;
        public static void Update()
        {
            z -= Time.deltaTime * 300;
            LoadindBaseGameObject.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, z);
        }

        public static void Show()
        {
            LoadingCanvas.SetActive(true);
        }

        public static void Hide()
        {
            LoadingCanvas.SetActive(false);
        }
    }
}