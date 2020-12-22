using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeedmeterManager : MonoBehaviour
{
	public CarSystemV2 CAR;
    public GameObject neeedle;
    public Text SpeedNumber;
    public Text gearNum;
    public float startPosiziton = 120f, endPosition = -120f;
    private float desiredPosition;


    private void FixedUpdate () {
		SpeedNumber.text = CAR.VisualSPEED.ToString ("0");
		gearNum.text = CAR.gearNum.ToString ("0");
        updateNeedle ();
		changeGear ();
    }

	    public void changeGear () {
        gearNum.text = (!CAR.reverse) ? (CAR.gearNum+1).ToString () : "R";
    }

    public void updateNeedle () {
        desiredPosition = startPosiziton - endPosition;
        float temp = CAR.engineRPM / 10000;
        neeedle.transform.eulerAngles = new Vector3 (0, 0, (startPosiziton - temp * desiredPosition));

    }
}
