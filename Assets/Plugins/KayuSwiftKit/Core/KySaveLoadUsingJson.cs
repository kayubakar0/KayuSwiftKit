using System;
using UnityEngine;

namespace KayuSwiftKit.Core
{
    public class KySaveLoadUsingJson : MonoBehaviour
    {
        private string _path;
        public SaveData saveData;

        private void Start()
        {
            _path = Application.persistentDataPath + "/savedata.json";
        }
        
        public void SaveDataToJson()
        {
            SaveData data = saveData;
            string json = JsonUtility.ToJson(data);

            System.IO.File.WriteAllText(_path, json);

            Debug.Log("Data Succes saved in " + _path);
        }

        public void LoadDataFromJson()
        {
            if(System.IO.File.Exists(_path))
            {
                string json = System.IO.File.ReadAllText(_path);
                saveData = JsonUtility.FromJson<SaveData>(json);
                Debug.Log("Succes Loaded Data From " + _path);
            }
            else
            {
                Debug.LogWarning("Save Data File Not Found");
            }
        }
    }
}


[System.Serializable]
public class SaveData
{
    // replace as you needed 
    public string data1;
    public string data2;
}