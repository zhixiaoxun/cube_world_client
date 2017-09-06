using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Uniblocks
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class Blocks : MonoBehaviour
    {

        public enum EBlockType
        {
            EBT_Snow = 0,
            EBT_Empty
        }
        private EBlockType blockType = EBlockType.EBT_Empty;
        public EBlockType BlockType = EBlockType.EBT_Empty;
        public Texture2D[] TextureArray;
        private StateMgr stateMgr = null;
#if UNITY_EDITOR
        private State.EStateType stateType = State.EStateType.EST_Unknown;
        public State.EStateType StateType = State.EStateType.EST_Unknown;
#endif
        // Use this for initialization
        virtual protected void Start()
        {
            stateMgr = new StateMgr();
            stateMgr.Init(this);
            stateMgr.RegistState(new NormalState());
            stateMgr.RegistState(new DamageState());
            stateMgr.RegistState(new DisappearState());
        }

        // Update is called once per frame
        void Update()
        {
            if (IsHidden())
                return;
            UpdateData();
            UpdateLogic();
#if UNITY_EDITOR
            UpdateDebugLogic();
#endif
        }
        public bool IsHidden()
        {
            return transform.gameObject.layer == LayerMask.NameToLayer("Hidden");
        }
        public void HideBlock()
        {
            transform.gameObject.layer = LayerMask.NameToLayer("Hidden");
        }
        public void ShowBlock()
        {
            transform.gameObject.layer = LayerMask.NameToLayer("Blocks");
        }
        public State.EStateType GetLastStateType()
        {
            if (stateMgr.GetLastState() == null)
                return State.EStateType.EST_Unknown;
            return stateMgr.GetLastState().GetStateType();
        }
        public State.EStateType GetCurrentStateType()
        {
            if (stateMgr.GetCurrentState() == null)
                return State.EStateType.EST_Unknown;
            return stateMgr.GetCurrentState().GetStateType();
        }
        public void SetCurrentStateType(State.EStateType type)
        {
#if UNITY_EDITOR
            StateType = type;
#else
        stateMgr.SetStateType(type);
#endif
        }
        virtual protected void UpdateData()
        {
            if (blockType != BlockType)
            {
                blockType = BlockType;
                switch (blockType)
                {
                    case EBlockType.EBT_Empty:
                        stateMgr.SetStateType(State.EStateType.EST_Disappear);
                        break;
                    case EBlockType.EBT_Snow:
                        stateMgr.RegistState(new AccumulateState());
                        stateMgr.RegistState(new MeltState());
                        break;
                    default:
                        {
                            this.gameObject.GetComponent<Renderer>().sharedMaterial.mainTexture = TextureArray[(int)blockType];
                            stateMgr.SetStateType(State.EStateType.EST_Normal);
                        }
                        break;
                }
            }
        }
#if UNITY_EDITOR
        virtual protected void UpdateDebugLogic()
        {
            if (stateType != StateType)
            {
                stateType = StateType;
                stateMgr.SetStateType(stateType);
            }
        }
#endif
        virtual protected void UpdateLogic()
        {
            switch (blockType)
            {
                case EBlockType.EBT_Snow:

                    break;
                default:
                    {

                    }
                    break;
            }
            stateMgr.Update();
        }
    }
}
