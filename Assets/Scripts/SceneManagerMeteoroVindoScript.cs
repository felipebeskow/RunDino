using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerMeteoroVindoScript : MonoBehaviour {
    
    public Transform meteoro;
    
    public Transform inicioHistoria;
    public Transform textHistoria;
    public Transform fimHistoria;

    //[Serialized]
    public float lerpFactor = 0;

    void Start(){
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        
        if (meteoro) {
            if (meteoro.position.z >= 2000) {
                SceneManager.LoadScene("DinossauroFu");
            }
        }

        Debug.Log(SceneManager.GetActiveScene().name);
        
        if(SceneManager.GetActiveScene().name == "Creditos") {
            Destroy(gameObject);
        }
        
    }

    private void FixedUpdate() {

        if (textHistoria) {
            textHistoria.position = Vector3.Lerp(inicioHistoria.position, fimHistoria.position, lerpFactor);
        }
        
    }
}
