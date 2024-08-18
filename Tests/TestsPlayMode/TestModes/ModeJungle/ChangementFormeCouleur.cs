using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ChangementFormeCouleur
{
    // A Test behaves as an ordinary method
    [Test]
    public void ChangementFormeCouleurSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ChangementFormeCouleurWithEnumeratorPasses()
    {

        //Arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Grille"));
        
        ModeJungle modeJungle= MonoBehaviour.Instantiate(Resources.Load<ModeJungle>("Prefabs/Test/TestModeJungle"));
        CarteJungle carte1= MonoBehaviour.Instantiate(Resources.Load<CarteJungle>("Prefabs/Test/CarteJungle"));
        CarteJungle carte2 = MonoBehaviour.Instantiate(Resources.Load<CarteJungle>("Prefabs/Test/CarteJungle"));
        CarteJungle carte3 = MonoBehaviour.Instantiate(Resources.Load<CarteJungle>("Prefabs/Test/CarteJungle"));
        carte1.gameObject.name = "rond-rouge";
        carte2.gameObject.name = "carre-rouge";
        carte3.gameObject.name = "rond-vert";
        modeJungle.grille = grille;
        grille.contenuGrille = new Carte[] { carte1, carte2, carte3 };
        //Act
        //On compare les formes qui ne sont pas en commun
        modeJungle.forme = true;
        modeJungle.carteRetournee = new Carte[] { carte1, carte2 };
        modeJungle.comparer();
        Assert.IsTrue(modeJungle.forme);
        //On compare les formes qui sont en commun
        modeJungle.carteRetournee = new Carte[] { carte1, carte3 };
        modeJungle.comparer();
        Assert.IsFalse(modeJungle.forme);
        //On compare les couleurs avec deux couleurs différentes
        modeJungle.carteRetournee = new Carte[] { carte1, carte3 };
        modeJungle.comparer();
        Assert.IsFalse(modeJungle.forme);
        //On compare les couleurs avec 2 couleurs identiques
        modeJungle.carteRetournee = new Carte[] { carte1, carte2 };
        modeJungle.comparer();
        Assert.IsTrue(modeJungle.forme);

        yield return null;
    }
}
