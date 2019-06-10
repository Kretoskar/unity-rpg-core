﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving {
    /// <summary>
    /// Logic of saving
    /// </summary>
    public class SavingSystem : MonoBehaviour {
        /// <summary>
        /// Load the scene that a player wast last in
        /// </summary>
        /// <param name="saveFile"></param>
        /// <returns></returns>
        public IEnumerator LoadLastScene(string saveFile) {
            // Load file
            Dictionary<string, object> state = LoadFile(saveFile);
            // make sure the key already exists in dictionary
            if(state.ContainsKey("lastSceneBuildIndex")) {
                //load scene
                int buildIndex = (int)state["lastSceneBuildIndex"];
                if (buildIndex != SceneManager.GetActiveScene().buildIndex)
                    yield return SceneManager.LoadSceneAsync(buildIndex);
            }
            //restore state
            RestoreState(state);
        }

        /// <summary>
        /// Save the game
        /// </summary>
        /// <param name="saveFile">save file path</param>
        public void Save(string saveFile) {
            Dictionary<string, object>  state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);

        }

        /// <summary>
        /// Load the game
        /// </summary>
        /// <param name="saveFile">save file path</param>
        public void Load(string saveFile) { 
            RestoreState(LoadFile(saveFile));
        }

        /// <summary>
        /// Load the file
        /// </summary>
        /// <param name="saveFile">save file path</param>
        /// <returns>file to be loaded</returns>
        private Dictionary<string, object> LoadFile(string saveFile) {
            string path = GetPathFromSaveFile(saveFile);
            if (!File.Exists(path)) {
                return new Dictionary<string, object>();
            }
            using (FileStream stream = File.Open(path, FileMode.Open)) {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// Save file to the path
        /// </summary>
        /// <param name="saveFile">save file path</param>
        /// <param name="state">state to be saved</param>
        private void SaveFile(string saveFile, object state) {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create)) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        /// <summary>
        /// Capture state of all SaveableEntities and store them in a dictionaty
        /// </summary>
        /// <returns>dictionary with saveable entities</returns>
        private void CaptureState(Dictionary<string, object> state) {
            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>()) {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
            state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }

        /// <summary>
        /// Restore the last state to be loaded
        /// </summary>
        /// <param name="state">state to be restored</param>
        private void RestoreState(Dictionary<string, object> state) {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>()) {
                string id = saveable.GetUniqueIdentifier();
                if(state.ContainsKey(id))
                    saveable.RestoreState(state[id]);
            }
        }

        /// <summary>
        /// Get save file location
        /// </summary>
        /// <param name="saveFile">save file path</param>
        /// <returns>save file location</returns>
        private string GetPathFromSaveFile(string saveFile) {
            return Path.Combine(Application.persistentDataPath + ".sav");
        }
    }
}
