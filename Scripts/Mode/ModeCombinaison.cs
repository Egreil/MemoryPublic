using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ModeCombinaison : Mode
{
    //Duree d'affichage
    public bool automatique;
    public Canvas boutonPret;
    public int duree; // en frame
    public int dureeEcoulee; // en frame
    public bool pret;// Pour lancer le décompte d'affichage
    //
    public List<AnimationClip> clips;
    public CarteCombinaison[] combinaison;// Les images tirées
    public AnimationClip[] possibilites;
    public CartePossibilites[] cartePossibilites; // Toutes les images possibles 
    public int indexReponse=0;
    // Start is called before the first frame update
    void Start()
    { 
        cartePossibilites=FindObjectsOfType<CartePossibilites>();
        triParAbscysse(cartePossibilites);
        combinaison=FindObjectsOfType<CarteCombinaison>();
        triParAbscysse(combinaison);

        afficherDecor();
        tirageDesAnimationsMode();
        Desactiver(cartePossibilites);
        Desactiver(combinaison);
        foreach (Carte c in cartePossibilites)
        {
            Debug.Log(c.anim.GetBool("shine"));
        }
        automatique =true;// A supprimer avant lancement
        if (duree>0){
            dureeEcoulee=2*duree;
        }
        else{
        //Lever une erreur si duree =0; ou possibilites.Length<CartePossibilites
        duree=100;
        dureeEcoulee=duree+1;
        }
        if (automatique){
            declencher(); 
        }
        else{
            //Afaire après dialogue. " Je ferai mieux de me tenir prete à mémoriser le chemin"
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {   
        calculTour();
        
    }

    public void declencher(){
        dureeEcoulee=0;
        Activer(combinaison);
    }
    public void setPret(){
        pret=true;
    }
    public override void calculTour(){
        if (dureeEcoulee<duree){
            dureeEcoulee+=1;
        }
        else if(dureeEcoulee==duree){
            pret=true;
            dureeEcoulee+=1;
        }
        if(pret){
            Activer(cartePossibilites);
            Desactiver(combinaison);
            pret = false;
        }
    }

    public override void tirageDesAnimationsMode(){
        while(indexReponse<combinaison.Length){
            Random rand= new Random();
            int index= rand.Next(0, possibilites.Length);
            combinaison[indexReponse].animFace=possibilites[index];
            combinaison[indexReponse].mode=this;
            combinaison[indexReponse].changerAnimation();
            combinaison[indexReponse].genererNom();
            combinaison[indexReponse].anim.SetBool("shine", true);
            indexReponse +=1;
        }
        indexReponse=0;
    }

    //Verifier les cartes 
    public override void testVictoire(){
        if(indexReponse==combinaison.Length){
            Victoire();
            Debug.Log("Victoire");
        }
    }
    public void Victoire(){

    }
    public void Defaite(){
        //resetPartie();
        Debug.Log("defaite");
    }
    //A mettre sur les carte


    //Affichage des cartes
    public void Activer(Carte[] tableau){
        foreach(Carte c in tableau){
            c.gameObject.SetActive(true);
            c.anim.SetBool("shine", true);
        }
    }
    public void Desactiver(Carte[] tableau){
        foreach(Carte c in tableau){
            c.gameObject.SetActive(false);
        }
    }

    public override void rejouer(){

    }
    public override bool comparer(){
        return true;
    }
    public override void afficherDecor(){
        for(int i=0;i<cartePossibilites.Length;i++){
            cartePossibilites[i].animFace= possibilites[i];
            
            cartePossibilites[i].mode=this;
            cartePossibilites[i].modeCombinaison=this;
            cartePossibilites[i].associerBouton();
            cartePossibilites[i].changerAnimation();
            cartePossibilites[i].genererNom();
            cartePossibilites[i].anim.SetBool("shine", true);
            //Debug.Log("ok");
        }
        
    }
    public void triParAbscysse(Carte[] tableau)
    {
        Debug.Log(tableau.Length);
        for (int i = 0; i < tableau.Length-1; i++)
        {
            int indexMin = i;
            for (int j=i+1;j<tableau.Length; j++)
            {
                
                //Debug.Log("j :" + j + "     indexMin :" + indexMin);
                if(tableau[j].transform.position.x < tableau[indexMin].transform.position.x)
                {
                    indexMin = j;
                }
            }
            Carte temp = tableau[i];
            tableau[i] = tableau[indexMin];
            tableau[indexMin] = temp; 
        }
    }
     
}
