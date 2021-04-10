using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerMenuInicialScript : MonoBehaviour {
    
    public void iniciar() {
        SceneManager.LoadScene("MeteoroVindo");
    }

    public void sair() {
        Application.Quit();
    }
}
