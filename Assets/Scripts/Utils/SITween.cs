using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Engine.Core.Util.SITween
{
    public class SITween
    {
        public enum EaseType
        {
            linear
        }

        private static List<SITween> tweens = new List<SITween>();
        private static List<SITween> tweensToAdd = new List<SITween>();
        private static List<SITween> tweensToRemove = new List<SITween>();

        private string id;
        private float time;
        private float runningTime = 0f;
        private float percentage = 0f;
        private bool isRunning = false;
        private bool needtoKill = false;
        private object target;

        private Action<object, SITween> onUpdate;
        private System.Action onComplete;

        public static void to(object target, Action<object, SITween> onUpdate, float time, System.Action onComplete = null)
        {      
            if (target != null && tweens != null)
            {
                SITween oldTween = null;
                foreach (SITween siTween in tweens)
                {
                    if (siTween.target != null && siTween.target == target)
                    {
                        oldTween = siTween;
                        break;
                    }
                }
                if (oldTween != null)
                {
                    //Debug.Log("Remove old tween: " + oldTween.id);
                    tweensToRemove.Add(oldTween);
                }                   
            }
           
            SITween regTween = new SITween();
            regTween.id = GenerateID();
            regTween.time = time;
            regTween.isRunning = true;
            regTween.target = target;
            regTween.onUpdate = onUpdate;
            regTween.onComplete = onComplete;
            //Debug.Log("Start tween: " + regTween.id);
            tweensToAdd.Add(regTween);
        }
       
        private void UpdatePercentage()
        {
            runningTime += Time.deltaTime;
            percentage = runningTime / time;
        }

        public void Update()
        {
            if (!isRunning || needtoKill)
            {
                return;
            }

            UpdatePercentage();

           // Debug.Log(String.Format("Update tween: {0} {1}", id, percentage));

            if (percentage < 1f)
            {             
                if (onUpdate != null)
                {               
                    onUpdate(target,this);
                }
            }
            else
            {
                //Debug.Log(String.Format("Complete tween: {0} {1}", id, percentage));
                if (onComplete != null)
                {                  
                    onComplete();
                }
                needtoKill = true;
            }
        }

        public static void UpdateTweens()
        {
            foreach (SITween siTween in tweensToAdd)
                tweens.Add(siTween);
            foreach (SITween siTween in tweensToRemove)
                tweens.Remove(siTween);

            List<SITween> tweenForKill = null;
            foreach (SITween siTween in tweens)
            {
                if (siTween.needtoKill)
                {
                    if (tweenForKill == null)
                    {
                        tweenForKill = new List<SITween>();
                    }
                    tweenForKill.Add(siTween);
                    continue;
                }
                siTween.Update();
            }

            if (tweenForKill != null)
            {
                foreach (SITween siTween in tweenForKill)
                {
                    //Debug.Log(String.Format("Kill tween: {0}", siTween.id));
                    tweens.Remove(siTween);
                }
            }
        }

        private static string GenerateID()
        {
            return System.Guid.NewGuid().ToString();
        }

        public float getTweenValue(float x, float y, EaseType easeType)
        {
            if (percentage >= 1f)
            {
                return y;
            }

            float result = x;
            switch (easeType)
            {
                case EaseType.linear:
                    result = linear(x, y, percentage);
                    break;
            }
            return result;
        }

        #region Easing Curves

        private float linear(float start, float end, float value)
        {
            return Mathf.Lerp(start, end, value);
        }
      
        #endregion

    }
}
