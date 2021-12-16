using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private float speedRotation = 0.1f;
    private float speedTranslation = 0.001f;

    void Update()
    {
        if (gameObject.CompareTag("Shield"))
        {
            gameObject.transform.Rotate(new Vector3(0, 1, 0) * speedRotation);
            gameObject.transform.Translate(new Vector3(0, 1, 0) * speedTranslation * Mathf.Sin(Time.time));
        }
        if (gameObject.CompareTag("ThunderBolt"))
        {
            gameObject.transform.Rotate(new Vector3(0, 0, 1) * speedRotation);
            gameObject.transform.Translate(new Vector3(0, 0, 1) * speedTranslation * Mathf.Sin(Time.time));
        }
    }
}
