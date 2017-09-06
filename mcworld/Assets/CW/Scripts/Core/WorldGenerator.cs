using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniblocks;
using Core.GameLogic;
using Core.Utils.Log;
using Core.RepresentLogic;

public class WorldGenerator : TerrainGenerator {

    public override void GenerateVoxelData()
    {
        int chunky = chunk.ChunkIndex.y;
        int SideLength = Engine.ChunkSideLength;

        var block = World._Instance._BlockManager.GetBlock(new Vector3(chunk.ChunkIndex.x, chunk.ChunkIndex.y, chunk.ChunkIndex.z));
        if (block == null)
        {
            LogHelper.DEBUG("GenerateVoxelData", "block is null pos {0}", chunk.ChunkIndex);
            return;
        }
        //LogHelper.DEBUG("GenerateVoxelData", "pos {0}", chunk.ChunkIndex);
        
        for (int x = 0; x < SideLength; x++)
        {
            for (int y = 0; y < SideLength; y++)
            {
                for (int z = 0; z < SideLength; z++)
                {
                    var node = block.getNode((short)x, (short)y, (short)z);
                    if (node.getContent() > 2)
                    {
                        chunk.SetVoxelSimple(x, y, z, World._Instance._BlockManager.ContentID2NodeID(node.getContent()));
                    }
                }
            }
        }
    }
}
