using Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class UILoading : UIWindowBase
    {
        private static UILoading m_instance = null;

        public static UILoading Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = GameObject.Find("UIStartLoading").GetComponent<UILoading>();
                }
                return m_instance;
            }
        }

        // 加载百分比
        public const string TXT_LOADING_PERCENT = "txtLoadingPercent";
        // 加载信息
        public const string TXT_LOADING_INFO = "txtLoadingInfo";
        // 加载进度条前景
        public const string IMG_LOADING_PROGRESS_FG = "imgForceground";
        // 加载文件信息
        public const string TXT_LOADING_TIP = "txtLoadingTip";
        // 加载信息Text组件
        Text txtLoadingInfo = null;
        // 加载文件Text组件
        Text txtLoadingSubInfo = null;
        // 加载百分比Text组件
        Text txtLoadingPercent = null;
        // 加载进度条前景对象
        Image objLoadingProgressFG = null;

        public string info = null;
        public int percent = 0;

        //int currentPercent = 0;
        //int targetPercent = 0;
        private GameObject fxObject = null;

        void OnDestroy()
        {
            txtLoadingInfo = null;
            txtLoadingPercent = null;
            objLoadingProgressFG = null;
            m_instance = null;

            if (fxObject != null)
            {
                DestroyImmediate(fxObject);
                fxObject = null;
            }
        }

        public override void Init()
        {
            base.Init();

            txtLoadingInfo = gameObject.transform.Find("imgBG/" + TXT_LOADING_INFO).gameObject.GetComponent<Text>();
            txtLoadingPercent = gameObject.transform.Find("imgBG/" + TXT_LOADING_PERCENT).gameObject.GetComponent<Text>();
            objLoadingProgressFG = gameObject.transform.Find("imgBG/pbLoading").Find(IMG_LOADING_PROGRESS_FG).GetComponent<Image>();
            txtLoadingSubInfo = gameObject.transform.Find("imgBG/" + TXT_LOADING_TIP).gameObject.GetComponent<Text>();
        }

        public void ShowGlobleLoadingUI(bool isShow)
        {
            gameObject.SetActive(isShow);
            if (fxObject == null)
            {
                //this.LoadFx(objLoadingProgressFG.transform, Vector3.zero, Quaternion.Euler(0, 0, 0), LoadFxResp);
            }
        }

        // 加载大状态
        public void SetLoadingInfo(string info)
        {
            this.info = info;
            txtLoadingInfo.text = this.info + this.percent.ToString() + "%";
        }
        // 加载子状态
        public void SetLoadingSubInfo(string info)
        {
            txtLoadingSubInfo.text = info;
        }
        public void SetLoadingStatus(int nPercent)
        {
            //targetPercent = nPercent;
            //txtLoadingPercent.text = nPercent.ToString() + "%";
            this.percent = nPercent;
            txtLoadingInfo.text = this.info + "..." + this.percent.ToString() + "%";

            float fScale = (float)nPercent / 100.0f;
            if (fScale > 1)
            {
                fScale = 1;
            }
            objLoadingProgressFG.fillAmount = fScale;

            if (fxObject != null)
            {
                float newX = -(objLoadingProgressFG.rectTransform.sizeDelta.x / 2) + (objLoadingProgressFG.rectTransform.sizeDelta.x * objLoadingProgressFG.fillAmount);

                if (newX != fxObject.transform.localPosition.x && objLoadingProgressFG.fillAmount < 1)
                {
                    fxObject.transform.localPosition = new Vector3(newX, 0, 0);
                }
                else if (objLoadingProgressFG.fillAmount >= 1)
                {
                    fxObject.SetActive(false);
                }
            }
        }

        private float CalcUIScalerFactor()
        {
            CanvasScaler scaler = GameObject.Find("Canvas").GetComponent("CanvasScaler") as CanvasScaler;
            var resolutionX = scaler.referenceResolution.x;
            var resolutionY = scaler.referenceResolution.y;
            return (Screen.width / resolutionX) * (1 - scaler.matchWidthOrHeight) + (Screen.height / resolutionY) * scaler.matchWidthOrHeight;
        }

    }
}
