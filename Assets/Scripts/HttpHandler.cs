using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpHandler : MonoBehaviour
{
    private string url= "https://rickandmortyapi.com/api";
    
    public void SendRequest()
    {
        StartCoroutine("GetCharacters");
    }

    IEnumerator GetCharacters()
    {
        UnityWebRequest request = UnityWebRequest.Get(url +"/character");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            //Debug.Log(request.downloadHandler.text);
            //byte[] result = request.downloadHandler.data;

            if (request.responseCode == 200)
            {
                JsonData data =JsonUtility.FromJson<JsonData>(request.downloadHandler.text);
                foreach (CharacterData character in data.results)
                {
                    Debug.Log( character.name+ " is a "+ character.species);
                }
                
            }
            else
            {
                Debug.Log(request.responseCode + "|" + request.error );
            }
        }
        
    }
   
}

[System.Serializable]
public class JsonData
{
    public CharacterData[] results;
}

[System.Serializable]
public class CharacterData
{
    public int id;
    public string name;
    public string species;
    public string image;
}
