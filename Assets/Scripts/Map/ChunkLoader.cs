using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    public Material material;
    public BlockType[] blockTypes = { 
        new BlockType { blockName = "Stone", isSolid = true, backfaceTexture = 0, frontfaceTexture = 0, topfaceTexture = 0, bottomfaceTexture = 0, leftfaceTexture = 0, rightfaceTexture = 0 },
        new BlockType { blockName = "Grass", isSolid = true, backfaceTexture = 2, frontfaceTexture = 2, topfaceTexture = 7, bottomfaceTexture = 2, leftfaceTexture = 2, rightfaceTexture = 2 },
        new BlockType { blockName = "Dirt", isSolid = true, backfaceTexture = 1, frontfaceTexture = 1, topfaceTexture = 1, bottomfaceTexture = 1, leftfaceTexture = 1, rightfaceTexture = 1 },
        new BlockType { blockName = "Wood", isSolid = true, backfaceTexture = 5, frontfaceTexture = 5, topfaceTexture = 6, bottomfaceTexture = 6, leftfaceTexture = 5, rightfaceTexture = 5 }
    };
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
