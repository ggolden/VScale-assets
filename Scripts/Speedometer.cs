using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Text velocityText;
    public Text positionText;
    public float reportingEvery = 10f;

    private float interval = 0.0f;
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

            if (velocityText != null) {
                velocityText.text = $"Velocity: {physics.velocity}";
            }

            if (positionText != null) {
                positionText.text = $"Position: {gameObject.transform.position.z}";
            }
        }
    }
}
