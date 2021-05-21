using UnityEngine;
using UnityEngine.SceneManagement;

public class DinossauroControllerScript : MonoBehaviour
{
    public float speed = 10f;
    public float laneSpeed = 3;
    public float factorLerp = 0.03f;
    public GameObject ArmLeft;
    public GameObject startL;
    public GameObject endL;
    public GameObject ArmRight;
    public GameObject startR;
    public GameObject endR;

    private Rigidbody rb;
    private Vector3 verticalTargetPosition;
    private int currentLane = 1;
    private bool canMove = true;
    private bool interpoolLeftSize;
    private float factorLeft;

    private float locateCity = 0;
    private float next = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        factorLeft = 0f;
        interpoolLeftSize = true;

    }

    void Update()
    {
        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeLane(-5);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeLane(5);
            }
        }


        Vector3 targetPosition = new Vector3(verticalTargetPosition.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * speed;

        if (canMove)
        {
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
            ArmRight.transform.position = Vector3.Slerp(startR.transform.position, endR.transform.position, factorLeft);
        }
    }

    void ChangeLane(int direction)
    {
        int targetLane = currentLane + direction;
        if (targetLane < -5 || targetLane > 6)
            return;
        currentLane = targetLane;
        verticalTargetPosition = new Vector3((currentLane - 1), 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NovaParteMapaTrigger"))
        {
            GameObject terreno = Instantiate(Resources.Load("TerrenoDino", typeof(GameObject))) as GameObject;
            locateCity += 200;
            terreno.transform.position = new Vector3(0f, 0f, locateCity);
            Destroy(terreno, 120f); 

            float inicioz = locateCity;
            float fimz = locateCity + 40;
            /*
            for (int i = 0; i < 3; i++)
            {
                GameObject pedra = Instantiate(Resources.Load("Pedra", typeof(GameObject))) as GameObject;
                pedra.transform.position = new Vector3(Random.Range(-1, 2), 0.25f, Random.Range(inicioz, fimz));
            }*/
        }
        else if (other.CompareTag("Obstaculos"))
        {
            next = 0.01f;
            speed = 0f;
            canMove = false;
        }
    }
}
