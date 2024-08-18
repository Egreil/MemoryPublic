using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarteClassique : Carte
{
    
    // Start is called before the first frame update
    void Start()
    {   
        tourne=false;
        frame=400;

    }

    // Update is called once per frame
    void Update()
    {
        calculAnimationRotation();
    } 




    public override void resetCarte(){
        
        // if (theme.Equals("etoile")){
        //     desactiver();
        // }
        //else{
            tournerVersDos();
        //}
    }

}
