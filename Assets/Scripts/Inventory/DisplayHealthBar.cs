using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class DisplayHealthBar : MonoBehaviour
{
    //Hp
    public TextMeshProUGUI text;
    public Image fill;

    //Mp
    public TextMeshProUGUI Mptext;
    public Image Mpfill;

    private void UpdateBar()
    {
        Player player = InventoryManager.Instance.player;
        float currentHealth = player.GetAttributeValue(AttributeType.HP);
        fill.fillAmount = currentHealth / player.maxHP;
        text.text = ($"HP {currentHealth} / {player.maxHP}");
    }

    private void UpdateMpBar()
    {
        Player player = InventoryManager.Instance.player;
        float currentMp = player.GetAttributeValue(AttributeType.MP);
        Mpfill.fillAmount = currentMp / player.maxMP;
        Mptext.text = ($"MP {currentMp} / {player.maxMP}");
    }

    private void Update()
    {
        UpdateBar();
        UpdateMpBar();
    }
}
