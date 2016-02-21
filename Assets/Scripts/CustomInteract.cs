using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class CustomInteract : MonoBehaviour {

    [SerializeField] private VRInteractiveItem m_InteractiveItem;       // Reference to the VRInteractiveItem to determine when to fill the bar.
    [SerializeField] private VRInput m_VRInput;                         // Reference to the VRInput to detect button presses
    [SerializeField] private Collider m_Collider;                       // Optional reference to the Collider used to detect the user's gaze, turned off when the UIFader is not visible.
	[SerializeField] private Material[] mats;
	[SerializeField] private Text T1;
//	[SerializeField] private GameObject audi;       // Reference to the VRInteractiveItem to determine when to fill the bar.

	private MeshRenderer planemesh0;
	private MeshRenderer planemesh1;
	private MeshRenderer planemesh2;
	private MeshRenderer planemesh3;

	private bool m_GazeOver;

	//private MaterialSwap mSwap;

	private Material[] planemesh0Tmp;
	private Material[] planemesh1Tmp;
	private Material[] planemesh2Tmp;
	private int count;

    // Use this for initialization
    void Start () {
		count = 1;//start at the first alternate color

        m_VRInput.OnDown += HandleDown;
        m_VRInput.OnUp += HandleUp;

        m_InteractiveItem.OnOver += HandleOver;
        m_InteractiveItem.OnOut += HandleOut;
		//mSwap = GameObject.GetComponent("MaterialSwap") as MaterialSwap;
		planemesh0 = GameObject.FindWithTag ("Mesh0").GetComponent<MeshRenderer>();
		planemesh1 = GameObject.FindWithTag ("Mesh1").GetComponent<MeshRenderer>();
		planemesh2 = GameObject.FindWithTag ("Mesh2").GetComponent<MeshRenderer>();
		planemesh3 = GameObject.FindWithTag ("Mesh Side Mirrors").GetComponent<MeshRenderer>();

		planemesh0Tmp = planemesh0.materials;
		planemesh1Tmp = planemesh1.materials;
		planemesh2Tmp = planemesh2.materials;
    }

//    private void HandleDown()
//    {
//        // If the user is looking at the bar start the FillBar coroutine and store a reference to it.
//		if (m_GazeOver) {
//			T1.text = "Tapping While Looking";
//			mSwap.SwitchMaterial ();
//		}
//	}

	private void HandleDown() {
		if (m_GazeOver) {
			T1.text = "Tapping While Looking";
			if (count <= mats.Length) {//reset to default color
				if (count == mats.Length) {
					count = 0;
				}

				planemesh0Tmp [1] = mats [count];
				planemesh0.materials = planemesh0Tmp;
				//T1.text = "Tapping While Looking" + planemesh0Tmp [1];

				planemesh1Tmp [0] = mats [count];
				planemesh1.materials = planemesh1Tmp;

				planemesh2Tmp [0] = mats [count];
				planemesh2.materials = planemesh2Tmp;

				planemesh3.material = mats [count];//returning first material in Cylinder GameObject. this holds the paint for the side mirrors
				//planemesh4.materials = mats;

				count++;//next color
			}
		}
	}

    private void HandleUp()
    {
        if (m_GazeOver)
        {
            T1.text = "Stopped Tapping While Looking";
        }
        else
        {
            T1.text = "Stopped Tapping Without Looking";
        }
    }

    private void HandleOver()
    {
        // The user is now looking at the bar.
        m_GazeOver = true;
        T1.text = "Looking at it!";
    }


    private void HandleOut()
    {
        // The user is no longer looking at the bar.
        m_GazeOver = false;
        T1.text = "Not looking at it!";
    }

    // Update is called once per frame
    void Update () {
	
	}
}
