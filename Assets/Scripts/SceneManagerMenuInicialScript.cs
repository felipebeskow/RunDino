using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerMenuInicialScript : MonoBehaviour {
    
    public GameObject meteoro;

    private void Start() {
        meteoro.GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * 0.5f;
    }
    public void iniciar() {
        SceneManager.LoadScene("MeteoroVindo");
    }

    public void sair() {
        Application.Quit();
    }
}
