using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//#if UNITY_EDITOR
//[ExecuteInEditMode]
//#endif
public class WeatherSystem : MonoBehaviour {

    [Range(0,23)]
    public float CurrentTime = 12;
    public GameObject Sun = null;
    [Range(0, 5)]
    [SerializeField]
    public float[] AtmosphereThickness = new float[24];
    [Range(0, 8)]
    [SerializeField]
    public float[] Exposure = new float[24];
    [SerializeField]
    public Color[] SkyTint = new Color[24];
    // Use this for initialization
    void Start () {
        int ceil = Mathf.CeilToInt(CurrentTime);
        int floor = Mathf.FloorToInt(CurrentTime);
        float percent = CurrentTime - floor;
        UpdateShaderParams(floor, ceil, percent);
        UpdateLightParams();
    }
	
	// Update is called once per frame
	void Update () {

        int ceil = Mathf.CeilToInt(CurrentTime);
        int floor = Mathf.FloorToInt(CurrentTime);
        float percent = CurrentTime - floor;
        UpdateShaderParams(floor, ceil, percent);
        UpdateLightParams();
    }

    void UpdateShaderParams(int floor, int ceil, float percent)
    {
        float currentAtmosphereThickness = Mathf.Lerp(AtmosphereThickness[floor], AtmosphereThickness[ceil], percent);
        float currentExposure = Mathf.Lerp(Exposure[floor], Exposure[ceil], percent);
        Color currentSkyTint = Color.Lerp(SkyTint[floor], SkyTint[ceil], percent);
        RenderSettings.skybox.SetFloat("_AtmosphereThickness", currentAtmosphereThickness);
        RenderSettings.skybox.SetFloat("_Exposure", currentExposure);
        RenderSettings.skybox.SetColor("_SkyTint", currentSkyTint);
    }
    void UpdateLightParams()
    {
        if (Sun != null)
        {
            float factor = (CurrentTime < 12 ?  CurrentTime % 12 : 12 - CurrentTime % 12) / 12.0f;
            float intensity = Mathf.Cos(Mathf.PI + Mathf.PI*0.5f*factor) + 1;
            Sun.GetComponent<Light>().intensity = intensity;
            float clamp = Mathf.Clamp(factor * 0.5f, 0.01f, 0.8f);
            RenderSettings.ambientSkyColor = new Color(clamp, clamp, clamp);
            RenderSettings.ambientEquatorColor = new Color(clamp, clamp, clamp);
            RenderSettings.ambientGroundColor = new Color(clamp, clamp, clamp);

            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0,0, (CurrentTime / 24.0f) * 360.0f  + 180.0f);
            this.transform.localRotation = rotation;
        }
    }
}
