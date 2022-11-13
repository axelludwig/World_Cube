using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleLogger : BaseSingleton<ConsoleLogger>
{
    public GameObject ServerConsolePanel;
    private Text ServerConsoleText;
    private List<string> DisplayedTexts = new List<string>();
    public int maxLinesNumber;

    [HideInInspector]
    public bool IsInited = false;

    public void Init()
    {
        ServerConsoleText = ServerConsolePanel.transform.Find("ServerConsoleText").gameObject.GetComponent<Text>();

        if (!NetworkManager.Singleton.IsServer)
        {
            Destroy(this.gameObject);
        }
        else
        {
            ServerConsolePanel.SetActive(true);
        }

        if (maxLinesNumber == 0)
        {
            maxLinesNumber = 15;
        }

        IsInited = true;
        Log("Console initialized");

        for (int i = 0; i < 20; i++)
        {
            Log(i.ToString());
        }
    }

    public void Log(string text)
    {
        if (IsInited == false)
        {
            throw new System.Exception("Server console not initialized");
        }

        DisplayedTexts.Add(text);
        if (DisplayedTexts.Count > maxLinesNumber)
        {
            DisplayedTexts.RemoveAt(0);
        }

        ServerConsoleText.text = string.Join("\n", DisplayedTexts);
    }
}
