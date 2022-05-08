using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoxelData
{
    public static readonly int CHUNK_WIDTH = 5;
    public static readonly int CHUNK_HEIGHT = 15;
    public static readonly int WORLD_SIZE_IN_CHUNKS = 100;
    public static readonly int RENDER_DISTANCE_IN_CHUNKS = 5;

    public static int WORLD_SIZE_IN_VOXELS
    {
        get { return WORLD_SIZE_IN_CHUNKS * CHUNK_WIDTH; }
    }


    public static readonly int textureAtlasSizeInBlocks = 4;
    public static float normalizedBlockTextureSize
    {
        get { return 1f / (float)textureAtlasSizeInBlocks; }
    }

    public static readonly Vector3[] cubeVoxelVertices = new Vector3[8] { //https://www.youtube.com/watch?v=h66IN1Pndd0&list=PLVsTSlfj0qsWEJ-5eMtXsYp03Y9yF1dEn&t=400s
        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 1.0f, 1.0f),
        new Vector3(0.0f, 1.0f, 1.0f),
    };

    public static readonly Vector3[] faceChecks = new Vector3[6] {
        new Vector3(0.0f, 0.0f, -1.0f),//Face arrière du cube
        new Vector3(0.0f, 0.0f, 1.0f),//Face avant du cube
        new Vector3(0.0f, 1.0f, 0.0f),//Face du haut du cube
        new Vector3(0.0f, -1.0f, 0.0f),//Face du bas du cube
        new Vector3(-1.0f, 0.0f, 0.0f),//Face gauche du cube
        new Vector3(1.0f, 0.0f, 0.0f) //Face droite du cube
    };

    public static readonly int[,] voxelTriangles = new int[6, 4] {
        {0,3,1,2},//Face arrière du cube
        {5,6,4,7},//Face avant du cube
        {3,7,2,6},//Face du haut du cube
        {1,5,0,4},//Face du bas du cube
        {4,7,0,3},//Face gauche du cube
        {1,2,5,6}//Face droite du cube
    };

    public static readonly Vector2[] voxelUvs = new Vector2[4]
    {
        new Vector2(0.0f, 0.0f),
        new Vector2(0.0f, 1.0f),
        new Vector2(1.0f, 0.0f),
        new Vector2(1.0f, 1.0f),
    };
}
