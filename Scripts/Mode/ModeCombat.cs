using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using Random=System.Random;

/** Ce mode permet d'opposer notre personnage à un ennemi
*   Il faut réaliser plus de paires que l'adversaire pour gagner
*   L'adversaire possède ou non un pouvoir qu'il peut activer à certaines conditions.
*   L'IA de l'ordi change en fonction de la difficulté du niveau
*   Ce mode doit pouvoir être compatible avec les autres modes de jeu??
*   Il dispose d'un affichage spécial des scores. j1,j2 paires restantes?
**/ 

public class ModeCombat : Mode
{

    private int attente=300; // Voir pour problème de lecture des cartes retournées.

    //Gestion du score, des tours
    private int[] scores;
    private int idJoueur;
    //Creation de la memoire de l'IA
    public int niveauIA;
    public Carte[] memoireIA;
    public int indexMemoireTemp;//monde de +1%2 chaque tirage de carte
    public Carte[] memoireTempTour;
    //varibales pour le retournement des cartes
    private bool paireTrouvee;
    public Carte[] paireCarte;
    public int numeroCartePaire;

    public Canvas canvas;
    public TMP_Text scoreIA;
    public TMP_Text scoreJ;
    TMP_Text[] textScores;

    // Start is called before the first frame update
    void Start()
    {
        scores= new int[]{0,0};
        textScores=new TMP_Text[]{scoreIA,scoreJ};
        idJoueur=0;
        tour=0;
        memoireIA=new Carte[niveauIA*2];
        indexMemoireTemp=0;
        memoireTempTour = new Carte[2];
        paireCarte=new Carte[2];
        numeroCartePaire=2;
        
        //test = true;// a supprimer et corriger dans les tests
        if (!test)
        {
            afficherDecor();
        }
        

        // Activation des instances qui gerent l'affichage du score.

    }

    // Update is called once per frame
    void Update()
    {   if( !test && grille!=null && totalCarteRetournee<grille.contenuGrille.Length){
        //Debug.Log("toto");
        calculTour();}
    }

    public override void afficherDecor(){
        canvas.gameObject.SetActive(true);
        canvas.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
        scoreIA.text = "0";
        scoreJ.text = "0";
    }

    public void actualiserAffichage()
    {
        scoreIA.text = scores[1].ToString();
        scoreJ.text = scores[idJoueur].ToString();
    }


    public override void calculTour(){
        
        if (attente <90){
            attente+=1;
        }
        else if(attente==90){ 
                
            attente+=1;
            resetCartes();
        }
        else if (nbrCarteRetournee==2 && attente>=91){
            if(!comparer()){
                attente=0; 
                tour=(tour+1)%2;
            }
            else{
                actualiserAffichage();
                testVictoire();
                if(tour!=idJoueur){
                    attente=91;
                }
            }
            
        }
        else if (tour!=idJoueur && attente>=181){
            jouerIA();
        }
        else{
            attente+=1;
        }
       // afficherScore(); A faire dans une autre classe
    }


    public override void rejouer(){
        resetPartie();
    }
   

    public void resetPartie(){
        nbrCarteRetournee=0;
        carteRetournee= new Carte[2];
        scores=new int[]{0,0};
        totalCarteRetournee=0;
    }

    public override bool comparer(){
        if(carteRetournee[0].gameObject.name==carteRetournee[1].gameObject.name){
            scores[tour]+=1;
            nbrCarteRetournee=0;
            totalCarteRetournee+=2;
            Debug.Log("Paire");
            //AFAIRE AJOUTER UNE PHRASE DU PERSONNAGE FAISANT LA PAIRE
            return true;
        }
        else{
            //Thread.Sleep(2000); pas fluide, bloque tout
            
            remplacerMemoire();
            Debug.Log("raté");
            return false;
        }
    }


    public override void testVictoire()
    {
        if (totalCarteRetournee == grille.contenuGrille.Length)
        {
            if (scores[idJoueur] >= scores[1])
            {
                Debug.Log("Victoire");
            }
            else
            {
                Debug.Log("Défaite");
                //anim.Victoire
            }
            //SceneManager.LoadScene("Ville");
            //Fermer le lvl et déclencher les actions en conséquence.
        }
    }


