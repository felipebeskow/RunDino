using UnityEngine;
using UnityEngine.SceneManagement;

public class DinossauroControllerScript : MonoBehaviour
{
    public float speed = 10f;
    public float laneSpeed = 1;
    public float factorLerp = 0.03f;
    public GameObject ArmLeft;
    public GameObject startL;
    public GameObject endL;

    private Rigidbody rb;
    private Vector3 verticalTargetPosition;
    private int currentLane = 1;
    private bool canMove = true;
    private bool interpoolLeftSize;
    private float factorLeft;

    /*private float locateCity = 0;
    private float next = 0;*/

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //startL = new GameObject();
        //endL = new GameObject();
        factorLeft = 0f;
        interpoolLeftSize = true;

        //startL.transform.position = ArmLeft.transform.position;
        //startL.transform.rotation = ArmLeft.transform.rotation;

        //endL.transform.position = new Vector3(-1.85f,-2.12f,2.64f);
    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeLane(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeLane(1);
            }
        }


        Vector3 targetPosition = new Vector3(verticalTargetPosition.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * speed;
        if (interpoolLeftSize)
        {
            factorLeft += factorLerp;
            if (factorLeft > 1)
                interpoolLeftSize = false;
        }
        else
        {
            factorLeft -= factorLerp;
            if (factorLeft < 0)
                interpoolLeftSize = true;
        }
        ArmLeft.transform.position = Vector3.Slerp(startL.transform.position, endL.transform.position, factorLeft);
    }

    void ChangeLane(int direction)
    {
        int targetLane = currentLane + direction;
        if (targetLane < 0 || targetLane > 2)
            return;
        currentLane = targetLane;
        verticalTargetPosition = new Vector3((currentLane - 1), 0, 0);
    }

    void LeftArmAnimation()
    { 
        
    }
}
