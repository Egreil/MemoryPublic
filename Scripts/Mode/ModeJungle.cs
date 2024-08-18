using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ModeJungle : Mode  
{
    
    public bool forme;
    private int attente = 300; // Voir pour probl�me de lecture des cartes retourn�es.
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        forme = false;   
    }

    // Update is called once per frame
    void Update()
    {
        if (!test) {
            calculTour();
        }
    }

    public override void afficherDecor()
    {

    }

    public override void testVictoire()
    {
        if (totalCarteRetournee == grille.contenuGrille.Length)
        {
            Debug.Log("Victoire");
            //SceneManager.LoadScene("Ville");
            //Fermer le lvl et d�clencher les actions en cons�quence.
        }

    }
    public override void calculTour()
    {
        if (attente < 90)
        {
            attente += 1;
        }
        else if (attente == 90)
        {
            resetCartes();
            attente += 1;
        }

        if (nbrCarteRetournee == 2 && attente >= 91)
        {
            comparer();
            testVictoire();
            attente = 0;
        }


    }

    public override void rejouer()
    {
        resetPartie();
    }

    public void resetPartie()
    {
        nbrCarteRetournee = 0;
        carteRetournee = new Carte[2];
        score = 0;
        totalCarteRetournee = 0;
    }

    public override bool comparer()
    {
    bool pair = false;
        if (forme)
        {
            pair=comparerForme();
        }
        else
        {
            pair=comparerCouleur();
        }

        if (pair)
        {
            //score += 1;
            nbrCarteRetournee = 0;
            totalCarteRetournee += 2;
            Debug.Log("Paire");
            testVictoire();
            forme = !forme; 
            
            return true;
        }
        else
        {
            //Thread.Sleep(2000); pas fluide, bloque tout
            //attente = 0;
            score += 1;
            Debug.Log("rate");
            return false;
        }
        

    }
// nom au format "nom-couleur"
    private bool comparerCouleur(){
        if (carteRetournee[0].name.Split("-")[1].Equals(carteRetournee[1].name.Split("-")[1]))
        {
            Debug.Log("Forme");
            return true;
        }
        return false;
    }
    private bool comparerForme(){
        if (carteRetournee[0].name.Split("-")[0].Equals(carteRetournee[1].name.Split("-")[0])){
            Debug.Log("Couleur");
        return true;
        }
    return false;
    }

    public override int[] setRemainingAnimations()
    {
        int[] sortie = new int[listeDesAnimations.Length];
        for (int i = 0; i < sortie.Length; i++)
        {
            sortie[i] = 1;
        }
        return sortie;
    }

    public override int donnerLongueur(){
        return grille.contenuGrille.Length;
    }
    // public override void tirageDesAnimationsMode()
    // {
    //     int longueur = grille.contenuGrille.Length;
    //     foreach (Carte carte in grille.contenuGrille)
    //     {

    //         Random rnd = new Random();
    //         int index = rnd.Next(0, longueur);
    //         //Debug.Log(index+ " length : "+longueur);
    //         carte.animFace = grille.listeDesAnimations[index];
    //         grille.remainingAnimations[index] -= 1;
    //         // Debug.Log("remaining "+grille.remainingAnimations[index]);
    //         // Debug.Log("face"+carte.animFace);
    //         if (grille.remainingAnimations[index] == 0)
    //         {
    //             for (int i = index; i < longueur-1; i++)
    //             {
    //                 grille.remainingAnimations[i] = grille.remainingAnimations[i + 1];
    //                 grille.listeDesAnimations[i] = grille.listeDesAnimations[i + 1];

                    
    //             }
    //             longueur -= 1;
    //         }
    //         carte.mode=this;
    //         carte.changerAnimation();
    //         carte.genererNom();
    //     }
    //}

}
