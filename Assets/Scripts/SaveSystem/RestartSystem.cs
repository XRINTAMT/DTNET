using System.Collections;
using UnityEngine;
using Photon.Pun;

public class RestartSystem : MonoBehaviourPunCallbacks
{
    void Start()
    {
        Save();
    }

    public void Save()
    {
        if (PlayerPrefs.GetInt("GuidedMode", 1) != 1)
        {
            return;
        }
        Invoke("ConditionalSave", 1);
    }

    private void ConditionalSave()
    {
        
        if (PhotonManager.offlineMode)
        {
            MakeASave();
        }
        else
        {
            if(PhotonNetwork.IsMasterClient)
                photonView.RPC("MakeASaveRPC", RpcTarget.All);
        }
    }

    [PunRPC]
    private void MakeASaveRPC()
    {
        DataSaver[] _savers = FindObjectsOfType<DataSaver>(true);
        foreach (DataSaver _saver in _savers)
        {
            _saver.Save();
        }
    }

    private void MakeASave()
    {
        DataSaver[] _savers = FindObjectsOfType<DataSaver>(true);
        foreach (DataSaver _saver in _savers)
        {
            _saver.Save();
        }
    }

    public void Load(string text = "")
    {

        if (PlayerPrefs.GetInt("GuidedMode") == 1) text = "That is not what you should be doing right now. Check the tasks list!";

        StartCoroutine(LoadDelay());
        foreach (FadeMessageManager Fade in FindObjectsOfType<FadeMessageManager>())
        {
            Fade.FadeWithText(text);
        }
    }

    IEnumerator LoadDelay()
    {
        for (float i = 0; i < 2; i += Time.deltaTime)
        {
            yield return 0;
        }

        if (PhotonManager.offlineMode)
        {
            LoadData();
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
                photonView.RPC("LoadDataRPC", RpcTarget.All);
        }
    }

    [PunRPC]
    private void LoadDataRPC()
    {
        DataSaver[] _savers = FindObjectsOfType<DataSaver>(true);
        foreach (DataSaver _saver in _savers)
        {
            _saver.Load();
        }
    }

    private void LoadData()
    {
        DataSaver[] _savers = FindObjectsOfType<DataSaver>(true);
        foreach (DataSaver _saver in _savers)
        {
            _saver.Load();
        }
    }
}