using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartePossibilites : Carte
{
    public ModeCombinaison modeCombinaison;
    // Start is called before the first frame update
    public override void onClickJoueur(){
        // Les cartes qui ne shine pas sont les cartes des possibilites et qu'on ne veut pas que cliquer sur la combinaison compte comme une saisie.
        if(modeCombinaison.combinaison[modeCombinaison.indexReponse].gameObject.name.Equals(this.gameObject.name)){
            //Debug.Log("test ok");
            modeCombinaison.combinaison[modeCombinaison.indexReponse].gameObject.SetActive(true);
            modeCombinaison.combinaison[modeCombinaison.indexReponse].anim.SetBool("shine", true);
            modeCombinaison.indexReponse+=1;
            modeCombinaison.testVictoire();
        }
        else{
            modeCombinaison.Defaite();
        }
        
    }
    public override void resetCarte(){}
}
