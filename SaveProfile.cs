using System;
using UnityEngine;

namespace SaveLoad
{
    [Serializable]
    public sealed class SaveProfile<T> where T : SaveProfileData
    {
        public string name;
        public T data;

        private SaveProfile() { } //default constructor - used by JSON converter
        
        /// <summary>
        /// Creates a new SaveProfile with the given name.
        /// </summary>
        /// <param name="name">string name and filename of the save profile</param>
        /// <param name="data">T data to be saved into this profile</param>
        public SaveProfile(string name, T data)
        {
            this.name = name;
            this.data = data;
        }

        /// <summary>
        /// Saves a SaveProfile to the Saves folder in encrypted JSON format
        /// </summary>
        /// <param name="overwrite">Should data which already exists be overwritten? Previous data will be lost forever!</param>
        /// <param name="encrypt">Optional, whether to encrypt the save file contents or not. Default is false</param>
        public void Save(bool overwrite = false, bool encrypt = true) => SaveManager.SaveAs(this, overwrite, encrypt);
    }

    public abstract record SaveProfileData
    {
        public void SaveAs(string saveName, bool overrite = false)
        {
            var profile = new SaveProfile<SaveProfileData>(saveName,this);
            SaveManager.SaveAs(profile, overrite);
        }
    }
}