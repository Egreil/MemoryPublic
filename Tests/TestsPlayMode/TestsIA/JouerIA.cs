using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class JouerIA
{
    // A Test behaves as an ordinary method
    [Test]
    public void JouerIASimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.

    /* Liste des cas
     * 1/ Debut de partie, la memoireIA est vide.L'IA joue une fois. L'IA tire 2 cartes al�atoires les retournes, 
     * les compares et les ajoute � sa m�moire s'il n'y a pas de paire.
     * paire impossible
     *
     * 2/ L'IA joue 1 fois, elle a d�j� sa memoire pleine et retourne les cartes qu'elle a identifi�e comme paire.
     * 
     * 3/ L'IA joue 1 fois, elle a déjà une paire prévue en mémoire.
     * 
     * 4/ L'IA par de 0 et joue seule jusqu'� terminer la partie.
     * */

    //1 L'IA pioche 2 cartes al�atoires et n'a pas d'infos en m�moire
    [UnityTest]
    public IEnumerator JouerIAMemoireNullEtNouvelleGrille()
    {   //Arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Grille"));
        CarteCombat carte1 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte2 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));

        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();
        yield return null;

        grille.contenuGrille = new Carte[] { carte1, carte2 };
        int i = 1;
        foreach (Carte c in grille.contenuGrille)
        {
            
            c.mode = modeCombat;
            c.gameObject.name = "carte" + i++;
        }

        modeCombat.grille = grille;
        modeCombat.niveauIA = 2;
        for (int j = 0; j < 10; j++)
        {
            modeCombat.paireCarte = new Carte[0];
            modeCombat.memoireIA = new Carte[] { null, null, null, null };
            // On passe une frame pour laisse la fonction Start se jouer
            //Act
            modeCombat.jouerIA();
            //comparer
            //resetdesCartes si faut

            Debug.Log((from c in grille.contenuGrille where c.anim.GetBool("shine") != true select c).ToArray().Length);
            //Assert
            Assert.AreEqual(0, (from c in grille.contenuGrille where c.anim.GetBool("shine") != true select c).ToArray().Length );

            foreach (Carte c in grille.contenuGrille)
            {
                c.resetCarte();
            }
        }
    }
    //2 L'IA Pioche une carte al�atoire et compare avec celles dans sa m�moire
    [UnityTest]
    public IEnumerator JouerIAMemoirePleineEtNouvelleGrille()
    {   //Arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Grille"));
        CarteCombat carte1 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte2 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte3 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte4 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte5 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte6 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();
        yield return null;

        grille.contenuGrille = new Carte[] { carte1, carte2, carte3, carte4, carte5, carte6};
        int i = 1;
        foreach (Carte c in grille.contenuGrille)
        {

            c.mode = modeCombat;
            c.gameObject.name = "carte" + i++;
        }

        modeCombat.grille = grille;
        modeCombat.niveauIA = 2;
       for (int j = 0; j < 10; j++)
       {
            modeCombat.paireCarte = new Carte[0];
            modeCombat.memoireIA = new Carte[] { carte1, carte2, carte3, carte4};
            carte5.gameObject.name = "carte3";
            carte6.gameObject.name = "carte4";
            // On passe une frame pour laisse la fonction Start se jouer
            //Act
            modeCombat.jouerIA();
            //comparer
            //resetdesCartes si faut

            Debug.Log((from c in grille.contenuGrille where c.anim.GetBool("shine") != true select c).ToArray().Length);
            //Assert
            Assert.AreEqual(4, (from c in grille.contenuGrille where c.anim.GetBool("shine") != true select c).ToArray().Length);
            Assert.IsTrue(modeCombat.paireCarte[1].gameObject.name.Equals("carte3")|| modeCombat.paireCarte[1].gameObject.name.Equals("carte4"));
            foreach (Carte c in grille.contenuGrille)
            {
                c.resetCarte();
            }
       }
    }

    //3 L'IA Pioche une carte les cartes de la paire pr�sente dans sa memoire
    [UnityTest]
    public IEnumerator JouerIAMemoirePossedantDouble()
    {   //Arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Grille"));
        CarteCombat carte1 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte2 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte3 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte4 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte5 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte6 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();
        yield return null;

        grille.contenuGrille = new Carte[] { carte1, carte2, carte3, carte4, carte5, carte6 };
        int i = 1;
        foreach (Carte c in grille.contenuGrille)
        {

            c.mode = modeCombat;
            c.gameObject.name = "carte" + i++;
        }

        modeCombat.grille = grille;
        modeCombat.niveauIA = 2;
        for (int j = 0; j < 10; j++)
        {
            modeCombat.paireCarte = new Carte[0];
            modeCombat.memoireIA = new Carte[] { carte1, carte2, carte3, carte4 };
            carte4.gameObject.name = "carte1";
            carte5.gameObject.name = "carte2";
            carte6.gameObject.name = "carte3";
            // On passe une frame pour laisse la fonction Start se jouer
            //Act
            modeCombat.jouerIA();
            //comparer
            //resetdesCartes si faut

            Debug.Log((from c in grille.contenuGrille where c.anim.GetBool("shine") != true select c).ToArray().Length);
            //Assert
            Assert.AreEqual(4, (from c in grille.contenuGrille where c.anim.GetBool("shine") != true select c).ToArray().Length);
            Assert.IsTrue(modeCombat.paireCarte[1].Equals(modeCombat.paireCarte[0]));
            foreach (Carte c in grille.contenuGrille)
            {
                c.resetCarte();
            }
        }
    }


    // 4 L'IA Joue une partie entiere seule.
    [UnityTest]
    public IEnumerator JouerIAPartieComplete()
    {   //Arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Grille"));
        CarteCombat carte1 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte2 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte3 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte4 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte5 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte6 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();
        yield return null;

        grille.contenuGrille = new Carte[] { carte1, carte2, carte3, carte4, carte5, carte6 };
        modeCombat.grille = grille;
        modeCombat.niveauIA = 2;
        
        int i = 1;
        foreach (Carte c in grille.contenuGrille)
        {

            c.mode = modeCombat;
            c.gameObject.name = "carte" + i++;
        }
        carte4.gameObject.name = "carte1";
        carte5.gameObject.name = "carte2";
        carte6.gameObject.name = "carte3";
        for (int j = 0; j < 10; j++)
        {
            modeCombat.paireCarte = new Carte[0];
            modeCombat.memoireIA = new Carte[] { null, null, null, null };

            int compteTour = 0;
            while (modeCombat.totalCarteRetournee < grille.contenuGrille.Length && compteTour < 100)
            {

                // On passe une frame pour laisse la fonction Start se jouer
                //Act
                Debug.Log("cartes restantes tour" + compteTour);
                foreach (Carte c in modeCombat.genererCartesRestantesInconnues())
                {
                    Debug.Log(c != null ? c.gameObject.name + " " + c.anim.GetBool("shine") : null);
                }
                modeCombat.jouerIA();
                if (!modeCombat.comparer())
                {
                    modeCombat.nbrCarteRetournee = 0;
                    modeCombat.carteRetournee[0].resetCarte();
                    modeCombat.carteRetournee[1].resetCarte();
                }
                Debug.Log("memoireIA tour" + compteTour);
                foreach (Carte c in modeCombat.memoireIA)
                {
                    Debug.Log(c != null ? c.gameObject.name + " " + c.anim.GetBool("shine") : null);
                }
                //resetdesCartes si faut
                compteTour++;
            }
            Debug.Log(compteTour);
            //Assert
            Assert.IsTrue(compteTour < 6);
            
            foreach (Carte c in grille.contenuGrille)
            {
                c.resetCarte();
            }
            modeCombat.totalCarteRetournee=0;

        }
        
    }
    //PartieCompleteA8Paire
    [UnityTest]
    public IEnumerator JouerIAPartiePartieCompleteA5Paire()
    {   //Arrange
        JeuDeBase grille = MonoBehaviour.Instantiate(Resources.Load<JeuDeBase>("Prefabs/Grille"));
        CarteCombat carte1 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte2 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte3 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte4 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte5 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte6 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte7 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte8 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte9 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        CarteCombat carte10 = MonoBehaviour.Instantiate(Resources.Load<CarteCombat>("Prefabs/Test/CarteCombat"));
        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();
        yield return null;

        grille.contenuGrille = new Carte[] { carte1, carte2, carte3, carte4, carte5, carte6,carte7,carte8,carte9,carte10 };
        modeCombat.grille = grille;
        modeCombat.niveauIA = 2;

        int i = 1;
        foreach (Carte c in grille.contenuGrille)
        {

            c.mode = modeCombat;
            c.gameObject.name = "carte" + i++;
        }
        carte6.gameObject.name = "carte1";
        carte7.gameObject.name = "carte2";
        carte8.gameObject.name = "carte3";
        carte9.gameObject.name = "carte4";
        carte10.gameObject.name = "carte5";
        modeCombat.paireCarte = new Carte[0];
        modeCombat.memoireIA = new Carte[] { null, null, null, null };

        int compteTour = 0;
        while (modeCombat.totalCarteRetournee < grille.contenuGrille.Length && compteTour < 100)
        {

            // On passe une frame pour laisse la fonction Start se jouer
            //Act
            modeCombat.jouerIA();
            if (!modeCombat.comparer())
            {
                modeCombat.nbrCarteRetournee = 0;
                modeCombat.carteRetournee[0].resetCarte();
                modeCombat.carteRetournee[1].resetCarte();
            }
            //resetdesCartes si faut
            compteTour++;
        }
        Debug.Log(compteTour);
        //Assert
        Assert.IsTrue(compteTour < 100);


    }
}
