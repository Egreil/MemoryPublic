using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestDeChaqueNiveau
{      /* 1/ Structure
        * chaque niveau possede une Camera un menu de pause, une fenetre de dialogue, 
        * une grille, un mode, 
        * des cartes du type du mode, des faders. Verifier qu'ils sont tous actifs.
        * 
        * 2/ Informations inintiales
        * chaque niveau possède toute les animations dont il a besoin.
        * Les liens entre elements (grille cam/ grille mode/mode grill/carte mode) son fait.
        * 
        * 3/ chaque niveau est genere efficacement et fonctionnel.
        * chaque carte possede le dos du mode.
        * chaque carte possede un nom qui est celui de son animFace.
        * Les scores s'affichent.
        * 
        * 4/ Les sauvegardes fonctionnent
        * 
        * */
    // A Test behaves as an ordinary method
    [Test]
    public void TestDeChaqueNiveauSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestDeChaqueNiveauWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
