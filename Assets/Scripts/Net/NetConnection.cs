using System;
using System.Collections;
using UnityEngine;
using Utils;

namespace Net
{
    public class NetConnection
    {
        private WWW _www;
        
        public void LoadText(string url, Action<string> onComplete = null, Action<object> onError = null)
        {
            Log.d("Start net: "+url);
            Loader.Me.StartCoroutine(CoroutineLoadText(url, onComplete, onError));
        }

        public void LoadImage(string url, Action<Texture2D> onComplete = null, Action<object> onError = null)
        {
            Loader.Me.StartCoroutine(CoroutineLoadImage(url, onComplete, onError));
        }

        private IEnumerator CoroutineLoadText(string url, Action<string> onComplete = null, Action<object> onError = null)
        {
            _www = new WWW(url);
            while(!_www.isDone)
                yield return null;

            if (NetManager.Connections.Contains(this))
            {
                if (_www.error != null)
                {
                    Log.d(_www.error);
                    if (onError != null)
                        onError.Invoke(_www.error);
                }
                else if (onComplete != null)
                {
                    onComplete.Invoke(_www.text);
                }
            }
            NetManager.Connections.Remove(this);
            LoadingUtil.Hide();
        }

        private IEnumerator CoroutineLoadImage(string url, Action<Texture2D> onComplete = null, Action<object> onError = null)
        {
            _www = new WWW(url);
            while (!_www.isDone)
                yield return null;

            if (NetManager.Connections.Contains(this))
            {
                if (_www.error != null)
                {
                    Log.d(_www.error);
                    if (onError != null)
                        onError.Invoke(_www.error);
                }
                else if (onComplete != null)
                {
                    Texture2D _texture2D = new Texture2D(360, 360);
                    _www.LoadImageIntoTexture(_texture2D);
                    onComplete.Invoke(_texture2D);
                }
            }
            NetManager.Connections.Remove(this);
            LoadingUtil.Hide();
        }

        public void Cancel()
        {
            NetManager.Connections.Remove(this);
            LoadingUtil.Hide();
        }
    }
}