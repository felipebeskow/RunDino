using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPiaf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);        
    }

    // Update is called once per frame
    void Update()
    {
        /* como controlar qual cena pode tocar, e qual não
        if(SceneManager.GetActiveScene().name == "NomeDaCenaDesejada")
        {
            source.Play();
        }
        else
        {
            source.Stop();
        }
        */
    }
}
