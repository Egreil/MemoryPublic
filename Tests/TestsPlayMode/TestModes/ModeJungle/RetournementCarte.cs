using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class TestPartieJungle
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestPartieSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // Activer 3 carte parmis 8 et voir si seulement 2 sont activees.
    [UnityTest] 
    public IEnumerator ActivationCarte()
    {
        //Arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Test/TestGrille8"));
        ModeJungle modeJungle = MonoBehaviour.Instantiate(Resources.Load<ModeJungle>("Prefabs/Test/TestModeJungle"));
        grille.mode = modeJungle;
        grille.test = true;
        modeJungle.test = true;
        modeJungle.grille = grille;

        yield return null;

        //Act
        grille.contenuGrille[0].onClick();
/*        Debug.Log(grille.contenuGrille[1].tourne);
        Debug.Log(modeJungle.nbrCarteRetournee);
        Debug.Log(grille.contenuGrille[1].anim.GetBool("shine"));*/
        grille.contenuGrille[1].onClick();
/*        Debug.Log(modeJungle.nbrCarteRetournee);
        Debug.Log(grille.contenuGrille[1].tourne);
        Debug.Log(grille.contenuGrille[1].anim.GetBool("shine"));*/
        grille.contenuGrille[2].onClick();


        //Assert
        Assert.AreEqual(2,modeJungle.nbrCarteRetournee);
        Assert.IsTrue(grille.contenuGrille[0].tourne);
        Assert.IsTrue(grille.contenuGrille[1].tourne);
        Assert.IsFalse(grille.contenuGrille[2].tourne);

    }

    // Activer 2 fois la même carte parmis 8
    // et voir si le nombre de carte retournée reste 1
    [UnityTest] 
    public IEnumerator ActivationCarteUnique()
    {
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Test/TestGrille8"));
        ModeJungle modeJungle = MonoBehaviour.Instantiate(Resources.Load<ModeJungle>("Prefabs/Test/TestModeJungle"));
        grille.mode = modeJungle;
        grille.test = true;
        modeJungle.test = true;
        modeJungle.grille = grille;

        yield return null;

        grille.contenuGrille[0].onClick();
        grille.contenuGrille[0].onClick();


        //Assert
        Assert.AreEqual(1, modeJungle.nbrCarteRetournee);
        Assert.IsTrue(grille.contenuGrille[0].tourne);


    }

    // Activer 2 carte identiques parmis 8
    // et voir si la paire est detectee. Les cartes restent actives.
    // Le score ne monte pas.
    [UnityTest]
    public IEnumerator ActivationCartePaire()
    {
        //arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Test/TestGrille8"));
        ModeJungle modeJungle = MonoBehaviour.Instantiate(Resources.Load<ModeJungle>("Prefabs/Test/TestModeJungle"));
        grille.mode = modeJungle;
        grille.test = true;
        //modeJungle.test = true;
        modeJungle.grille = grille;
        
        //act
        yield return null;
        modeJungle.forme = false;
        foreach (Carte c in grille.contenuGrille)
        {
            if (c.name.Contains("rouge"))
            {
                c.onClick();
                Debug.Log(c.gameObject.name+" shine:"+c.anim.GetBool("shine"));
            }
        }
        
        //on passe le nombre de frame nécessaire à la réalisation de l'animation
        for (int i = 0; i < grille.contenuGrille[0].getFrameTotalAnimation() + 1; i++)
        {
            yield return null;
        }
        //modeJungle.comparer();
        //assert
        Assert.AreEqual(0, modeJungle.nbrCarteRetournee);
        Assert.AreEqual(2,grille.contenuGrille.Count(c=>c.anim.GetBool("shine")));

    }


    // Activer 2 carte Differentes parmis 8 et voir si la paire
    // est declaree fausse et les cartes desactivée.
    // Le score monte de 1.
    [UnityTest] 
    public IEnumerator DesactivationCartesSansPaire()
    {
        //arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Test/TestGrille8"));
        ModeJungle modeJungle = MonoBehaviour.Instantiate(Resources.Load<ModeJungle>("Prefabs/Test/TestModeJungle"));
        grille.mode = modeJungle;
        grille.test = true;
        //modeJungle.test = true; Situation en reel
        modeJungle.grille = grille;

        //act
        yield return null;
        modeJungle.forme = false;
        bool found = false;
        foreach (Carte c in grille.contenuGrille)
        {
            if (found && c.name.Contains("rouge"))
            {
                c.onClick();
                Debug.Log(c.gameObject.name + " shine:" + c.anim.GetBool("shine"));
            }
            if (!found && !c.name.Contains("rouge"))
            {
                c.onClick();
                Debug.Log(c.gameObject.name + " shine:" + c.anim.GetBool("shine"));
                found = true;
            }
        }
        
        //on passe le nombre de frame nécessaire à la réalisation de l'animation
        for (int i = 0; i < grille.contenuGrille[0].getFrameTotalAnimation()*2 + 10; i++)
        {
            yield return null;
        }
        //assert
        Assert.AreEqual(0, modeJungle.nbrCarteRetournee);
        Assert.AreEqual(0, grille.contenuGrille.Count(c => c.anim.GetBool("shine")));
        Assert.AreEqual(1, modeJungle.score);

    }




    /// A VOIR PLUS TARD///////////////////
    /// 
    /// <returns></returns>er une carte qui coupe le tour ou joue une animation durant le tour.
    // Déclenche un son et monte le score de 1.

    [UnityTest]
    public IEnumerator ActiverActionTour()
    {
        //CouperTour()
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
