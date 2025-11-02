using UnityEngine;
using UnityEngine.Events; // 追加

public class EnergyCharge : MonoBehaviour
{
    [SerializeField] private float maxEnergy = 100f; // 最大能量值（可在Inspector设置）

    [SerializeField] public UnityEvent onCharge; // InspectorでActionを設定

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 事件Action时调用此方法（最大能量和当前能量都设为maxEnergy）
    public void RestoreEnergyToMax()
    {
        PlayerPrefs.SetFloat("MoveBar_Energy", maxEnergy);
        PlayerPrefs.SetFloat("MoveBar_MaxEnergy", maxEnergy);
        PlayerPrefs.Save();
    }

    // 静的メソッド：只回复当前能量至最大能量（不改变最大能量）
    public static void RestoreCurrentEnergyToMax()
    {
        float maxEnergy = PlayerPrefs.GetFloat("MoveBar_MaxEnergy", 100f);
        PlayerPrefs.SetFloat("MoveBar_Energy", maxEnergy);
        PlayerPrefs.Save();
    }

    // 触发事件（可在Inspector或其他脚本调用）
    public void TriggerCharge()
    {
        if (onCharge != null)
            onCharge.Invoke(); // InspectorでRestoreEnergyToMaxを登録しておく
    }
    // RestoreEnergyToMaxでmaxEnergyをPlayerPrefsに保存済み
}
