using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Core.RepresentLogic
{
    public class RepresentManager : Singleton<RepresentManager>
    {
        protected override IEnumerator OnInitCoroutine()
        {
            GameObject cameraObject = new GameObject("MainCamera");
            GameObject.DontDestroyOnLoad(cameraObject);
            Camera camera = cameraObject.AddComponent<Camera>();
            camera.clearFlags = CameraClearFlags.Skybox;
            camera.transparencySortMode = TransparencySortMode.Perspective;
            camera.fieldOfView = 60;
            camera.nearClipPlane = 0.3f;
            camera.farClipPlane = 1000;
            camera.depth = 0;
            camera.tag = "MainCamera";

            cameraObject.AddComponent<FlareLayer>();
            cameraObject.AddComponent<GUILayer>();
            cameraObject.AddComponent<AudioListener>();
            cameraObject.AddComponent<AmplifyOcclusionEffect>();
            PostProcessLayer postProcessLayer = cameraObject.AddComponent<PostProcessLayer>();
            if (postProcessLayer != null)
            {
                GameObject obj = GameObject.Find("PostProcessing");
                if (obj == null)
                    Debug.LogError("There is not PostProcessing object in the scene!");
                else
                {
                    var component = obj.GetComponent<PostProcessingRes>();
                    postProcessLayer.Init(component.Resources);
                    postProcessLayer.volumeTrigger = cameraObject.transform;
                    postProcessLayer.volumeLayer = LayerMask.GetMask("PostProcessing");
                    //postProcessLayer.ambientOcclusion.enabled = true;
                }
            }

            yield return 1;
        }
    }
}
