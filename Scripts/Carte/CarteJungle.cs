using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarteJungle : Carte
{

    // Start is called before the first frame update
    void Start()
    {
        frame = 400;
        stop = false;
        tourne = false;
    }

    // Update is called once per frame
    void Update()
    {
        calculAnimationRotation();
    }
    


    public override void resetCarte(){
        tournerVersDos();
    }

    public override bool Equals(object carte) //OK
    {
        // If the passed object is null, return False
        if (carte == null)
        {
            return false;
        }
        // If the passed object is not Customer Type, return False
        if (!(carte is CarteJungle))
        {
            return false;
        }
        return (this.gameObject.name.Equals(((CarteJungle)carte).gameObject.name));
    }
}
