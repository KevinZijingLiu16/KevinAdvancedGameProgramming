using UnityEngine;

public class VolumeSettingController : SettingController
{
    [SerializeField] private float defaultVolume = 0.5f;
    protected override void Awake()
    {
        settingKey = "Game_Volume";
        defaultValue = defaultVolume;

        base.Awake();
    }

    protected override void ApplySetting(float value)
    {
        AudioListener.volume = value;
    }
}
