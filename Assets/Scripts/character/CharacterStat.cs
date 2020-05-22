using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel; // Add this using statement

public class CharacterStat
{
    public readonly ReadOnlyCollection<StatModifier> StatModifiers; // Add this variable
        // Add this variable
    private float lastBaseValue = float.MinValue;
    private float BaseValue;
    // Add these variables
    private bool isDirty = true;
    private float _value;

    private readonly List<StatModifier> statModifiers;

    public CharacterStat(float baseValue)
    {
        BaseValue = baseValue;
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly(); // Add this line to the constructor
    }

    // Change Value
    public float Value {
        get {
            if(isDirty || lastBaseValue != BaseValue) {
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0; // This will hold the sum of our "PercentAdd" modifiers
     
        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];
     
            if (mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PercentAdd) // When we encounter a "PercentAdd" modifier
            {
                sumPercentAdd += mod.Value; // Start adding together all modifiers of this type
                // If we're at the end of the list OR the next modifer isn't of this type
                if (i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd; // Multiply the sum with the "finalValue", like we do for "PercentMult" modifiers
                    sumPercentAdd = 0; // Reset the sum back to 0
                }
            }
            else if (mod.Type == StatModType.PercentMult) // Percent renamed to PercentMult
            {
                finalValue *= 1 + mod.Value;
            }
        }
     
        return Mathf.Round(finalValue);
    }


    public void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }
     
    // Add this method to the CharacterStat class
    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0; // if (a.Order == b.Order)
    }

    // And change the RemoveModifier method
    public bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }


    public bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;
     
        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }
}
