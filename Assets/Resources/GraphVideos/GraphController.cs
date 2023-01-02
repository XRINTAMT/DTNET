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
        public VideoPlayer Player;
    }

    [SerializeField] VitalsGraph[] Graphs;
    [SerializeField] VideoClip[] ClipsDatabase;

    void Start()
    {
        for(int i = 0; i < Graphs.Length; i++)
        {
            Graphs[i].Player = gameObject.AddComponent<VideoPlayer>();
            Graphs[i].Player.isLooping = true;
            Graphs[i].Player.clip = ClipsDatabase[Graphs[i].initialVideo];
            Graphs[i].Player.renderMode = VideoRenderMode.RenderTexture;
            Graphs[i].Player.targetTexture = Graphs[i].TextureToTranslateTo;
            Graphs[i].Player.audioOutputMode = VideoAudioOutputMode.None;
        }
    }

    public void ChangeVideo(int GraphID, int VideoID)
    {
        Graphs[GraphID].Player.Stop();
        Graphs[GraphID].Player.clip = ClipsDatabase[VideoID];
        Graphs[GraphID].Player.Play();
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
