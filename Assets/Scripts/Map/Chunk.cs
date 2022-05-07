using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour

{
    public MeshRenderer meshRenderer;
    public MeshFilter meshFilter;

    int vertexIndex = 0;
    List < Vector3 > vertices = new List<Vector3>();
    List < int > triangles = new List<int>();
    List<Vector2> uvs = new List<Vector2>();

    byte[,,] voxelMap = new byte[VoxelData.CHUNK_WIDTH, VoxelData.CHUNK_HEIGHT, VoxelData.CHUNK_WIDTH];

    ChunkLoader chunkLoader;

    void Start()
    {
        chunkLoader = GameObject.Find("ChunkLoader").GetComponent<ChunkLoader>();
        populateVoxelMap();
        createMeshData();
        createMesh();
    }


    bool checkVoxel(Vector3 voxel)
    {
        int x = Mathf.FloorToInt(voxel.x);
        int y = Mathf.FloorToInt(voxel.y);
        int z = Mathf.FloorToInt(voxel.z);
        if (x < 0 || x > VoxelData.CHUNK_WIDTH - 1 || y < 0 || y > VoxelData.CHUNK_HEIGHT - 1 || z < 0 || z > VoxelData.CHUNK_WIDTH - 1) return false;
        return chunkLoader.blockTypes[voxelMap[x,y,z]].isSolid;
    }

    void populateVoxelMap()
    {
        for (int y = 0; y < VoxelData.CHUNK_HEIGHT; y++)
        {
            for (int x = 0; x < VoxelData.CHUNK_WIDTH; x++)
            {
                for (int z = 0; z < VoxelData.CHUNK_WIDTH; z++)
                {
                    voxelMap[x, y, z] = 0;
                }
            }
        }
    }

    void createMeshData()
    {
        for (int y = 0; y < VoxelData.CHUNK_HEIGHT; y++)
        {
            for (int x = 0; x < VoxelData.CHUNK_WIDTH; x++)
            {
                for (int z = 0; z < VoxelData.CHUNK_WIDTH; z++)
                {
                    addVoxelDataToChunk(new Vector3(x, y, z));
                }
            }
        }
    }

    void addVoxelDataToChunk(Vector3 position)
    {
        for (int i = 0; i < 6; i++)
        {
            if(!checkVoxel(position+VoxelData.faceChecks[i]))
            {
                for (int j = 0; j < 6; j++)
                {
                    int triangleIndex = VoxelData.voxelTriangles[i, j];
                    vertices.Add(VoxelData.cubeVoxelVertices[triangleIndex] + position);
                    triangles.Add(vertexIndex);
                    vertexIndex++;
                }
                addTexture(0);
            }
        }
    }

    void createMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }

    void addTexture(int textureId)
    {
        float y = textureId / VoxelData.textureAtlasSizeInBlocks;
        float x = textureId - (y * VoxelData.textureAtlasSizeInBlocks);
        x *= VoxelData.normalizedBlockTextureSize;
        y *= VoxelData.normalizedBlockTextureSize;

        y = 1f - y - VoxelData.normalizedBlockTextureSize;
        uvs.Add(new Vector2(x, y));
        uvs.Add(new Vector2(x, y + VoxelData.normalizedBlockTextureSize));
        uvs.Add(new Vector2(x + VoxelData.normalizedBlockTextureSize, y));
        uvs.Add(new Vector2(x + VoxelData.normalizedBlockTextureSize, y));
        uvs.Add(new Vector2(x, y + VoxelData.normalizedBlockTextureSize));
        uvs.Add(new Vector2(x + VoxelData.normalizedBlockTextureSize, y+ VoxelData.normalizedBlockTextureSize));
    }
}
