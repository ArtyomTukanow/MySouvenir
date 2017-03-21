﻿using Assets.Engine.Core.Util.SITween;
using Assets.Scripts.Event;
using Managers;
using Net;
using UnityEngine;
using Utils;

public class Loader : MonoBehaviour
{

    public static MonoBehaviour Me;

	void Update ()
	{
	    if (Input.GetKey(KeyCode.Escape))
	    {
	        if (ApplicationManager.UiManager.MenuIsOpened)
	        {
	            ApplicationManager.UiManager.closeMenu();
	        }
	        else if (ApplicationManager.UiManager.LastLayers.Count > 0)
	        {
	            ApplicationManager.UiManager.GoBackToLastLayer();
	        }
	        else
	        {
	            Application.Quit();
	        }
	    }

	    LoadingUtil.Update();
	    EventDispatcher.Update();
	    SITween.UpdateTweens();
	}

    void Start ()
    {
        Me = this;
        ApplicationManager.Start();
        NetManager.LoadText(NetManager.MainUrl + "try-parse.php", null, completeTryParse);
        LoadingUtil.Start();
    }

    private void completeTryParse(string message)
    {
        Log.d(message);
    }
}
