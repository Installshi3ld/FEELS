using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeOfCons: MonoBehaviour

{

    public LeavesProperties[] leavesProperties;
    // Start is called before the first frame update
    void Start()

    {

        for (int i = 0; i < leavesProperties.Length; i++)
        {

            leavesProperties[i].Setup();
            AddButtonsListeners();

        }

    }



    private void AddButtonsListeners()
    {
        
        for (int index = 0; index < leavesProperties.Length; ++index)
        {
            if (leavesProperties[index].button != null)
                AddButtonListener(leavesProperties[index].button, index);
        }
    }

    private void AddButtonListener(Button button, int index)
    {
        button.onClick.AddListener(() =>
        {
            

            leavesProperties[index].renderer.material = leavesProperties[index].material;
;
        });
    }

}
