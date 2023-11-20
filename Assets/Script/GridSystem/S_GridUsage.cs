using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GridUsage : Class
{
    public bool statement;
    public GameObject building;

    public S_GridUsage(bool statement_ = false, GameObject building_ = null)
    {
        statement = statement_;
        building = building_;
    }
}
