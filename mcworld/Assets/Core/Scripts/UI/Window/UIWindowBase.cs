using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Core.UI
{
    public class UIWindowBase : MonoBehaviour, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        // 是否独占
        public bool isExclusive = false;
        // RectTransform组件
        public RectTransform rectTransform = null;

        // Text控件列表
        public Dictionary<string, Text> txtMap = new Dictionary<string, Text>();

        // Button控件列表
        public Dictionary<string, Button> btnMap = new Dictionary<string, Button>();

        // 界面背景图
        public Image imgBackGround = null;

        public virtual void Awake()
        {
            imgBackGround = gameObject.GetComponent<Image>();
            RegisterWindowElemEvent();
        }

        public virtual void Start()
        {
        }

        public virtual void RegisterWindowElemEvent()
        {
            // Debug.Log("=================================" + gameObject.name);
            //Text[] texts = GameObject.FindObjectsOfTypeAll(Type.GetType(Text));

            RectTransform[] clildrenObjects = gameObject.GetComponentsInChildren<RectTransform>();
            foreach (RectTransform childTransform in clildrenObjects)
            {
                GameObject child = childTransform.gameObject;
                Text textComponent = child.GetComponent<Text>();
                Button buttonComponent = child.GetComponent<Button>();

                if (textComponent != null)
                {
                    txtMap[child.name] = textComponent;
                }
                if (buttonComponent != null)
                {
                    RegisterButtonClickEvent(child.name, buttonComponent);
                }

                //Debug.Log(child.name);
            }

            //int childCount = gameObject.transform.childCount;
            //for (int i = 0; i < childCount; ++i )
            //{
            //	Transform childTransform = gameObject.transform.GetChild(i);
            //	GameObject child = childTransform.gameObject;

            //	Text textComponent = child.GetComponent<Text>();
            //	Button buttonComponent = child.GetComponent<Button>();

            //	if (textComponent != null)
            //	{
            //		txtMap[child.name] = textComponent;
            //	}
            //	if (buttonComponent != null)
            //	{
            //		RegisterButtonClickEvent(child.name, buttonComponent);
            //	}

            //	Debug.Log(child.name);
            //}

        }

        public virtual void RegisterButtonClickEvent(string btnName, Button buttonComponent)
        {
            btnMap[btnName] = buttonComponent;
            buttonComponent.onClick.AddListener(
                delegate ()
                {
                    OnButtonClick(btnName);
                }
            );
        }

        public virtual void SetImageBackGround(Sprite sprite)
        {
            imgBackGround.overrideSprite = sprite;
        }

        public virtual void SetImageBackGround(string objName, Sprite sprite)
        {
            GameObject obj = GameObject.Find(objName);
            if (obj != null)
            {
                Image image = obj.GetComponent<Image>();
                if (image != null)
                {
                    image.overrideSprite = sprite;
                }
            }
        }

        public virtual void SetText(string txtName, string txtContent)
        {
            if (txtMap.ContainsKey(txtName))
            {
                txtMap[txtName].text = txtContent;
            }
        }
        public virtual void SetText(string txtName, string txtContent, Color color)
        {
            if (txtMap.ContainsKey(txtName))
            {
                txtMap[txtName].text = txtContent;
                txtMap[txtName].color = color;
            }
        }
        public virtual void SetTextColor(string txtName, Color color)
        {
            if (txtMap.ContainsKey(txtName))
            {
                txtMap[txtName].color = color;
            }
        }
        public virtual string GetText(string txtName)
        {
            if (txtMap.ContainsKey(txtName))
            {
                return txtMap[txtName].text;
            }
            return null;
        }
        public virtual void SetButtonText(GameObject obj, string txtContent, Color color, string btnTextName = "Text")
        {
            obj.transform.Find(btnTextName).GetComponent<Text>().text = txtContent;
            obj.transform.Find(btnTextName).GetComponent<Text>().color = color;
        }

        public virtual void SetButtonText(string btnName, string txtContent, string btnTextName = "Text")
        {
            if (!btnMap.ContainsKey(btnName))
                return;

            btnMap[btnName].gameObject.transform.Find(btnTextName).GetComponent<Text>().text = txtContent;
        }

        public virtual void SetButtonText(GameObject obj, string txtContent, string btnTextName = "Text")
        {
            obj.transform.Find(btnTextName).GetComponent<Text>().text = txtContent;
        }
        public virtual void SetButtonImage(GameObject obj, Sprite sprite)
        {
            obj.GetComponent<Image>().overrideSprite = sprite;
        }
        public virtual void SetButtonImage(string btnName, Sprite sprite)
        {
            if (!btnMap.ContainsKey(btnName))
                return;

            btnMap[btnName].gameObject.GetComponent<Image>().overrideSprite = sprite;
        }
        public virtual void SetButtonColor(GameObject obj, Color color)
        {
            obj.GetComponent<Image>().color = color;
        }
        public virtual void SetButtonColor(string btnName, Color color)
        {
            if (!btnMap.ContainsKey(btnName))
                return;
            btnMap[btnName].GetComponent<Image>().color = color;
        }
        public virtual Color GetButtonColor(GameObject obj)
        {
            return obj.GetComponent<Image>().color;
        }
        public virtual void SetElemScale(GameObject obj, float scale)
        {
            obj.transform.localScale = new Vector3(scale, scale, 1);
        }
        public virtual void SetButtonEnable(string btnName, bool isEnable)
        {
            btnMap[btnName].gameObject.SetActive(isEnable);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            //Debug.Log("OnPointerClick " + gameObject.name);
        }

        public virtual void OnButtonClick(string strObjName)
        {
            //Debug.Log("UIWindow.OnButtonClick " + strObjName);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            //Debug.Log("OnPointerUp " + gameObject.name);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            //Debug.Log("OnPointerDown " + gameObject.name);
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            //Debug.Log("OnBeginDrag " + gameObject.name);
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            //Debug.Log("OnDrag " + gameObject.name);
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            //Debug.Log("OnEndDrag " + gameObject.name);
        }

        public virtual void Update()
        {

        }

        public virtual void Init()
        {
            rectTransform.offsetMin = new Vector2(0.0f, 0.0f);
            rectTransform.offsetMax = new Vector2(0.0f, 0.0f);
            rectTransform.localScale = new Vector3(1, 1, 1);
        }

        public virtual void UnInit()
        {
            txtMap.Clear();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            OnShow();
        }

        public void Hide()
        {
            OnHide();
            gameObject.SetActive(false);
        }

        public bool IsHide()
        {
            return !gameObject.activeSelf;
        }

        public virtual void OnShow()
        {

        }

        public virtual void OnHide()
        {

        }

        static public void OpenWindow<T>(bool bExclusive = false)
        {
            Type type = typeof(T);
            string uiName = type.Name;
            //string uiName = UIManager.Instance.GetUINameByUIClassType(type);

            UIManager.Instance.OpenWindow(uiName, type);
        }

        static public void CloseWindow<T>(bool bDestory = false)
        {
            Type type = typeof(T);
            string uiName = type.Name;

            UIManager.Instance.CloseWindow(uiName, bDestory);
        }

        static public T GetWindow<T>() where T : UIWindowBase
        {
            Type type = typeof(T);
            UIWindowBase window = UIManager.Instance.GetWindow(type);

            if (window != null)
                return window as T;

            return null;
        }
    }
}

// 动态添加按钮
//void Start () {
//    List<string> btnsName = new List<string>();
//    btnsName.Add("BtnPlay");
//    btnsName.Add("BtnShop");
//    btnsName.Add("BtnLeaderboards");

//    foreach(string btnName in btnsName)
//    {
//        GameObject btnObj = GameObject.Find(btnName);
//        Button btn = btnObj.GetComponent<Button>();
//        btn.onClick.AddListener(delegate() {
//            this.OnClick(btnObj); 
//        });
//    } 
//}

//public void OnClick(GameObject sender)
//{
//    switch (sender.name)
//    {
//    case "BtnPlay":
//        Debug.Log("BtnPlay");
//        break;
//    case "BtnShop":
//        Debug.Log("BtnShop");
//        break;
//    case "BtnLeaderboards":
//        Debug.Log("BtnLeaderboards");
//        break;
//    default:
//        Debug.Log("none");
//        break;
//    }
//}