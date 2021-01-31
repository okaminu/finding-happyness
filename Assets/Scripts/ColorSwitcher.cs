using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ColorSwitcher : MonoBehaviour
{
    public string FocusedLayer = "Colored";

    public GameObject colorCamera;
    public GameObject cinematicObject1;
    public GameObject cinematicObject2;
    public GameObject cinematicObject3;
    public PostProcessVolume volume;
    ColorGrading colorGradingLayer = null;
    private bool finishedDisablingColor = false;
    private bool finishedEnablingColor = false;
    private bool finishedEnablingFog = false;
    private bool finishedDisabling1Fog = false;
    private bool finishedDisabling2Fog = false;
    private bool sawCinematic1 = false;
    private bool sawCinematic2 = false;
    private bool sawCinematic3 = false;
    float smoothTime = 2.5f;
    float yVelocityFog = 0.0f;
    float yVelocityFog2 = 0.0f;
    float yVelocityColor = 0.0f;
    float yVelocityColor2 = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGetSettings(out colorGradingLayer);
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(cinematicObject1.transform.position, transform.position) <= 5 && !sawCinematic1) {
            sawCinematic1 = true;   
        }

        if (sawCinematic1 && !finishedDisablingColor && !finishedEnablingFog)
        {
            disableColor();
            enableFog();
        }

        if (Vector3.Distance(cinematicObject2.transform.position, transform.position) <= 4 && !sawCinematic2)
        {
            sawCinematic2 = true;
        }

        if (sawCinematic2 && !finishedDisabling1Fog)
        {
            disableFog();
        }

        if (Vector3.Distance(cinematicObject3.transform.position, transform.position) <= 5 && !sawCinematic3)
        {
            sawCinematic3 = true;
        }



        if (sawCinematic3 && !finishedEnablingColor && !finishedDisabling2Fog)
        {
            disableFog2();
            enableColor();
        }
    }

    void disableFog()
    {
        if (RenderSettings.fogDensity > 0.25f)
        {
            double newFogDensity = Mathf.SmoothDamp((RenderSettings.fogDensity * 100) * -1, 0.25f * 100, ref yVelocityFog2, 0.9f) / 100;
            RenderSettings.fogDensity = Mathf.Abs((float)(System.Math.Round(newFogDensity, 3)));
        }
        else
        {
            finishedDisabling1Fog = true;
        }
    }

    void disableFog2()
    {
        if (RenderSettings.fogDensity > 0.01f)
        {

            double newFogDensity = Mathf.SmoothDamp((RenderSettings.fogDensity * 100) * -1, 0, ref yVelocityFog2, 0.7f) / 100;
            RenderSettings.fogDensity = Mathf.Abs((float)(System.Math.Round(newFogDensity, 3)));
        }
        else
        {
            finishedDisabling2Fog = true;
        }
    }

    void disableColor()
    {
        if (colorGradingLayer.saturation.value >= -100)
        {
            colorGradingLayer.saturation.value = Mathf.SmoothDamp(colorGradingLayer.saturation.value, -100, ref yVelocityColor, 0.4f);
        } else
        {
            finishedDisablingColor = true;
        }
    }
    void enableColor()
    {
        if (colorGradingLayer.saturation.value < 0)
        {
            colorGradingLayer.saturation.value = Mathf.SmoothDamp(colorGradingLayer.saturation.value, 3, ref yVelocityColor2, 0.8f);
        } else
        {
            finishedEnablingColor = true;
        }
    }

    void enableFog()
    {
        if (RenderSettings.fogDensity < 0.3)
        {
            double newFogDensity = Mathf.SmoothDamp((RenderSettings.fogDensity * 100), 300f, ref yVelocityFog, 3f) / 100;
            RenderSettings.fogDensity = (float)(System.Math.Round(newFogDensity, 3));
        } else {
            finishedEnablingFog = true;
        }
    }

}
