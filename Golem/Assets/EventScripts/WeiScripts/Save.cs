using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public class Save : MonoBehaviour
{


    [Header("複数Bag/Item")]
    public List<Bag> myBags = new List<Bag>();
    public List<Item> myItems = new List<Item>();


    [Header("複数Flagスクリプト（Obj/Map/Fogをまとめて登録）")]
    public List<MonoBehaviour> allFlagScripts = new List<MonoBehaviour>();
 


    private const string SaveFolderName = "SaveData";
    private const string SingleSaveFileName = "Save.json"; // 単一セーブファイル

    [Serializable]
    private class ScriptableEntry
    {
        public string key;
        public string json;
    }

    [Serializable]
    private class FlagScriptEntry
    {
        public string type;
        public List<bool> values = new List<bool>();
    }

    [Serializable]
    private class MoveBarEntry
    {
        public float currentEnergy;
        public float maxEnergy;
    }

    [Serializable]
    private class FullSaveData
    {
        public List<ScriptableEntry> bags = new List<ScriptableEntry>();
        public List<ScriptableEntry> items = new List<ScriptableEntry>();
        public List<FlagScriptEntry> flagScripts = new List<FlagScriptEntry>();
        public MoveBarEntry moveBar; // 追加
    }

    private string SaveDirectoryPath => Path.Combine(Application.persistentDataPath, SaveFolderName);

    private string GetFilePath(string fileName)
    {
        return Path.Combine(SaveDirectoryPath, fileName);
    }

    private void EnsureSaveDirectory()
    {
        if (!Directory.Exists(SaveDirectoryPath))
        {
            Directory.CreateDirectory(SaveDirectoryPath);
        }
    }

    private void WriteJson(string fileName, string json)
    {
        EnsureSaveDirectory();
        File.WriteAllText(GetFilePath(fileName), json);
    }

    private bool TryReadJson(string fileName, out string json)
    {
        string path = GetFilePath(fileName);
        if (!File.Exists(path))
        {
            json = null;
            return false;
        }

        json = File.ReadAllText(path);
        return true;
    }

    private FullSaveData ReadFullSaveData()
    {
        if (!TryReadJson(SingleSaveFileName, out var json) || string.IsNullOrEmpty(json))
        {
            return new FullSaveData();
        }

        var data = JsonUtility.FromJson<FullSaveData>(json);
        return data ?? new FullSaveData();
    }

    private void WriteFullSaveData(FullSaveData data)
    {
        WriteJson(SingleSaveFileName, JsonUtility.ToJson(data));
    }

    private List<Bag> GetBagTargets()
    {
        var result = new List<Bag>();
        if (myBags != null)
        {
            foreach (var b in myBags)
            {
                if (b != null && !result.Contains(b)) result.Add(b);
            }
        }
        return result;
    }

    private List<Item> GetItemTargets()
    {
        var result = new List<Item>();
        if (myItems != null)
        {
            foreach (var i in myItems)
            {
                if (i != null && !result.Contains(i)) result.Add(i);
            }
        }
        return result;
    }

    private static ScriptableEntry FindByKey(List<ScriptableEntry> list, string key)
    {
        if (list == null || string.IsNullOrEmpty(key)) return null;
        return list.Find(x => x != null && x.key == key);
    }

    // 静的フラグ配列を反射で取得
    private static bool[] GetStaticFlags(System.Type type)
    {
        FieldInfo field = type.GetField("Flags", BindingFlags.Static | BindingFlags.NonPublic);
        if (field == null) return null;
        return (bool[])field.GetValue(null);
    }

    // 静的フラグ配列を反射で設定
    private static void SetStaticFlags(System.Type type, bool[] values)
    {
        FieldInfo field = type.GetField("Flags", BindingFlags.Static | BindingFlags.NonPublic);
        if (field == null) return;
        if (values == null) values = new bool[8];
        field.SetValue(null, values);
    }

    // Inspector の gameFlags 表示を更新
    private static void RefreshGameFlagsDisplay(MonoBehaviour target)
    {
        if (target == null) return;

        FieldInfo gameFlagsField = target.GetType().GetField("gameFlags", BindingFlags.NonPublic | BindingFlags.Instance);
        FieldInfo staticFlagsField = target.GetType().GetField("Flags", BindingFlags.Static | BindingFlags.NonPublic);

        if (gameFlagsField == null || staticFlagsField == null) return;

        bool[] staticFlags = (bool[])staticFlagsField.GetValue(null);
        if (staticFlags == null) return;

        bool[] gameFlags = new bool[staticFlags.Length];
        for (int i = 0; i < staticFlags.Length; i++)
        {
            gameFlags[i] = staticFlags[i];
        }

        gameFlagsField.SetValue(target, gameFlags);
    }

    public void SaveBag()
    {
        // 複数Bagを同時保存
        var data = ReadFullSaveData();
        data.bags = new List<ScriptableEntry>();

        foreach (var bag in GetBagTargets())
        {
            data.bags.Add(new ScriptableEntry
            {
                key = bag.GetSaveKey(),
                json = JsonUtility.ToJson(bag)
            });
        }

        WriteFullSaveData(data);
    }

    public void LoadBag()
    {
        // 複数Bagを同時読込
        var data = ReadFullSaveData();
        foreach (var bag in GetBagTargets())
        {
            var entry = FindByKey(data.bags, bag.GetSaveKey());
            if (entry != null && !string.IsNullOrEmpty(entry.json))
            {
                JsonUtility.FromJsonOverwrite(entry.json, bag);
            }
        }
    }

    public void SaveItem()
    {
        // 複数Itemを同時保存
        var data = ReadFullSaveData();
        data.items = new List<ScriptableEntry>();

        foreach (var item in GetItemTargets())
        {
            data.items.Add(new ScriptableEntry
            {
                key = item.GetSaveKey(),
                json = JsonUtility.ToJson(item)
            });
        }

        WriteFullSaveData(data);
    }

    public void LoadItem()
    {
        // 複数Itemを同時読込
        var data = ReadFullSaveData();
        foreach (var item in GetItemTargets())
        {
            var entry = FindByKey(data.items, item.GetSaveKey());
            if (entry != null && !string.IsNullOrEmpty(entry.json))
            {
                JsonUtility.FromJsonOverwrite(entry.json, item);
            }
        }
    }

    public void SaveFlags()
    {
        var data = ReadFullSaveData();
        data.flagScripts = new List<FlagScriptEntry>();

        // ObjFlag の static Flags を保存
        var objFlags = GetStaticFlags(typeof(ObjFlag));
        if (objFlags != null)
        {
            data.flagScripts.Add(new FlagScriptEntry
            {
                type = nameof(ObjFlag),
                values = new List<bool>(objFlags)
            });
        }

        // MapFlag の static Flags を保存
        var mapFlags = GetStaticFlags(typeof(MapFlag));
        if (mapFlags != null)
        {
            data.flagScripts.Add(new FlagScriptEntry
            {
                type = nameof(MapFlag),
                values = new List<bool>(mapFlags)
            });
        }

        // FogFlag の static Flags を保存
        var fogFlags = GetStaticFlags(typeof(FogFlag));
        if (fogFlags != null)
        {
            data.flagScripts.Add(new FlagScriptEntry
            {
                type = nameof(FogFlag),
                values = new List<bool>(fogFlags)
            });
        }

        WriteFullSaveData(data);
    }

    public void LoadFlags()
    {
        var data = ReadFullSaveData();

        foreach (var entry in data.flagScripts)
        {
            if (entry.type == nameof(ObjFlag))
            {
                SetStaticFlags(typeof(ObjFlag), entry.values?.ToArray());
            }
            else if (entry.type == nameof(MapFlag))
            {
                SetStaticFlags(typeof(MapFlag), entry.values?.ToArray());
            }
            else if (entry.type == nameof(FogFlag))
            {
                SetStaticFlags(typeof(FogFlag), entry.values?.ToArray());
            }
        }

        // gameFlags 表示を同期（オプション）
        if (allFlagScripts != null)
        {
            foreach (var obj in allFlagScripts)
            {
                RefreshGameFlagsDisplay(obj);
            }
        }
    }

    public void SaveMove()
    {
        var data = ReadFullSaveData();
        data.moveBar = new MoveBarEntry
        {
            currentEnergy = PlayerPrefs.GetFloat("MoveBar_Energy", 100f),
            maxEnergy = PlayerPrefs.GetFloat("MoveBar_MaxEnergy", 100f)
        };
        WriteFullSaveData(data);
    }

    public void LoadMove()
    {
        var data = ReadFullSaveData();
        if (data.moveBar != null)
        {
            PlayerPrefs.SetFloat("MoveBar_Energy", data.moveBar.currentEnergy);
            PlayerPrefs.SetFloat("MoveBar_MaxEnergy", data.moveBar.maxEnergy);
            PlayerPrefs.Save();
        }
    }

    public void SaveAll()
    {
        var data = new FullSaveData();

        foreach (var bag in GetBagTargets())
        {
            data.bags.Add(new ScriptableEntry { key = bag.GetSaveKey(), json = JsonUtility.ToJson(bag) });
        }

        foreach (var item in GetItemTargets())
        {
            data.items.Add(new ScriptableEntry { key = item.GetSaveKey(), json = JsonUtility.ToJson(item) });
        }

        // Flags 保存
        data.flagScripts = new List<FlagScriptEntry>();
        var objFlags = GetStaticFlags(typeof(ObjFlag));
        if (objFlags != null)
            data.flagScripts.Add(new FlagScriptEntry { type = nameof(ObjFlag), values = new List<bool>(objFlags) });

        var mapFlags = GetStaticFlags(typeof(MapFlag));
        if (mapFlags != null)
            data.flagScripts.Add(new FlagScriptEntry { type = nameof(MapFlag), values = new List<bool>(mapFlags) });

        var fogFlags = GetStaticFlags(typeof(FogFlag));
        if (fogFlags != null)
            data.flagScripts.Add(new FlagScriptEntry { type = nameof(FogFlag), values = new List<bool>(fogFlags) });

        // MoveBar PlayerPrefs 保存
        data.moveBar = new MoveBarEntry
        {
            currentEnergy = PlayerPrefs.GetFloat("MoveBar_Energy", 100f),
            maxEnergy = PlayerPrefs.GetFloat("MoveBar_MaxEnergy", 100f)
        };

        WriteFullSaveData(data);
    }

    public void LoadAll()
    {
        var data = ReadFullSaveData();

        foreach (var bag in GetBagTargets())
        {
            var entry = FindByKey(data.bags, bag.GetSaveKey());
            if (entry != null && !string.IsNullOrEmpty(entry.json))
                JsonUtility.FromJsonOverwrite(entry.json, bag);
        }

        foreach (var item in GetItemTargets())
        {
            var entry = FindByKey(data.items, item.GetSaveKey());
            if (entry != null && !string.IsNullOrEmpty(entry.json))
                JsonUtility.FromJsonOverwrite(entry.json, item);
        }

        // Flags 読込
        foreach (var entry in data.flagScripts)
        {
            if (entry.type == nameof(ObjFlag))
                SetStaticFlags(typeof(ObjFlag), entry.values?.ToArray());
            else if (entry.type == nameof(MapFlag))
                SetStaticFlags(typeof(MapFlag), entry.values?.ToArray());
            else if (entry.type == nameof(FogFlag))
                SetStaticFlags(typeof(FogFlag), entry.values?.ToArray());
        }

        // MoveBar PlayerPrefs 読込
        if (data.moveBar != null)
        {
            PlayerPrefs.SetFloat("MoveBar_Energy", data.moveBar.currentEnergy);
            PlayerPrefs.SetFloat("MoveBar_MaxEnergy", data.moveBar.maxEnergy);
            PlayerPrefs.Save();
        }

        // gameFlags 表示同期
        if (allFlagScripts != null)
            foreach (var obj in allFlagScripts)
                RefreshGameFlagsDisplay(obj);
    }

    // ============================================================
    // 【新しいFlagスクリプトを追加する場合の手順】
    // ============================================================
    //
    // 例：CustomFlag という新しいFlagスクリプトを追加する場合
    //
    // ステップ1：CustomFlag.cs を作成（ObjFlag / MapFlag / FogFlag と同じ構造）
    //   - static bool[] Flags = new bool[8]; を持つ
    //   - SetFlag / UnsetFlag / ToggleFlag / RelayAction などのメソッドを持つ
    //   - 他に特別な実装は不要
    //
    // ステップ2：Save.cs の SaveFlags() メソッドに case を追加
    //
    //     // CustomFlag の static Flags を保存
    //     var customFlags = GetStaticFlags(typeof(CustomFlag));
    //     if (customFlags != null)
    //     {
    //         data.flagScripts.Add(new FlagScriptEntry
    //         {
    //             type = nameof(CustomFlag),
    //             values = new List<bool>(customFlags)
    //         });
    //     }
    //
    // ステップ3：Save.cs の LoadFlags() メソッドに case を追加
    //
    //     else if (entry.type == nameof(CustomFlag))
    //     {
    //         SetStaticFlags(typeof(CustomFlag), entry.values?.ToArray());
    //     }
    //
    // ステップ4：Save.cs の SaveAll() メソッドに case を追加
    //
    //     var customFlags = GetStaticFlags(typeof(CustomFlag));
    //     if (customFlags != null)
    //         data.flagScripts.Add(new FlagScriptEntry { type = nameof(CustomFlag), values = new List<bool>(customFlags) });
    //
    // ステップ5：Save.cs の LoadAll() メソッドに case を追加
    //
    //     else if (entry.type == nameof(CustomFlag))
    //         SetStaticFlags(typeof(CustomFlag), entry.values?.ToArray());
    //
    // ステップ6：Inspector で CustomFlag を持つ GameObject を allFlagScripts に登録（表示更新用・任意）
    //
    // 以上で自動的に保存/読込の対象になります！
    // ============================================================
}
