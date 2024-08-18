using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;

public class TirageCarteAleatoire
{
    // A Test behaves as an ordinary method
    [Test]
    public void TirageCarteAleatoireSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TirageCarteAleatoireWithEnumeratorPasses()
    {
        //Arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Grille"));
        CarteCombat carte1 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte2 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte3 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte4 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte5 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte6 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        grille.contenuGrille=new Carte[]{carte1,carte2,carte3,carte4,carte5,carte6};
        

        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();
        int j = 0;
        foreach(Carte c in grille.contenuGrille){
            c.mode=modeCombat;
            c.gameObject.name = "carte" + j++;
        }
        
        modeCombat.grille=grille;
        modeCombat.niveauIA = 2;
        modeCombat.paireCarte=new Carte[0];
        var sprite = Resources.Load<Sprite>("Sprites/fond");
        modeCombat.memoireIA=new Carte[]{null,null,null,null};
        yield return null; // On passe une frame pour laisse la fonction Start se jouer
        //Act
        modeCombat.tirerCarteAleatoire();

        Debug.Log((from c in grille.contenuGrille where c.anim.GetBool("shine")!=true select c).ToArray().Length);
        //Assert
        Assert.AreEqual((from c in grille.contenuGrille where c.anim.GetBool("shine")!=true select c).ToArray().Length,5);

        
    }

    //2 On tire une carte aleatoire et on fouille dans la memoire si un double est present
    [UnityTest]
    public IEnumerator RechercheDansMemoireApresTirageAleatoire()
    {
        //Arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Grille"));
        CarteCombat carte1 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte2 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte3 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte4 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte5 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte6 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        grille.contenuGrille=new Carte[]{carte1,carte2,carte3,carte4,carte5,carte6};
        

        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();
        yield return null;
        int j = 1;
        foreach(Carte c in grille.contenuGrille){
            c.mode=modeCombat;
            c.gameObject.name = "carte" + j++;
        }
        
        modeCombat.grille=grille;
        modeCombat.niveauIA = 2;
        modeCombat.paireCarte=new Carte[0];
        modeCombat.memoireIA=new Carte[]{null,null,null,null};
         // On passe une frame pour laisse la fonction Start se jouer
        //Act
        modeCombat.tirerCarteAleatoire();

        Debug.Log((from c in grille.contenuGrille where c.anim.GetBool("shine")!=true select c).ToArray().Length);
        //Assert
        Assert.AreEqual((from c in grille.contenuGrille where c.anim.GetBool("shine")!=true select c).ToArray().Length,5);
        

    }


    [UnityTest]
    public IEnumerator DeuxTiragesAleatoires()
    {
        //Arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Grille"));
        CarteCombat carte1 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte2 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        grille.contenuGrille = new Carte[] { carte1, carte2 };


        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();
        yield return null;
        int j = 1;
        foreach (Carte c in grille.contenuGrille)
        {
            c.mode = modeCombat;
            c.gameObject.name="carte"+j++;
        }

        modeCombat.grille = grille;
        modeCombat.niveauIA = 2;
        modeCombat.paireCarte = new Carte[0];
        modeCombat.memoireIA = new Carte[] { null, null, null, null };
        // On passe une frame pour laisse la fonction Start se jouer
        for(int i = 0; i < 10; i++) { 
        //Act
        Carte c1=modeCombat.tirerCarteAleatoire();
        Carte c2=modeCombat.tirerCarteAleatoire();

        Debug.Log((from c in grille.contenuGrille where c.anim.GetBool("shine") != true select c).ToArray().Length);
        //Assert
        Assert.AreEqual(0,(from c in grille.contenuGrille where c.anim.GetBool("shine") != true select c).ToArray().Length);
        Assert.AreNotEqual(c1, c2);
        c1.resetCarte();
        c2.resetCarte();
        }

    }
}
