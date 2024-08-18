using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RemplirMemoire
{
 /*   [TestInitialize]
    public void TestInitialize()
    {

    }*/
    // A Test behaves as an ordinary method
    [Test]
    public void RemplirMemoireSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator RemplirMemoireWithEnumeratorPasses()
    {
        //Arrange
        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();

        modeCombat.niveauIA = 2;
        modeCombat.remplacerMemoire();
        Sprite sprite = Resources.Load<Sprite>("Sprites/fond");
        Debug.Log(sprite);
        CarteCombat carte1 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte2 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte3 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));

        modeCombat.memoireIA = new Carte[2 * modeCombat.niveauIA];
        for(int i=0; i < modeCombat.niveauIA*2; i++)
        {
            modeCombat.memoireIA[i] = carte1;
        }
        modeCombat.memoireTempTour = new Carte[]
        {
            carte2,carte3
        };
        Carte[] attenteTest = new Carte[] { carte1, carte1, carte2, carte3 };
        //Act Ajout sur MemoireVide


        modeCombat.remplacerMemoire();
/*
        for (int i = 0; i < modeCombat.niveauIA * 2; i++)
        {
            Assert.IsTrue(modeCombat.memoireIA[i]== attenteTest[i]);
            Debug.Log(modeCombat.memoireIA[i] +" "+ attenteTest[i]);
        }*/
        Assert.AreEqual(modeCombat.memoireIA, attenteTest);

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
    


    [UnityTest]
    public IEnumerator RemplirMemoireAvecUnIdentique()
    {
        //Arrange
        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();

        modeCombat.niveauIA = 2;
        modeCombat.remplacerMemoire();
        var sprite = Resources.Load<Sprite>("Sprites/fond");
        CarteCombat carte1 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte2 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte3 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        modeCombat.memoireIA = new Carte[2 * modeCombat.niveauIA];
        for (int i = 0; i < modeCombat.niveauIA * 2; i++)
        {
            modeCombat.memoireIA[i] = carte1;
        }
        modeCombat.memoireTempTour = new Carte[]
        {
            carte1,carte2
        };
        Carte[] attenteTest = new Carte[] { carte1, carte1, carte1, carte2 };
        //Act Ajout sur MemoireVide

        modeCombat.remplacerMemoire();


        //Assert
        Assert.AreEqual(modeCombat.memoireIA, attenteTest);


        // Use yield to skip a frame.
        yield return null;
    }




    [UnityTest]
    public IEnumerator RemplirMemoireAvecDeuxIdentiques()
    {
        //Arrange
        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();

        modeCombat.niveauIA = 2;
        modeCombat.remplacerMemoire();
        var sprite = Resources.Load<Sprite>("Sprites/fond");
        CarteCombat carte1 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte2 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte3 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        modeCombat.memoireIA = new Carte[2 * modeCombat.niveauIA];
        for (int i = 0; i < modeCombat.niveauIA; i++)
        {
            modeCombat.memoireIA[i] = carte1;
        }
        modeCombat.memoireIA[modeCombat.niveauIA] = carte2;
        modeCombat.memoireIA[modeCombat.niveauIA + 1] = carte3;
        modeCombat.memoireTempTour = new Carte[]
        {
            carte1,carte1
        };
        Carte[] attenteTest = new Carte[] { carte1, carte1, carte2, carte3 };
        //Act Ajout sur MemoireVide

        modeCombat.remplacerMemoire();


        //Assert
        Assert.AreEqual(modeCombat.memoireIA, attenteTest);


        // Use yield to skip a frame.
        yield return null;
    }





    [UnityTest]
    public IEnumerator RemplirMemoireDeuxExecutions()
    {
        //Arrange
        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();

        modeCombat.niveauIA = 2;
        modeCombat.remplacerMemoire();
        var sprite = Resources.Load<Sprite>("Sprites/fond");
        CarteCombat carte1 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte2 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte3 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte4 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte5 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        modeCombat.memoireIA = new Carte[2 * modeCombat.niveauIA];
        for (int i = 0; i < modeCombat.niveauIA*2; i++)
        {
            modeCombat.memoireIA[i] = carte1;
        }
        modeCombat.memoireTempTour = new Carte[]{carte2,carte3};
        
        //Act Ajout sur MemoireVide

        modeCombat.remplacerMemoire();
        modeCombat.memoireTempTour = new Carte[] { carte4, carte5 };
        modeCombat.remplacerMemoire();
        Carte[] attenteTest = new Carte[] { carte2, carte3, carte4, carte5 };
        //Assert
        Assert.AreEqual(modeCombat.memoireIA, attenteTest);


        // Use yield to skip a frame.
        yield return null;
    }

    [UnityTest]
    public IEnumerator RemplirMemoirePleineDeNull()
    {
        //Arrange
        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();

        modeCombat.niveauIA = 2;
        modeCombat.remplacerMemoire();
        var sprite = Resources.Load<Sprite>("Sprites/fond");
        CarteCombat carte1 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte2 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte3 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte4 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte5 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        modeCombat.memoireIA = new Carte[2 * modeCombat.niveauIA];
        for (int i = 0; i < modeCombat.niveauIA * 2; i++)
        {
            modeCombat.memoireIA[i] = null;
        }
        modeCombat.memoireTempTour = new Carte[] { carte2, carte3 };

        //Act Ajout sur MemoireVide
        modeCombat.remplacerMemoire();
        Carte[] attenteTest = new Carte[] { null,null,carte2, carte3 };
        //Assert
        Assert.AreEqual(modeCombat.memoireIA, attenteTest);


        // Use yield to skip a frame.
        yield return null;
    }
}
