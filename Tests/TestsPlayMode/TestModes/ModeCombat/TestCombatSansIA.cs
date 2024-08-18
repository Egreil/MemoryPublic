using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestCombatSansIA
{
    // A Test behaves as an ordinary method
    [Test]
    public void TestCombatSansIASimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestCombatSansIAWithEnumeratorPasses()
    {
        // JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Grille"));
        // ModeCombat modeCombat = MonoBehaviour.Instantiate(Resources.Load<ModeCombat>("Prefabs/Test/TestModeCombatJoueurEtIA"));
        

        // grille.mode = modeCombat;
        // grille.test = true;
        // modeCombat.test = true;
        // modeCombat.grille = grille;

        // yield return null;

        // //Act
        // grille.contenuGrille[0].onClick();
        // /*        Debug.Log(grille.contenuGrille[1].tourne);
        //         Debug.Log(modeJungle.nbrCarteRetournee);
        //         Debug.Log(grille.contenuGrille[1].anim.GetBool("shine"));*/
        // grille.contenuGrille[1].onClick();
        // /*        Debug.Log(modeJungle.nbrCarteRetournee);
        //         Debug.Log(grille.contenuGrille[1].tourne);
        //         Debug.Log(grille.contenuGrille[1].anim.GetBool("shine"));*/
        // grille.contenuGrille[2].onClick();


        // //Assert
        // Assert.AreEqual(2, modeCombat.nbrCarteRetournee);
        // Assert.IsTrue(grille.contenuGrille[0].tourne);
        // Assert.IsTrue(grille.contenuGrille[1].tourne);
        // Assert.IsFalse(grille.contenuGrille[2].tourne);
        yield return null;
    }
}
