using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

/** Les classe fille de celle ci servent à l'affichage des éléments de décors en fonction du thème choisi
* La classe abstraite mode permet de normaliser le contenu de chaque classe mode fille et de gérer des éléments 
* autres propres au mode de jeu.
* Chaque fille mode de jeu dois avoir sa façon de calculer le score.
* Chaque fille va recevoir en attribut l'ensemble des images disponibles en association avec le mode de jeu.
* La méthode rejouer remet a 0 les variables dépendantes du mode de jeu.
*
**/

public abstract class Mode : MonoBehaviour
{
    public AnimationClip Dos;
    public AnimationClip[] listeDesAnimations;
    public JeuDeBase grille;


    public int nbrCarteRetournee=0;// Nombre sur le tour de jeu
    public int totalCarteRetournee=0;// Total sur le plateau
    public Carte[] carteRetournee=new Carte[2];

    public abstract void calculTour();
    public abstract void testVictoire();
    public abstract void afficherDecor();
    public abstract void rejouer();
    public abstract bool comparer();

    //Pour le VS IA
    public int tour=0;

    public void resetCartes(){
        carteRetournee[0].resetCarte();
        carteRetournee[1].resetCarte();
        nbrCarteRetournee=0;
    }

    //Pour les tests
    public bool test = false;

    public virtual int[] setRemainingAnimations()
    {
        int[] sortie = new int[listeDesAnimations.Length];
        for (int i = 0; i < sortie.Length; i++)
        {
            sortie[i] = 2;
        }
        return sortie;
    }

        public virtual int donnerLongueur(){
            return grille.contenuGrille.Length / 2;
        }

        public virtual void tirageDesAnimationsMode()
        {
            int longueur = donnerLongueur();
            foreach (Carte carte in grille.contenuGrille)
            {
                
                Random rnd = new Random();
                int index = rnd.Next(0, longueur);
                //Debug.Log(index+ " length : "+longueur);
                carte.animFace = grille.listeDesAnimations[index];
                grille.remainingAnimations[index] -= 1;
                // Debug.Log("remaining "+remainingAnimations[index]);
                // Debug.Log("face"+carte.animFace);
                if (grille.remainingAnimations[index] == 0)
                {
                    for (int i = index; i < longueur-1; i++)
                    {
                        grille.remainingAnimations[i] = grille.remainingAnimations[i + 1];
                        grille.listeDesAnimations[i] = grille.listeDesAnimations[i + 1];

                        
                    }
                    longueur -= 1;
                }
                carte.mode=this;
                Debug.Log(carte.mode.Dos);
                carte.changerAnimation();
                carte.genererNom();
                
            }
        }

    /*
        public void copierArrayAnimations(AnimationClip[] avant, AnimationClip[] apres)
        {
            int i = 0;
            foreach (AnimationClip s in apres)
            {
                avant[i] = s;
                i++;
            }
        }


        public void RejouerAnimations()
        {
            foreach (Carte m in grille.contenuGrille)
            {
                m.resetCarte();
            }
            rejouer();
            copierArrayAnimations(listeDesAnimations, listeDesAnimationsCopie);
            setRemainingAnimations();
        }

        public void setRemainingAnimations()
        {
            for (int i = 0; i < remainingAnimations.Length; i++)
            {
                remainingAnimations[i] = 2;

            }
        }*/
}
