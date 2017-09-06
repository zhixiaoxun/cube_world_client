using System.Collections;
using System.Collections.Generic;
using SLua;
using Core.Utils.Log;

[CustomLuaClass]
public abstract class SingletonBase
{
    public abstract IEnumerator InitCoroutine();
    public abstract bool Init();
    public abstract void Exit();

    public bool Login()
    {
        return OnLogin();
    }

    public void Logout()
    {
        OnLogout();
    }

    public void Update()
    {
        OnUpdate();
    }

    public void LateUpdate()
    {
        OnLateUpdate();
    }

    public void FixedUpdate()
    {
        OnFixedUpdate();
    }

    public void EndOfFrame()
    {
        OnEndOfFrame();
    }

    protected virtual IEnumerator OnInitCoroutine()
    {
        OnInit();
        yield return null;
    }

    protected virtual bool OnInit()
    {
        return true;
    }

    protected virtual void OnExit()
    {
    }

    protected virtual bool OnLogin()
    {
        return true;
    }

    protected virtual void OnLogout()
    {
    }

    protected virtual void OnUpdate()
    {
    }

    protected virtual void OnLateUpdate()
    {
    }

    protected virtual void OnFixedUpdate()
    {
    }

    protected virtual void OnEndOfFrame()
    {
    }
}

/*
 * 单例
 * 构造时机受控制的单例
 */
[CustomLuaClass]
public abstract class Singleton<T> : SingletonBase where T : Singleton<T>, new()
{
    protected static T _instance = null;

#if UNITY_EDITOR
    public static T InitInstance()
    {
        if (_instance == null)
            new T().Init();

        return _instance;
    }
#endif

    public override string ToString()
    {
        return typeof(T).Name;
    }

    public static T Instance
    {
        get
        {
#if UNITY_EDITOR && !DISABLE_INSTANCE_AUTO_NEW
            InitInstance();
#endif
            return _instance;
        }
    }

    public override IEnumerator InitCoroutine()
    {
        if (_instance == null)
            _instance = this as T;
        else
            LogHelper.ERROR("Singleton", "Singleton of Type:{0} has been created", typeof(T).Name);

        yield return OnInitCoroutine();
    }

    public override bool Init()
    {
        LogHelper.DEBUG("Singleton", "Singleton<{0}>.InitInstance()", typeof(T).Name);

        if (_instance == null)
            _instance = this as T;
        else
            LogHelper.DEBUG("Singleton", "Singleton of Type:{0} has been created", typeof(T).Name);

        return OnInit();
    }

    public override void Exit()
    {
        OnExit();

        if (_instance != this as T)
        {
            LogHelper.DEBUG("Singleton", "object of Type:{0} is not the instance", typeof(T).Name);
            return;
        }

        _instance = null;
    }
}


public delegate IEnumerator UpdateProgressDelegate(string name, int step, int count);

public class SingletonBatch
{
    List<SingletonBase> _singletons = new List<SingletonBase>();
    bool _init = false;

    delegate bool TraverseDelegate(SingletonBase singleton);

    static TraverseDelegate _loginHandler = new TraverseDelegate((singleton) => { return singleton.Login(); });
    static TraverseDelegate _logoutHandler = new TraverseDelegate((singleton) => { singleton.Logout(); return true; });
    static TraverseDelegate _updateHandler = new TraverseDelegate((singleton) => { singleton.Update(); return true; });
    static TraverseDelegate _lateUpdateHandler = new TraverseDelegate((singleton) => { singleton.LateUpdate(); return true; });
    static TraverseDelegate _fixedHandler = new TraverseDelegate((singleton) => { singleton.FixedUpdate(); return true; });
    static TraverseDelegate _endOfFrameHandler = new TraverseDelegate((singleton) => { singleton.EndOfFrame(); return true; });

    public int Count
    {
        get { return _singletons.Count; }
    }

    public void Add(SingletonBase singleton)
    {
        _singletons.Add(singleton);
    }

    public IEnumerator InitCoroutine(UpdateProgressDelegate updateProgressHandler, int step, int count)
    {
        Log("OnInitCoroutine({0}, {1})", step, count);

        for (int i = 0, c = _singletons.Count; i < c; ++i)
        {
            var singleton = _singletons[i];

            yield return singleton.InitCoroutine();

            yield return updateProgressHandler(singleton.ToString(), step + i, count);
        }

        _init = true;
    }

    public bool Init()
    {
        Log("Init");

        int count = _singletons.Count;
        int successCount = 0;
        for (int i = 0; i < count; ++i)
        {
            var singleton = _singletons[i];
            if (!singleton.Init())
                continue;
            ++successCount;
        }
        _init = true;
        return successCount == count;
    }

    public void Exit()
    {
        Log("Exit");

        for (int i = _singletons.Count - 1; i >= 0; --i)
            _singletons[i].Exit();

        _singletons.Clear();
    }

    bool Traverse(string log, TraverseDelegate handler)
    {
        // Log(log);

        int count = _singletons.Count;
        int successCount = 0;
        for (int index = 0; index < count; ++index)
        {
            if (!handler(_singletons[index]))
                continue;
            ++successCount;
        }

        return successCount == count;
    }

    public bool Login()
    {
        if (!_init)
            return false;

        return Traverse("Login", _loginHandler);
    }

    public void Logout()
    {
        if (!_init)
            return;

        Traverse("Logout", _logoutHandler);
    }

