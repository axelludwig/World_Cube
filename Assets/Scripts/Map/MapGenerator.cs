using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public static int CHUNK_SIZE = 16;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < CHUNK_SIZE; i++)
        {
            for (int j = 0; j < CHUNK_SIZE; j++)
            {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Instantiate(cube, new Vector3(i, 0, j), transform.rotation, transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
