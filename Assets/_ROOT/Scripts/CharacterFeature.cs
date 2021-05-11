using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterFeature : MonoBehaviour
{
    [SerializeField]
    private FeatureOfCharacter featureName;


    public void UpdateFeature(float newValue)
    {
        CharacterSettings.Features[featureName] = newValue;
    }
}
