using UnityEngine;

public class LifeCycle : MonoBehaviour
{
    private SingletonManager _singletons = new SingletonManager();

    public SingletonManager Singletons
    {
        get { return _singletons; }
    }

    public void Add(int batchIndex, SingletonBase singleton)
    {
        _singletons.Add(batchIndex, singleton);
    }

    void Start()
    {

    }
    void Update()
    {
        _singletons.Update();
    }
    void LateUpdate()
    {
        _singletons.LateUpdate();
    }
    void FixedUpdate()
    {
        _singletons.FixedUpdate();
    }
    void EndOfFrame()
    {
        _singletons.EndOfFrame();
    }
}
