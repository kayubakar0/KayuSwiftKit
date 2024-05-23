using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace Plugins.KayuSwiftKit.Core
{
    public class KySimpleCallApi : MonoBehaviour
    {
        // Gantilah URL dengan URL API Anda
        private string apiUrl = $"http://127.0.0.1:8000/api/v1/customer/";
        
        [SerializeField] private int pageNumber = 1; // Nomor halaman yang ingin diambil

        void Start()
        {
            StartCoroutine(LoadJsonData());
        }

        IEnumerator LoadJsonData()
        {
            string apiUrlWithPage = apiUrl + "?page=" + pageNumber;
            
            UnityWebRequest www = null;
            
            www = UnityWebRequest.Get(apiUrlWithPage);

            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Gagal memuat data: " + www.error);
            }
            else
            {
                string jsonData = www.downloadHandler.text;
                Debug.Log(jsonData);
                ProcessJsonData(jsonData);
            }
        }

        void ProcessJsonData(string jsonData)
        {
            try
            {
                // Validasi JSON sebelum mengonversinya
                if (string.IsNullOrEmpty(jsonData))
                {
                    Debug.LogError("JSON data kosong atau tidak valid.");
                    return;
                }

                // Gunakan JsonUtility untuk mengonversi JSON ke objek MyDataList
                MyDataList myDataList = JsonUtility.FromJson<MyDataList>(jsonData);

                // Sekarang, Anda dapat mengakses array data
                foreach (MyData myData in myDataList.data)
                {
                    // Tampilkan atau lakukan sesuatu dengan setiap item data
                    Debug.Log("ID: " + myData.id + ", Nama: " + myData.name + ", Type: " + myData.type + ", Nation: " + myData.nation);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Gagal memproses data JSON: " + e.Message);
            }
        }
    }
}

[System.Serializable]
public class MyDataList
{
    public MyData[] data;
}

[System.Serializable]
public class MyData
{
    public int id;
    public string name;
    public string type;
    public string nation;
}