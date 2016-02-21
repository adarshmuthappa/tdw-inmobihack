using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using VRStandardAssets.Utils;

public class CameraPlayerController : MonoBehaviour
{
    private int count;
    private float downPress;
    private Vector3 moveAmountMouse;
    private static System.DateTime startTime;
    private static System.TimeSpan diff;
    private static int minPressTime = 200;
    private static System.DateTime def;
    private int UC = 0;
    private bool flag = true;
    private float tmp;
    private float backtmp;
    private bool backFlag;
    private float i;
    private Vector3 forDir;
    private Vector3 backDir;

	public float stepSize;
    public float stepSizeBack;
    public float longPressThresholdInSeconds;
    public float backThresholdInSeconds;
    public Text T1;
    public Text T2;
    public Text T3;
    public Text T4;

	private void OnEnable()
    {
        OVRTouchpad.Create();
        backFlag = false;
    }

	void HandleTouchHandler(object sender, System.EventArgs e)
    {
        // Handle events
        var touchArgs = (OVRTouchpad.TouchArgs)e;
        OVRTouchpad.TouchEvent touchEvent = touchArgs.TouchType;

		switch (touchEvent)
        {
            case OVRTouchpad.TouchEvent.SingleTap:
                Debug.Log("SINGLE CLICK\n");
                count += 1;
                Color col0 = new Color(Random.value, Random.value, Random.value);
                //GameObject.FindWithTag("Cube0").GetComponent<Renderer>().material.color = col0;
                //			Vector3 delta = new Vector3 (Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
                //			transform.position = transform.position + delta*stepSize;
                break;

		case OVRTouchpad.TouchEvent.Left:
                Debug.Log("FRONT SWIPE\n");
                Color col1 = new Color(Random.value, Random.value, Random.value);
                //GameObject.FindWithTag("Cube1").GetComponent<Renderer>().material.color = col1;
                //			transform.position = transform.position + new Vector3 (-0.5f, 0, 0);
                //			onBack2();
                break;

		case OVRTouchpad.TouchEvent.Right:
                Debug.Log("BACK SWIPE\n");
                Color col2 = new Color(Random.value, Random.value, Random.value);
                //GameObject.FindWithTag("Cube2").GetComponent<Renderer>().material.color = col2;
                //			transform.position = transform.position + new Vector3 (0.5f, 0, 0);
                //			Vector3 delta = new Vector3 (Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
                //			transform.position = transform.position - delta*stepSizeBack;
                backFlag = true;
                backtmp = Time.time;
                backDir = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
                break;

		case OVRTouchpad.TouchEvent.Up:
                Debug.Log("UP SWIPE\n");
                Color col3 = new Color(Random.value, Random.value, Random.value);
                //GameObject.FindWithTag("Cube3").GetComponent<Renderer>().material.color = col3;
                //			Vector3 disp = new Vector3 (0, 0, 0.5f);
                //			transform.position = transform.position + disp;
                break;

		case OVRTouchpad.TouchEvent.Down:
                Debug.Log("DOWN SWIPE\n");
                Color col4 = new Color(Random.value, Random.value, Random.value);
                //GameObject.FindWithTag("Cube4").GetComponent<Renderer>().material.color = col4;
                //			transform.position = transform.position + new Vector3 (0, 0, -0.5f);
                break;
        }
    }


	private void OnDisable()
    {
        OVRTouchpad.TouchHandler -= HandleTouchHandler;
    }

	// Use this for initialization
	void Start()
    {
        OVRTouchpad.TouchHandler += HandleTouchHandler;
        count = 0;
        def = new System.DateTime(2010, 1, 1, 0, 0, 0);
        startTime = def;
        //T4.text = "Check";
        UC = 1;
    }

	void onPress()
    {
        //T2.text = "On PRess";
        //Vector3 delta = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        transform.position = transform.position + forDir * stepSize;
    }

	void onBack()
    {
        //T2.text = "On Back";
        //Vector3 delta = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        transform.position = transform.position - backDir * stepSize;
    }

	void onBack2()
    {
        //T2.text = "On Back";
        Vector3 delta = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        for (i = 0; i <= 2; i += stepSizeBack)
        {
            //T4.text = "ctr - " + i;
            transform.position = transform.position - delta * stepSizeBack;
        }
    }

	void Update()
    {
        // MOUSE INPUT
        //		if (Input.GetMouseButtonDown(0))
        //		{
        //			T2.text = "Mouse D In";
        //			if (flag) {
        //				T4.text = "ST = def";
        //				startTime = System.DateTime.UtcNow;
        //				flag = false;
        //			} else {
        //				T4.text = "ST not = def";
        //				diff = System.DateTime.UtcNow - startTime;
        //				T4.text = diff.TotalMilliseconds.ToString();
        //				if (diff.Milliseconds > minPressTime) {
        //					onPress ();
        //				}
        //			}
        //			T2.text = "Mouse D Out";
        //		}
        //		else if (Input.GetMouseButtonUp(0))
        //		{
        //			T2.text = "Mouse Up";
        //			startTime = def;
        //			flag = true;
        //		}
        //		T3.text = startTime.ToString();
        //T1.text = transform.ToString();
        if (backFlag)
        {
            if ((Time.time - backtmp) < backThresholdInSeconds)
            {
                onBack();
            }
            else {
                backFlag = false;
            }
        }
        else {
            if (Input.GetMouseButtonDown(0))
            {
                tmp = Time.time;
                forDir = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
            }
            if (Input.GetMouseButtonUp(0) && ((Time.time - tmp) < longPressThresholdInSeconds))
            {
                //T4.text = "Short click for " + (Time.time - tmp);
            }
            if (Input.GetMouseButton(0) && ((Time.time - tmp) > longPressThresholdInSeconds))
            {
                //T4.text = "Long click for " + (Time.time - tmp);
                onPress();
            }
        }
    }
}