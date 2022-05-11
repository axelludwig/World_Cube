using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugScreen : MonoBehaviour
{
    ChunkLoader chunkLoader;
    TextMeshProUGUI text;
    float fps;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        chunkLoader = GameObject.Find("ChunkLoader").GetComponent<ChunkLoader>();
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFpsCounter();

        string debugText = "Menu de debug de chiasse";


        debugText += "\n" + fps + " fps";
        debugText += "\nCoordonnées gros : " + (int)ChunkLoader.player.transform.position.x + "," + (int)ChunkLoader.player.transform.position.y + "," + (int)ChunkLoader.player.transform.position.z;
        text.text = debugText;
    }

    void UpdateFpsCounter()
    {
        if (timer > 1f)
        {
            fps = (int)(1f / Time.unscaledDeltaTime);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
