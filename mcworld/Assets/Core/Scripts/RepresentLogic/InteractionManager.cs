using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Core.Config;

namespace Core.RepresentLogic
{
    public class InteractionManager : Singleton<InteractionManager>
    {
        protected override IEnumerator OnInitCoroutine()
        {
            GameObject interactionObject = new GameObject("Interaction");
            GameObject.DontDestroyOnLoad(interactionObject);

            // EasyTouch
            GameObject easyTouchObject = new GameObject("EasyTouch");
            easyTouchObject.transform.parent = interactionObject.transform;
            EasyTouch com = easyTouchObject.AddComponent<EasyTouch>();
            com.enableRemote = false;
            com.autoUpdatePickedUI = false;
            com.enabledNGuiMode = false;
            com.autoSelect = false;

            // InputManager
            GameObject inputObject = new GameObject("InputManager");
            GameObject.DontDestroyOnLoad(inputObject);
            inputObject.transform.parent = interactionObject.transform;
            CoreEnv.inputMngr = inputObject.AddComponent<InputManager>();

            yield return 1;
        }
    }
}
