using System.Collections;
using System.Collections.Generic;
using HoloToolkit.UX.Dialog;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewTask3Server : MonoBehaviour
{

    public GameObject allElements2D;
    public Slider aim1;
    public Slider rateNow1;
    public Slider rateNow3;
    public Text aim2;
    public Text rateNow2;
    public Text inforamtion;
    //public Text textRate;
    //public Text textAim;


    public int Serverport = 8808;
    public const short RegisterHotsMsgId = 1003;

    private int trialNumber = 0;
    private bool isconnect = false;
    private int progress100 = 8;
    private Slider aim;
    private Slider rateNow;
    private Text textAim;
    private Text textRate;
    private float everyStartTime;
    private int buttonTimes;


    public class Message : MessageBase
    {
        public float aimRate;
        public bool letterChange;
        public int buttonRecords;
        public int state;
    }

    // Use this for initialization
    void Start()
    {
        Image[] uii = allElements2D.GetComponentsInChildren<Image>(true);
        foreach (Image u in uii)
        {
            u.enabled = false;
        }

        Text[] uit = allElements2D.GetComponentsInChildren<Text>(true);
        foreach (Text u in uit)
        {
            u.text=" ";
        }

        aim1.value = 0;
        rateNow1.value = 0;
        rateNow3.value = 0;
        aim2.text = " ";
        rateNow2.text = " ";

        SetupServer();
        inforamtion.text = "Please Choose the task mode...";
    }

    // Update is called once per frame
    void Update()
    {
        if (inforamtion.text == "Please focus on the content")
        {
            float interval = Time.time - everyStartTime;
            switch (trialNumber)
            {
                case 1:
                    textRate.text = (buttonTimes / interval).ToString("0.0");
                    break;
                case 2:
                    textRate.text = (buttonTimes / interval).ToString("0.0")+ " times/s";
                    break;
                case 3:
                    rateNow.value = (buttonTimes / interval) / progress100;
                    break;
                case 4:
                    rateNow.value = (buttonTimes / interval) / progress100;
                    break;
            }
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
        inforamtion.text = "Please focus on the content";
        Message tempMessage = netmsg.ReadMessage<Message>();
        switch (trialNumber)
        {
            case 0:
                break;
            case 1:
                textAim.text = ((int) tempMessage.aimRate).ToString();
                break;
            case 2:
                textAim.text = ((int) tempMessage.aimRate)+" times/s";
                break;
            case 3:
                aim.value = tempMessage.aimRate / progress100;
                break;
            case 4:
                aim.value = tempMessage.aimRate / progress100;
                break;
        }
        buttonTimes = tempMessage.buttonRecords;
        if (tempMessage.letterChange == true)
        {
            everyStartTime = Time.time;
            Debug.Log("changed!!!!!");
        }

        if (tempMessage.state == 0)
        {
            isconnect = false;
            Start();
        }
    }

    public void B1()
    {
        textAim = aim2;
        textRate = rateNow2;
        trialNumber = 1;
        inforamtion.text = "This is Trial 1";
    }

    public void B2()
    {
        textAim = aim2;
        textRate = rateNow2;
        trialNumber = 2;
        inforamtion.text = "This is Trial 2";
    }

    public void B3()
    {
        aim = aim1;
        Image[] ui1 = aim1.GetComponentsInChildren<Image>(true);
        foreach (Image u in ui1)
        {
            u.enabled = true;
        }
        rateNow = rateNow1;
        Image[] ui2 = rateNow1.GetComponentsInChildren<Image>(true);
        foreach (Image u in ui2)
        {
            u.enabled = true;
        }
        trialNumber = 3;
        inforamtion.text = "This is Trial 3";
    }

    public void B4()
    {
        aim = aim1;
        Image[] ui1 = aim1.GetComponentsInChildren<Image>(true);
        foreach (Image u in ui1)
        {
            u.enabled = true;
        }
        rateNow = rateNow3;
        Image[] ui2 = rateNow3.GetComponentsInChildren<Image>(true);
        foreach (Image u in ui2)
        {
            u.enabled = true;
        }
        trialNumber = 4;
        inforamtion.text = "This is Trial 4";
    }
}
