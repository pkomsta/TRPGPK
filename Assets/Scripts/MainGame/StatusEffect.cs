using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class StatusEffect : MonoBehaviour
{
   
    public enum EffectType
    {
        Regeneration,
        StatsBuff,
        StatDebuff,
        Bleed,
        Poison,
        burn,
        slow

    }
    
    public EffectType effectType;
    public Sprite effectIcon;
    protected int effectValue;
    public int effectDuration = 1;
    int durationLeft;
    protected Abilities ability;
    GameObject iconObject;
    GameObject icon;
    protected Unit target;
    protected Unit caster;

    private void Start()
    {
        iconObject = new GameObject("icon");
        iconObject.AddComponent<Image>();
       var getImage = gameObject.transform.parent.gameObject.transform.Find("Canvas").transform.Find("EffectIcons");
       icon = Instantiate(iconObject, getImage.transform);
        icon.GetComponent<Image>().sprite = effectIcon;
        if(effectType == EffectType.StatDebuff || effectType == EffectType.StatsBuff)
        {
            EffectTick(target);
        }
    }

    public abstract void EffectTick(Unit unit);

    public abstract string EffectBattleLogMessage();

    void ReduceDuration()
    {
        if (target.isDead)
        {

            UpdateOnDestroy();
        }

        if (Gameboard.Instance.CurrentTeam == target.Side
            && durationLeft > 0)
        {
            EffectTick(target);
            if(effectType != EffectType.StatDebuff && effectType != EffectType.StatsBuff)
            {
                Gameboard.Instance.UpdateBattleLog(EffectBattleLogMessage());
            }
            
            durationLeft--;
        }else if(durationLeft <= 0)
        {
            UpdateOnDestroy();
        }
        if (target.isDead)
        {

            UpdateOnDestroy();
        }

    }

    public void SetEffectValue(int value)
    {
        effectValue = value;
    }
    

    public void SetAbiliti(Abilities abilities,Unit unit)
    {
        Gameboard.Instance.onEndTurn.AddListener(ReduceDuration);
        durationLeft = effectDuration;
        ability = abilities;
        if(unit == null)
        {
            target = ability.GetTarget();
        }else
        target = unit;
        caster = ability.GetCaster();
    }

    public virtual void UpdateOnDestroy()
    {
        Destroy(icon);
        Destroy(gameObject);
    }
}
