using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random=System.Random;
using System;


public class JeuDeBase : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public string theme;
    public Mode mode;
    public bool test;
    public GestionDeSauvegarde gestionDeSauvegarde;
    public ScriptDialogue dialogue;
    //public AnimationClip animation;

    //Test de Point
    // public int nbrCarteRetournee=0;
    // public Carte[] carteRetournee= new Carte[2];
    // private int totalCarteRetournee=0;
    // private int score=0;
    // private int attente=300;

 
    

    //Genreration des cartes
    // public Sprite dos;
    // public Sprite[] listeDesImages;
    // public Sprite[] listeDesImagesCopie;
    // private int[] remainingImages;
    public AnimationClip[] listeDesAnimations;
    public AnimationClip[] listeDesAnimationsCopie;
    public int[] remainingAnimations;

    //Récupération des cartes du plateau.
    public Carte[] contenuGrille;
    public int nombreDImage=0;// ContenuGrille.length

    void Start()
    {//Récupérer toutes les images de la grille.
    
                
                mode = FindObjectsOfType<Mode>()[0];
                //Debug.Log(mode);
                mode.grille = this;
                //Pour l'evenementiel:
                dialogue=FindObjectOfType<ScriptDialogue>();
                gestionDeSauvegarde=FindObjectOfType<GestionDeSauvegarde>();
                
        if (!test) { 
            try { 
            contenuGrille=FindObjectsOfType<Carte>();
            cam = FindObjectsOfType<Camera>()[0];
            this.GetComponent<Canvas>().worldCamera=cam;
            
            // as GameObject[];
                
        //On initialise le dos de carte ici car les cartes sont crées avant la grille.
        //typeTheme=Type.GetType(theme);
        /*
        for(int i=0;i<contenuGrille.Length;i++){
            Carte c=contenuGrille[i];

            c.gameObject.AddComponent(Type.GetType(theme));
            Destroy(c.GetComponent<Carte>());
            contenuGrille[i]=(Carte)c;
            //Activation du script correspondant
            //On recupere le component possedant le string theme, et on le converti en behaviour pour accéder à enabled.
            //((Behaviour)c.GetComponent(theme)).enabled=true;
            
            //Attribution des animations/images
            //c.dos=dos; //Une amélioration peut être d'aller chercher le dos de carte dans les fichiers.
            
        }
        
        contenuGrille=(Carte[])FindObjectsOfType(Type.GetType(theme));*/
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);

            }
        }
        //animDos=contenuGrille[0].GetComponent(theme).animDos;
        listeDesAnimations =copierArrayAnimations(mode.listeDesAnimations);
        listeDesAnimationsCopie=copierArrayAnimations(listeDesAnimations);
        

        
        
        // remainingImages=new int[listeDesImages.Length];
        // listeDesImagesCopie=new Sprite[listeDesImages.Length];
        // copierArray(listeDesImagesCopie,listeDesImages);


    //FileInfo[] file =recupereListeDesImages();
        associerAttributsCarte();
        remainingAnimations=mode.setRemainingAnimations();
        tirageDesAnimations();//Tirage des animations et association des noms de carte
        
        Debug.Log("Fin Jeu de base");

    }

    // Update is called once per frame
    void Update()
    {
    }

    public string dossierImage;
    
    public void associerAttributsCarte(){
        foreach(Carte m in contenuGrille){
            m.associerBouton();
        }
    }
    public void Rejouer(){
        RejouerAnimations();
        tirageDesAnimations();
    }
    public void victoire(){
        //Sauvegarder
        gestionDeSauvegarde.save.dernierNiveau=SceneManager.GetActiveScene().name;
        
        //Activer les dialogues s'il y en a
        activerDialogue(SceneManager.GetActiveScene().name+"Fin");
        //Le dialogue active le fader

        //A la fin du fader retour à la carte
    }

    public void activerDialogue(string nomDuFichier){
        dialogue.gameObject.SetActive(true);
        dialogue.ouvrirFichierDialogue(SceneManager.GetActiveScene().name+"Fin");
    }
    
    // public void resetPartie(){
    //     nbrCarteRetournee=0;
    //     carteRetournee= new Carte[2];
    //     score=0;
    //     attente=300;
    //     totalCarteRetournee=0;
    // }
    // public void testVictoire(){
    //     if(totalCarteRetournee==contenuGrille.Length){
    //         Debug.Log("Victoire"); 
    //         SceneManager.LoadScene("Ville");
    //         //Fermer le lvl et déclencher les actions en conséquence.
    //     }
    // }
    //////////////////////////////POUR LES ANIMATIONS ///////////////////////////////////////////////////////
    public void tirageDesAnimations()
    {
        mode.tirageDesAnimationsMode();
    }


    public AnimationClip[] copierArrayAnimations(AnimationClip[] apres){
        AnimationClip[] avant = new AnimationClip[apres.Length];
        Debug.Log(avant.Length);
                int i=0;
        foreach(AnimationClip s in apres){
            avant[i]=s;
            i++;
        }
        return avant;
    }


    public void RejouerAnimations(){
        foreach(Carte m in contenuGrille){
            m.resetCarte();
        }
        mode.rejouer();
        listeDesAnimations=copierArrayAnimations(listeDesAnimationsCopie);
        remainingAnimations=mode.setRemainingAnimations();
    }



















/////////////////////////////////////////// POUR LES IMAGES ///////////////////////////////////////
    // public void tirageDesImages(){
    // //     //Récupérer la liste des images
    // // listeDesImages=recupereListeDesImages();
    // int longueur=contenuGrille.Length/2;
    //     foreach (Carte carte in contenuGrille){
            
    // //     //tirageDuNombreAleatoire
    //         Random rnd= new Random();
    //         int index=rnd.Next(0,longueur); 

    // //     //assocition de l'image à la carte
    //         //carte.GetComponent<Image>().sprite=listeDesImages[index];
    //         carte.face=listeDesImages[index];

    // //     //retirer l'image de la liste
    //         remainingImages[index]-=1;
    //         //Debug.Log("remaining "+remainingImages[index]);
    //         // A corriger
    //         //Debug.Log("face"+carte.face);
    //         if(remainingImages[index]==0){
    //             for(int i=index;i<longueur;i++){
    //                 remainingImages[i]=remainingImages[i+1];
    //                 listeDesImages[i]=listeDesImages[i+1];
                    
    //                 longueur-=1;                
    //             }
    //         }
    //     }
    // }

    // public void copierArrayImages(Sprite[] avant , Sprite[] apres){
    //             int i=0;
    //     foreach(Sprite s in apres){
    //         avant[i]=s;
    //         i++;
    //     }
    // }

    // public void RejouerImages(){
    //     copierArrayImages(listeDesImages,listeDesImagesCopie);
    //     setRemainingImage();

    // }

    // public void setRemainingImage(){
    //     for(int i=0;i<remainingImages.Length;i++){
    //         remainingImages[i]=2;
            
    //     }
    // }




    // public FileInfo[] recupereListeDesImages(){
        
    //     DirectoryInfo dir= new DirectoryInfo("./Assets/Images/ThemesJeu");//Ajouter +dossierImage
    //     FileInfo[] files=dir.GetFiles("*.jpeg");
    //     string str="";
    //     foreach(FileInfo f in files){
    //         Debug.Log(f.Name);
    //     }
    //     return files;

    // }
}






