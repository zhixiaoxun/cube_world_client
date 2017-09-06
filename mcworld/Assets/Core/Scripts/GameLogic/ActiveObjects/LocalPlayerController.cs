using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.GameLogic.ActiveObjects;
using Core.GameLogic.ActiveObjects.FSM;
using Core.Config;
using Core.Utils;
using System;

namespace Core.GameLogic.ActiveObjects
{
    public class LocalPlayerController : PlayerController
    {
        protected override Vector3 GetMoveDir()
        {
            return GetMoveDirInput();
            //return GetMoveDirAuto();
        }

        private Vector3 GetMoveDirInput()
        {
            //触摸控件或者鼠标指向
            Vector3 dir;
            if (CoreEnv.inputMngr.JoyStickVec.magnitude > 0)
            {
                dir = (transform.forward * CoreEnv.inputMngr.JoyStickVec.y) + (transform.right * CoreEnv.inputMngr.JoyStickVec.x);
            }
            else
            {
                float h = Input.GetAxisRaw("Horizontal");
                float v = Input.GetAxisRaw("Vertical");

                Vector3 dirWalk = transform.forward * v;
                Vector3 dirStrafe = transform.right * h;
                dir = dirWalk + dirStrafe;
            }
            dir.y = 0;
            dir.Normalize();

            return dir;
        }

        private int lastDir = 1;
        private long lastTime = 0;
        private Vector3 GetMoveDirAuto()
        {
            //触摸控件或者鼠标指向
            Vector3 dir;
            if (CoreEnv.inputMngr.JoyStickVec.magnitude > 0)
            {
                dir = (transform.forward * CoreEnv.inputMngr.JoyStickVec.y) + (transform.right * CoreEnv.inputMngr.JoyStickVec.x);
            }
            else
            {
                float v = 0;
                float h = 0;
                long timeNow = TimeHelper.DateTimeToUnixTime(DateTime.Now);
                int interval = 500;
                if (lastDir == 0)
                    interval = 1000;

                if (timeNow - lastTime < interval)
                {
                    v = lastDir;
                }
                else
                {
                    lastTime = timeNow;
                    if (lastDir == 1)
                    {
                        lastDir = 0;
                    }
                    else if (lastDir == 0)
                    {
                        lastDir = -1;
                    }
                    else if (lastDir == -1)
                    {
                        lastDir = 1;
                    }
                }
                //float h = Input.GetAxisRaw("Horizontal");
                //float v = Input.GetAxisRaw("Vertical");

                Vector3 dirWalk = transform.forward * v;
                Vector3 dirStrafe = transform.right * h;
                dir = dirWalk + dirStrafe;
            }
            dir.y = 0;
            dir.Normalize();

            return dir;
        }
    }
}
