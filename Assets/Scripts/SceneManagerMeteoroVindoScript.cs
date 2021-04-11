using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerMeteoroVindoScript : MonoBehaviour {
    
    public Transform meteoro;

    void Start(){
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        
        if (meteoro) {
            if (meteoro.position.z >= 5000) {
                SceneManager.LoadScene("RunDino");
            }
        }
        /*
        if(SceneManager.GetActiveScene().name == "NomeDaCenaDesejada") {
            source.Stop();
        }
        */
    }
}
