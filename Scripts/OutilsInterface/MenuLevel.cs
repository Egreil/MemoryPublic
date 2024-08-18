using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLevel : MonoBehaviour
{

    public string map;
    public bool pause;
    string currentScene;
    // Start is called before the first frame update
    void Start()
    {   pause=false;
        Application.targetFrameRate = 60;
        this.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
        map="Map";
        currentScene=SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if ( !currentScene.Equals("accueil") && !currentScene.Equals("Ville1") && Input.GetKeyDown("escape")){
 
            ActiverDesactiverMenuPause();
            
        }
    }
    public void ActiverDesactiverMenuPause(){
        if(gameObject.GetComponent<Canvas>().enabled){
            Reprendre();
        }
        else{
            //Time.timeScale=0.0f;
            gameObject.GetComponent<Canvas>().enabled=!gameObject.GetComponent<Canvas>().enabled;
            pause=true;
        }
    }

    public void Reprendre(){
        pause=false;
        gameObject.GetComponent<Canvas>().enabled=false;
        //Time.timeScale=1.0f;
    }
    public void QuitterDefi(){
        SceneManager.LoadScene(map);
    }
    public void Reessayer(){
        FindObjectsOfType<JeuDeBase>()[0].Rejouer();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
