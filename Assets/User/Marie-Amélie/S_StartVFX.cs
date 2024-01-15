using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class S_StartVFX : MonoBehaviour
{
    public VisualEffect vfx;
    // Start is called before the first frame update
    void Start()
    {
        vfx.pause = true;
        StartCoroutine(PlayVFX());
    }

    IEnumerator PlayVFX()
    {
        yield return new WaitForSeconds(6f);
        vfx.pause = false;
    }

}
