using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModeDecompte : Mode
{
    public int toursRestant;
    public TMP_Text toursRestantAffiche;

    private int frame;// frame ecoulee sur une animation
    public int angleRotation;// en degré
    public int tempsRotation;//  total de frame de l'animation
    private float vitesseRotation;
    public CarteMouvante[] carteTournantes;
    public CanvasTournant[] canvasTournants;
    // Start is called before the first frame update
    void Start()
    {   frame=tempsRotation+1;
        vitesseRotation=(float)angleRotation/tempsRotation;
        carteTournantes=FindObjectsOfType<CarteMouvante>();//Faire que les Canvas soient indépendant pour plus de libertes
        canvasTournants=FindObjectsOfType<CanvasTournant>();// Chaque canvas possède sa propre liste de cartes.
    }

    // Update is called once per frame
    void Update()
    {
        calculTour();
    }

    public override bool comparer(){
        if(carteRetournee[0].gameObject.name.Equals(carteRetournee[1].gameObject.name)){
            nbrCarteRetournee=0;
            totalCarteRetournee+=2;
            testVictoire();
            return true;
        }
        tourne();
        return false;
    }
    public void tourne(){
        frame=0;
    }

    public override void testVictoire(){
        if(totalCarteRetournee==grille.contenuGrille.Length){
            Debug.Log("Victoire");
        }
    }
    public override void calculTour(){
        if(frame<tempsRotation){
            foreach(CanvasTournant c in canvasTournants){
                c.transform.Rotate(0f,0f,vitesseRotation);
            } 
            foreach(CarteMouvante c in carteTournantes){// A voir si on garde ce truc pour plus de réalisme;
                c.transform.Rotate(0f,0f,-vitesseRotation);
            }
            frame+=1;
        }
        if(frame==tempsRotation){
            nbrCarteRetournee=0;//Permet de rejouer
            frame+=1;
            toursRestant-=1;
            actualiserAffichage();
            resetCartes();
        }
        if(nbrCarteRetournee==2 && frame>=tempsRotation){
            comparer();
        }
        
    }
    public override void rejouer(){

    }
    public override void afficherDecor(){
        actualiserAffichage();
    }

    public void actualiserAffichage(){
        toursRestantAffiche.text=toursRestant.ToString();
        testDefaite();
    }
    public void testDefaite(){
        if(toursRestant==0){
            Debug.Log("Defaite");
        }
    }
}
