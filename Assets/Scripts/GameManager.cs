using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject map;

    [Header("Move")]
    [SerializeField] float movingSpeed = 1.0f;
    [SerializeField] List<Transform> pins;
    [SerializeField] List<Vector3> targets;

    [Header("Scale")]
    [SerializeField] float scalingSpeed = 1.0f;
    [SerializeField] float mapTargetScaleFactor = 1.0f;
    [SerializeField] Vector3 mapInitialScale;

    private void Start()
    {
        for (int i = 0; i < pins.Count; i++)
        {
            targets.Add(new Vector3(pins[i].localPosition.x * -1, 0, pins[i].localPosition.z * -1));
        }
        mapInitialScale = map.transform.parent.localScale;
    }

    void Update()
    {
        if (Vector3.Distance(map.transform.localPosition, targets[0]) > 0.001f)
        {
            float step = movingSpeed * Time.deltaTime;
            map.transform.localPosition = Vector3.MoveTowards(map.transform.localPosition, targets[0], step);
        }

        if (Vector3.Distance(map.transform.parent.localScale, mapInitialScale* mapTargetScaleFactor) > 0.001f)
        {
            float step = scalingSpeed * Time.deltaTime;
            map.transform.parent.localScale = Vector3.Lerp(map.transform.parent.localScale, mapInitialScale* mapTargetScaleFactor, step);
        }
        else
        {
            map.transform.parent.localScale = mapInitialScale * mapTargetScaleFactor;
        }
    }
}
