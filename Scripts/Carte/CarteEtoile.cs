using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarteEtoile : Carte
{
    // Start is called before the first frame update
    void Start()
    {   
    }

    void Update(){
        //changerAnimation();
    }


    public override void onClick(){
        
        if (!anim.GetBool("shine") && mode.nbrCarteRetournee < 2) {
            activer();
            ajouterAuxCartesRetournees();
        }
    }

    public override void resetCarte(){
        desactiver();
    }


    public void activer(){

        anim.SetBool("shine",true);
        Debug.Log("actif");
    }
    public void desactiver(){
        anim.SetBool("shine",false);
        Debug.Log("desactiver");
    }
    

}
