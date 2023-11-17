using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class S_GivePosition : MonoBehaviour, IConsoleCommand
{
    [SerializeField] private string commandWord = string.Empty;

    string IConsoleCommand.CommandWord => commandWord;

    /*
    [SerializeField]
    private S_PositionCommand positionCommand;

    [SerializeField]
    private List<GameObject> gameObjectsToFollow = new List<GameObject>();

    [SerializeField]
    private List<string> gameObjectsToFollowName = new List<string>();*/

    public bool Processed(string[] args)
    {
        //StartCoroutine(givePositionEveryXSecond());
        return true;
    }

    /*private void giveObjectPosition(string commandName)
    {
        bool isThereAMatch = false;

        foreach(string gameObjectCommandName in gameObjectsToFollowName)
        {
            if (gameObject.name == commandName)
            {
                isThereAMatch = true;
                StartCoroutine(givePositionEveryXSecond(go, howLon));
            }
        }

        if(!isThereAMatch)
        {
            Debug.Log("Invalid name: none object found");
        }
        
    }*/
    private IEnumerator givePositionEveryXSecond(GameObject go, float howLong)
    {
        while(howLong >= 0)
        {
            Debug.Log("Working ?");
            Debug.Log(go.transform.position);
            howLong--;

            yield return new WaitForSeconds(1f);
        }

    }


}