    public void Update()
    {
        if (!_init)
            return;

        Traverse("Update", _updateHandler);
    }

    public void LateUpdate()
    {
        if (!_init)
            return;

        Traverse("LateUpdate", _lateUpdateHandler);
    }

    public void FixedUpdate()
    {
        if (!_init)
            return;

        Traverse("FixedUpdate", _fixedHandler);
    }

    public void EndOfFrame()
    {
        if (!_init)
            return;

        Traverse("EndOfFrame", _endOfFrameHandler);
    }

    void Log(string format, params object[] values)
    {
        LogHelper.DEBUG("Singleton", "SingletonBatch" + string.Format(format, values));
    }
}

public class SingletonManager
{
    SingletonBatch[] _batches = new SingletonBatch[4];
    int _count = 0;

    delegate bool TraverseDelegate(SingletonBatch batch);

    static TraverseDelegate _initHandler = new TraverseDelegate((batch) => { return batch.Init(); });
    static TraverseDelegate _loginHandler = new TraverseDelegate((batch) => { return batch.Login(); });
    static TraverseDelegate _logoutHandler = new TraverseDelegate((batch) => { batch.Logout(); return true; });
    static TraverseDelegate _updateHandler = new TraverseDelegate((batch) => { batch.Update(); return true; });
    static TraverseDelegate _lateUpdateHandler = new TraverseDelegate((batch) => { batch.LateUpdate(); return true; });
    static TraverseDelegate _fixedHandler = new TraverseDelegate((batch) => { batch.FixedUpdate(); return true; });
    static TraverseDelegate _endOfFrameHandler = new TraverseDelegate((batch) => { batch.EndOfFrame(); return true; });

    public SingletonManager()
    {

    }

    public void Add(int batchIndex, SingletonBase singleton)
    {
        var batch = _batches[batchIndex];
        if (batch == null)
        {
            batch = new SingletonBatch();
            _batches[batchIndex] = batch;
        }

        batch.Add(singleton);

        ++_count;
    }

    public IEnumerator InitCoroutine(int batchIndex = -1, UpdateProgressDelegate updateProgressHandler = null)
    {
        Log("InitCoroutine({0})", batchIndex);

        if (batchIndex == -1)
        {
            int step = 0;

            for (int i = 0, c = _batches.Length; i < c; ++i)
            {
                var batch = _batches[i];
                if (batch == null)
                    continue;

                yield return batch.InitCoroutine(updateProgressHandler, step, _count);

                step += batch.Count;
            }
        }
        else
        {
            var batch = _batches[batchIndex];
            if (batch == null)
                yield return null;

            int stepBase = 0;
            for (int i = 0, c = batchIndex; i < c; ++i)
                stepBase += _batches[i].Count;

            yield return batch.InitCoroutine(updateProgressHandler, stepBase, _count);
        }
    }

    bool Traverse(int batchIndex, TraverseDelegate handler)
    {
        if (batchIndex == -1)
        {
            int count = _batches.Length;
            int successCount = 0;
            for (int i = 0; i < count; ++i)
            {
                var batch = _batches[i];
                if (batch != null && !handler(batch))
                    continue;
                ++successCount;
            }

            return successCount == count;
        }
        else
        {
            var batch = _batches[batchIndex];
            if (batch != null && handler(batch))
                return true;
            return false;
        }
    }

    bool Reverse(int batchIndex, TraverseDelegate handler)
    {
        if (batchIndex == -1)
        {
            int count = _batches.Length;
            int successCount = 0;
            for (int i = count - 1; i >= 0; --i)
            {
                var batch = _batches[i];
                if (batch != null && !handler(batch))
                    continue;
                ++successCount;
            }

            return successCount == count;
        }
        else
        {
            var batch = _batches[batchIndex];
            if (batch != null && handler(batch))
                return true;
            return false;
        }
    }

    public bool Init(int batchIndex = -1)
    {
        Log("Init");

        return Traverse(batchIndex, _initHandler);
    }

    public bool Login(int batchIndex = -1)
    {
        Log("Login");

        return Traverse(batchIndex, _loginHandler);
    }

    public void Logout(int batchIndex = -1)
    {
        Log("Logout");

        Reverse(batchIndex, _logoutHandler);
    }

    public void Update(int batchIndex = -1)
    {
        // Log("Update");

        Traverse(batchIndex, _updateHandler);
    }

    public void LateUpdate(int batchIndex = -1)
    {
        // Log("LateUpdate");

        Traverse(batchIndex, _lateUpdateHandler);
    }

    public void FixedUpdate(int batchIndex = -1)
    {
        // Log("FixedUpdate");

        Traverse(batchIndex, _fixedHandler);
    }

    public void Exit(int batchIndex = -1)
    {
        Log("Exit");

        if (batchIndex == -1)
        {
            for (int i = _batches.Length - 1; i >= 0; --i)
            {
                var batch = _batches[i];
                if (batch == null)
                    continue;

                batch.Exit();
                _batches[i] = null;
            }
        }
        else
        {
            var batch = _batches[batchIndex];
            if (batch == null)
                return;

            batch.Exit();
            _batches[batchIndex] = null;
        }
    }

    public void EndOfFrame(int batchIndex = -1)
    {
        Log("EndOfFrame");

        Traverse(batchIndex, _endOfFrameHandler);
    }

    private void Log(string format, params object[] values)
    {
        LogHelper.DEBUG("Singleton", "SingletonBatch" + string.Format(format, values));
    }
}
