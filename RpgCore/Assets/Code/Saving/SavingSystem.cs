﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace RPG.Saving {
    public class SavingSystem : MonoBehaviour {

        public void Save(string saveFile) {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create)) {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, CaptureState());
            }
        }

        public void Load(string saveFile) {
            string path = GetPathFromSaveFile(saveFile);
            print("Loading from " + path);
            using (FileStream stream = File.Open(path, FileMode.Open)) {
                BinaryFormatter formatter = new BinaryFormatter();
                RestoreState(formatter.Deserialize(stream));
            }
        }

        /// <summary>
        /// Capture state of all SaveableEntities and store them in a dictionaty
        /// </summary>
        /// <returns>dictionary with saveable entities</returns>
        private object CaptureState() {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>()) {
                print(saveable.GetUniqueIdentifier());
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
            return state;
        }

        private void RestoreState(object state) {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>()) {
                saveable.RestoreState(stateDict[saveable.GetUniqueIdentifier()]);
            }

        }

        private string GetPathFromSaveFile(string saveFile) {
            return Path.Combine(Application.persistentDataPath + ".sav");
        }
    }
}
