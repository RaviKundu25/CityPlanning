using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public string[] ChannelsToJoinOnConnect;
    public CommandTransferManager commandTransferManager;

    void Start()
    {
        commandTransferManager = FindObjectOfType<CommandTransferManager>();
        commandTransferManager.UserName = "user" + Environment.TickCount % 99;
        commandTransferManager.ChannelsToJoinOnConnect = ChannelsToJoinOnConnect;
        commandTransferManager.Connect();
    }
}
