using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;// polje Text

public class ResourceManager : MonoBehaviour
{
    public float Gold;
    public float maxGold = 10000;

    public Text goldDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        goldDisplay.text = "" + Gold;
    }
}
