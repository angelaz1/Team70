using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSkybox : MonoBehaviour
{
    public Material skyboxMaterial;

    private void Awake()
    {
        RenderSettings.skybox = skyboxMaterial;
        RenderSettings.skybox.SetColor("_Tint", Color.white);
    }
}
