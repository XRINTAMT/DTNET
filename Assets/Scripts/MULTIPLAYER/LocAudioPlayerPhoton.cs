using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LocAudioPlayerPhoton : MonoBehaviour
{
    public LocalizedAudioPlayer localizedAudioPlayers;
    private void Awake()
    {
        if (PhotonManager.offlineMode)
            Destroy(this);

        localizedAudioPlayers = FindObjectOfType<LocalizedAudioPlayer>();
        if (!PhotonManager._viewerApp)
        {
            localizedAudioPlayers.playPhrase += PlayPhrase;
        }
    }

    void PlayPhrase(int id,string language)
    {
        if (!PhotonManager._viewerApp && !PhotonManager.offlineMode)
        {
            GetComponent<PhotonView>().RPC("PlayPhraseRPC", RpcTarget.All,id,language);
        }
    }

    [PunRPC]
    void PlayPhraseRPC(int id, string language)
    {
        Debug.Log("PlayPhraseRPC");

        if (PhotonManager._viewerApp)
        {
            localizedAudioPlayers.PlayPhrasePhoton(id, language);
        }
    }
    // Start is called before the first frame update


}
