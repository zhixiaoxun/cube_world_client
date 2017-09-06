using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Triggers chunk spawning around the player.

namespace Uniblocks
{

    public class ChunkLoader : MonoBehaviour
    {

        private Index LastPos;
        private Index currentPos;
        private List<Index> CacheChunkIndexes = new List<Index>();
        void Awake()
        {

        }


        public void Update()
        {

            // don't load chunks if engine isn't initialized yet
            if (!Engine.Initialized || !ChunkManager.Initialized)
            {
                return;
            }

            // don't load chunks if multiplayer is enabled but the connection isn't established yet
            if (Engine.EnableMultiplayer)
            {
                if (!Network.isClient && !Network.isServer)
                {
                    return;
                }
            }

            // track which chunk we're currently in. If it's different from previous frame, spawn chunks at current position.

            currentPos = Engine.PositionToChunkIndex(transform.position);
            if (currentPos.IsEqual(LastPos) == false)
            {
                //ChunkManager.SpawnChunks(currentPos.x, currentPos.y, currentPos.z);

                StartCoroutine(ReplaceChunks(currentPos));

                // (Multiplayer) update server position
                if (Engine.EnableMultiplayer && Engine.MultiplayerTrackPosition && Engine.UniblocksNetwork != null)
                {
                    UniblocksClient.UpdatePlayerPosition(currentPos);
                }
            }
            LastPos = currentPos;
        }
        private IEnumerator ReplaceChunks(Index currentPos)
        {
            List<Index> NeedRemoveIndexes = new List<Index>();
            List<Index> NeedAddIndexes = new List<Index>();
            for (int k = -2; k <= 2; k++)
                for (int i = -2; i <= 2; i++)
                    for (int j = -2; j <= 2; j++)
                    {
                        Index index = new Index(currentPos.x + i, currentPos.y + k, currentPos.z + j);
                        NeedAddIndexes.Add(index);
                    }

            foreach (var index in CacheChunkIndexes)
            {
                if (NeedAddIndexes.Exists(p => p.IsEqual(index)) == false)
                    NeedRemoveIndexes.Add(index);
            }

            foreach (var index in NeedRemoveIndexes)
            {
                ReplaceChunkDatas(index);
            }
            NeedRemoveIndexes.Clear();
            yield return new WaitForEndOfFrame();

            foreach (var index in NeedAddIndexes)
            {
                ReplaceChunkPrefabs(index);
            }
            NeedAddIndexes.Clear();
            yield return new WaitForEndOfFrame();

            //Debug.LogError("CacheChunkIndexes count : " + CacheChunkIndexes.Count);
            yield return 0;
        }
        private void ReplaceChunkDatas(Index index)
        {
            Chunk chunk = ChunkManager.GetChunkComponent(index);
            if (chunk != null)
            {
                for (int i = 0; i < chunk.transform.childCount; i++)
                    DestroyObject(chunk.transform.GetChild(i).gameObject);
                int SideLength = Engine.ChunkSideLength;
                for (int x = 0; x < SideLength; x++)
                {
                    for (int y = 0; y < SideLength; y++)            ///高度
                    {
                        for (int z = 0; z < SideLength; z++)
                        { // 循环创建chunk中每一个Voxel

                            ushort data = chunk.GetVoxelSimple(x, y, z);
                            if (data > 200)
                            {
                                ushort span = 100;
                                chunk.SetVoxel(x, y, z, (ushort)(data - span), true);
                            }
                        }
                    }
                }
                if (CacheChunkIndexes.Exists(p => p.IsEqual(index)) == true)
                    CacheChunkIndexes.Remove(index);
            }
        }
        private void ReplaceChunkPrefabs(Index index)
        {
            if (CacheChunkIndexes.Exists(p => p.IsEqual(index)) == true)
                return;
            
            Chunk chunk = ChunkManager.GetChunkComponent(index);
            if (chunk != null)
            {
                int SideLength = Engine.ChunkSideLength;
                for (int x = 0; x < SideLength; x++)
                {
                    for (int y = 0; y < SideLength; y++)            ///高度
                    {
                        for (int z = 0; z < SideLength; z++)
                        { // 循环创建chunk中每一个Voxel

                            ushort data = chunk.GetVoxelSimple(x, y, z);
                            if (data > 100 && data <= 200)
                            {
                                ushort span = 100;
                                chunk.SetVoxel(x, y, z, (ushort)(data + span), true);
                            }
                        }
                    }
                }
                CacheChunkIndexes.Add(index);
            }
        }

        // multiplayer
        public void OnConnectedToServer()
        {
            if (Engine.EnableMultiplayer && Engine.MultiplayerTrackPosition)
            {
                StartCoroutine(InitialPositionAndRangeUpdate());
            }
        }

        IEnumerator InitialPositionAndRangeUpdate()
        {
            while (Engine.UniblocksNetwork == null)
            {
                yield return new WaitForEndOfFrame();
            }
            UniblocksClient.UpdatePlayerPosition(currentPos);
            UniblocksClient.UpdatePlayerRange(Engine.ChunkSpawnDistance);
        }
    }

}