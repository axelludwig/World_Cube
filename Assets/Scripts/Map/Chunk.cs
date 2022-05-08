using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk

{
    GameObject chunkObject;
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
    MeshCollider meshCollider;
    public ChunkCoordinates coordinates;

    int vertexIndex = 0;
    List < Vector3 > vertices = new List<Vector3>();
    List < int > triangles = new List<int>();
    List<Vector2> uvs = new List<Vector2>();

    byte[,,] voxelMap = new byte[VoxelData.CHUNK_WIDTH, VoxelData.CHUNK_HEIGHT, VoxelData.CHUNK_WIDTH];

    ChunkLoader chunkLoader;

    public Chunk(ChunkCoordinates coordinates, ChunkLoader chunkLoader)
    {
        this.chunkLoader = chunkLoader;
        this.coordinates = coordinates;
        chunkObject = new GameObject();
        chunkObject.name = "Chunk";
        meshFilter = chunkObject.AddComponent<MeshFilter>();
        meshRenderer = chunkObject.AddComponent<MeshRenderer>();
        meshCollider = chunkObject.AddComponent<MeshCollider>();
        meshRenderer.material = chunkLoader.material;
        chunkObject.transform.SetParent(chunkLoader.transform);
        chunkObject.transform.position = new Vector3(coordinates.x * VoxelData.CHUNK_WIDTH, 0f, coordinates.z * VoxelData.CHUNK_WIDTH) ;

        populateVoxelMap();
        createMeshData();
        createMesh();

        meshCollider.sharedMesh = meshFilter.mesh;
    }

    void populateVoxelMap()
    {

        for (int y = 0; y < VoxelData.CHUNK_HEIGHT; y++)
        {
            for (int x = 0; x < VoxelData.CHUNK_WIDTH; x++)
            {
                for (int z = 0; z < VoxelData.CHUNK_WIDTH; z++)
                {
                    voxelMap[x, y, z] = chunkLoader.getVoxel(new Vector3(x, y, z) + position);
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
                    if(BlockTypes.blockTypes[voxelMap[x, y, z]].isSolid)
                    {
                        addVoxelDataToChunk(new Vector3(x, y, z));
                    }
                }
            }
        }
    }

    /// <summary>
    /// Vérifie si le voxel aux coordonnées données est solide ou non
    /// </summary>
    bool checkVoxel(Vector3 voxel)
    {
        int x = Mathf.FloorToInt(voxel.x);
        int y = Mathf.FloorToInt(voxel.y);
        int z = Mathf.FloorToInt(voxel.z);
        if (!IsVoxelInChunk(x, y, z))
        {
            return BlockTypes.blockTypes[chunkLoader.getVoxel(voxel + position)].isSolid;
        }
        else
        {
            return BlockTypes.blockTypes[voxelMap[x,y,z]].isSolid;
        }
    }

    bool IsVoxelInChunk(int x, int y, int z)
    {

        if (x < 0 || x > VoxelData.CHUNK_WIDTH - 1 || y < 0 || y > VoxelData.CHUNK_HEIGHT - 1 || z < 0 || z > VoxelData.CHUNK_WIDTH - 1)
            return false;
        else return true;

    }



    void addVoxelDataToChunk(Vector3 pos)
    {
        for (int i = 0; i < 6; i++)
        {
            if(!checkVoxel(pos + VoxelData.faceChecks[i]))
            {
                byte blockId = voxelMap[(int)pos.x, (int)pos.y, (int)pos.z];

                vertices.Add(pos + VoxelData.cubeVoxelVertices[VoxelData.voxelTriangles[i, 0]]);
                vertices.Add(pos + VoxelData.cubeVoxelVertices[VoxelData.voxelTriangles[i, 1]]);
                vertices.Add(pos + VoxelData.cubeVoxelVertices[VoxelData.voxelTriangles[i, 2]]);
                vertices.Add(pos + VoxelData.cubeVoxelVertices[VoxelData.voxelTriangles[i, 3]]);
                addTexture(BlockTypes.blockTypes[blockId].getTextureId(i));
                triangles.Add(vertexIndex);
                triangles.Add(vertexIndex+1);
                triangles.Add(vertexIndex+2);
                triangles.Add(vertexIndex+2);
                triangles.Add(vertexIndex+1);
                triangles.Add(vertexIndex+3);
                vertexIndex += 4;
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
        uvs.Add(new Vector2(x + VoxelData.normalizedBlockTextureSize, y + VoxelData.normalizedBlockTextureSize));
    }

    public bool isActive
    {
        get { return chunkObject.activeSelf; }
        set { chunkObject.SetActive(value); }
    }

    public Vector3 position
    {
        get { return chunkObject.transform.position; }
    }
}

public class ChunkCoordinates
{
    public int x;
    public int z;

    public ChunkCoordinates(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public bool Equals(ChunkCoordinates other)
    {
        if (other == null) return false;
        if (other.x == x && other.z == z) return true;
        return false;
    }
}
