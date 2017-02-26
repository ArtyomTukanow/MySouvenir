using Managers;
using Net;
using UnityEngine;
using Utils;

public class Loader : MonoBehaviour
{

    public static MonoBehaviour Me;

	void Update ()
	{
	}

    void Start ()
    {
        Me = this;
        ApplicationManager.Start();
        NetManager.LoadText(NetManager.MainUrl + "try-parse.php", null, completeTryParse);
    }

    private void completeTryParse(string message)
    {
        Log.d(message);
    }
}
