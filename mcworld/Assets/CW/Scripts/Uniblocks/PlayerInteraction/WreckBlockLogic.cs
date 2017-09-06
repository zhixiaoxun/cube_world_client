using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Uniblocks
{
    public class WreckBlockLogic : MonoBehaviour
    {
        private int frame = 0;
        private float frameTimer = 0.2f;
        private VoxelInfo wreckedVoxelInfo = null;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (wreckedVoxelInfo != null)
            {
                frameTimer -= Time.deltaTime;
                if (frameTimer < 0)
                {
                    frame++;
                    frameTimer = 0.2f;
                }
                if (frame >= 11)
                {
                    Voxel.DestroyBlock(wreckedVoxelInfo);
                    wreckedVoxelInfo = null;
                    frame = 0;
                    
                }
                this.gameObject.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0.0625f * frame, 0));
            }
        }

        public void BeginWreck(VoxelInfo voxelInfo)
        {
            frame = 0;
            frameTimer = 0.2f;
            wreckedVoxelInfo = voxelInfo;
        }

        public void StopWreck(VoxelInfo voxelInfo)
        {
            frame = 0;
            frameTimer = 0.2f;
            this.gameObject.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0.0625f * frame, 0));
            this.gameObject.GetComponent<Renderer>().material = this.gameObject.GetComponent<Renderer>().sharedMaterial;
            wreckedVoxelInfo = null;
        }

        public void OnLook(VoxelInfo voxelInfo)
        {
            if (wreckedVoxelInfo != null && !wreckedVoxelInfo.index.IsEqual(voxelInfo.index))
                StopWreck(wreckedVoxelInfo);
        }
    }
}
