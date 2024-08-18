using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonaMap : MonoBehaviour
{

    private GestionDeSauvegarde gestionDeSauvegarde; // Accès sauvegarde


    //Gestion de la position actuelle
    public int indexNiveau;
    public string niveauActuel;
    public MenuLevel menu;
    public Camera camera;
    
    //Deplacement de Sona
    public int ADroite; // ADroite =1 pour droite, -1 pour gauche
    //private int ADroiteTemp; // Pour éviter les bugs.
    public List<Niveau> chemin;
    public int indexNiveauTemp;
    public int frameVoyage;
    private int totalFrameVoyage;
    private float deltaX;
    private float deltaY;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        menu=FindObjectOfType<MenuLevel>();
        chemin=new List<Niveau>();
        totalFrameVoyage=60;
        frameVoyage=61;
        ADroite=1;

        gestionDeSauvegarde=FindObjectOfType<GestionDeSauvegarde>();
        camera=FindObjectOfType<Camera>();
        gestionDeSauvegarde.save.positionSona="Ville1";
        niveauActuel=gestionDeSauvegarde.save.positionSona;
        //recuperer la position du niveau
        positionner();
        indexNiveauTemp=indexNiveau;
        anim=this.GetComponent<Animator>();
        spriteRenderer=this.GetComponent<SpriteRenderer>();
        // trouver l'index du niveau dans la liste des niveaux
        //Calculer l'itinéraire (par flèche ou par click) en passant par les points.
        //Deplacer Sona entre chaque point et activer les animations en conséquence.

    }

    // Update is called once per frame
    void Update()
    {   
        if(!menu.pause){
            camera.transform.position=new Vector3(this.transform.position.x, this.transform.position.y,-1);
        if(Input.GetKeyDown("right")){
            
                indexNiveauTemp+=1;
                ajouterCheminDroite();
            
        }
        else if(Input.GetKeyDown("left")){
            
                indexNiveauTemp-=1;
                ajouterCheminGauche();
            
        }
        
        deplacer();
        }
    }

    //Cette fonction positionne Sona à la position notée dans la sauvegarde
    public void positionner(){
        Debug.Log(gestionDeSauvegarde.save.niveauxDebloques.Count);
        for(int n=0;n<gestionDeSauvegarde.save.niveauxDebloques.Count;n++){
            if(gestionDeSauvegarde.listDesObjetsNiveaux[n].gameObject.name.Equals(gestionDeSauvegarde.save.positionSona)){
                this.transform.position=gestionDeSauvegarde.listDesObjetsNiveaux[n].transform.position;
                indexNiveau=n;
                //Debug.Log(gestionDeSauvegarde.listDesObjetsNiveaux.Length);
                //Debug.Log(gestionDeSauvegarde.listDesObjetsNiveaux[n].transform.position);
                break;
            }
        }
        
    }
    public void deplacer(){
        if(chemin.Count>0){
            //Reset du compte de frame
            if(frameVoyage==totalFrameVoyage+1){
                frameVoyage=0;
            }
            //Calcul du chemin jusque prochaine destination
            if(frameVoyage==0){
                deltaX=(gestionDeSauvegarde.listDesObjetsNiveaux[indexNiveau+ADroite].transform.position.x-gestionDeSauvegarde.listDesObjetsNiveaux[indexNiveau].transform.position.x)/totalFrameVoyage;
                deltaY=(gestionDeSauvegarde.listDesObjetsNiveaux[indexNiveau+ADroite].transform.position.y-gestionDeSauvegarde.listDesObjetsNiveaux[indexNiveau].transform.position.y)/totalFrameVoyage;   
            }
            //deplacement
            this.transform.Translate(new Vector2(deltaX,deltaY));
            //mise à jour des infos de position après arrivée à destination + on enleve l'étape du chemin
            if(frameVoyage==totalFrameVoyage){
                    indexNiveau+=ADroite;
                    niveauActuel=gestionDeSauvegarde.listDesObjetsNiveaux[indexNiveau].gameObject.name;
                chemin.RemoveAt(0);
            }
            frameVoyage+=1;
        }
        else{
            spriteRenderer.flipX=false;
            anim.SetBool("marche",false);
        }

    }

    public void ajouterCheminDroite(){
        if(!(ADroite>0)){
            supprimerChemin();
        }
        if(ADroite>0){
            if(gestionDeSauvegarde.listDesObjetsNiveaux.Length-1>indexNiveauTemp){
            //if(!gestionDeSauvegarde.listDesObjetsNiveaux[indexNiveauTemp+1].gameObject.name.Equals("Fini")){
            chemin.Add(gestionDeSauvegarde.listDesObjetsNiveaux[indexNiveauTemp+ADroite]);
            anim.SetBool("marche",true);
            spriteRenderer.flipX=false;
            }
        }
        
    }

    public void ajouterCheminGauche(){
        if(ADroite>0){
            supprimerChemin();
        }
        if(ADroite<0){
            if(indexNiveauTemp>0){
                chemin.Add(gestionDeSauvegarde.listDesObjetsNiveaux[indexNiveauTemp+ADroite]);
                anim.SetBool("marche",true);
                spriteRenderer.flipX=true;
            }
        }
    }

    public void supprimerChemin(){
        if(chemin.Count==0){
            ADroite=-ADroite;
            indexNiveauTemp=indexNiveau;
            
        }
        else{
            chemin=new List<Niveau>{chemin[0]};
        }
    }


}