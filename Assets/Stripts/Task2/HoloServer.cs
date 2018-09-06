using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HoloServer : MonoBehaviour {

    public GameObject allElements2D;
    private GameObject keyPattern;
    public GameObject keyPattern1;
    public GameObject keyPattern2;
    public GameObject keyPattern3;
    public GameObject keyPattern4;
    public Image TheAlertUiImage;
    //HOLO服务器！！！！！！！！！！！！！
    public int Serverport = 8808;
    public const short RegisterHotsMsgId = 1005;
    private int keyInfo, actionInfo;

    public Text TheUiText;
    private Image TheUiImage;
    private int blink = 0;
    private bool isconnect = false;
    private float disappearTime = 30000;

    public class Actionmsg : MessageBase
    {
        public int infor1;
        public int infor2;
        public int infor3;
    }

    void Start()
    {
        TheUiText.text = " ";
        TheUiImage= keyPattern1.transform.Find("Image1").GetComponent<Image>();
        Image[] uii = allElements2D.GetComponentsInChildren<Image>(true);
        foreach (Image u in uii)
        {
            u.enabled = false;
        }
        SetupServer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time> disappearTime)
        {
            TheUiImage.enabled = false;
            TheUiText.text = " ";
        }
    }

    void SetupServer()
    {
        NetworkServer.Reset();
        while (!isconnect)
        {
            if (NetworkServer.Listen(Serverport))
            {
                isconnect = true;
                NetworkServer.RegisterHandler(RegisterHotsMsgId, Onreceiving);
            }
        }
    }
    void Onreceiving(NetworkMessage netmsg)
    {
        Actionmsg Message = netmsg.ReadMessage<Actionmsg>();      
        if (Message.infor3==1 & blink==0)
        {
            InvokeRepeating("RepeatingImage", 0.3f, 0.07f);
        }
        else
        {
            keyInfo = Message.infor1;
            actionInfo = Message.infor2;
            switch (keyInfo)
            {
                case 0:
                    keyPattern = keyPattern1;//up down 
                    ShowImage(actionInfo);
                    break;
                case 1:
                    ShowText(actionInfo);//text 
                    break;
                case 2:
                    keyPattern = keyPattern2;
                    ShowImage(actionInfo);
                    break;
                case 3:
                    keyPattern = keyPattern3;
                    ShowImage(actionInfo);
                    break;
                case 4:
                    keyPattern = keyPattern4;
                    ShowImage(actionInfo);
                    break;
            }
        }
        
    }

    private void RepeatingImage()
    {
        blink++;
        // Debug.Log(blink % 2);
        if (blink % 2 == 0)
        {
            TheAlertUiImage.enabled = true;
        }
        else
        {
            TheAlertUiImage.enabled = false;
        }

        if (blink == 7)
        {
            CancelInvoke("RepeatingImage");
            blink = 0; 
        }
    }

    private void ShowImage(int info)
    {
        switch (info)
        {
            case 1:
                TheUiImage = keyPattern.transform.Find("Image1").GetComponent<Image>();
                //InvokeRepeating("RepeatingImage", 0.3f, 0.07f);
                //TheUiImage.enabled = true;
                break;
            case 2:
                TheUiImage = keyPattern.transform.Find("Image2").GetComponent<Image>();
                //InvokeRepeating("RepeatingImage", 0.3f, 0.07f);
                //TheUiImage.enabled = true;
                break;
            case 3:
                TheUiImage = keyPattern.transform.Find("Image3").GetComponent<Image>();
                //InvokeRepeating("RepeatingImage", 0.3f, 0.07f);
                //TheUiImage.enabled = true;
                break;
            case 4:
                TheUiImage = keyPattern.transform.Find("Image4").GetComponent<Image>();
                //InvokeRepeating("RepeatingImage", 0.3f, 0.07f);
                //TheUiImage.enabled = true;
                break;
        }
        TheUiImage.enabled = true;
        disappearTime = Time.time + 1.3f;
    }

    private void ShowText(int info)
    {
        switch (info)
        {
            case 1:
                TheUiText.text = "Abuse";
                //TheUiText.text = "A";
                break;
            case 2:
                TheUiText.text = "Blade";
                //TheUiText.text = "W";
                break;
            case 3:
                TheUiText.text = "Cargo";
                //TheUiText.text = "D";
                break;
            case 4:
                TheUiText.text = "Dizzy";
                //TheUiText.text = "S";
                break;
        }
        disappearTime = Time.time + 2f;
        Debug.Log(TheUiText.text);
    }
}
