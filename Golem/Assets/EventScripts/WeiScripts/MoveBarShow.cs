using UnityEngine;
using UnityEngine.UI;

public class MoveBarShow : MonoBehaviour
{
    public Slider energySlider;
    private float maxEnergy = 100f;

    void Start()
    {
        if (energySlider != null)
        {
            // 最大能量はPlayerPrefsから取得
            maxEnergy = PlayerPrefs.GetFloat("MoveBar_MaxEnergy", 100f);
            energySlider.maxValue = maxEnergy;
            // 显示当前能量
            float energy = PlayerPrefs.GetFloat("MoveBar_Energy", maxEnergy);
            energySlider.value = energy;
        }
    }

    void Update()
    {
        if (energySlider != null)
        {
            // 最大能量はPlayerPrefsから取得
            maxEnergy = PlayerPrefs.GetFloat("MoveBar_MaxEnergy", 100f);
            energySlider.maxValue = maxEnergy;
            float energy = PlayerPrefs.GetFloat("MoveBar_Energy", maxEnergy);
            energySlider.value = energy;
        }
    }
}
