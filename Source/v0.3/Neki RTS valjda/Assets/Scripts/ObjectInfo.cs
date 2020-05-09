using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //nav mash
using UnityEngine.UI;
public class ObjectInfo : MonoBehaviour
{
    //ova klasa bi se mogla uzeti kao ona Unit sto sam koristio za domace
    public GameObject iconCam;  

    public CanvasGroup InfoPanel;
    public CanvasGroup ActionPanel;
    

    public bool isSelected = false;

    public string objectName;

    private Text tName;

    public Slider sHealth;
    public Text tHealth;
    public int currentHealth;
    public int maxHealth;

    public Slider sMana;
    public Text tMana;
    public int currentMana;
    public int maxMana;

    public int minDamage;
    public int maxDamage;
    public Text tDamage;
    public Text tArmor;
    public int Armor;

    // Start is called before the first frame update
    void Start()
    {
        tDamage.text = "Damage: "+minDamage +"-"+ maxDamage;
        tArmor.text = "Defense: "+Armor;
    }

    // Update is called once per frame
    void Update()
    {
        if(maxMana <= 0)
        {
            sMana.gameObject.SetActive(false);
        }
        if (currentHealth <= 0)
            Destroy(gameObject);

        if(isSelected)
        {
            InfoPanel.alpha = 1;
            InfoPanel.blocksRaycasts = true;
            InfoPanel.interactable = true;
            ActionPanel.alpha = 1;
            ActionPanel.blocksRaycasts = true;
            ActionPanel.interactable = true;

            sHealth.maxValue = maxHealth;
            sHealth.value = currentHealth;
            iconCam.SetActive(isSelected);
            tHealth.text = currentHealth + "/" + maxHealth;

            sMana.maxValue = maxMana;
            sMana.value = currentMana;
            iconCam.SetActive(isSelected);
            tMana.text = currentMana + "/" + maxMana;

            tName.text = objectName;

        }
        else
        {
            InfoPanel.alpha = 0;
            InfoPanel.blocksRaycasts = false;
            InfoPanel.interactable = false;
            ActionPanel.alpha = 0;
            ActionPanel.blocksRaycasts = false;
            ActionPanel.interactable = false;
        }
    }
    


    

    
    
}
