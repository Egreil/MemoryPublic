using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using System.Exception;


public class ScriptDialogue : MonoBehaviour
{
    //Récupération des 2 personnages
    public GameObject perso1;
    public GameObject perso2;
    //Récupération des 2 fenêtres de texte
    public TMP_Text textPerso1;
    public TMP_Text textPerso2;
    private TMP_Text textPerso;


    //Initialisation de la lecture ficher
    String fichierDialogue;
    StreamReader sr;
    //Pour l'affichage progressif
    int compte=1;
    int compteFin =0;
    string ligne="";

    public GameObject fader;
    public ActionDialogue[] actions;




    // Start is called before the first frame update
    void Start()
    {   // On desactive tout pour préparer le lvl à coup sur.
        activerPerso(perso1,1,false);
        activerPerso(perso2,2,false);
        
        //1 voir si il y a un dialogue a ce niveau 
        ouvrirFichierDialogue(SceneManager.GetActiveScene().name);
        actions=FindObjectsOfType<ActionDialogue>();
        
        //if(new Fil())
        //2 Vérifier la sauvegarde pour savoir si le niveau n'a pas déjà été terminé sinon réafficher les dialogues.
        //3 Récupérer le fichier script du dialogue

        
    }

    // Update is called once per frame
    void Update()
    {   if (compte<compteFin){
            textPerso.text=textPerso.text+ligne[compte];
            compte+=1;
        }
        if(Input.GetKeyDown("space")){
            if(compte<compteFin){
                textPerso.text=ligne;
                compte=compteFin;
            }
            else{//4 Lire le dialogue
                lectureLigneSuivante();
            }

        }
    }

    public void lectureLigneSuivante(){
                ligne= sr.ReadLine();
                
                if(ligne!=null){
                    associerBulle();
                    entrerPersonnage();
                    activerAnimation();
                    activerFader();
                }
                else{
                    //Fermer Fenêtre dialogue.
                    //Fermer fichier
                    sr.Close();
                    gameObject.SetActive(false);
                }
    }
    //tx-texte-side-Animation.
    public void associerBulle(){
        if(ligne.StartsWith("tx")){
            string[] temp=ligne.Split("-");
            if(temp[2].Equals("0")){
                textPerso=textPerso1;
            }
            else{
                textPerso=textPerso2;
            }
            ligne=temp[1];
            setupBulle(textPerso,ligne); // Preparation à l'écriture du texte
        }
        
            
    }
    public void setupBulle(TMP_Text TMP, string texte){
        TMP.text="";
        compte =0;
        compteFin=texte.Length;
    }
    
    //personnage= nom du personnage  et side=0 pour gauche, 1 pour droite, animation represente l'entree possible.
    //format: entrer-perso-side-animation;
    public void entrerPersonnage(){  
        if (ligne.StartsWith("entrer")){
            string[] temp=ligne.Split("-");
            string personnage=temp[1];
            if(temp[2].Equals("0")){
                perso1.GetComponent<Image>().sprite=Resources.Load<Sprite>("Sprites/Personnages/"+personnage);
                activerPerso(perso1,1,true);
            }
            else{
                perso2.GetComponent<Image>().sprite=Resources.Load<Sprite>("Sprites/Personnages/"+personnage);
                activerPerso(perso2,2,true);
            }
            finAnimation();
        }
    }

    //Activer perso+text
    public void activerPerso(GameObject perso,int id, bool activer){
        perso.gameObject.SetActive(activer);
        if (id==1){
            textPerso1.gameObject.SetActive(activer);
        }
        else{
            textPerso2.gameObject.SetActive(activer);
        }

    }
    //Desactiver perso+text
    public void finAnimation(){
        lectureLigneSuivante();
    }
    //Type d'entree
    // Flash-slide-vanish
    // entree= true pour une entree et false pour une sortie
    public void flash(){

    }
    
    public void ouvrirFichierDialogue(String nomDuFichier){
        fichierDialogue="./Assets/Dialogues/Dialogue"+nomDuFichier+".txt";
        if(!File.Exists(fichierDialogue)){
            gameObject.SetActive(false);
        }
        try
        { 
            //Pass the file path and file name to the StreamReader constructor
            sr = new StreamReader(fichierDialogue);

            //Lancement de la première phrase du dialogue
            lectureLigneSuivante();
        }
        catch(Exception e)
        {
            Debug.Log("Exception: " + e.Message);
            gameObject.SetActive(false);
        }
    }
    //Possibilités:FadeIn-FadeOut-chargerScene
    //fader-action-scene
    public void activerFader(){
        if(ligne.StartsWith("fader")){
            string[] ligneSplit=ligne.Split("-");
            string action= ligneSplit[1];
            fader.GetComponent<Animator>().SetBool(action,true);
            Debug.Log("active "+ action+ " du fader");
            // if(action.Equals(chargerScene)){
            //     //sauvegarder?
            //     //
            //     SceneManager.LoadScene(ligneSplit[2]);
            // }
        }
        
        
    }

    public void activerAnimation(){
        if(ligne.StartsWith("animation")){
            string[] ligneSplit=ligne.Split("-");
            string action= ligneSplit[1];
            foreach(ActionDialogue a in actions){
                if(a.gameObject.name.Equals(action)){
                    a.activer();
                    Debug.Log(a);
                   break; 
                }
            }
        }
    }
}
    
