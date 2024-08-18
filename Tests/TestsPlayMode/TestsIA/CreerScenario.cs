using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CreerScenario
{
    
    
   
    // A Test behaves as an ordinary method
    [Test]
    public void CreerScenarioSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator CreerScenarioWithEnumeratorPasses()
    {
        //A
        GameObject mode = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Test/TestModeCombat"));
        ModeCombat modeCombat = mode.GetComponent<ModeCombat>();

        //Act
        modeCombat.niveauIA = 3;
        modeCombat.memoireIA = new Carte[2 * modeCombat.niveauIA];


        //Assert
        //Assert.IsNotNull(mode);
        Debug.Log(modeCombat.memoireIA.Length);
        Assert.AreEqual(modeCombat.memoireIA.Length, 6);

        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
