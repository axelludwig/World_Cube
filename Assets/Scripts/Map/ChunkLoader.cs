using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    public Material material;
    public BlockType[] blockTypes = { new BlockType { blockName = "Default", isSolid = true } };
}

[System.Serializable]
public class BlockType
{
    public string blockName;
    public bool isSolid;
}
