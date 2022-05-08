using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    public Transform player;
    public Vector3 spawnPosition;

    public Material material;

    Chunk[,] chunks = new Chunk[VoxelData.WORLD_SIZE_IN_CHUNKS, VoxelData.WORLD_SIZE_IN_CHUNKS]; //Gros tableau sa mère qui contient tous les chunks
    List<ChunkCoordinates> activeChunks = new List<ChunkCoordinates>();
    ChunkCoordinates playerChunkCoordinates;
    ChunkCoordinates playerLastChunkCoords;

    private void Start()
    {
        spawnPosition = new Vector3((VoxelData.WORLD_SIZE_IN_CHUNKS*VoxelData.CHUNK_HEIGHT) / 2, VoxelData.CHUNK_HEIGHT, (VoxelData.WORLD_SIZE_IN_CHUNKS*VoxelData.CHUNK_WIDTH) / 2);
        generateWorld();
        playerLastChunkCoords = getChunkCoordinatesFromVector3(player.position);
    }

    private void Update()
    {
        playerChunkCoordinates = getChunkCoordinatesFromVector3(player.position);
        if(!playerChunkCoordinates.Equals(playerLastChunkCoords))
        {
            checkViewDistance();
        }
    }

    void generateWorld()
    {
        for (int x = VoxelData.WORLD_SIZE_IN_CHUNKS/2 - VoxelData.RENDER_DISTANCE_IN_CHUNKS; x < VoxelData.WORLD_SIZE_IN_CHUNKS / 2 + VoxelData.RENDER_DISTANCE_IN_CHUNKS; x++)
        {
            for (int z = VoxelData.WORLD_SIZE_IN_CHUNKS / 2 - VoxelData.RENDER_DISTANCE_IN_CHUNKS; z < VoxelData.WORLD_SIZE_IN_CHUNKS / 2 + VoxelData.RENDER_DISTANCE_IN_CHUNKS; z++)
            {
                generateChunk(x, z);
            }
        }
        player.position = spawnPosition;
    }

    public byte getVoxel(Vector3 pos)
    {
        if (pos.x < 0 || pos.x > VoxelData.WORLD_SIZE_IN_VOXELS - 1 || pos.y < 0 || pos.y > VoxelData.CHUNK_HEIGHT - 1 || pos.z < 0 || pos.z > VoxelData.WORLD_SIZE_IN_VOXELS - 1)
            return 0;
        if (pos.y < 1)
            return 1;
        else if (pos.y == VoxelData.CHUNK_HEIGHT - 1)
            return 3;
        else
            return 2;
    }

    public ChunkCoordinates getChunkCoordinatesFromVector3(Vector3 pos)
    {
        int x = Mathf.FloorToInt(pos.x / VoxelData.CHUNK_WIDTH);
        int z = Mathf.FloorToInt(pos.z / VoxelData.CHUNK_WIDTH);
        return new ChunkCoordinates(x, z);
    }

    void checkViewDistance()
    {
        ChunkCoordinates coords = getChunkCoordinatesFromVector3(player.position);
        List<ChunkCoordinates> previouslyActiveChunks = new List<ChunkCoordinates>();

        for (int x = coords.x - VoxelData.RENDER_DISTANCE_IN_CHUNKS; x < coords.x + VoxelData.RENDER_DISTANCE_IN_CHUNKS; x++)
        {
            for (int z = coords.z - VoxelData.RENDER_DISTANCE_IN_CHUNKS; z < coords.z + VoxelData.RENDER_DISTANCE_IN_CHUNKS; z++)
            {
                if(isChunkInWorld(new ChunkCoordinates(x, z)))
                {
                    if(chunks[x, z] == null)
                    {
                        generateChunk(x, z);
                    }
                    else if(!chunks[x, z].isActive)
                    {
                        chunks[x, z].isActive = true;
                        activeChunks.Add(new ChunkCoordinates(x, z));
                    }
                }

                for (int i = 0; i < previouslyActiveChunks.Count; i++)
                {
                    if (previouslyActiveChunks[i].x == x && previouslyActiveChunks[i].z == z)
                    {
                        previouslyActiveChunks.RemoveAt(i);
                    }
                }
            }
        }

        previouslyActiveChunks.ForEach(item => chunks[item.x, item.z].isActive = false);
    }

    void generateChunk(int x, int z)
    {
        ChunkCoordinates chunkCoordinates = new ChunkCoordinates(x, z);
        chunks[x, z] = new Chunk(chunkCoordinates, this); 
        activeChunks.Add(chunkCoordinates);
    }

    bool isChunkInWorld(ChunkCoordinates coordinates)
    {
        if(coordinates.x > 0 && coordinates.x < VoxelData.WORLD_SIZE_IN_CHUNKS -1 && coordinates.z > 0 && coordinates.z < VoxelData.WORLD_SIZE_IN_CHUNKS - 1)
        {
            return true;
        }
        return false;
    }

    bool isVoxelInWorld(Vector3 position)
    {
        if (position.x >= 0 && position.x < VoxelData.WORLD_SIZE_IN_VOXELS  && position.y >= 0 && position.y < VoxelData.CHUNK_HEIGHT && position.z >= 0 && position.z < VoxelData.WORLD_SIZE_IN_VOXELS)
        {
            return true;
        }
        return false;
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
