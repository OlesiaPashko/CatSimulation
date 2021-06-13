
using System.Collections.Generic;
using System.Linq.Expressions;

public enum FeatureOfCharacter
{
    Belly_god,
    ToiletEndurance, 
    Loquacity,
    Playfulness,
    Somnolence,
    Cleanliness
}

public static class CharacterSettings
{
    public static Dictionary<FeatureOfCharacter, float> Features = new Dictionary<FeatureOfCharacter, float>()
    {
        {FeatureOfCharacter.Belly_god, 0.3f},
        {FeatureOfCharacter.ToiletEndurance, 0.4f},
        {FeatureOfCharacter.Loquacity, 0.3f},
        {FeatureOfCharacter.Playfulness, 0.3f},
        {FeatureOfCharacter.Somnolence, 0.3f},
        {FeatureOfCharacter.Cleanliness, 0.3f},
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
        
        if (need == InteractableType.Sleep)
        {
            return FeatureOfCharacter.Somnolence;
        }
        
        if (need == InteractableType.Сleanness)
        {
            return FeatureOfCharacter.Cleanliness;
        }
        
        if (need == InteractableType.Fun)
        {
            return FeatureOfCharacter.Playfulness;
        }

        return FeatureOfCharacter.ToiletEndurance;
    }
}
