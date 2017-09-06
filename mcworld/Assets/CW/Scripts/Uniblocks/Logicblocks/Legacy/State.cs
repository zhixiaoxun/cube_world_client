using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Uniblocks
{
    public abstract class State
    {
        public enum EStateType
        {
            EST_Normal = 0,          ///正常
            EST_Damage,              ///破坏
            EST_Disappear,           ///消失
            EST_Grow,                ///生长
            EST_Melt,                ///消融
            EST_Accumulate,          ///堆积    
            EST_Unknown
        }
        protected EStateType eStateType = EStateType.EST_Unknown;
        public EStateType GetStateType() { return eStateType; }

        public virtual void Enter(Blocks block) { }
        public virtual void Update(Blocks block) { }
        public virtual void Exit(Blocks block) { }
    }
    public class NormalState : State
    {
        public NormalState() { eStateType = EStateType.EST_Normal; }
        public override void Enter(Blocks block)
        {
            block.transform.localScale = Vector3.one;
            block.ShowBlock();
        }
        public override void Exit(Blocks block) { }
    }
    public class DamageState : State
    {
        public DamageState() { eStateType = EStateType.EST_Normal; }
        public override void Enter(Blocks block)
        {
        }
        public override void Update(Blocks block) { }
        public override void Exit(Blocks block) { }
    }
    public class DisappearState : State
    {
        public DisappearState() { eStateType = EStateType.EST_Disappear; }
        public override void Enter(Blocks block)
        {
            block.HideBlock();
        }
        public override void Update(Blocks block) { }
        public override void Exit(Blocks block) { }
    }
    public class GrowState : State
    {
        public GrowState() { eStateType = EStateType.EST_Grow; }
        public override void Enter(Blocks block) { }
        public override void Update(Blocks block) { }
        public override void Exit(Blocks block) { }
    }
    public class MeltState : State
    {
        private float fMeltTime = 20.0f;
        private float MeltTimer = 0.0f;
        public MeltState(float meltTime = 20.0f)
        {
            fMeltTime = meltTime;
            MeltTimer = 0.0f;
            eStateType = EStateType.EST_Melt;
        }
        public override void Enter(Blocks block)
        {
            if (block.GetLastStateType() == EStateType.EST_Accumulate)
            {
                if (block.transform.localScale.z < 1 && block.transform.localScale.z > 0)
                    MeltTimer = fMeltTime * (1.0f - block.transform.localScale.z);
            }
            else if (block.GetLastStateType() == EStateType.EST_Disappear)
            {
                block.SetCurrentStateType(EStateType.EST_Disappear);
                return;
            }
        }
        public override void Update(Blocks block)
        {
            MeltTimer += Time.deltaTime;
            if (MeltTimer >= fMeltTime)
                block.SetCurrentStateType(EStateType.EST_Disappear);
            else
            {
                float scaleZ = 1.0f - MeltTimer / fMeltTime;
                if (scaleZ < block.transform.localScale.z)
                {
                    if (scaleZ < 0.01f)
                        scaleZ = 0;
                    block.transform.localScale = new Vector3(1, 1, scaleZ);
                }
            }
        }
        public override void Exit(Blocks block)
        {
            MeltTimer = 0.0f;
        }

    }
    public class AccumulateState : State
    {
        private float fAccumulateTime = 20.0f;
        private float AccumulateTimer = 0.0f;
        public AccumulateState(float accumulateTime = 20.0f)
        {
            fAccumulateTime = accumulateTime;
            AccumulateTimer = 0.0f;
            eStateType = EStateType.EST_Accumulate;
        }
        public override void Enter(Blocks block)
        {
            if (block.GetLastStateType() == EStateType.EST_Melt)
            {
                if (block.transform.localScale.z < 1 && block.transform.localScale.z > 0)
                    AccumulateTimer = fAccumulateTime * block.transform.localScale.z;
            }
            else if (block.GetLastStateType() == EStateType.EST_Normal)
            {
                block.SetCurrentStateType(EStateType.EST_Normal);
                return;
            }
            else if (block.GetLastStateType() == EStateType.EST_Disappear)
            {
                block.transform.localScale = new Vector3(1, 1, 0);
            }

            block.ShowBlock();
        }
        public override void Update(Blocks block)
        {
            AccumulateTimer += Time.deltaTime;
            if (AccumulateTimer >= fAccumulateTime)
                block.SetCurrentStateType(EStateType.EST_Normal);
            else
            {
                float scaleZ = AccumulateTimer / fAccumulateTime;
                if (scaleZ > block.transform.localScale.z)
                {
                    if (scaleZ > 0.99f)
                        scaleZ = 1;
                    block.transform.localScale = new Vector3(1, 1, scaleZ);
                }
            }
        }
        public override void Exit(Blocks block)
        {
            AccumulateTimer = 0.0f;
        }
    }
    public class StateMgr
    {
        public State CurrentState = null;
        public State LastState = null;
        private Blocks ownerblock = null;
        public Dictionary<State.EStateType, State> StateDictionary = new Dictionary<State.EStateType, State>();
        public void Init(Blocks block)
        {
            ownerblock = block;
        }
        public void RegistState(State state)
        {
            if (!StateDictionary.ContainsKey(state.GetStateType()))
                StateDictionary[state.GetStateType()] = state;
        }
        public void UnRegistState(State state)
        {
            StateDictionary.Remove(state.GetStateType());
        }
        public void Update()
        {
            if (ownerblock != null && CurrentState != null)
                CurrentState.Update(ownerblock);
        }
        public State GetCurrentState()
        {
            return CurrentState;
        }
        public State GetLastState()
        {
            return LastState;
        }
        private void SetState(State state)
        {
            if (ownerblock == null || state == null || CurrentState == state)
                return;

            if (CurrentState != null)
                CurrentState.Exit(ownerblock);
            LastState = CurrentState;
            CurrentState = state;
            CurrentState.Enter(ownerblock);
        }
        public void SetStateType(State.EStateType type)
        {
            if (StateDictionary.ContainsKey(type))
                SetState(StateDictionary[type]);
            else
                Debug.LogError("You need regist this state before use.");
        }
    };
}