    /*Quand l'IA joue, elle doit jouer lentement, chercher dans sa memoire si 2 cartes identiques ont été identifiées. 
    * Sinon elle tire une carte aleatoire parmis les cartes restantes.
    * L'IA retient les niveauIA *2 dernieres cartes qui ont été retournée et qui n'ont pas formé une paire.
    * Besoin d'une memoire temporaire pour n'ajouter à la mémoire qu'en cas de non paire.
    * Si aucune paire n'est dans sa mémoire elle pioche une carte restant absente de sa mémoire et la compare à sa mémoire.
    * L'IA peut utiliser un pouvoir lorsque les conditions sont vérifiées.
    */
    public void jouerIA(){
        //1 fouiller memoire
        fouillerMemoireIA(null);
        if (numeroCartePaire<2){
            activerCarte(paireCarte[numeroCartePaire]);
            numeroCartePaire +=1;
            activerCarte(paireCarte[numeroCartePaire]);
            numeroCartePaire += 1;
            Debug.Log("Paire en memoire");
        }
        else{
            Carte c=tirerCarteAleatoire();
            fouillerMemoireIA(c);
            if (numeroCartePaire < 2)
            {
                Debug.Log("Paire apres un tirage :"+ c.gameObject.name+" et "+ paireCarte[numeroCartePaire].gameObject.name) ;
                Debug.Log(paireCarte[numeroCartePaire]==c);
                activerCarte(paireCarte[numeroCartePaire]);
                
                numeroCartePaire += 1;
            }
            else
            {
                
                Carte c2=tirerCarteAleatoire();
                Debug.Log("Tire Deux carte aleatoires:"+c.gameObject.name +" et "+c2.gameObject.name);
            }
        }
    }

    //On fouille la mémoire a la recherche de paire si c vaut null, sinon on cherche c dans la mémoire. c N'est pas dans la memoire de l'IA d'après les tests précédents
    public void fouillerMemoireIA(Carte c){
        if (c != null)
        {
            for (int i = 0; i < memoireIA.Length; i++)
            {
                if (memoireIA[i] != null && memoireIA[i].Equals(c))
                {
                    paireCarte = new Carte[] { null, memoireIA[i] };
                    //activerCarte(memoireIA[i]);
                    memoireIA[i] = null;
                    numeroCartePaire = 1;
                    return;
                }
            }
        }
        else
        {
            for (int i = 0; i < memoireIA.Length; i++)
            {
                for (int j = i + 1; j < memoireIA.Length; j++)
                {
                    if (memoireIA[i] != null && memoireIA[j] != null && memoireIA[i].Equals(memoireIA[j]))
                    {
                        paireTrouvee = true;
                        paireCarte = new Carte[] { memoireIA[i], memoireIA[j] };
                        memoireIA[i] = null;
                        memoireIA[j] = null;
                        numeroCartePaire = 0;
                        return;
                    }
                }

            }
        }
    }

    public void activerCarte(Carte carte){
        carte.onClick();
    }

    public Carte[] genererCartesRestantesInconnues(){//A faire
    //1 recupérer les cartes restante cachée 
        List<Carte> cartesRestantesInconnuesListe= new List<Carte>();
        foreach(Carte c in grille.contenuGrille){
            if(c.anim.GetBool("shine")==false){
                cartesRestantesInconnuesListe.Add(c);
            }
        }
    //2 Retirer les cartes presentes dans la memoire de l'IA;
        foreach(Carte c in memoireIA){
                cartesRestantesInconnuesListe.RemoveAll(mc=>mc==c);
            }
            // if (cartesRestantesInconnuesListe.Contains(c)){
            //     cartesRestantesInconnuesListe.Remove(c);
            // }
        
        return cartesRestantesInconnuesListe.ToArray();
    }

    public Carte tirerCarteAleatoire(){
        Carte[] cartesRestantesInconnues=genererCartesRestantesInconnues();
        Debug.Log("cartesRestantesInconnues");
        foreach(Carte c in cartesRestantesInconnues)
        {
            Debug.Log(c.gameObject.name);
        }
        Random rnd=new Random();
        int index = rnd.Next(0,cartesRestantesInconnues.Length);
        activerCarte(cartesRestantesInconnues[index]); //Ajouter la mémoire temporaire
        return cartesRestantesInconnues[index];
    }

    public void remplacerMemoire()
    {   int n = 0;
        foreach(Carte c in memoireTempTour)
        { 
            if (!(Array.Exists(memoireIA, mc=>mc==c))) {
                for (int index = 1; index < memoireIA.Length; index++)
                {
                    memoireIA[index - 1] = memoireIA[index];

                }
                memoireIA[memoireIA.Length - 1] = memoireTempTour[n];
                    
            }
            n += 1;
        }
    }

}




// function deroulementPartie(){

//     //verification lors de 2 tirages.
//     if (nombreTire==2){
//         testDePoint()
//         console.log("vu");
    
//     //verification si toutes les cartes on été trouvée ou non

//     //changement de jouer
    

//     document.getElementById("joueur").innerText=joueur+1;
//     }


// }




// function testDePoint(){
//     let carte1=associationCarteImage.get(carteRetournee[0])
//     let carte2=associationCarteImage.get(carteRetournee[1])
//     console.log(carte1 + " et "+carte2)
//     if(carte1==carte2){
//         score[joueur]+=1;
//         document.getElementById("scoreJ"+(joueur+1)).innerText=score[joueur];
//         nombreTire=0;

//     }
//     else{//Ajouter un temps d'attente
//         document.getElementById(carteRetournee[0]).setAttribute("src","./images/dosCarte.jpeg");
//         document.getElementById(carteRetournee[1]).setAttribute("src","./images/dosCarte.jpeg");
//         joueur=(joueur+1)%2;
//         nombreTire=0;

//     }


// }