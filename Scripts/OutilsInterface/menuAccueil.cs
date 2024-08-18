using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuAccueil : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nouveauJeu(){
       SceneManager.LoadScene("Ville1");
       Debug.Log("click");
    }
    public void defis(){
        Debug.Log("click défis");
    }

    public void generique(){
        Debug.Log("click generique");
    }
    
    public void version(){

    }
}
