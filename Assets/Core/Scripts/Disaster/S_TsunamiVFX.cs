using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;


[CreateAssetMenu(fileName = "SO_VFXTsunami", menuName = "SingletonContainer/VFX/Tsunami")]
public class S_TsunamiVFX : S_VFXElement
{
    public override void Triggers()
    {
        VisualEffect vfx = effect.GetComponent<VisualEffect>();
        vfx.pause = false;

        throw new System.NotImplementedException();
    }
}
