using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    private void Update()
    {
        transform.Rotate(new Vector3(45.0f, 0.0f, 45.0f) * Time.deltaTime);
    }
}
