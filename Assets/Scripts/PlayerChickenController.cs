using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChickenController : MonoBehaviour
{
    public float speed = 10f;
    public float laneSpeed = 1;

    private Rigidbody rb;
    private Animator anim;
    private Vector3 verticalTargetPosition;
    private int currentLane = 1;

    void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        anim.Play("Run In Place");
    }

    void Update() {
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLane(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLane(1);
        }


        Vector3 targetPosition = new Vector3(verticalTargetPosition.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed * Time.deltaTime);

    }

    private void FixedUpdate() {
        rb.velocity = Vector3.forward * speed;
    }

    void ChangeLane(int direction) {
        int targetLane = currentLane + direction;
        if (targetLane < 0 || targetLane > 2)
            return;
        currentLane = targetLane;
        verticalTargetPosition = new Vector3((currentLane - 1), 0, 0);
    }
}
