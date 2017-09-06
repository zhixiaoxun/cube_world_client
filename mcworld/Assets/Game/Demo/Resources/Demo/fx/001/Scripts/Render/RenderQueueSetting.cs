using UnityEngine;
using System.Collections;

public class RenderQueueSetting : MonoBehaviour
{

    public enum RenderQuee
    {
        Background = 1000,
        Geometry = 2000,
        AlphaTest = 2450,
        Transparent = 3000,
        Overlay = 4000,
    }
    
    private Renderer m_renderer;

    public RenderQuee m_RenderQuee = RenderQuee.Transparent;
    public int m_nQueueID = 0;
    public bool m_bSharedMaterial = true;
	// Use this for initialization
	void Start () {
        m_renderer = transform.GetComponent<Renderer>();
        if (m_bSharedMaterial)
            m_renderer.sharedMaterial.renderQueue = (int)m_RenderQuee + m_nQueueID;
        else
            m_renderer.material.renderQueue = (int)m_RenderQuee + m_nQueueID;

       
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_EDITOR
        if (m_bSharedMaterial)
            m_renderer.sharedMaterial.renderQueue = (int)m_RenderQuee + m_nQueueID;
        else
            m_renderer.material.renderQueue = (int)m_RenderQuee + m_nQueueID;
#endif
	}
}
