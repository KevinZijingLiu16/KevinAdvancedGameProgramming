 using UnityEngine;
using UnityEngine.UI;

public class BrightnessSettingController : SettingController
{
    [Header("Brightness Settings")]
    [SerializeField] private Image brightnessOverlay;
    [SerializeField] private float defaultBrightness = 0.7f;


    protected override void Awake()
    {

        settingKey = "Game_Brightness";
        defaultValue = defaultBrightness;
        base.Awake();
    }

    protected override void ApplySetting(float value)
    {
        if (brightnessOverlay != null)
        {
            
            float alpha = 1 - value;
            Color c = brightnessOverlay.color;
            c.a = alpha;
            brightnessOverlay.color = c;
        }
    }
}
