using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackManager : MonoBehaviour
{
    private float rotationSpeed = 0.5f;

    void Update()
    {
        gameObject.transform.GetChild(0).transform.Rotate(new Vector3(0, 1, 0) * rotationSpeed);
    }
}
