using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTournant : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Canvas>().worldCamera=FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
