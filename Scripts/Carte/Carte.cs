using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Carte : MonoBehaviour
{
    // Setup des variables
    public Sprite dos;
    public Sprite face;
    public AnimationClip animDos;
    public AnimationClip animFace;
    
    public Mode mode;

    public Animator anim;
    protected AnimatorOverrideController animatorOverrideController;
    protected AnimationClipOverrides clipOverrides;

   
    // Calcul des animations
    public bool tourne = false;
    public bool stop = false;
    protected int frame = 300;
    protected int frameTotalAnimation = 90;
    // Start is called before the first frame updat
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void associerBouton(){
        this.GetComponent<Button>().onClick.AddListener(this.onClickJoueur);
    }

    public virtual void onClick()
    {
        if (!anim.GetBool("shine") && !tourne && mode.nbrCarteRetournee < 2)
        {
            tournerVersImage();
            ajouterAuxCartesRetournees();
            RemplirMemoireTempTour();
        }
    }

    public virtual void onClickJoueur()
    {
        if (mode.tour == 0)
        {
            onClick();
        }
    }

    //public abstract void comparer();
    public abstract void resetCarte();
    public virtual void genererNom(){
        gameObject.name=animFace.name;
    }
    public void changerAnimation(){

        animDos=mode.Dos;
        anim = GetComponent<Animator>();
        //Debug.Log(animDos);
        animatorOverrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = animatorOverrideController;

        clipOverrides = new AnimationClipOverrides(animatorOverrideController.overridesCount);
        animatorOverrideController.GetOverrides(clipOverrides);

       // Debug.Log(animDos+" "+animFace);
        clipOverrides["Dos"] = animDos;
        Debug.Log(clipOverrides["Dos"]);
        clipOverrides["Face"] = animFace;
        animatorOverrideController.ApplyOverrides(clipOverrides); 
    }

    public void ajouterAuxCartesRetournees(){
        //Debug.Log(mode);
        if(mode.nbrCarteRetournee<2){
	    mode.carteRetournee[mode.nbrCarteRetournee]=this;
	    mode.nbrCarteRetournee+=1;
        }  
    }

    public AnimationClipOverrides getClipOverrides()
    {
        return this.clipOverrides;
    }


    public virtual void activer()
    {
        anim.SetBool("shine", true);
        Debug.Log("actif");
    }
    public virtual void desactiver()
    {
        anim.SetBool("shine", false);
        Debug.Log("desactiver");
    }







    /// <summary>
    /// Listing des fonctions pour gérer les rotations de cartes.
    /// </summary>
    public virtual void calculAnimationRotation()
    {
        if (tourne == true && frame < frameTotalAnimation)
        {
            this.transform.Rotate(0f, (float)(180/ frameTotalAnimation), 0f); //180 parce que un demi tour est 180°
            if (frame == frameTotalAnimation*0.5)
            {
                activer();
            }
            frame += 1;
        }
        if (frame == frameTotalAnimation)
        {
            stop = true;
            frame += 1;
        }

        if (frame > frameTotalAnimation && frame <= frameTotalAnimation*2 && stop == false)
        {
            this.transform.Rotate(0f, (float)(180 / frameTotalAnimation), 0f);
            if (frame == frameTotalAnimation*1.5)
            {
                desactiver();
            }
            frame += 1;
        }
    }

    public void tournerVersImage()
    {
        if (mode.nbrCarteRetournee < 2)
        {
            tourne = true;
            frame = 0;
        }
    }

    public void tournerVersDos()
    {
        
        //if (mode.nbrCarteRetournee == 2)
        //{
            stop = false;
            tourne = false;
        //}
    }

    public int getFrameTotalAnimation()
    {
        return frameTotalAnimation;
    }

//Pour IA
    public void RemplirMemoireTempTour() //OK
    {
        if (mode is ModeCombat)
        {
            ((ModeCombat)mode).memoireTempTour[((ModeCombat)mode).indexMemoireTemp] = this;
            ((ModeCombat)mode).indexMemoireTemp = (((ModeCombat)mode).indexMemoireTemp + 1) % 2;
        }
    }
}
