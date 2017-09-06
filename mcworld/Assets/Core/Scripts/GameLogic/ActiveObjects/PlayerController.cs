using UnityEngine;
using Core.Config;
using Core.GameLogic.ActiveObjects.FSM;
using Core.Utils;

namespace Core.GameLogic.ActiveObjects
{
    public class PlayerController : MonoBehaviour
    {
        protected bool bGoingTarget = false;
        protected Vector3 targetPos = Vector3.zero;
        protected Vector3 targetDir = Vector3.zero;
        protected bool bNeedJump = false;
        public bool bInClimbRegion = false;
        public Transform curLadderTrans = null;
        public Player player;
        public float _MoveSpeed = 2.5f; // unit per second

        protected bool m_bTouchGround = false;
        protected float m_fJumpSpeed = 0;
        public Vector3 m_vMoveSpeed;

        public float m_fJumpHeight = 1.0f;//跳跃高度
        public float m_fGravity = 20f;//重力加速度

        protected void Start()
        {
            m_fJumpSpeed = Mathf.Sqrt(2.0f * m_fGravity * m_fJumpHeight);//根据跳跃高度调整跳跃初始速度
        }

        protected void Update()
        {
            UpdateMovement();
            UpdatePosition();
        }

        public void SetTargetPos(Vector3 pos)
        {
            targetPos = pos;

            if (Vector3.Distance(targetPos, transform.position) <= 0.01f)
            {
                bGoingTarget = false;
            }
            else
            {
                bGoingTarget = true;
            }
        }

        public void SetTargetDir(Vector3 dir)
        {
            targetDir = dir;
        }

        public void NeedJump(bool bNeed)
        {
            bNeedJump = bNeed;
        }

        protected virtual Vector3 GetMoveDir()
        {
            if (!bGoingTarget)
                return Vector3.zero;
            
            return targetPos - transform.position;
        }

        protected virtual void UpdateMovement()
        {
            if (!player._IsReady)
                return;

            if (player._LogicState == LogicStateDef.ATTACK)
                return;

            //触摸控件或者鼠标指向
            Vector3 dir = GetMoveDir();

            //计算移动速度
            if (m_bTouchGround)
            {
                m_vMoveSpeed.x = dir.x * _MoveSpeed;
                m_vMoveSpeed.z = dir.z * _MoveSpeed;
                m_vMoveSpeed.y = 0.0f;
            }
            else
            {
                m_vMoveSpeed.y -= m_fGravity * Time.deltaTime;
            }

            //攀爬状态下单独处理
            if (bInClimbRegion && isClimbing(dir, curLadderTrans.forward, transform.position, curLadderTrans.position))
            {
                //处于跳跃状态进入攀爬则等待跳跃过程完成
                if (player._LogicState == LogicStateDef.JUMP)
                {
                    return;
                }
                //player._PlayerRigidbody.useGravity = false;

                dir.x = 0;
                dir.y = 6.0f;
                dir.z = 0;
                player.Run();
                transform.Translate(dir * Time.deltaTime, Space.World);
            }
            else
            {
                //移动角色
                if (player._LogicState == LogicStateDef.JUMP)
                {
                    player._PlayerCharaContrl.Move(m_vMoveSpeed * Time.deltaTime);
                }
                else
                {
                    if ( (player is LocalPlayer && Input.GetKey(KeyCode.Space)) || !(player is LocalPlayer) && bNeedJump
                        && m_bTouchGround)
                    {
                        player.Jump();
                        m_vMoveSpeed.y = m_fJumpSpeed;
                        bNeedJump = false;
                        //m_vMoveSpeed.y = Mathf.Sqrt(2.0f * m_fGravity * m_fJumpHeight);
                    }
                    else if (m_vMoveSpeed.magnitude <= 0.05f)
                    {
                        player.Idle();
                    }
                    else
                    {
                        player.Run();
                    }

                    player._PlayerCharaContrl.Move(m_vMoveSpeed * Time.deltaTime);

                }
            }

            if(!(player is LocalPlayer) && transform.forward != targetDir)
                transform.forward = targetDir;

            if (m_vMoveSpeed != Vector3.zero)
            {
                //m_bTouchGround = player.m_characterController.collisionFlags.HasFlag(CollisionFlags.CollidedBelow);
                m_bTouchGround = player._PlayerCharaContrl.isGrounded;
            }

            if (Vector3.Distance(transform.position, targetPos) <= 0.01f)
            {
                bGoingTarget = false;
                targetPos = Vector3.zero;
            }
        }

        void UpdatePosition()
        {
            if (!player._IsReady)
                return;

            if (player is LocalPlayer)
                (player as LocalPlayer).UpdatePosition();
        }

        protected bool isClimbing(Vector3 moveDir, Vector3 ladderDir, Vector3 playerPos, Vector3 ladderPos)
        {
            if (!bInClimbRegion)
                return false;
            if (moveDir == Vector3.zero || ladderDir == Vector3.zero)
                return false;

            //行进方向与楼梯正方向夹角足够小，而且玩家相对楼梯的方位与玩家前进方向不在同一边才能被判定为攀爬
            float absAngle = System.Math.Abs(Vector3.Angle(moveDir, ladderDir));
            float absDisAngle = System.Math.Abs(Vector3.Angle((playerPos - ladderPos), moveDir));
            if (absAngle <= 60 && absDisAngle > 90)
            {
                return true;
            }
            else if (absAngle >= 120 && absDisAngle > 90)
            {
                return true;
            }

            return false;
        }
    }
}