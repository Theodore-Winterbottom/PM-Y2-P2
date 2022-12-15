using System;
using System.Collections.Generic;

public class PlayerStats
{
    public float BaseValue;

    public float Value
    {
        get
        {
            if (isDirty)
            {
                _Value = CalculateFinalValue();
                isDirty = false;
            }
        }
    }
    
            
    
    private bool isDirty = true;
    private float _Value;

    private readonly List<StatModifier> statModifiers;

    public PlayerStats(float baseValue)
    {
        BaseValue = baseValue;
        statModifiers = new List<StatModifier>();

    }

    public void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(ComapareModifierOrder);
    }

    private int ComapareModifierOrder(StatModifier a, StatModifier b)
    {
        if(a.Order < b.Order)
            return -1;
        else if(a.Order > b.Order)
            return 1;
        return 0; // if (a.Order == b.Order)
    }

    public bool RemoveModifier(StatModifier mod)
    {
        isDirty = true;
        return statModifiers.Remove(mod);
    }

    public bool RemoveAllModifiersFromSource(object source)
    {
        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if(statModifiers[i].Source == source)
            {
                isDirty = true;
                statModifiers.RemoveAt(i);
            }
        }
    }

    private float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0;

        for ( int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if(mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PercentAdd)
            {
                sumPercentAdd += mod.Value;

                if(i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if(mod.Type == StatModType.PercentMult)
            {
                finalValue *= 1 + mod.Value;
            }
            
        }

        // 12.0001f != 12f
        return (float)Math.Round(finalValue, 4);
    }

}
