using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Lana;
using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour
{
    public Slider slider;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI potionCounter;
    [SerializeField] private float fadeTime;
    [SerializeField] private TextMeshProUGUI fadeAwayText;

    void Start()
    {
        playerController.OnPotionCollected += CollectPotion;
    }

    void Update() {
        if(fadeTime > 0)
        {
            fadeTime -= Time.deltaTime;
            fadeAwayText.color = new Color(fadeAwayText.color.r, fadeAwayText.color.g, fadeAwayText.color.b, fadeTime);
        }
    }
   
    public void CollectPotion(int curPotion)
    {
        potionCounter.text = "Potions: "+ curPotion.ToString() + "/5";   
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        
    }
}
