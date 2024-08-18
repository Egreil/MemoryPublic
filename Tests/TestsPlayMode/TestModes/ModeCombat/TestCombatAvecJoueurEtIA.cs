using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestCombatAvecJoueurEtIA
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestCombatAvecJoueurEtIASimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.

    [UnityTest]
    public IEnumerator JouerPartieCompleteIAseule()
    {
        //arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Test/TestGrille8Combat"));
        ModeCombat modeCombat = MonoBehaviour.Instantiate(Resources.Load<ModeCombat>("Prefabs/Test/TestModeCombatJoueurEtIA"));
        grille.mode = modeCombat;
        //grille.test = true;
        //modeJungle.test = true; Situation en reel
        modeCombat.grille = grille;

        //act

        yield return null;
        int t = 0;
        while (t < 20 && grille.contenuGrille.Count(c => !c.anim.GetBool("shine")) > 0) {
            modeCombat.tour=1; //on dit que c'est au tour de l'IA

            //on passe le nombre de frame n�cessaire � la r�alisation de l'animation
            for (int i = 0; i < grille.contenuGrille[0].getFrameTotalAnimation() * 2 + 10; i++)
            {
                yield return null;
            }
            for (int i = 0; i < grille.contenuGrille[0].getFrameTotalAnimation() * + 10; i++) // on attend longtemps que l'IA Joue
            {
                yield return null;
            }
            t += 1;
        } 
    
        //assert
        Assert.AreEqual(0, modeCombat.nbrCarteRetournee);
        Assert.AreEqual(8, grille.contenuGrille.Count(c => c.anim.GetBool("shine")));
        Assert.IsTrue(t<20);
    }
    
    // [UnityTest]
    // public IEnumerator JouerPartieCompleteContreIAModeCombatCarteCombat()
    // {
    //     //arrange
    //     JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Test/TestGrille8Combat"));
    //     ModeCombat modeCombat = MonoBehaviour.Instantiate(Resources.Load<ModeCombat>("Prefabs/Test/TestModeCombatJoueurEtIA"));
    //     grille.mode = modeCombat;
    //     //grille.test = true;
    //     //modeJungle.test = true; Situation en reel
    //     modeCombat.grille = grille;

    //     //act

    //     yield return null;
    //     int t = 0;
    //     while (t < 20 && grille.contenuGrille.Count(c => !c.anim.GetBool("shine")) > 0) {
            

    //         //on passe le nombre de frame n�cessaire � la r�alisation de l'animation
    //         for (int i = 0; i < grille.contenuGrille[0].getFrameTotalAnimation() * 2 + 10; i++)
    //         {
    //             yield return null;
    //         }
    //         for (int i = 0; i < grille.contenuGrille[0].getFrameTotalAnimation() * + 10; i++) // on attend longtemps que l'IA Joue
    //         {
    //             yield return null;
    //         }
    //         t += 1;
    //     } 
    
    //     //assert
    //     Assert.AreEqual(0, modeCombat.nbrCarteRetournee);
    //     Assert.AreEqual(8, grille.contenuGrille.Count(c => c.anim.GetBool("shine")));
    //     Assert.IsTrue(t<20);
    // }
    
}
