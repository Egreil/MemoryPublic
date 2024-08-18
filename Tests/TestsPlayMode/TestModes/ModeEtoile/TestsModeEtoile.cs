using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestsModeEtoile
{
    //1-Mise en place du plateau dos
    //2-Mise en place du plateau face carte
    //3-Paire
    //4-Non Paire 
    //5-Victoire
    //6- Defaite

    // A Test behaves as an ordinary method
    [Test]
    public void ModeEtoileTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ModeEtoileDos()
    {
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Test/TestGrille8Etoile"));
        ModeEtoile mode = MonoBehaviour.Instantiate(Resources.Load<ModeEtoile>("Prefabs/Modes/ModeEtoile/ModeEtoile"));
        grille.mode = mode;
        mode.test=true;
        mode.grille = grille;
        yield return null;
        
        Assert.AreEqual(grille.contenuGrille.Length, grille.contenuGrille.Count(c => c.animDos.name.Equals(c.getClipOverrides()["Dos"].name)));
       
        
    }

    [UnityTest]
    public IEnumerator ModeEtoileFaces()
    {
        //arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Test/TestGrille8Etoile"));
        ModeEtoile mode = MonoBehaviour.Instantiate(Resources.Load<ModeEtoile>("Prefabs/Modes/ModeEtoile/ModeEtoile"));
        grille.mode = mode;
        grille.test=true;
        mode.grille = grille;
        yield return null;
        mode.test=true;
        //assert
        Assert.AreEqual(grille.contenuGrille.Length, grille.contenuGrille.Count(c => c.gameObject.name.Equals(c.getClipOverrides()["Face"].name)));
    }

    [UnityTest]
    public IEnumerator ModeEtoilePaire()
    {
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Test/TestGrille8Etoile"));
        ModeEtoile mode = MonoBehaviour.Instantiate(Resources.Load<ModeEtoile>("Prefabs/Modes/ModeEtoile/ModeEtoile"));
        grille.mode = mode;
        grille.test=true;
        mode.grille = grille;
        mode.test=true;
        yield return null;
        
        //act
        string nomCarte=grille.contenuGrille[0].name;
        // On ne bloque pas le nombre maximale de touches retournables 
        //pour verifier les contraites du onCLickJoueur();
        foreach(Carte c in grille.contenuGrille){
            Debug.Log(nomCarte+"      "+c.gameObject.name);
                if(c.gameObject.name.Equals(nomCarte)){
                c.onClick();
                Debug.Log(c.anim.GetBool("shine"));
            }
        }
        mode.comparer();
        yield return null;
        //assert
        Assert.AreEqual(2, grille.contenuGrille.Count(c => c.anim.GetBool("shine")==true));
    }

    [UnityTest]
    public IEnumerator ModeEtoileNonPaire()
    {
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Test/TestGrille8Etoile"));
        ModeEtoile mode = MonoBehaviour.Instantiate(Resources.Load<ModeEtoile>("Prefabs/Modes/ModeEtoile/ModeEtoile"));
        grille.mode = mode;
        grille.test=true;
        mode.grille = grille;
        mode.test=false;
        yield return null;
        
        //act
        string nomCarte=grille.contenuGrille[0].name;
        bool found=false;
        foreach(Carte c in grille.contenuGrille){
            //La premiere carte est toujours activée
            if(c.gameObject.name.Equals(nomCarte) && !found){
                c.onClick();
                found=true;
            }
            //La seconde carte sera la première suivante differente
            if(!c.gameObject.name.Equals(nomCarte)){
                c.onClick();
                break;
            }
        }
        mode.comparer();
        //assert
        for (int i=0;i<121;i++){
            Debug.Log(i);
            Assert.IsTrue(grille.contenuGrille[0].anim.GetBool("shine"));
            
            yield return null;
        }
        Assert.IsTrue(!grille.contenuGrille[0].anim.GetBool("shine"));
        Assert.AreEqual(0, grille.contenuGrille.Count(c => c.anim.GetBool("shine")==true));
    }

    [UnityTest]
    public IEnumerator ModeEtoileVictoire()
    {
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Test/TestGrille8Etoile"));
        ModeEtoile mode = MonoBehaviour.Instantiate(Resources.Load<ModeEtoile>("Prefabs/Modes/ModeEtoile/ModeEtoile"));
        grille.mode = mode;
        grille.test=true;
        mode.grille = grille;
        yield return null;
        mode.test=false;
        //act
        try{
            int i=0;
            while(mode.totalCarteRetournee<grille.contenuGrille.Length && i<5){
                bool found=false;
                string nomCarte="";
                foreach(Carte c in grille.contenuGrille){
                    //La premiere carte est toujours activée
                    
                    if(!c.anim.GetBool("shine") && !found){
                        c.onClick();
                        nomCarte=c.gameObject.name;
                        found=true;
                    }
                    //La seconde carte sera la première suivante differente
                    else if(c.gameObject.name.Equals(nomCarte) && found){
                        c.onClick();
                        break;
                    }
                }
                mode.comparer();
                i++;
            }
        }
        catch(Exception e){
            Debug.Log("Victoire :"+e.Message);
            
        }
       //assert
        Assert.AreEqual(grille.contenuGrille.Length, grille.contenuGrille.Count(c => c.anim.GetBool("shine")==true));
    }
}
