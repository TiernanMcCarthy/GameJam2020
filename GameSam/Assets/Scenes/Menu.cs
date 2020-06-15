using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{

    public UnityEngine.UI.Text Connect;


    public UnityEngine.UI.InputField IPEntry;


    public UnityEngine.UI.Button HostButton;

    public UnityEngine.UI.Button JoinButton;

    public UnityEngine.UI.Button QuitButton;

    public UnityEngine.UI.Button BackButton;

    public GameObject Canvas;

    public IPHolder IP;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Reset()
    {
        HostButton.gameObject.SetActive(true);
        JoinButton.gameObject.SetActive(true);
        QuitButton.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(false);
        IPEntry.gameObject.SetActive(false);
        IPEntry.text = "";
    }

    public void Host()
    {
        Destroy(Canvas);
        SceneManager.LoadScene("Levelboy");
        Debug.Log("DO somehting here");
    }

    public void Join()
    {
        HostButton.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(true);
        IPEntry.gameObject.SetActive(true);
    }

    public void JoinServer()
    {
        IP.IPAddress = IPEntry.text;
        SceneManager.LoadScene("Levelboy");
    }

    public void Back()
    {
        Reset();
    }
}
