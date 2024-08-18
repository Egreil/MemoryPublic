using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;



[System.Serializable]

public class Save 
{
    /**La sauvegarde comporte une liste des niveaux triés par ordre d'avancement du jeu.
   
     Chaque fois qu'un niveau est terminé, le nom du niveau devient le dernier élémenents de la liste et sa partie de la map est débloquée avec un
     accès au niveau suivant.
     Lors du chargement de la map à l'ouverture du jeu tout morceau de map jusqu'au dernier niveau terminé sont réaffichés.
     Les niveaux terminés s'ajoutent aux "souvenirs" (niveau rejouables);
     Sona repart du dernier niveau terminé.
    **/
    public List<string> niveauxDebloques = new List<string>();

    // List qui retient le plus haut niveau de difficulté auquel le niveau a été terminé.
    // Booleen pour les éléments débloqués, clés, succès,... 
    public string dernierNiveau="";
    public string positionSona = "";
    public bool genererListeNiveau=true;// Devient false après le première ouverture de la map. Creer un prefab pour stocker la liste des objects à travers les niveaux
    



    public int adversaireActuel = 0;
    public string mode = "";


    //parametrage touches J1
    public string vHJ1="z";
    public string vBJ1="s";
    public string dpGJ1="q";
    public string dpDJ1="d";
    public string attJ1="t";
    public string sautJ1 = "space";


}