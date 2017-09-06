using CodeStage.AdvancedFPSCounter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Core.Performance
{
    public class PerformanceManager : Singleton<PerformanceManager>
    {
        protected override IEnumerator OnInitCoroutine()
        {
            GameObject performObject = new GameObject("Performance");
            GameObject.DontDestroyOnLoad(performObject);
            //GameObject fpsObject = GameObject.Instantiate(Resources.Load("Utils/FPSCounter")) as GameObject;
            //fpsObject.transform.parent = performObject.transform;
            AFPSCounter.Instance.gameObject.transform.parent = performObject.transform;

            AFPSCounter.Instance.fpsCounter.Anchor = CodeStage.AdvancedFPSCounter.Labels.LabelAnchor.UpperLeft;
            AFPSCounter.Instance.memoryCounter.Anchor = CodeStage.AdvancedFPSCounter.Labels.LabelAnchor.UpperRight;
            AFPSCounter.Instance.versionInfoCounter.Anchor = CodeStage.AdvancedFPSCounter.Labels.LabelAnchor.UpperRight;
            AFPSCounter.Instance.deviceInfoCounter.Anchor = CodeStage.AdvancedFPSCounter.Labels.LabelAnchor.LowerLeft;

            AFPSCounter.Instance.deviceInfoCounter.CpuModel = true;
            AFPSCounter.Instance.deviceInfoCounter.GpuModel = true;
            AFPSCounter.Instance.deviceInfoCounter.RamSize = true;
            AFPSCounter.Instance.deviceInfoCounter.ScreenData = true;
            AFPSCounter.Instance.deviceInfoCounter.NetData = true;
            AFPSCounter.Instance.deviceInfoCounter.bShowLoaclIP = true;
            AFPSCounter.Instance.deviceInfoCounter.bShowNetInfo = true;
            AFPSCounter.Instance.deviceInfoCounter.bShowServerInfo = true;
            AFPSCounter.Instance.deviceInfoCounter.bShowNetState = true;

            AFPSCounter.Instance.fpsCounter.Enabled = true;
            AFPSCounter.Instance.memoryCounter.Enabled = true;
            AFPSCounter.Instance.versionInfoCounter.Enabled = true;
            AFPSCounter.Instance.deviceInfoCounter.Enabled = true;

            AFPSCounter.Instance.keepAlive = true;
            Camera testCam = AFPSCounter.Instance.gameObject.AddComponent<Camera>();
            AFPSCounter.Instance.gameObject.AddComponent<GUILayer>();
            testCam.depth = 55;
            testCam.cullingMask = (1 << AFPSCounter.Instance.gameObject.layer);
            testCam.clearFlags = CameraClearFlags.Depth;
            if (Screen.width >= 1280)
            {
                AFPSCounter.Instance.FontSize = 20;
            }

            yield return 1;
        }
    }
}
