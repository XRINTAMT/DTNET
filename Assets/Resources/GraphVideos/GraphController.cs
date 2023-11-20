using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GraphController : MonoBehaviour
{
    [System.Serializable]
    class VitalsGraph
    {
        [SerializeField] string name;
        [field: SerializeField] public RenderTexture TextureToTranslateTo { get; private set; }
        [field: SerializeField] public int initialVideo { get; private set; }
        public int currentVideo;
        public VideoPlayer Player;
        public bool Noisy;
    }

    [SerializeField] VitalsGraph[] Graphs;
    [SerializeField] VideoClip[] ClipsDatabase;
    [SerializeField] int[] NoisyReplacements;

    void Start()
    {
        for(int i = 0; i < Graphs.Length; i++)
        {
            Graphs[i].Player = gameObject.AddComponent<VideoPlayer>();
            Graphs[i].Player.isLooping = true;
            Graphs[i].Player.clip = ClipsDatabase[Graphs[i].initialVideo];
            Graphs[i].currentVideo = Graphs[i].initialVideo;
            Graphs[i].Player.renderMode = VideoRenderMode.RenderTexture;
            Graphs[i].Player.targetTexture = Graphs[i].TextureToTranslateTo;
            Graphs[i].Player.audioOutputMode = VideoAudioOutputMode.None;
        }
    }

    public void NoisyUpdate(int GraphID)
    {
        for(int i = 0; i < NoisyReplacements.Length; i++)
        {
            int from = NoisyReplacements[i] / 10 - 1;
            int to = NoisyReplacements[i] % 10 - 1;
            if (from == Graphs[GraphID].currentVideo && Graphs[GraphID].Noisy)
            {
                ChangeVideo(GraphID, to);
            }
            else
            {
                if (to == Graphs[GraphID].currentVideo && !Graphs[GraphID].Noisy)
                {
                    ChangeVideo(GraphID, from);
                }
            }
        }
    }

    public void ChangeVideo(int GraphID, int VideoID)
    {
        Graphs[GraphID].Player.Stop();
        Graphs[GraphID].Player.clip = ClipsDatabase[VideoID];
        Graphs[GraphID].currentVideo = VideoID;
        Graphs[GraphID].Player.Play();
    }

    public void AddNoise(int GraphID)
    {
        Graphs[GraphID].Noisy = true;
        NoisyUpdate(GraphID);
    }

    public void RemoveNoise(int GraphID)
    {
        Graphs[GraphID].Noisy = false;
        NoisyUpdate(GraphID);
    }

    public void ChangeVideoInspector(string deserialized)
    {
        string[] args = deserialized.Split(',');
        if (args.Length != 2)
        {
            Debug.LogError("This function accepts exactly 2 arguments!");
            return;
        }
        int GraphID, VideoID;
        if (int.TryParse(args[0], out GraphID) && int.TryParse(args[1], out VideoID))
        {
            ChangeVideo(GraphID, VideoID);
        }
        else
        {
            Debug.LogError("Could not parse some of your inputs: " + deserialized);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
