using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObscureObj : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < transform.GetComponent<Renderer>().materials.Count(); i++)
        {
            transform.GetComponent<Renderer>().materials[i].renderQueue = 2002;
        }
    }
}
