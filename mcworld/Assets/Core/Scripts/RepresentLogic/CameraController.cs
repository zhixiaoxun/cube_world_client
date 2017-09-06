using UnityEngine;
using Core.Utils;
using Core.Config;

namespace Core.RepresentLogic
{
    public class CameraController : MonoBehaviour
    {
        private Camera mainCamera = null;
        public Transform target = null;
        public float maxDistance = 4;
        public float rotationX = 0;
        public float rotationY = 0;

        public float sensitivityX = 15.0f;
        public float sensitivityY = 15.0f;

        public float minimumX = -360.0f;
        public float maximumX = 360.0f;

        public float minimumY = -89.0f;
        public float maximumY = 89.0f;

        private float rotationYaxis = 0.0f;
        private float rotationXaxis = 0.0f;

        private Quaternion originalCameraRotation;
        private Quaternion originalPlayerRotation;

        private bool bFirstPerson;
        public bool IsFirstPerson
        {
            get
            {
                return bFirstPerson;
            }
            set
            {
                bFirstPerson = value;
                UpdateCameraMode();
            }
        }

        // Use this for initialization
        void Start()
        {
            mainCamera = Camera.main;

            mainCamera.nearClipPlane = 0.01f;

            if (target == null)
            {
                target = ObjHelper.GetChild(transform, "camera_slot");
                SetDefaultCamPos();
            }

            //mainCamera.transform.parent = transform;
            mainCamera.transform.parent = target;

            IsFirstPerson = false;
            originalCameraRotation = mainCamera.transform.localRotation;
            originalPlayerRotation = transform.localRotation;
        }

        public void UpdateCameraMode()
        {
            if (!bFirstPerson)
                mainCamera.transform.position = target.position + (-target.forward) * maxDistance;
            else
                mainCamera.transform.position = target.position;
        }

        void SetDefaultCamPos()
        {
            Quaternion q = Quaternion.identity;
            // Quaternion.Euler()
            q.eulerAngles = new Vector3(rotationX, rotationY, 0);
            Vector3 vecCamera = -(Vector3.forward);
            vecCamera = q * vecCamera;
            mainCamera.gameObject.transform.position = target.position + Vector3.Normalize(vecCamera) * maxDistance;
            mainCamera.gameObject.transform.forward = Vector3.Normalize(target.position - mainCamera.gameObject.transform.position);
        }

        private void Update()
        {
            UpdateCameraRotation();
        }

        private void UpdateCameraRotation()
        {
            if (!CoreEnv.inputMngr.MobileInput)
            {
                // Read the mouse input axis
                rotationYaxis += CoreEnv.inputMngr.SwipeVec.x * sensitivityX;
                rotationXaxis += CoreEnv.inputMngr.SwipeVec.y * sensitivityY;

                rotationYaxis = ClampAngle(rotationYaxis, minimumX, maximumX);
                rotationXaxis = ClampAngle(rotationXaxis, minimumY, maximumY);
            }
            else
            {
                rotationYaxis += CoreEnv.inputMngr.SwipeVec.x;
                rotationXaxis += CoreEnv.inputMngr.SwipeVec.y;

                rotationYaxis = ClampAngle(rotationYaxis, minimumX, maximumX);
                rotationXaxis = ClampAngle(rotationXaxis, minimumY, maximumY);
            }

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationYaxis, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationXaxis, Vector3.left);


            //mainCamera.transform.localRotation = originalCameraRotation * yQuaternion;
            //transform.localRotation = originalPlayerRotation * xQuaternion;

            target.transform.localRotation = originalCameraRotation * yQuaternion;
            transform.localRotation = originalPlayerRotation * xQuaternion;

            //if (!IsFirstPerson && rotationXaxis > 0)
            //{
            //    mainCamera.gameObject.transform.position = target.position - target.forward * maxDistance * Mathf.Lerp(1.0f, 0.0f, rotationXaxis / 90.0f);
            //}

        }

        private const float pi = 3.141592654f;
        private void LateUpdate()
        {

            //第三人称时做遮挡检测
            if (!IsFirstPerson)
            {
                float nDistanceCT = maxDistance;

                //向上看时缩小Camera和Target的距离
                float nPlayerHeight = 0.9f;
                float nScaleAngle = 45.0f;
                if (rotationXaxis > Mathf.Asin(nPlayerHeight / nDistanceCT) * 180.0 / pi)
                {
                    nDistanceCT = nPlayerHeight / Mathf.Sin(rotationXaxis * pi / 180.0f);
                    if (rotationXaxis > nScaleAngle)
                    {
                        nDistanceCT = nDistanceCT * Mathf.Lerp(1.0f, 0.0f, Mathf.Sin((rotationXaxis - nScaleAngle) / (90.0f - nScaleAngle) / 2.0f * pi));
                    }

                }

                //遮挡测试
                RaycastTileResult nRet;
                Vector3 nOriginPosition = target.position - target.forward * nDistanceCT;
                nRet = RaycastTile(target.position, nOriginPosition, true, true);

                if (nRet.m_bHit)
                {
                    mainCamera.gameObject.transform.position = nRet.m_vHitPosition;
                }
                else
                {
                    mainCamera.gameObject.transform.position = target.position - target.forward * nDistanceCT;
                }
            }
        }

        public struct RaycastTileResult
        {
            public bool m_bHit;
            public Vector3 m_vHitPosition;

        }

        //使用Physics.Linecast做遮挡测试
        public RaycastTileResult RaycastTile(Vector3 from, Vector3 end, bool ignore1, bool ignore2)
        {
            RaycastTileResult nRet;
            nRet.m_bHit = false;
            nRet.m_vHitPosition = Vector3.zero;
            RaycastHit nTemp;
            nRet.m_bHit = Physics.Linecast(from, end, out nTemp, ~(1 << 2));

            if (nRet.m_bHit)
            {
                nRet.m_vHitPosition = nTemp.point + 0.1f * nTemp.normal;
            }

            return nRet;
        }

        public static float ClampAngle(float angle, float min, float max)
        {
            if (angle < -360F)
                angle += 360F;
            if (angle > 360F)
                angle -= 360F;
            return Mathf.Clamp(angle, min, max);
        }
    }
}