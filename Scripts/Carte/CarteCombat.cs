using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarteCombat : Carte
{
    // Start is called before the first frame update
    
    // Start is called before the first frame update
    void Start()
    {
        tourne = false;
        frame = 400;

        anim = GetComponent<Animator>();//On le laisse pour faire fonctionner les tests mais c'est inutile autrement.

    }

    // Update is called once per frame
    void Update()
    {
        //calculAnimationRotation();
    }


    public override void onClick(){ //OK
        //if(!anim.GetBool("shine") && mode.nbrCarteRetournee < 2) {
        activer();
        ajouterAuxCartesRetournees();
        RemplirMemoireTempTour(); 
        //}
    }

    public void RemplirMemoireTempTour() //OK
    {
        if (mode is ModeCombat)
        {
            ((ModeCombat)mode).memoireTempTour[((ModeCombat)mode).indexMemoireTemp] = this;
            ((ModeCombat)mode).indexMemoireTemp = (((ModeCombat)mode).indexMemoireTemp + 1) % 2;
        }
    }

    public override void resetCarte(){ //OK
        desactiver();
    }

    public override void genererNom(){ //OK
        gameObject.name=animFace.name;
    }

    public void activer() //OK
    {
        if (!anim.GetBool("shine")) { 
        anim.SetBool("shine", true);
        Debug.Log("actif");
        }
    }
    public void desactiver(){ //OK
        anim.SetBool("shine",false);
        Debug.Log("desactiver");
    }
    
/*    public override void associerBouton() //OK
    {
        this.GetComponent<Button>().onClick.AddListener(onClickJoueur);
    }*/
    

    public override bool Equals(object carte) //OK
    {
        // If the passed object is null, return False
        if (carte == null)
        {
            return false;
        }
        // If the passed object is not Customer Type, return False
        if (!(carte is CarteCombat))
        {
            return false;
        }
        return (this.gameObject.name.Equals(((CarteCombat)carte).gameObject.name));
    }

}
