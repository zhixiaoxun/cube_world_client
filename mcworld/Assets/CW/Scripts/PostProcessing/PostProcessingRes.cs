using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingRes : MonoBehaviour {

    public PostProcessResources Resources = null;
    // Use this for initialization
    void Start () {
        if (Resources == null)
            Debug.LogError("There is not PostProcessResources component in this object!");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
