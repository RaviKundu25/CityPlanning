// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using UnityEngine;
//using Microsoft.Geospatial;
//using Microsoft.Maps.Unity;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System.Linq;

public class VoiceCommandManager : MonoBehaviour
{
    //public MapRenderer map;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public GameObject[] InformationPanel;

    private void Start()
    {

        foreach (GameObject info in InformationPanel)
        {
            info.SetActive(false);
        }
        keywords.Add("Show Landmark", () =>
        {
            transform.GetComponent<SceneManager>().commandTransferManager
            .sendMessage(transform.GetComponent<SceneManager>().ChannelsToJoinOnConnect[0], "Show Landmark");
        });
        keywords.Add("Show Buildings", () =>
        {
            transform.GetComponent<SceneManager>().commandTransferManager
            .sendMessage(transform.GetComponent<SceneManager>().ChannelsToJoinOnConnect[0], "Show Buildings");
        });
        keywords.Add("Show Pin", () =>
        {
            transform.GetComponent<SceneManager>().commandTransferManager
            .sendMessage(transform.GetComponent<SceneManager>().ChannelsToJoinOnConnect[0], "Show Pin");
        });
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray(), ConfidenceLevel.Medium);
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Casts the ray and get the first game object hit
            Physics.Raycast(ray, out hit);
            if(hit.transform != null)
            {
                if (hit.transform.parent != null)
                {
                    if (hit.transform.parent.tag == "Pin")
                    {
                        //showInfo();
                    }
                }
            }
        }
    }

    public void showLandmark()
    {
        //map.SetMapScene(new MapSceneOfLocationAndZoomLevel(new LatLon(28.516925, 77.0803061), 20));
    }

    public void showBuildings()
    {
        //for (int i = 0; i < map.transform.childCount; i++)
        //{
        //    if (map.transform.GetChild(i).tag == "Building")
        //    {
        //        setStateOfObject(map.transform.GetChild(i).gameObject, true);
        //    }
        //}
    }

    public void showPin()
    {
        //for (int i = 0; i < map.transform.childCount; i++)
        //{
        //    if (map.transform.GetChild(i).tag == "Pin")
        //    {
        //        setStateOfObject(map.transform.GetChild(i).gameObject, true);
        //    }
        //}
    }

    public void showInfo()
    {
        //for (int i = 0; i < map.transform.childCount; i++)
        //{
        //    if (map.transform.GetChild(i).tag == "Info")
        //    {
        //        setStateOfObject(map.transform.GetChild(i).gameObject, true);
        //    }
        //}
    }

    public void setStateOfObject(GameObject obj,bool stateOfObj)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            obj.transform.GetChild(i).gameObject.SetActive(stateOfObj);
        }
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        // if the keyword recognized is in our dictionary, call that Action.
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }

        Debug.Log(args.text);
    }
    public void ModelsToggleHandler(bool toggle)
    {
        //map.MapTerrainType = toggle ? MapTerrainType.Default : MapTerrainType.Elevated;
    }

    public void MapShapeHandler(int shape)
    {
        //if (shape == 0)
        //{
        //    map.MapShape = MapShape.Block;
        //}
        //else
        //{
        //    map.MapShape = MapShape.Cylinder;
        //}
    }

    void SeatleInformationPanel()
    {
        foreach(GameObject info in InformationPanel)
        {
            info.SetActive(true);
        }
    }
    public void AnimateToPlace(string location)
    {
        //if (location == "Space Needle")
        //{
        //    map.SetMapScene(new MapSceneOfLocationAndZoomLevel(new LatLon(47.62051, -122.349303), 17.5f));
        //}
        //else if (location == "NYC")
        //{
        //    map.SetMapScene(new MapSceneOfLocationAndZoomLevel(new LatLon(40.708707, -74.010632), 15.0f));
        //}
        //else if (location == "Golden Gate")
        //{
        //    map.SetMapScene(new MapSceneOfLocationAndZoomLevel(new LatLon(37.81869, -122.4787177), 14.5f));
        //}
        //else if (location == "Colosseum")
        //{
        //    map.SetMapScene(new MapSceneOfLocationAndZoomLevel(new LatLon(41.890153, 12.492332), 17.5f));
        //}
        //else if (location == "Atomium")
        //{
        //    map.SetMapScene(new MapSceneOfLocationAndZoomLevel(new LatLon(50.894928, 4.341533), 18.0f));
        //}
    }
}