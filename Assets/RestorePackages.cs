using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public static class RestorePackages 
{
    [MenuItem("NuGet/Create package.json for all packages")]
    public static void FindAll()
    {
        //get all folder in directory 
        string[] folders = System.IO.Directory.GetDirectories(Application.dataPath + "/Packages");
        foreach (var folder in folders)
        {
            string folderName = folder.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            folderName = folderName.Substring(folderName.LastIndexOf("/") + 1)/*.Replace(".","")*/.ToLower();
            
            var item = new PackageJson()
            {
                name = "com.jarvigames." + folderName,
                version = "1.0.0", //не уверен нужно ли итерировать чтобы пакет новой версии подтянулся
                displayName = folderName,
                description = folderName + " this package auto generated",
                unity = "2020.3",
                keywords = new List<string>(),
                dependencies = new Dictionary<string, string>(),
            };
            File.WriteAllText(folder + "/package.json", JsonConvert.SerializeObject(item, Formatting.Indented));
        }
        
        AssetDatabase.Refresh();
        Debug.Log("Packages json created!");
        
    }
}

public class PackageJson
{
    public string name { get; set; }
    public string version { get; set; }
    public string displayName { get; set; }
    public string description { get; set; }
    public string unity { get; set; }
    public List<string> keywords { get; set; }
    public Dictionary<string, string> dependencies { get; set; }
}