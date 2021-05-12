
using System.Collections.Generic;
using System.Linq.Expressions;

public enum FeatureOfCharacter
{
    Belly_god,
    ToiletEndurance, 
    Loquacity
}

public static class CharacterSettings
{
    public static Dictionary<FeatureOfCharacter, float> Features = new Dictionary<FeatureOfCharacter, float>()
    {
        {FeatureOfCharacter.Belly_god, 0.1f},
        {FeatureOfCharacter.ToiletEndurance, 0.1f},
        {FeatureOfCharacter.Loquacity, 0.8f}
    };

    public static FeatureOfCharacter GetFeatureForNeed(InteractableType need)
    {
        if (need == InteractableType.Communication)
        {
            return FeatureOfCharacter.Loquacity;
        }

        if (need == InteractableType.Food)
        {
            return FeatureOfCharacter.Belly_god;
        }

        return FeatureOfCharacter.ToiletEndurance;
    }
}
