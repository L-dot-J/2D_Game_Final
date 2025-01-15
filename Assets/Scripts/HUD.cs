using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Lana;
using System.Collections;
using System.Collections.Generic;

public class HUD : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TextMeshProUGUI potionCounter;
    // public Slider slider;
    void Start()
    {
         if (playerController != null)
    {
        playerController.OnPotionCollected += CollectPotion;
    }
    else
    {
        Debug.LogError("PlayerController is not assigned in HUD script!");
    }
    }

    public void CollectPotion(int curPotion)
    {
        potionCounter.text = "Potions: "+ curPotion.ToString();
    }
    // public void SetMaxHealth(int health)
    // {
    //     slider.maxValue = health;
    //     slider.value = health;
    // }
    // public void SetHealth(int health)
    // {
    //     slider.value = health;
    // }
}
