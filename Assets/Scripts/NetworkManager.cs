using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using ExitGames.Client.Photon;
using System;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] bool roomJoined = false;
    [SerializeField] bool isConnectingToMaster = false;
    [SerializeField] string roomName = "CityPlanning";
    [SerializeField] string gameVersion = "0.1";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void searchForRoom()
    {
        isConnectingToMaster = true;
        if (PhotonNetwork.IsConnected)
        {
            OnConnectedToMaster();
        }
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }

        Debug.Log("Connecting...");
    }
    #region CONNECTION
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to master!");
        Debug.Log("Joining room...");

        //PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.JoinRoom(roomName);

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("Disconnected with reason {0}", cause);
    }


    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room!");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("Room join failed " + message);
        Debug.Log("Creating room...");
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 8, IsOpen = true, IsVisible = true }, TypedLobby.Default);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        Debug.Log("Got " + roomList.Count + " rooms.");
        foreach (RoomInfo room in roomList)
        {
            Debug.Log("Room: " + room.Name + ", " + room.PlayerCount);
        }
    }

    public void CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Avatar"), Vector3.zero, Quaternion.identity, 0);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "CustomHandLeft"), Vector3.zero, Quaternion.identity, 0);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "CustomHandRight"), Vector3.zero, Quaternion.identity, 0);
    }
    #endregion
    #region ROOM_PROPS

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log(newPlayer.UserId + " has entered the room");
        //OnPlayersChanged?.Invoke();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log(otherPlayer.UserId + " has left the room");
        //OnPlayersChanged?.Invoke();

    }

    #endregion
}