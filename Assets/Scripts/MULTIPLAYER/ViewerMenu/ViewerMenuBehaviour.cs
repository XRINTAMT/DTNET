using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class ViewerMenuBehaviour : MonoBehaviour
{
    PhotonManager photonManager;
    [SerializeField] GameObject RoomListEntry;
    [SerializeField] GameObject Container;
    [SerializeField] List<GameObject> Entries;
    List<RoomInfo> roomInfo => photonManager.roomInfo;
    int roomNum = 0;

    private void Awake()
    {
        Entries = new List<GameObject>();
    }
    // Start is called before the first frame update
    void Start()
    {
        photonManager = FindObjectOfType<PhotonManager>();
        Refresh();
    }

    public void AddRoom(string _roomName)
    {
        GameObject _newEntry = Instantiate(RoomListEntry);
        _newEntry.transform.SetParent(Container.transform);
        _newEntry.GetComponent<RoomListEntryBehaviour>().SetUp(_roomName);
        Entries.Add(_newEntry);
    }

    public void Refresh()
    {
        Clear();
        Debug.Log("Refreshed");
        Debug.Log("Num of rooms:" + roomInfo.Count);
        for (int i = 0; i < roomInfo.Count; i++)
        {
            Debug.Log(roomInfo[i].Name);
            AddRoom(roomInfo[i].Name);
        }
    }

    private void Clear()
    {
        foreach(GameObject _entry in Entries)
        {
            Destroy(_entry);
        }
        Entries.Clear();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void CreateANewRoom()
    {
        string code = Random.Range(1000, 9999).ToString();
        while(roomInfo.Any(_info => _info.Name == code))
        {
            Debug.Log(code + " generating new");
            code = Random.Range(1000, 9999).ToString();
        }
        Debug.Log("Creating a room with code " + code);
        PhotonNetwork.CreateRoom(code);
    }

    // Update is called once per frame
    void Update()
    {
        if(roomNum != roomInfo.Count)
        {
            Refresh();
            roomNum = roomInfo.Count;
        }
    }
}
