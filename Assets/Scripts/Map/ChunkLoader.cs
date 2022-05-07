using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    public Material material;

    private void Start()
    {
        generateWorld();
    }

    void generateWorld()
    {
        for (int x = 0; x < VoxelData.WORLD_SIZE_IN_CHUNKS; x++)
        {
            for (int z = 0; z < VoxelData.WORLD_SIZE_IN_CHUNKS; z++)
            {
                Chunk chunk = new Chunk(new ChunkCoordinates(x, z), this);
            }
        }
    }
}

[System.Serializable]
public class BlockType
{
    public string blockName;
    public bool isSolid;

    [Header("Texture values")]
    public int backfaceTexture;
    public int frontfaceTexture;
    public int topfaceTexture;
    public int bottomfaceTexture;
    public int leftfaceTexture;
    public int rightfaceTexture;


    public int getTextureId(int faceIndex)
    {
        switch (faceIndex)
        {
            case 0:
                return backfaceTexture;
            case 1:
                return frontfaceTexture;
            case 2:
                return topfaceTexture;
            case 3:
                return bottomfaceTexture;
            case 4:
                return leftfaceTexture;
            case 5:
                return rightfaceTexture;
            default:
                throw new System.Exception("Impossible de récupérer la texture");
        }
    }
}
