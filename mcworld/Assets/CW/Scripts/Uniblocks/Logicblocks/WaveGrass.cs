using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class WaveGrass : MonoBehaviour {

    [Range(0, 15)]
    public float WindSpeed = 10;
    [Range(0, 5)]
    public float WaveSize = 1.8f;
    [Range(0, 5)]
    public float WindAmount = 1;
    private Vector4 WaveAndDistance;
    private float randtimeAdd = 0;
    // Use this for initialization
    void Start () {
        WaveAndDistance = new Vector4(WindSpeed, WaveSize, WindAmount, 1);
        randtimeAdd = Random.Range(0, 100.0f);
    }
	
	// Update is called once per frame
	void Update () {
        float amount = Mathf.Sin(Time.time + randtimeAdd) * Mathf.Cos(Time.time + randtimeAdd) + WindAmount;
        WaveAndDistance.x = WindSpeed;
        WaveAndDistance.y = WaveSize; 
        WaveAndDistance.z = amount;
        gameObject.GetComponent<Renderer>().material.SetVector("_WaveAndDistance", WaveAndDistance);
        if(Camera.main != null)
            gameObject.GetComponent<Renderer>().material.SetVector("_CameraPosition", Camera.main.transform.position);

    }
}
