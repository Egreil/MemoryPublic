using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Niveau : MonoBehaviour
{

    public void ouvertureLevel(){
        SceneManager.LoadScene(this.name);//LoadSceneMode.Additive
    }
}
