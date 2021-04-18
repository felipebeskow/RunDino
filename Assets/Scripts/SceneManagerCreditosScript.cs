using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerCreditosScript : MonoBehaviour
{
    public Transform Inicio;
    public Transform Text;
    public Transform Fim;
    public float TextLerp = 0;
    void Update() {
        Text.position = Vector3.Lerp(Inicio.position, Fim.position, TextLerp);

        if(TextLerp > 1f){
            //Application.Quit();
            SceneManager.LoadScene(0);
        } else {
            TextLerp += 0.0005f;
        }
    }
}
