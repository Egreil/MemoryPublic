using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class ModeEtoile : Mode
{

    private int attente=300; // Voir pour problème de lecture des cartes retournées.
    private int score=0;


    // Start is called before the first frame update
    void Start()
    {   
        test=false;
        nbrCarteRetournee=0;
        totalCarteRetournee=0;
        carteRetournee=new Carte[2];
        //Debug.Log("Fin ModeEtoile");
    }

    // Update is called once per frame
    void Update()
    {
       if( !test && grille!=null && totalCarteRetournee<grille.contenuGrille.Length){
        //Debug.Log("toto");
        calculTour();
        }
    }

    public override void afficherDecor(){

    }

    public override void testVictoire(){
        if(totalCarteRetournee==grille.contenuGrille.Length){
            Debug.Log("Victoire"); 
            grille.victoire();
            //SceneManager.LoadScene("Ville");
            //Fermer le lvl et déclencher les actions en conséquence.
        }

    }
    public override void calculTour(){
        // l'attente ici sert à creer un délai lors du retour face cachée
        if (attente < 120)
        {
            attente += 1;
        }
        else if (attente == 120)
        {
            resetCartes();
            attente += 1;
        }

        if (nbrCarteRetournee == 2 && attente >= 121)
        {
            comparer();
            testVictoire();
        }
    }
    public override void rejouer(){
        resetPartie();
    }
   
    public void resetPartie(){
        nbrCarteRetournee=0;
        carteRetournee= new Carte[2];
        score=0;
        totalCarteRetournee=0;
    }

    public override bool comparer(){
        if(carteRetournee[0].gameObject.name==carteRetournee[1].gameObject.name){
            score+=1;
            nbrCarteRetournee=0;
            totalCarteRetournee+=2;
            Debug.Log("Paire");
            testVictoire();
            return true;
            }
        else{
            //Thread.Sleep(2000); pas fluide, bloque tout
            attente=0;
            Debug.Log("raté");
            return false;
        }

    }
}
