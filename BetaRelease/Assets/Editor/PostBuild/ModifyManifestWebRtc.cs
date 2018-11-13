using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
//using System.Linq;
using System.Xml.Linq;

public class ModifyManifestWebRtc : IPostprocessBuild
{
    public int callbackOrder { get { return 0; } }
    public void OnPostprocessBuild(BuildTarget target, string path)
    {
        string fileLocation = path + "/ApolloLens/Package.appxmanifest";
        XDocument configDoc = XDocument.Load(fileLocation);
        Debug.Log("MyCustomBuildProcessor.OnPostprocessBuild for target " + target + " at path " + path);
        Debug.Log(configDoc.ToString());
    }
}




//static class ConfigData
//{

//    // Database connection string
//    public static string getConString()
//    {
//        XDocument configDoc = XDocument.Load(fileLocation);
//        return configDoc.Descendants("connectionStrings").Descendants()
//            .Select(x => x.Attribute("connectionString"))
//            .First().Value;
//    }


//    // Wireman auotcad blocks version control
//    public static bool blockCheck(string blockName, string blockVersion)
//    {
//        XDocument confingDoc = XDocument.Load(fileLocation);
//        var xmlVersion = confingDoc.Descendants("Block")
//            .Where(x => x.Attribute("name").Value == blockName).First()
//            .Attribute("version").Value;
//        return (xmlVersion == blockVersion);
//    }
//}

