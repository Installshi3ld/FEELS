using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ManageLifeCycles : MonoBehaviour
{
    [SerializeField]
    private List<S_LifeCycleScriptableObject> cycles = new List<S_LifeCycleScriptableObject>();


    // Start is called before the first frame update
    void Start()
    {
        foreach(S_LifeCycleScriptableObject cycle in cycles)
        {
            Debug.Log("UWU");
        }
    }

    private IEnumerator manageLifeCycles()
    {
        yield return null;
    }
}
