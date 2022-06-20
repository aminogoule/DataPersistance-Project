using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField inputName;
    static public MainMenuUI InstanceMenu;

    public Text playerName;
    public string BestScore;
    public string currentPlayer;
    private void Awake()
    {
        if (InstanceMenu != null)
        {
            Destroy(gameObject);
            return;
        }

        InstanceMenu = this;
        DontDestroyOnLoad(gameObject);
        inputName.text = "Default Name";
        LoadProgress();

    }
    private void Start()
    {
        playerName.text = "Player name: "+playerName.text +"\n"+"Has "+BestScore;
    }
    public void OnStartClicked()
    {
        InstanceMenu.currentPlayer = inputName.text;
        SceneManager.LoadScene("main");
        //Debug.Log("test");

    }
    public void OnExitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    [System.Serializable]
    class SaveData
    {
        public string bestScore;
        public string playerName;
    }
    //SaveData data = new SaveData();

    public void SaveProgress()
    {
        SaveData data = new SaveData();
        data.bestScore = BestScore;
        data.playerName = InstanceMenu.currentPlayer;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/bestscore.json", json);
        
    }
    public void LoadProgress()
    {
        string path = Application.persistentDataPath + "/bestscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            InstanceMenu.playerName.text = data.playerName;
            InstanceMenu.BestScore = data.bestScore;
        }
    }

}
