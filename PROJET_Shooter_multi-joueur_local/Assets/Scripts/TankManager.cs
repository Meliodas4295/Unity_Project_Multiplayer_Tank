using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
    [SerializeField]
    private float speedTranslation;
    [SerializeField]
    private float speedRotation;
    public bool isLaunch = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.forward * verticalInput * speedTranslation * Time.deltaTime);
        transform.Rotate(new Vector3(0,1,0) * horizontalInput * speedRotation * Time.deltaTime);
    }
}
