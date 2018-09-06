using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using HoloToolkit.UX.Dialog;

public class Lunch : MonoBehaviour
{

    [SerializeField]
    private Dialog dialogPrefab;

    [SerializeField]
    public GameObject allElements1;

    [SerializeField]
    public GameObject allElements3;

    [SerializeField]
    public Text theInfor;

    public string informationType;

    private bool isDialogLaunched;
    private bool showinformation = false;
    private float stratTime = 0;
    private float endTime = 0;


    protected IEnumerator LaunchDialog(DialogButtonType buttons, string title, string message)
    {
        isDialogLaunched = true;

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

        //only let one dialog be created at a time
        isDialogLaunched = false;

        yield break;
    }

    // Use this for initialization
    void Start()
    {
        //初始化，不显示UI
        UIProcessing(allElements1);
        UIProcessing(allElements3);
        if (isDialogLaunched == false)
        {
            theInfor.text= null;
            // Launch Dialog with two buttons
            StartCoroutine(LaunchDialog(DialogButtonType.Graphics | DialogButtonType.Text, "Choose your information type:", "Choose the information type for this task."));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showinformation==true)
        {
            endTime = Time.time;
            if (endTime- stratTime > 3f)
            {
                theInfor.text = null;
            }
        }
    }

    protected void OnClosed(DialogResult result)
    {
        // Get the result text from the Dialog
        informationType = result.Result.ToString();
        theInfor.text = informationType;
        Debug.Log(informationType);
        showinformation = true;
        stratTime = Time.time;
    }

    void UIProcessing(GameObject theObject)
    {
        Image[] uii = theObject.GetComponentsInChildren<Image>(true);
        foreach (Image u in uii)
        {
            u.enabled = false;
        }

        Text[] uit = theObject.GetComponentsInChildren<Text>(true);
        foreach (Text u in uit)
        {
            u.enabled = false;
        }
    }
}
