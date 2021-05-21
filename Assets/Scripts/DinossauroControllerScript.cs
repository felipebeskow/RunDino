using System.Collections;
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
    public GameObject Corpo;
    public GameObject startC;
    public GameObject endC;

    private Rigidbody rb;
    private Vector3 verticalTargetPosition;
    private int currentLane = 1;
    private bool canMove = true;
    private bool interpoolLeftSize;
    private float factorLeft;

    private float locateCity = 0;
    private float next = 0; 
    private IEnumerator coroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        factorLeft = 0f;
        interpoolLeftSize = true;
        coroutine = FimDino();

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
            float fimz = locateCity + 400;
            
            for (int i = 0; i < 8; i++)
            {
                GameObject asterois = Instantiate(Resources.Load("Asteroids", typeof(GameObject))) as GameObject;
                asterois.transform.position = new Vector3(Random.Range(-5, 6), 1f, Random.Range(inicioz, fimz));
            }
        }
        else if (other.CompareTag("Obstaculos"))
        {
            next = 0.01f;
            speed = 0f;
            canMove = false;
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator FimDino()
    {
        yield return new WaitForSeconds(1);
        float factor = 0;

        while (factor <= 1)
        {
            Corpo.transform.rotation = Quaternion.Lerp(startC.transform.rotation, endC.transform.rotation, factor);
        }
        
    }
}
