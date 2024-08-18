using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DetecterPaire
{
    // A Test behaves as an ordinary method
    [Test]
    public void DetecterPaireSimplePasses()
    {
        // Use the Assert class to test conditions
        
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator DetecterPaireParmis4identiques()
    {
        //A
        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();

        modeCombat.niveauIA = 2;
        CarteCombat carte1 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte2 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte3 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        modeCombat.memoireIA = new Carte[2 * modeCombat.niveauIA];
        for (int i = 0; i < modeCombat.niveauIA*2; i++)
        {
            modeCombat.memoireIA[i] = carte1;
        }
        

        Carte[] attentes = new Carte[] { carte1, carte1 };

        
        //Ajouter la v�rification du retrait de la paire de la m�moire

        //Act

        modeCombat.fouillerMemoireIA(null);

        //Assert
        Assert.AreEqual(modeCombat.paireCarte, attentes);

        yield return null;
    }

    [UnityTest]
    public IEnumerator DetecterPaireParmis2identiques2null()
    {
        //A
        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();

        modeCombat.niveauIA = 2;
        var sprite = Resources.Load<Sprite>("Sprites/fond");
        CarteCombat carte1 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        modeCombat.memoireIA = new Carte[2 * modeCombat.niveauIA];
        for (int i = 0; i < modeCombat.niveauIA*2; i++)
        {
            modeCombat.memoireIA[i] = carte1;
        }
        modeCombat.memoireIA[0]=null;
        modeCombat.memoireIA[3]=null;

        Carte[] attentesPaire = new Carte[]{carte1,carte1};
        Carte[] attentesMemoire=new Carte[]{ null,null,null,null};
        //Ajouter la v�rification du retrait de la paire de la m�moire

        //Act

        modeCombat.fouillerMemoireIA(null);

        //Assert
        Assert.AreEqual(attentesPaire,modeCombat.paireCarte );
        Assert.AreEqual(attentesMemoire,modeCombat.memoireIA );

        yield return null;
    }

    [UnityTest]
    public IEnumerator DetecterPaireParmis1CartesHorsMemoire()
    {
        //A
        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();

        modeCombat.niveauIA = 2;
        modeCombat.remplacerMemoire();
        CarteCombat carte1 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte2 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte3 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte4 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        modeCombat.memoireIA = new Carte[2 * modeCombat.niveauIA];
        
        modeCombat.memoireIA[0] = carte1;
        modeCombat.memoireIA[1] = carte2;
        modeCombat.memoireIA[2] = carte3;
        modeCombat.memoireIA[3] = carte4;



        Carte[] attentes = new Carte[] { null, carte1 };
        Carte[] attentesMemoire = new Carte[] { null, carte2,carte3,carte4 };

        //Ajouter la v�rification du retrait de la paire de la m�moire

        //Act
        Carte c = carte1;
        modeCombat.fouillerMemoireIA(c);

        //Assert
        Assert.AreEqual(attentesMemoire,modeCombat.memoireIA );
        Assert.IsTrue( 1 == modeCombat.numeroCartePaire);
        Assert.AreEqual(attentes,modeCombat.paireCarte );

        yield return null;
    }
    [UnityTest]
    public IEnumerator DetecterAbsencePaireParmis1CartesHorsMemoire()
    {
        //Arrange
        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();

        modeCombat.niveauIA = 2;
        modeCombat.remplacerMemoire();
        CarteCombat carte1 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte2 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte3 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte4 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
         yield return null;
        int i = 1;
        Carte[] grille = new Carte[] { carte1, carte2, carte3, carte4 };
        foreach (Carte c in grille)
        {
            c.mode = modeCombat;
            c.gameObject.name = "carte" + i++;
        }
        modeCombat.memoireIA = new Carte[2 * modeCombat.niveauIA];
        modeCombat.memoireIA[0] = carte3;
        modeCombat.memoireIA[1] = carte2;
        modeCombat.memoireIA[2] = null;
        modeCombat.memoireIA[3] = carte4;



        Carte[] attentes = new Carte[] { null, null };
        Carte[] attentesMemoire = new Carte[] { carte3, carte2, null, carte4 };

        //Ajouter la v�rification du retrait de la paire de la m�moire
       
        //Act
        Carte c1 = carte1;
        modeCombat.fouillerMemoireIA(c1);

        //Assert
        Assert.AreEqual(attentesMemoire, modeCombat.memoireIA);
        Assert.AreEqual(2, modeCombat.numeroCartePaire);

        
    }

}
