using UnityEngine;
using UnityEngine.UI;


public abstract class SettingController : MonoBehaviour
{
    [SerializeField] protected Slider settingSlider;

    protected string settingKey;
    protected float defaultValue = 0.5f;

   

    protected virtual void Awake()
    {
        LoadSetting();
    }

    protected virtual void OnEnable()
    {
        if (settingSlider != null)
            settingSlider.onValueChanged.AddListener(OnValueChanged);
    }

    protected virtual void OnDisable()
    {
        if (settingSlider != null)
            settingSlider.onValueChanged.RemoveListener(OnValueChanged);
    }

    protected virtual void LoadSetting()
    {
        float value = PlayerPrefs.GetFloat(settingKey, defaultValue);
        if (settingSlider != null)
            settingSlider.value = value;
        ApplySetting(value);
    }

    protected virtual void OnValueChanged(float value)
    {
        ApplySetting(value);
        PlayerPrefs.SetFloat(settingKey, value);
    }

    public virtual void SaveSetting()
    {
        PlayerPrefs.Save();
    }

    
    protected abstract void ApplySetting(float value);
}
