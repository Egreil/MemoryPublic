using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Le mode classique
*   Les cartes se retournes? 
*   On compare les cartes par rapport à leur animation.
*   On gagne en retournant toute les cartes
*   On affiche le nombre de coups nécessaire
*   
**/

public class ModeClassique : Mode
{


    private int attente=300;
    private int score=0;


    // Start is called before the first frame update
    void Start()
    {
        foreach(Carte c in grille.contenuGrille){
            c.mode=this;
            c.changerAnimation();
        }
        attente=300;
        
    }

    // Update is called once per frame
    void Update()
    {
        calculTour();
        if(nbrCarteRetournee==2 && attente >190){
            attente=0;
            //Debug.Log(carteRetournee[0].GetComponent<Image>().sprite);
        }
        if (attente==190){
        if (nbrCarteRetournee==2){
            comparer();
        }
        attente+=1;
        }
        else{attente+=1;
        }
    }

    public override void afficherDecor(){

    }

    public override void testVictoire(){
        if(totalCarteRetournee==grille.contenuGrille.Length){
            Debug.Log("Victoire"); 
            //SceneManager.LoadScene("Ville");
            //Fermer le lvl et déclencher les actions en conséquence.
        }

    }
    public override void calculTour(){

    }
    public override void rejouer(){
            resetPartie();
    }
   
    public void resetPartie(){
        nbrCarteRetournee=0;
        carteRetournee= new Carte[2];
        score=0;
        attente=300;
        totalCarteRetournee=0;
    }

    public override bool comparer(){
        if(carteRetournee[0].gameObject.name==carteRetournee[1].gameObject.name){
            score+=1;
            nbrCarteRetournee=0;
            totalCarteRetournee+=2;
            Debug.Log("Paire");
            return true;
            }
        else{
            carteRetournee[0].resetCarte();
            carteRetournee[1].resetCarte();
            nbrCarteRetournee=0;
            Debug.Log("raté");
            return false;
        }

    }
}
