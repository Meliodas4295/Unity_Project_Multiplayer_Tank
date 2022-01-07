using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private float speedRotation = 0.1f;
    private float speedTranslation = 0.003f;

    void Update()
    {
        Transform transformation = gameObject.transform;
        if (gameObject.CompareTag("Shield"))
        {
            transformation.Rotate(new Vector3(0, 1, 0) * speedRotation);
            transformation.Translate(new Vector3(0, 1, 0) * speedTranslation * Mathf.Sin(Time.time));
        }
        if (gameObject.CompareTag("ThunderBolt"))
        {
            transformation.Rotate(new Vector3(0, 0, 1) * speedRotation);
            transformation.Translate(new Vector3(0, 0, 1) * speedTranslation * Mathf.Sin(Time.time));
        }
        if (gameObject.CompareTag("UnlimitedAmmunition"))
        {
            transformation.Rotate(new Vector3(0, 0, 1) * speedRotation);
            transformation.Translate(new Vector3(0, 0, 1) * speedTranslation * Mathf.Sin(Time.time));
        }
    }
}
