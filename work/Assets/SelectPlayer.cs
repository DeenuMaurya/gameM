using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

     public void SelectPlayerCount(int playerCount)
    {
        PlayerPrefs.SetInt("PlayerCount", playerCount);
        SceneManager.LoadScene(1);
    }
}
