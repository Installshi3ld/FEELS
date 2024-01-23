using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class S_BehaviorAI : MonoBehaviour
{
    [SerializeField]
    private NavMeshData myNavMesh;

    private S_AIManager myAIManager;
    private FeelsMatchSprite myMatchSprite;

    private void MovingAround()
    {

    }

    private void Start()
    {
        StartCoroutine(EndingMyOwnSuffering());
    }

    IEnumerator EndingMyOwnSuffering()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }

    public void Toto(S_AIManager manager, FeelsMatchSprite identity)
    {
        myAIManager = manager;
        myMatchSprite = identity;
    }

    private void OmgIAmDyingFromCringe()
    {
        myAIManager.IAmDead(myMatchSprite);
    }

    private void OnDestroy()
    {
        OmgIAmDyingFromCringe();
    }
}
