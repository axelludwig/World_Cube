using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlockTypes
{
    public static readonly BlockType[] blockTypes = {
        new BlockType { blockName = "Air", isSolid = false, backfaceTexture = 0, frontfaceTexture = 0, topfaceTexture = 0, bottomfaceTexture = 0, leftfaceTexture = 0, rightfaceTexture = 0 },
        new BlockType { blockName = "Bedrock", isSolid = true, backfaceTexture = 9, frontfaceTexture = 9, topfaceTexture = 9, bottomfaceTexture = 9, leftfaceTexture = 9, rightfaceTexture = 9 },
        new BlockType { blockName = "Stone", isSolid = true, backfaceTexture = 0, frontfaceTexture = 0, topfaceTexture = 0, bottomfaceTexture = 0, leftfaceTexture = 0, rightfaceTexture = 0 },
        new BlockType { blockName = "Grass", isSolid = true, backfaceTexture = 2, frontfaceTexture = 2, topfaceTexture = 7, bottomfaceTexture = 2, leftfaceTexture = 2, rightfaceTexture = 2 },
        new BlockType { blockName = "Dirt", isSolid = true, backfaceTexture = 1, frontfaceTexture = 1, topfaceTexture = 1, bottomfaceTexture = 1, leftfaceTexture = 1, rightfaceTexture = 1 },
        new BlockType { blockName = "Wood", isSolid = true, backfaceTexture = 5, frontfaceTexture = 5, topfaceTexture = 6, bottomfaceTexture = 6, leftfaceTexture = 5, rightfaceTexture = 5 },
        new BlockType { blockName = "Sand", isSolid = true, backfaceTexture = 10, frontfaceTexture = 10, topfaceTexture = 10, bottomfaceTexture = 10, leftfaceTexture = 10, rightfaceTexture = 10 }
    };
}
