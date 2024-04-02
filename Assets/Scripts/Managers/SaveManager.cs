using System.IO;
using UnityEngine;

public static class SaveManager
{

    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
            SaveFirstTime();
        }
    }

    public static void Save(string saveString)
    {
        File.WriteAllText(SAVE_FOLDER + "save.txt", saveString);
    }

    public static string Load()
    {
        if (Directory.Exists(SAVE_FOLDER))
        {
            string saveString = File.ReadAllText(SAVE_FOLDER + "save.txt");
            return saveString;
        }
        else
        {
            return null;
        }
    }

    private static void SaveFirstTime()
    {
        SaveProps initSave = new SaveProps();
        initSave.soundVolume = 0.5f;
        initSave.musicVolume = 0.25f;
        initSave.playerXP = 0;
        string firstSaveString = JsonUtility.ToJson(initSave);
        Save(firstSaveString);
    }
}

public class SaveProps
{
    public float soundVolume;
    public float musicVolume;
    public int playerXP;

}
