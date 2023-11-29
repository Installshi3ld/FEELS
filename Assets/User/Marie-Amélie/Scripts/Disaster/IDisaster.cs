using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDisaster
{
    string Description { get; set; }
    void ProvoqueDisaster();
}
