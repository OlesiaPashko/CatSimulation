
using System.Collections.Generic;

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
        {FeatureOfCharacter.Belly_god, 0.5f},
        {FeatureOfCharacter.ToiletEndurance, 0.5f},
        {FeatureOfCharacter.Loquacity, 0.5f}
    };
}
