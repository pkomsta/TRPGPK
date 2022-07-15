using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeroDescribtionPanel : MonoBehaviour
{
    public TextMeshProUGUI heroName;
    public TextMeshProUGUI resource;
    public TextMeshProUGUI attack;
    public TextMeshProUGUI magickAttack;
    public TextMeshProUGUI vitality;
    public TextMeshProUGUI wisdom;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI range;
    public TextMeshProUGUI describtion;


    public void FillTextFilds(Hero hero)
    {
        heroName.text = "Name: " + hero.unitName;
        resource.text = "Resource: " + hero.resource;
        attack.text = "Attack: " + hero.attack + " Growth: " + hero.attackGrowth;
        magickAttack.text = "Magick Attack: " + hero.magickAttack + " Growth: " + hero.magickAttackGrowth;
        vitality.text = "Vitality: " + hero.vitality + " Growth: " + hero.vitalityGrowth;
        wisdom.text = "Wisdom: " + hero.wisdom + " Growth: " + hero.wisdomGrowth;
        speed.text = "Speed: " + hero.speed;
        range.text = "Range: " + hero.range;
        describtion.text = hero.unitDescribtion;
    }

}
