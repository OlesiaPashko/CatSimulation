using UnityEngine;
using UnityEngine.UI;

public class CharacterFeature : MonoBehaviour
{
    [SerializeField]
    private FeatureOfCharacter featureName;
    
    [SerializeField]
    private Slider slider;


    private void Start()
    {
        slider.value = CharacterSettings.Features[featureName];
    }

    public void UpdateFeature(float newValue)
    {
        CharacterSettings.Features[featureName] = newValue;
    }
}
