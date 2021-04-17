using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerRunDinoScript : MonoBehaviour {

    public Transform cam;
    
    public float deltaHorizontal;
    public float deltaVertical;

    public Image img;

    private float y;
    private bool down;

    private bool fadeOut = false;
    private float indCor = 0;

    void Start() {
        y=20;        
        down=false;
    }

    void FixedUpdate() {

        if (y>27f){
            down=true;   
        } else if (y<20f){
            down=false;
        }

        if(down){
            y-=0.25f;
        }else{
            y+=0.25f;
        }
        
        float x = Input.GetAxis("Vertical") * deltaVertical * Time.deltaTime;
        float z = Input.GetAxis("Horizontal") * deltaHorizontal * Time.deltaTime;

        if((cam.position.z <= 450f) && (z < 0.0f)){
            z = 0f;
        }

        if((cam.position.z >= 620f) && (z > 0.0f)){
            z = 0f;
        }

        if ((cam.position.x <= 850f) && (x < 0f)) {
            x = 0f;
        }

        if ((cam.position.x >= 2150f)){
            fadeOut = true;
            Debug.Log(img.color);
        }

        if (fadeOut) {
            indCor+=0.05f;
            img.color = new Color(1f,1f,1f,indCor);
        }

        if (indCor > 1) {
            SceneManager.LoadScene("Creditos");
        }

        cam.position = new Vector3(cam.position.x, y, cam.position.z);
        cam.Translate(x, 0, z);
        cam.Rotate(-cam.rotation.x,-cam.rotation.y,-cam.rotation.z);

    }
}
