using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using DTNET.Models;

namespace DTNET.Actions {
    public class LoadNewScene : MonoBehaviour
    {
        private const string BLOOD_SAMPLE_SCENE_NAME = "BloodSampling" ;
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
