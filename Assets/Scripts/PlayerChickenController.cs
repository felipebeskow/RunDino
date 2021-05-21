using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerChickenController : MonoBehaviour
{
    public float speed = 10f;
    public float laneSpeed = 1;
    public Image img;

    private Rigidbody rb;
    private Animator anim;
    private Vector3 verticalTargetPosition;
    private int currentLane = 1;
    private float locateCity = 0;
    private bool canMove;
    private float next = 0;
    private bool startScene;
    private float startFactor;

    void Start() {
        startScene = true;
        startFactor = 1f;
        canMove = false;
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        anim.Play("Idle");
    }

    void Update() {
        if (startScene)
        {
            img.color = new Color(255f, 255f, 255f, startFactor);
            startFactor -= 0.005f;
            rb.velocity = Vector3.forward * 0;
            if (startFactor < 0)
            {
                startScene = false;
                canMove = true;
                anim.Play("Run In Place");
            }
        } 

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

            Vector3 targetPosition = new Vector3(verticalTargetPosition.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed * Time.deltaTime);

            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, transform.position.z-2);
        }


    }

    private void FixedUpdate() {
        if (canMove)
        {
            rb.velocity = Vector3.forward * speed;
        }

        if (next > 0)
        {
            Debug.Log(next);
            next += 0.01f;
            img.color = new Color(0f, 0f, 0f, next);
            if (next >= 1)
            {
                SceneManager.LoadScene("Creditos");
            }
        }
    }

    void ChangeLane(int direction) {
        int targetLane = currentLane + direction;
        if (targetLane < 0 || targetLane > 2)
            return;
        currentLane = targetLane;
        verticalTargetPosition = new Vector3((currentLane - 1), 0, 0);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("NovaParteMapaTrigger")) { 
            GameObject city = Instantiate(Resources.Load("Cidade", typeof(GameObject))) as GameObject;
            locateCity += 32;
            city.transform.position = new Vector3(0f, 0f, locateCity); 
            Destroy(city, 60f);

            float inicioz = locateCity;
            float fimz = locateCity + 28;

            for (int i=0; i<3; i++) {
                GameObject pedra = Instantiate(Resources.Load("Pedra", typeof(GameObject))) as GameObject;
                pedra.transform.position = new Vector3(Random.Range(-1, 2), 0.25f, Random.Range(inicioz, fimz));
            }
        } else if (other.CompareTag("Obstaculos"))
        {
            next = 0.01f;
            speed = 0f;
            canMove = false;
            anim.Play("Idle");
            anim.Play("Turn Head");
        }
    }
}
