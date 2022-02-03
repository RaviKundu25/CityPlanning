using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

public class GetMyWeather : MonoBehaviour
{

    //public UILabel myWeatherLabel;
    public RawImage myWeatherCondition;

    //retrieved from weather API
    public string countryName;
    public string cityName;
    public int conditionID;
    public string conditionName;
    public string conditionImage;

    void Start()
    {
        StartCoroutine(SendRequest());
    }

    IEnumerator SendRequest()
    {
        string url = "http://api.openweathermap.org/data/2.5/weather?q=" + cityName + "," + countryName + "&APPID=e3a642cec13d52496490dfa8e9ba11d3";
        Debug.Log(url);
        WWW request = new WWW(url); //get our weather
        yield return request;

        if (request.error == null || request.error == "")
        {
            var N = JSON.Parse(request.text);

            string temp = N["main"]["temp"].Value; //get the temperature
            float tempTemp; //variable to hold the parsed temperature
            float.TryParse(temp, out tempTemp); //parse the temperature
            float finalTemp = Mathf.Round((tempTemp - 273.0f) * 10) / 10; //holds the actual converted temperature

            int.TryParse(N["weather"][0]["id"].Value, out conditionID); //get the current condition ID
            //conditionName = N["weather"][0]["main"].Value; //get the current condition Name
            conditionName = N["weather"][0]["description"].Value; //get the current condition Description
            conditionImage = N["weather"][0]["icon"].Value; //get the current condition Image

            //put all the retrieved stuff in the label
            Debug.Log(
                "Country: " + countryName
                + "\nCity: " + cityName
                + "\nTemperature: " + finalTemp + " C"
                + "\nCurrent Condition: " + conditionName
                + "\nCondition Code: " + conditionID);
        }
        else
        {
            Debug.Log("WWW error: " + request.error);
        }

        //get our weather image
        WWW conditionRequest = new WWW("http://openweathermap.org/img/w/" + conditionImage + ".png");
        yield return conditionRequest;

        if (conditionRequest.error == null || conditionRequest.error == "")
        {
            //create the material, put in the downloaded texture and make it visible
            var texture = conditionRequest.texture;
            myWeatherCondition.texture = texture;
        }
        else
        {
            Debug.Log("WWW error: " + conditionRequest.error);
        }
    }
}