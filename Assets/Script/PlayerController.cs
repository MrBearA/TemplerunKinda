using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int laneNumber = 1; // 0 = Left, 1 = Middle, 2 = Right
    public float laneDistance = 2.0f; // Distance between lanes
    public float jumpHeight = 1.5f;
    private bool isJumping = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && laneNumber > 0)
        {
            laneNumber--;
            MoveLane(false);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && laneNumber < 2)
        {
            laneNumber++;
            MoveLane(true);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartCoroutine(Jump());
        }
    }

    private void MoveLane(bool goingRight)
    {
        Vector3 direction = goingRight ? Vector3.right : Vector3.left;
        transform.position += direction * laneDistance;
    }

    private IEnumerator Jump()
    {
        float startTime = Time.time;
        while (Time.time < startTime + 0.5f) // 0.5 seconds duration
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(0, jumpHeight, (Time.time - startTime) * 2), transform.position.z);
            isJumping = true;
            yield return null;
        }
        startTime = Time.time;
        while (Time.time < startTime + 0.5f) // 0.5 seconds to come down
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(jumpHeight, 0, (Time.time - startTime) * 2), transform.position.z);
            yield return null;
        }
        isJumping = false;
    }
}
