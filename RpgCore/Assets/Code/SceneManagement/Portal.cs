using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace RPG.SceneManagement {
    public class Portal : MonoBehaviour {

        [SerializeField]
        private int _indexOfSceneToLoad = -1;

        private void OnTriggerEnter(Collider other) {
            print("xd");
            if (other.tag == "Player") {
                print("xdp");
                SceneManager.LoadScene(_indexOfSceneToLoad);
            }
        }
    }
}
