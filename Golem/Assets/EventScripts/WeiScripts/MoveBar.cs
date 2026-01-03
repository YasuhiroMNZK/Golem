using UnityEngine;
using UnityEngine.UI; // 追加
using UnityEngine.Events; // 追加

public class MoveBar : MonoBehaviour
{
    // Inspectorで設定可能なEnergy
     private float energy = 100f;
     private float maxEnergy = 100f;

    // 毎秒移動時に減少するエネルギー値（他スクリプトから参照用）
    [SerializeField] private float energyConsumptionRate = 10f;

    [SerializeField] private Slider energySlider; // InspectorでSliderを割り当て
    [SerializeField] private UnityEvent onEnergyDepleted; // InspectorでActionを設定

    private Vector3 lastPosition;
    private bool energyDepletedTriggered = false;
    private static float savedEnergy = -1f; // Scene間で保持

    private void Awake()
    {
        // PlayerPrefsからenergy値をロード
        if (PlayerPrefs.HasKey("MoveBar_Energy"))
        {
            savedEnergy = PlayerPrefs.GetFloat("MoveBar_Energy");
        }
        if (savedEnergy >= 0f)
        {
            energy = savedEnergy;
        }
    }

    private void Start()
    {
        lastPosition = transform.position;

        // Slider初期化
        if (energySlider != null)
        {
            energySlider.maxValue = maxEnergy;
            energySlider.value = energy;
        }
    }

    private void OnDisable()
    {
        // Scene切り替え時にenergy値のみ保存
        PlayerPrefs.SetFloat("MoveBar_Energy", energy);
        PlayerPrefs.Save();
        savedEnergy = energy;
    }

    private void Update()
    {
        // 最大能量はPlayerPrefsから取得
        float newMaxEnergy = PlayerPrefs.GetFloat("MoveBar_MaxEnergy", 100f);
        if (maxEnergy != newMaxEnergy)
        {
            maxEnergy = newMaxEnergy;
            // 最大能量が変化したらenergyも最大値にリセット
            energy = maxEnergy;
        }

        // 現在位置と前回位置のXZ方向の距離を計算
        Vector3 currentPosition = transform.position;
        Vector2 lastPosXZ = new Vector2(lastPosition.x, lastPosition.z);
        Vector2 currPosXZ = new Vector2(currentPosition.x, currentPosition.z);
        float distanceMoved = Vector2.Distance(lastPosXZ, currPosXZ);

        // 移動量に応じてenergyを減少
        if (distanceMoved > 0f)
        {
            float energyToConsume = distanceMoved * energyConsumptionRate;
            ConsumeEnergy(energyToConsume);
        }

        lastPosition = currentPosition;

        // Sliderの値を更新
        if (energySlider != null)
        {
            energySlider.value = energy;
        }

        // energyが0以下になった時に一度だけActionを発火
        if (!energyDepletedTriggered && energy <= 0f)
        {
            if (onEnergyDepleted != null)
                onEnergyDepleted.Invoke();
            energyDepletedTriggered = true;
        }
        // energyが回復した場合、再度トリガー可能にする
        if (energy > 0f)
        {
            energyDepletedTriggered = false;
        }

        // maxEnergyが変化した場合、energyがmaxEnergyを超えないようにする
        if (energy > maxEnergy)
        {
            energy = maxEnergy;
        }
    }

    // エネルギーを消費する（他スクリプトから呼び出し）
    public void ConsumeEnergy(float amount)
    {
        energy -= amount;
        energy = Mathf.Max(energy, 0);
        savedEnergy = energy;
    }

    

    // 現在のエネルギー値を取得
    public float GetEnergy()
    {
        return energy;
    }

    // エネルギー値を設定（例：エネルギー回復）
    public void SetEnergy(float value)
    {
        energy = Mathf.Clamp(value, 0, maxEnergy);
        savedEnergy = energy;
    }

    // 指定量だけエネルギーを回復（保存されている最大エネルギーを上限にする）
    public void AddEnergy(float amount)
    {
        // PlayerPrefs に保存されている最大エネルギーを使用
        float newEnergy = energy + amount;
        energy = Mathf.Clamp(newEnergy, 0f, maxEnergy);
        savedEnergy = energy;
    }

    // エネルギー消費レートを取得
    public float GetEnergyConsumptionRate()
    {
        return energyConsumptionRate;
    }
}
