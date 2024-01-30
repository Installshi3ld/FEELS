using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]

using UnityEngine;

public class HeatWaveEffect : MonoBehaviour
{
    public Material heatWaveMaterial; // Assurez-vous d'assigner le matériau dans l'inspecteur

    public float intensity = 1.0f;
    public float speed = 1.0f;

    private void Update()
    {
        // Assurez-vous que le matériau est valide
        if (heatWaveMaterial != null)
        {
            // Passez les propriétés au shader
            heatWaveMaterial.SetFloat("_Intensity", intensity);
            heatWaveMaterial.SetFloat("_Speed", speed);
        }
    }
}