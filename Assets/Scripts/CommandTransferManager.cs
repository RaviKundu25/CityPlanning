using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using System;

public class CommandTransferManager : MonoBehaviour,IChatClientListener
{
    public string[] ChannelsToJoinOnConnect;
    public string[] FriendsList;
    public int HistoryLengthToFetch;
    public string UserName { get; set; }
    public ChatClient chatClient;
    [SerializeField]
    protected internal ChatSettings chatAppSettings;

    void Update()
    {
        if (this.chatClient != null)
        {
            this.chatClient.Service();
        }
    }
    public void Connect()
    {
        if (string.IsNullOrEmpty(this.UserName))
        {
            this.UserName = "user" + Environment.TickCount % 99;
        }

        if (this.chatAppSettings == null)
        {
            this.chatAppSettings = ChatSettings.Instance;
        }

        this.chatClient = new ChatClient(this);
        this.chatClient.UseBackgroundWorkerForSending = true;
        this.chatClient.Connect(this.chatAppSettings.AppId, "1.0", new Photon.Chat.AuthenticationValues(this.UserName));

        Debug.Log("Connecting as: " + this.UserName);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        if (level == ExitGames.Client.Photon.DebugLevel.ERROR)
        {
            Debug.LogError(message);
        }
        else if (level == ExitGames.Client.Photon.DebugLevel.WARNING)
        {
            Debug.LogWarning(message);
        }
        else
        {
            Debug.Log(message);
        }
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log("State changed to : " + state);
    }

    public void OnConnected()
    {
        if (this.ChannelsToJoinOnConnect != null && this.ChannelsToJoinOnConnect.Length > 0)
        {
            this.chatClient.Subscribe(this.ChannelsToJoinOnConnect, this.HistoryLengthToFetch);
        }

        if (this.FriendsList != null && this.FriendsList.Length > 0)
        {
            this.chatClient.AddFriends(this.FriendsList);
        }

        this.chatClient.SetOnlineStatus(ChatUserStatus.Online);
    }

    public void OnDisconnected()
    {
        Debug.Log("OnDisconnected called.");
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            msgs = string.Format("Channel : {0} :: User : {1} :: Message : {2}", channelName, senders[i], messages[i]);
            Debug.Log(msgs);
            switch (messages[i])
            {
                case "Show Landmark":
                    transform.GetComponent<VoiceCommandManager>().showLandmark();
                    break;
                case "Show Buildings":
                    transform.GetComponent<VoiceCommandManager>().showBuildings();
                    break;
                case "Show Pin":
                    transform.GetComponent<VoiceCommandManager>().showPin();
                    break;
            }
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        string msg = string.Format("Channel : {0} :: User : {1} :: Message : {2}", channelName, sender, message);
        Debug.Log(msg);
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        Debug.Log("OnStatusUpdate called.");
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        foreach (string channel in channels)
        {
            this.chatClient.PublishMessage(channel, "has joined the channel.");
        }

        Debug.Log("OnSubscribed: " + string.Join(", ", channels));
    }

    public void OnUnsubscribed(string[] channels)
    {
        Debug.Log("OnUnsubscribed from channels.");
    }

    public void OnUserSubscribed(string channel, string user)
    {
        Debug.Log(user + " has joined the channel : " + channel);
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        Debug.Log(user + " has left the channel : " + channel);
    }

    public void sendMessage(string channel,string msg)
    {
        this.chatClient.PublishMessage(channel, msg);
    }
}
