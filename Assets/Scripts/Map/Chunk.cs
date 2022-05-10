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

    private bool _isActive;
    public bool isVoxelMapPopulated = false;

    public Chunk(ChunkCoordinates coordinates, ChunkLoader chunkLoader, bool generateOnLoad)
    {
        this.chunkLoader = chunkLoader;
        this.coordinates = coordinates;
        isActive = true;

        if(generateOnLoad)
        {
            Init();
        }

    }

    public void Init()
    {
        chunkObject = new GameObject();
        chunkObject.name = "Chunk " + coordinates.x + ", " + coordinates.z;
        meshFilter = chunkObject.AddComponent<MeshFilter>();
        meshRenderer = chunkObject.AddComponent<MeshRenderer>();
        meshCollider = chunkObject.AddComponent<MeshCollider>();
        meshRenderer.material = chunkLoader.material;
        meshCollider.sharedMesh = meshFilter.mesh;
        chunkObject.transform.SetParent(chunkLoader.transform);
        chunkObject.transform.position = new Vector3(coordinates.x * VoxelData.CHUNK_WIDTH, 0f, coordinates.z * VoxelData.CHUNK_WIDTH);

        populateVoxelMap();
        UpdateChunk();
        
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
        isVoxelMapPopulated = true;

    }
    void UpdateChunk()
    {
        ClearMeshData();

        for (int y = 0; y < VoxelData.CHUNK_HEIGHT; y++)
        {
            for (int x = 0; x < VoxelData.CHUNK_WIDTH; x++)
            {
                for (int z = 0; z < VoxelData.CHUNK_WIDTH; z++)
                {
                    if(BlockTypes.blockTypes[voxelMap[x, y, z]].isSolid)
                    {
                        UpdateMeshData(new Vector3(x, y, z));
                    }
                }
            }
        }

        createMesh();
    }

    void ClearMeshData()
    {
        vertexIndex = 0;
        vertices.Clear();
        triangles.Clear();
        uvs.Clear();
    }

    public void DestroyVoxel(Vector3 pos)
    {
        EditVoxel(pos, 0);
    }

    public void EditVoxel(Vector3 pos, byte newID)
    {
        int xCheck = Mathf.FloorToInt(pos.x);
        int yCheck = Mathf.FloorToInt(pos.y);
        int zCheck = Mathf.FloorToInt(pos.z);

        xCheck -= Mathf.FloorToInt(chunkObject.transform.position.x);
        zCheck -= Mathf.FloorToInt(chunkObject.transform.position.z);

        voxelMap[xCheck, yCheck, zCheck] = newID;

        UpdateSurroundingVoxels(xCheck, yCheck, zCheck);

        UpdateChunk();
    }

    void UpdateSurroundingVoxels(int x, int y, int z)
    {
        Vector3 thisVoxel = new Vector3(x, y, z);
        for (int i = 0; i < 6; i++)
        {
            Vector3 currentVoxel = thisVoxel + VoxelData.faceChecks[i];
            if (!IsVoxelInChunk((int)currentVoxel.x, (int)currentVoxel.y, (int)currentVoxel.z))
            {
                chunkLoader.getChunkFromVector3(thisVoxel + position).UpdateChunk();
            }
        }
    }

    /// <summary>
    /// Vérifie si le voxel aux coordonnées données est solide ou non
    /// </summary>
    bool checkVoxel(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x);
        int y = Mathf.FloorToInt(pos.y);
        int z = Mathf.FloorToInt(pos.z);
        if (!IsVoxelInChunk(x, y, z))
        {
            return chunkLoader.CheckForVoxel(pos + position);
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

    public byte GetVoxelFromGlobalVector3(Vector3 pos)
    {
        int xCheck = Mathf.FloorToInt(pos.x);
        int yCheck = Mathf.FloorToInt(pos.y);
        int zCheck = Mathf.FloorToInt(pos.z);

        xCheck -= Mathf.FloorToInt(chunkObject.transform.position.x);
        zCheck -= Mathf.FloorToInt(chunkObject.transform.position.z);

        return voxelMap[xCheck, yCheck, zCheck];
    }



    void UpdateMeshData(Vector3 pos)
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
        get { return _isActive; }
        set {
            _isActive = value;
            if(chunkObject != null)
            {
                chunkObject.SetActive(value); 
            }
        }
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

    public ChunkCoordinates()
    {
        x = 0;
        z = 0;
    }

    public ChunkCoordinates(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public ChunkCoordinates(Vector3 pos)
    {
        int xCheck = Mathf.FloorToInt(pos.x);
        int zCheck = Mathf.FloorToInt(pos.z);

        x = xCheck / VoxelData.CHUNK_WIDTH;
        z = zCheck / VoxelData.CHUNK_WIDTH;
    }

    public bool Equals(ChunkCoordinates other)
    {
        if (other == null) return false;
        if (other.x == x && other.z == z) return true;
        return false;
    }
}
