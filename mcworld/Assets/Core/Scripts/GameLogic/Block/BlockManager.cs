using System.Collections.Generic;
using Uniblocks;
using UnityEngine;
using Core.RepresentLogic;

namespace Core.GameLogic.Block
{
    public class BlockManager
    {
        public Dictionary<Vector3, IMapBlock> _BlockMap { get; private set; } = new Dictionary<Vector3, IMapBlock>();
        public Queue<Vector3> _BlockCreateQueue { get; private set; } = new Queue<Vector3>();
        public Queue<Vector3> _BlockDestroyQueue { get; private set; } = new Queue<Vector3>();

        private World _World = null;

        public void Init(World world)
        {
            _World = world;
        }

        public void UnInit()
        {

        }

        public void Active()
        {
            if (_BlockCreateQueue.Count > 0)
            {
                var blockPos = _BlockCreateQueue.Dequeue();
                if (blockPos != null)
                {
                    CreateBlock(blockPos);
                }
            }

            if (_BlockDestroyQueue.Count > 0)
            {
                var blockPos = _BlockDestroyQueue.Dequeue();
                if (blockPos != null)
                {
                    DestroyBlock(blockPos);
                }
            }
        }

        public IMapBlock AddBlock(Vector3 blockPos)
        {
            _BlockMap[blockPos] = _World._CppCore.GetBlock(blockPos);
            return _BlockMap[blockPos];
        }

        public IMapBlock GetBlock(Vector3 blockPos)
        {
            if (_BlockMap.ContainsKey(blockPos))
                return _BlockMap[blockPos];

            return _World._CppCore.GetBlock(blockPos);
        }

        public ushort ContentID2NodeID(ushort content)
        {
            return RepresentMap.Instance._ContentIDMap[_World._CppCore.GetNodeName(content)];
        }

        public void OnBlockReceived(Vector3 blockPos)
        {
            _BlockCreateQueue.Enqueue(blockPos);
            AddBlock(blockPos);
        }

        private void OnBlockDiscard(Vector3 blockPos)
        {
            _BlockDestroyQueue.Enqueue(blockPos);
        }

        private void CreateBlock(Vector3 blockPos)
        {
            ChunkManager.SpawnChunk((int)blockPos.x, (int)blockPos.y, (int)blockPos.z);
        }

        private void DestroyBlock(Vector3 blockPos)
        {
            ChunkManager.DestroyChunk((int)blockPos.x, (int)blockPos.y, (int)blockPos.z);
        }
    }
}
