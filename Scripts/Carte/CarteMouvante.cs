using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarteMouvante : Carte
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void resetCarte(){
        anim.SetBool("shine",false);
    }
    public override void onClickJoueur(){
        if( mode.nbrCarteRetournee<2 && !anim.GetBool("shine")){
            this.anim.SetBool("shine",true);
            ajouterAuxCartesRetournees();
        }

    }
}
