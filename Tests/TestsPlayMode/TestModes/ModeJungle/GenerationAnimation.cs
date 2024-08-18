using System.Collections;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;

public class GenerationGrille
{
    // A Test behaves as an ordinary method
    [Test]
    public void GenerationGrilleSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.

    //Evaluation de la presence des animations
    [UnityTest]
    public IEnumerator TestPresenceDesAnimations()
    {
        //Arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Test/TestGrille8"));
        ModeJungle modeJungle= MonoBehaviour.Instantiate(Resources.Load<ModeJungle>("Prefabs/Test/TestModeJungle"));
        grille.mode = modeJungle;
        grille.test=true;
        modeJungle.test = true;
        modeJungle.grille = grille;
        
        //Act
        foreach (AnimationClip a in modeJungle.listeDesAnimations)
        {
            Debug.Log(a.name);
        }
        yield return null;

        //Assert
        Assert.AreEqual(8, modeJungle.listeDesAnimations.Length);
        Assert.AreEqual(0, modeJungle.listeDesAnimations.Count(a => a == null));
        
       
        
    }
    //verification du contenu de la grille.
    [UnityTest]
    public IEnumerator TestDistributionDesAnimations()
    {
        //Arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Test/TestGrille8"));
        ModeJungle modeJungle = MonoBehaviour.Instantiate(Resources.Load<ModeJungle>("Prefabs/Test/TestModeJungle"));
        grille.mode = modeJungle;
        grille.test=true;
        modeJungle.test = true;
        modeJungle.grille = grille;

        //Act
        yield return null;
        foreach (AnimationClip a in grille.listeDesAnimationsCopie)
        {
            Debug.Log(a.name);
        }
        foreach (Carte c in grille.contenuGrille)
        {
            Debug.Log(c.getClipOverrides()["Face"].name);
        }
        
        //Assert
        for(int i= 0; i < grille.contenuGrille.Length; i++)
        {
            Assert.IsTrue(Array.Exists(grille.contenuGrille,c => c.getClipOverrides()["Face"].name.Equals(grille.listeDesAnimationsCopie[i].name)));
        }

    }

        [UnityTest]
    public IEnumerator TestGenererTag()
    {
        //Arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Test/TestGrille8"));
        ModeJungle modeJungle = MonoBehaviour.Instantiate(Resources.Load<ModeJungle>("Prefabs/Test/TestModeJungle"));
        grille.mode = modeJungle;
        grille.test=true;
        modeJungle.test = true;
        modeJungle.grille = grille;

        //Act
        yield return null;
        foreach (Carte c in grille.contenuGrille)
        {
            Debug.Log(c.gameObject.name);
        }
        
        //Assert
        Assert.AreEqual(8,grille.contenuGrille.Count(c => c.getClipOverrides()["Face"].name.Equals(c.gameObject.name)));
        

    }

    [UnityTest]
    public IEnumerator TestAnimationDos()
    {
        //Arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Test/TestGrille8"));
        ModeJungle modeJungle = MonoBehaviour.Instantiate(Resources.Load<ModeJungle>("Prefabs/Test/TestModeJungle"));
        grille.mode = modeJungle;
        grille.test=true;
        modeJungle.test = true;
        modeJungle.grille = grille;

        //Act
        yield return null;
        foreach (Carte c in grille.contenuGrille)
        {
            Debug.Log(c.gameObject.name);
        }
        
        //Assert
        Assert.AreEqual(8,grille.contenuGrille.Count(c => c.getClipOverrides()["Dos"].name.Equals(modeJungle.Dos.name)));
        

    }

}
