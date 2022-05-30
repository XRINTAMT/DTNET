using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace DTNET.CSystem {
    public class LoadScene : MonoBehaviour
    {

        private const string BLOOD_SAMPLE_SCENE_NAME = "BloodSample" ;
        private const string MAIN_SCENE_NAME = "MainMenu" ;
        public void LoadBloodSampleSceneBeginner() 
        {
            GameMode.SelectedMode = "Beginner";
            SceneManager.LoadScene(BLOOD_SAMPLE_SCENE_NAME);
        }

        public void LoadBloodSampleSceneExperience() 
        {
            GameMode.SelectedMode = "Experience";
            SceneManager.LoadScene(BLOOD_SAMPLE_SCENE_NAME);
        }

        public void LoadMainMenuScene() 
        {
            SceneManager.LoadScene(MAIN_SCENE_NAME);
        }
    }
}
