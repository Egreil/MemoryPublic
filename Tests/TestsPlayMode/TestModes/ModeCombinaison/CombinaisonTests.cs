using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class CombinaisonTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void CombinaisonSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    //Test de lancement de la partie, generation de la grille
    //Test de l'affichage de la combinaison sur toute la duree.
    //Test de l'absence de combinaison et affichage des cartesPossibilites
    //Test de la défaite
    //Test de la victoire
    [UnityTest]
    public IEnumerator GenerationNiveauCombinaison()
    {
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Modes/Combinaison/GrilleModeCombinaison"));
        ModeCombinaison mode = MonoBehaviour.Instantiate(Resources.Load<ModeCombinaison>("Prefabs/Modes/Combinaison/ModeCombinaison5Ville"));
        grille.mode = mode;
        mode.grille = grille;

       

        Assert.AreEqual(mode.combinaison.Length, mode.combinaison.Count(c => c.gameObject.name.Equals(c.getClipOverrides()["Face"])));
        Assert.AreEqual(mode.cartePossibilites.Length, mode.cartePossibilites.Count(c => c.gameObject.name.Equals(c.getClipOverrides()["Face"])));
        yield return null;//return après sinon tout est désactivé.
    }

    // Test de l'affichage sur la duree.
    [UnityTest]
    public IEnumerator SetPretCombinaison()
    {
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Modes/Combinaison/GrilleModeCombinaison"));
        ModeCombinaison mode = MonoBehaviour.Instantiate(Resources.Load<ModeCombinaison>("Prefabs/Modes/Combinaison/ModeCombinaison5Ville"));
        grille.mode = mode;
        mode.grille = grille;
        mode.declencher();
        yield return null;
        Assert.IsTrue(mode.duree > 0);
        while (mode.dureeEcoulee<mode.duree)
        {
            Assert.IsTrue(!mode.pret);
            Assert.AreEqual(mode.combinaison.Length, mode.combinaison.Count(c => c.anim.GetBool("shine")));
            Assert.AreEqual(0, mode.cartePossibilites.Count(c => c.anim.GetBool("shine")));
            yield return null;
        }
        yield return null;
        //AssertIsTrue(mode.pret);
        Assert.AreEqual(0, mode.combinaison.Count(c => c.anim.GetBool("shine")));
        Assert.AreEqual(mode.cartePossibilites.Length, mode.cartePossibilites.Count(c => c.anim.GetBool("shine")));

        
    }
    // Test de l'affichage sur la duree.
    [UnityTest]
    public IEnumerator DefaiteCombinaison()
    {
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Modes/Combinaison/GrilleModeCombinaison"));
        ModeCombinaison mode = MonoBehaviour.Instantiate(Resources.Load<ModeCombinaison>("Prefabs/Modes/Combinaison/ModeCombinaison5Ville"));
        grille.mode = mode;
        mode.grille = grille;
        mode.declencher();
        yield return null;
        Assert.IsTrue(mode.duree > 0);
        while (mode.dureeEcoulee < mode.duree)
        {
            yield return null;
        }
        yield return null;
        for(int i=0; i < mode.cartePossibilites.Length; i++)
        {
            if (!mode.cartePossibilites[i].gameObject.name.Equals(mode.combinaison[0].gameObject.name)){
                mode.cartePossibilites[i].onClickJoueur();
            }
        }
        Assert.IsTrue(!mode.combinaison[0].anim.GetBool("shine"));


    }
    [UnityTest]
    public IEnumerator VictoireCombinaison()
    {
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Modes/Combinaison/GrilleModeCombinaison"));
        ModeCombinaison mode = MonoBehaviour.Instantiate(Resources.Load<ModeCombinaison>("Prefabs/Modes/Combinaison/ModeCombinaison5Ville"));
        grille.mode = mode;
        mode.grille = grille;
        mode.declencher();
        yield return null;
        Assert.IsTrue(mode.duree > 0);
        while (mode.dureeEcoulee < mode.duree)
        {
            yield return null;
        }
        yield return null;
        int j = 0;
        while (mode.indexReponse < mode.combinaison.Length && j<mode.combinaison.Length) { 
            for (int i = 0; i < mode.cartePossibilites.Length; i++)
            {
                if (mode.cartePossibilites[i].gameObject.name.Equals(mode.combinaison[mode.indexReponse].gameObject.name)){
                    //Debug.Log(j);
                    //Debug.Log("i :" + mode.cartePossibilites[i].gameObject.name + "        j :" + mode.combinaison[mode.indexReponse].gameObject.name);
                    mode.cartePossibilites[i].onClickJoueur();
                    break;
                   }
            }
            j += 1;
        }
        Assert.AreEqual(mode.combinaison.Length, mode.combinaison.Count(c => c.anim.GetBool("shine")));
    }

}
