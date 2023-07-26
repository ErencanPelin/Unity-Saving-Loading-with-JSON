using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using Newtonsoft.Json;

namespace SaveLoad
{
    [UsedImplicitly]
    public static class SaveManager
    {
        private static readonly string saveFolder = Application.persistentDataPath + "/gameData";

        /// <summary>
        /// Deletes a given save from the saves folder
        /// </summary>
        /// <param name="profileName">the name of the save file being deleted</param>
        public static void DeleteSave(string profileName)
        {
            //check if the file exists
            if (!File.Exists($"{saveFolder}/{profileName}"))
                throw new Exception($"Save file: {profileName} not found. Make sure you've saved the file before accessing it, and the profileName is correct.");
            //remove the file
            File.Delete($"{saveFolder}/{profileName}");
        }
        
        /// <summary>
        /// Loads and returns a SaveProfile from the Saves Folder. Uses the given profile name to retrieve the file.
        /// The file is automatically decrypted and turned into a SaveProfile object
        /// </summary>
        /// <param name="profileName">The name of the profile being loaded</param>
        /// <param name="encryptionEnabled">Optional, whether or not to use decryption on the save file. Set to true if the file being loaded is encrypted</param>
        /// <returns>new SaveProfile Object</returns>
        public static SaveProfile<T> LoadAs<T>(string profileName) where T : SaveProfileData
        {
            try
            {
                //check if file exists
                if (!File.Exists($"{saveFolder}/{profileName}"))
                    throw new Exception(
                        $"Save file: {profileName} not found. Make sure you've saved the file before accessing it, and the profileName is correct.");

                // Read the entire file and save its contents.
                var fileContents = File.ReadAllText($"{saveFolder}/{profileName}"); //encrypted
                // Deserialize the JSON data 
                return JsonConvert.DeserializeObject<SaveProfile<T>>(fileContents);
            }
            catch(Exception e)
            {
                throw new Exception($"Failed to load data from {profileName}. Full stack trace:\n{e}");
            }
        }

        /// <summary>
        /// Saves a SaveProfile to the Saves folder in encrypted JSON format
        /// </summary>
        /// <param name="save">SaveProfile being saved</param>
        /// <param name="overwrite">Should data which already exists be overwritten? Previous data will be lost forever!</param>
        /// <param name="encryptionEnabled">Optional, whether to encrypt the save file contents or not. Default is false</param>
        internal static void SaveAs<T>(SaveProfile<T> save, bool overwrite = false) where T : SaveProfileData
        {
            try
            {
                if (!overwrite && File.Exists($"{saveFolder}/{save.name}"))
                    throw new Exception(
                        $"Save file: {save.name} already exists, please use a different save profile name.");

                //Serialize the object into JSON and save string.
                var jsonString = JsonConvert.SerializeObject(save, Formatting.Indented,
                    new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                // Write JSON to file.
                if (!Directory.Exists(saveFolder)) //create the saves folder if we don't already have it!
                    Directory.CreateDirectory(saveFolder);
                //write the encrypted text into the file
                File.WriteAllText($"{saveFolder}/{save.name}", jsonString);

                Debug.Log($"Successfully saved data into: {saveFolder}/{save.name}");
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to save data into {save.name}. Full stack trace:\n{e}");
            }
        }
    }
}