using UnityEngine;

namespace Core.GameLogic.ActiveObjects
{
    public class BulletComponent : MonoBehaviour
    {
        public float speed = 10;
        public Transform target;
        public Vector3 targetPosition;
        public bool isSetup = false;
        public System.Action OnDestroy = null;

        public void Setup(Transform target, float speed, Vector3 targetPosition)
        {
            this.target = target;

            this.targetPosition = targetPosition;

            if (target != null)
            {
                targetPosition = target.position;
            }
            this.speed = speed;

            isSetup = true;
        }

        void Update()
        {
            if (!isSetup) return;
            //射向目标
            if (target != null)
            {
                targetPosition = target.position;
            }

            transform.LookAt(targetPosition);
            float step = speed * Time.deltaTime;
            float distance = Vector3.Distance(targetPosition, transform.position);

            if (distance < step)
            {
                //到达目标
                transform.position = targetPosition;
                if (OnDestroy != null)
                    OnDestroy();
                GameObject.Destroy(gameObject);
            }
            else
            {
                transform.Translate(step * transform.forward, Space.World);
            }

        }
    }
}