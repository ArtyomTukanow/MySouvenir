using System;
using UnityEngine;

namespace Assets.Scripts.Event
{
    public class EventDispatcher
    {

        public static event Action<float,float> onTouch;
        public static event Action onTouchUp;
        public static event Action<float,float> onTouchDown;

        public static void addOnTouch(Action<float, float> call)
        {
            onTouch += call;
        }

        public static void addOnTouchUp(Action call)
        {
            onTouchUp += call;
        }

        public static void addOnTouchDown(Action<float, float> call)
        {
            onTouchDown += call;
        }

        public static void removeFromTouch(Action<float, float> call)
        {
            onTouch -= call;
        }

        public static void removeFromTouchUp(Action call)
        {
            onTouchUp -= call;
        }

        public static void removeFromTouchDown(Action<float, float> call)
        {
            onTouchDown -= call;
        }

        private static bool _isTouched;
        public static void Update()
        {
            if (Input.touchCount > 0)
            {
                if(_isTouched)
                    onTouch(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
                else if(Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    _isTouched = true;
                    onTouchDown(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
                }
            }
            else if(_isTouched)
            {
                _isTouched = false;
                onTouchUp();
            }
        }
    }
}