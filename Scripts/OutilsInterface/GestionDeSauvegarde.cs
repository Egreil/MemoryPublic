using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine.SceneManagement;
using System.IO;


public class GestionDeSauvegarde : MonoBehaviour
{
    public Save save;// Voir s'il n'est pas nécessaire de dissocier la sauvegarde de la partie et celle du jeu global.
    public Niveau[] listDesObjetsNiveaux;
    public GameObject brouillard;
    public SonaMap sonaMap;
    public int indexNiveauTemp;
    
 void Start()
    {
        Application.targetFrameRate = 60;
        
        //save = new Save();
        //SaveGame();
        if (!File.Exists(Application.persistentDataPath + "/Sona.save"))
        {Debug.Log("create successed");
            CreateSave();
            SaveGame();          
        }
        else
        {   
            LoadGame();
            // astronaute.game = this;
			// astronaute.GetComponent<Vie>().game=this;
            // Jimmy = Instantiate(astronaute);
            // posXJimmy = save.posMapACharger[0];
            // posYJimmy = save.posMapACharger[1];
            // Jimmy.transform.position = new Vector3(posXJimmy, posYJimmy, 1);

            // camera.jimmy = Jimmy;
            // mainCamera = Instantiate(camera);
			
			// Jimmy.GetComponent<Vie>().textVie=mainCamera.GetComponent<CameraJeu>().vie;
			// Jimmy.GetComponent<Vie>().o2=mainCamera.GetComponent<CameraJeu>().o2;
			// Jimmy.GetComponent<Mouvement>().battery=mainCamera.GetComponent<CameraJeu>().battery;

            // fader.GetComponent<Animator>().SetTrigger("fadeout");

            
            // Debug.Log("load successed");
        }
        
        SaveGame();
        if(SceneManager.GetActiveScene().name.Equals("Map")){
            sonaMap=Resources.Load<SonaMap>("Prefabs/SonaMap");
            Debug.Log("Map");
            GenererListeNiveaux();
            MAJNiveauDebloques();
            creerSona();
            int i=0;
            if(!"fini".Equals(save.dernierNiveau)){
                while (i<save.niveauxDebloques.Count && !save.niveauxDebloques[i].Equals(save.dernierNiveau)){
                    activerMapSansAnimation(i); //l'index i suffit puisqu'on a déjà en parallèle la liste des objects des niveaux.
                    i++;
                }
                activerMapAvecAnimation(i);
            }
            //ActiverMap();
        }
        


    }
    // Update is called once per frame
    
    private void CreateSave()
    {
        save = new Save();/*
        save.ordrePersonnageHistoire = ordrePersonnageHistoire;
        save.ordreFondHistoire = ordreFondHistoire;
        save.mode = mode;

        save.nombreJoueur = nombreJoueur;
        save.persoP1 = persoP1;
        save.persoP2 = persoP2;
        save.fond = fond;*/
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Sona.save");
        bf.Serialize(file, save);
        file.Close();
    }
    
    public void LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/Sona.save", FileMode.Open);
        file.Position = 0;
        save = bf.Deserialize(file) as Save;
        file.Close();
    }

    public void NouvellePartie() // On Efface les données de la dernière partie mais on conserve les succès et meilleurs scores.
    {
        SaveGame();
        Debug.Log("nouveau");
        // save.checkPointPosition = new List<float>() { -314f,-69f } ;
        // save.checkPointMap = "Basement";
        save.niveauxDebloques = new List<string>();
        save.dernierNiveau="";
        SaveGame();
        LoadGame();
        SceneManager.LoadScene("Ville1");
    }

    public void Credit()
    {

    }
    public void ChargerPartie()// En réalité charge la map.
    {
        Debug.Log("charger");
	//save.posMapACharger=save.checkPointPosition;
	SaveGame();
    if(save.dernierNiveau.Equals("")){
        SceneManager.LoadScene("Ville1");
    }
    else{
        SceneManager.LoadScene("Map");
    }
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    public void ResetSauvegarde()//Celle ci efface absolument tout!!!!!!!!!!!!!!!!!!
    {
        save = new Save(); 
        SaveGame();
    }

    public void ajoutNiveauSauvegarde(){
        save.niveauxDebloques.Add(SceneManager.GetActiveScene().name);
        ChargerPartie();
    }
    public void RetourDernierePorte()
    {
        //Debug.Log(fader);
        //fader.GetComponent<Animator>().SetTrigger("retour");
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////// Gestion de l'affichage de la MAP ///////////////////////////////////////////////////
    ////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///
    public void GenererListeNiveaux(){
        listDesObjetsNiveaux=FindObjectsOfType<Niveau>();
        trierParAbcysse(listDesObjetsNiveaux);
    }
    public void trierParAbcysse(Niveau[] tableau){
        for(int i=0;i<listDesObjetsNiveaux.Length-1;i++){
            int indexMin=i;
            for(int j=i+1;j<listDesObjetsNiveaux.Length;j++){
                if(listDesObjetsNiveaux[indexMin].transform.position.x>listDesObjetsNiveaux[j].transform.position.x){
                    indexMin=j;
                }
            }
            Niveau temp=listDesObjetsNiveaux[i];
            listDesObjetsNiveaux[i]=listDesObjetsNiveaux[indexMin];
            listDesObjetsNiveaux[indexMin]=temp;
        }
    }
    public void activerMapAvecAnimation(int index){
        listDesObjetsNiveaux[index].gameObject.SetActive(true);
        
        //Ajouter un animator avec l'animation correspondante à l'apparition progressive du la zone. COMPLIQUE
        /* déplacer l'object qui donne l'impression de l'apprition vers le prochain niveau et activer son animation d'apparition 
        PLUS SIMPLE ET MOINS GOURMAND.*/
       // brouillard.GetComponent<Animator>().SetBool("Actif",true);
        Vector3 positionSuivant=listDesObjetsNiveaux[index+1].transform.position;
        //brouillard.transform.position=new Vector2(positionSuivant.x,positionSuivant.y);
        //listDesObjetsNiveaux.GetComponent<Animator>.SetBool("Actif",true);
    }
    public void activerMapSansAnimation(int index){
        listDesObjetsNiveaux[index].gameObject.SetActive(true);
    }
    public void MAJNiveauDebloques(){
        int i=0;
        save.niveauxDebloques=new List<string>();
        while(!listDesObjetsNiveaux[i].gameObject.name.Equals(save.dernierNiveau) && !listDesObjetsNiveaux[i].name.Equals("Fini")){
            // Debug.Log("i:"+listDesObjetsNiveaux[i].gameObject.name);
            // Debug.Log("dernier niveau"+save.dernierNiveau);
            save.niveauxDebloques.Add(listDesObjetsNiveaux[i].gameObject.name);
            i++;
        }
        
        indexNiveauTemp=i;//Pour passer ce nombre à Sona
        if(!listDesObjetsNiveaux[i].name.Equals("Fini")){
            save.niveauxDebloques.Add(listDesObjetsNiveaux[i].gameObject.name);
        }
        SaveGame();
    }
    public void creerSona(){
        sonaMap= MonoBehaviour.Instantiate(sonaMap);
        sonaMap.indexNiveau=indexNiveauTemp;
    }
    public void sauvegarderPositionSona(){

    }
    public void sauvegarderDernierNiveau(Niveau niveau){
        save.dernierNiveau=niveau.gameObject.name;
    }
}
