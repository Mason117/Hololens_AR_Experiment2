using System.Collections;
using System.Collections.Generic;
using HoloToolkit.UX.Dialog;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Task3Server : MonoBehaviour
{

    public GameObject allElements2D;
    public Slider aim1;
    public Slider rateNow1;
    public Slider aim2;
    public Slider rateNow2;
    public Text inforamtion;
    [SerializeField]
    private Dialog dialogPrefab;

    public int Serverport = 8808;
    public const short RegisterHotsMsgId = 1003;

    private bool isconnect = false;
    private int progress100 = 8;
    private bool isDialogLaunched;
    private Slider aim;
    private Slider rateNow;
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
        StartCoroutine(LaunchDialog(DialogButtonType.Pattern1 | DialogButtonType.Pattern2, "Choose your Pattern type:", "Choose the Pattern type for this task."));
        SetupServer();
        inforamtion.text = "Please stand by...";
    }

    // Update is called once per frame
    void Update()
    {
        if (inforamtion.text== "Please focus on the lettter")
        {
            float interval = Time.time - everyStartTime;
            rateNow.value = (buttonTimes / interval)/ progress100;
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
        inforamtion.text = "Please focus on the lettter";
        Message tempMessage = netmsg.ReadMessage<Message>();

        aim.value = tempMessage.aimRate / progress100;
        buttonTimes = tempMessage.buttonRecords;
        //这个代码没有办法应对如果刚好这次传送丢失的情况
        if (tempMessage.letterChange==true)
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

    protected IEnumerator LaunchDialog(DialogButtonType buttons, string title, string message)
    {

        //Open Dialog by sending in prefab
        Dialog dialog = Dialog.Open(dialogPrefab.gameObject, buttons, title, message);

        if (dialog != null)
        {
            //listen for OnClosed Event
            dialog.OnClosed += OnClosed;
        }

        // Wait for dialog to close
        while (dialog.State < DialogState.InputReceived)
        {
            yield return null;
        }

        yield break;
    }

    protected void OnClosed(DialogResult result)
    {
        // Get the result text from the Dialog
        Debug.Log(result.Result.ToString());
        string temp = result.Result.ToString();
        switch (temp)
        {
            case "Pattern1":
                aim = aim1;
                rateNow = rateNow1;
                Image[] ui1 = aim1.GetComponentsInChildren<Image>(true);
                foreach (Image u in ui1)
                {
                    u.enabled = true;
                }
                Debug.Log(result.Result.ToString());
                Image[] ui2 = rateNow1.GetComponentsInChildren<Image>(true);
                foreach (Image u in ui2)
                {
                    u.enabled = true;
                }
                Image[] ui3 = aim2.GetComponentsInChildren<Image>(true);
                foreach (Image u in ui3)
                {
                    u.enabled = false;
                }
                Image[] ui4 = rateNow2.GetComponentsInChildren<Image>(true);
                foreach (Image u in ui4)
                {
                    u.enabled = false;
                }
                break;
            case "Pattern2":
                aim = aim2;
                rateNow = rateNow2;
                Image[] ui5 = aim1.GetComponentsInChildren<Image>(true);
                foreach (Image u in ui5)
                {
                    u.enabled = false;
                }
                Image[] ui6 = rateNow1.GetComponentsInChildren<Image>(true);
                foreach (Image u in ui6)
                {
                    u.enabled = false;
                }
                Image[] ui7 = aim2.GetComponentsInChildren<Image>(true);
                foreach (Image u in ui7)
                {
                    u.enabled = true;
                }
                Image[] ui8 = rateNow2.GetComponentsInChildren<Image>(true);
                foreach (Image u in ui8)
                {
                    u.enabled = true;
                }
                break;
        }
    }

}
