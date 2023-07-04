using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    private float interval = 0.0f;
    public float reportingEvery = 10f;
    private Rigidbody physics;

    // Start is called before the first frame update
    void Start()
    {
        physics = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        interval += Time.fixedDeltaTime;
        if (interval > reportingEvery) {
            interval -= reportingEvery;
            Debug.Log($"@ z={gameObject.transform.position.z} velocity={physics.velocity}");
        }
    }
}
