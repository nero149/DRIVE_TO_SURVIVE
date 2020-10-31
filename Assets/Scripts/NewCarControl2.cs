using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Made with a youtube Tutorial, very simple Code that will be a lot changed,Aceleration and Braking
public class NewCarControl2 : MonoBehaviour
{	
	internal enum driveType{
    frontWheelDrive,
    rearWheelDrive,
    allWheelDrive
    }
    [SerializeField]private driveType CarType;
	
    private void Awake() 
	{
        getObjects();
    }
	

 public void GetInput()
	{
		m_horizontalInput = Input.GetAxis("Horizontal");
		m_verticalInput = Input.GetAxis("Vertical");
		m_AccelerationInput = Input.GetAxis("Accelerate");
		m_BrakingInput = Input.GetAxis("Braking");
		m_HandBrakingInput = Input.GetAxis("Handbrake");
	}

	private void Steer() //Need Ackerman Curve and Animations Curve
	{
		//m_steeringAngle = maxSteerAngle * m_horizontalInput;
		//ActualSteerAngle = m_steeringAngle - maxSteerAngle;
		//frontDriverW.steerAngle = ActualSteerAngle + maxSteerAngle;
		//frontPassengerW.steerAngle = ActualSteerAngle + maxSteerAngle;
		if (m_horizontalInput > 0 ) {
				//rear tracks size is set to 1.5f       wheel base has been set to 2.55f
            frontDriverW.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * m_horizontalInput;
            frontPassengerW.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * m_horizontalInput;
        } else if (m_horizontalInput < 0 ) {                                                          
            frontDriverW.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * m_horizontalInput;
            frontPassengerW.steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * m_horizontalInput;
			//transform.Rotate(Vector3.up * steerHelping);

        } else {
            frontDriverW.steerAngle =0;
            frontPassengerW.steerAngle =0;
        }

    }
	

	private void Accelerate() 
	{
		//rearDriverW.motorTorque = m_AccelerationInput * motorForce;
        //rearPassengerW.motorTorque =  m_AccelerationInput * motorForce;
		//frontDriverW.motorTorque =  m_AccelerationInput * motorForce;
        //frontPassengerW.motorTorque =  m_AccelerationInput * motorForce;
	}
		
	private void Braking() 
	{


	}
	private void HandBraking()
	{
		if( m_HandBrakingInput > 0 ){
		frontDriverW.brakeTorque = HandbrakPower;
        frontPassengerW.brakeTorque = HandbrakPower;
		rearDriverW.brakeTorque = HandbrakPower;
        rearPassengerW.brakeTorque = HandbrakPower;
		}else{ 
		frontDriverW.brakeTorque = brakPower;
        frontPassengerW.brakeTorque = brakPower;
		rearDriverW.brakeTorque = brakPower;
        rearPassengerW.brakeTorque = brakPower;
		}
	}
	
	private void MoveWheel()
	{
		if (CarType == driveType.allWheelDrive){
		frontDriverW.motorTorque = m_AccelerationInput * motorForce/4;
        frontPassengerW.motorTorque = m_AccelerationInput * motorForce/4; // /4
		rearDriverW.motorTorque = m_AccelerationInput * motorForce/4;
        rearPassengerW.motorTorque = m_AccelerationInput * motorForce/4;
		frontDriverW.brakeTorque = brakPower;
        frontPassengerW.brakeTorque = brakPower;
		rearDriverW.brakeTorque = brakPower;
        rearPassengerW.brakeTorque = brakPower;
        }else if(CarType == driveType.rearWheelDrive){
		rearDriverW.motorTorque = m_AccelerationInput * motorForce/2; // /2
        rearPassengerW.motorTorque = m_AccelerationInput * motorForce/2;
		frontDriverW.brakeTorque = brakPower;
        frontPassengerW.brakeTorque = brakPower;
		rearDriverW.brakeTorque = brakPower;
        rearPassengerW.brakeTorque = brakPower;
        }else{
		frontDriverW.motorTorque = m_AccelerationInput * motorForce /2; // /2
        frontPassengerW.motorTorque = m_AccelerationInput * motorForce /2;
		frontDriverW.brakeTorque = brakPower;
        frontPassengerW.brakeTorque = brakPower;
		rearDriverW.brakeTorque = brakPower;
        rearPassengerW.brakeTorque = brakPower;
		}
		KPH = CarBody.velocity.magnitude * 3.6f;

	}
		
    private void addDownForce(){

        CarBody.AddForce(-transform.up * DownForceValue * CarBody.velocity.magnitude );

    }		
		
	private void GetFriction() 
	{
			frontDriverW.GetGroundHit(out wheelHit);
			frontPassengerW.GetGroundHit(out wheelHit1);
			rearDriverW.GetGroundHit(out wheelHit2);
			rearPassengerW.GetGroundHit(out wheelHit3);
			Slip[0] = wheelHit.forwardSlip;
			Slip[1] = wheelHit1.forwardSlip;
			Slip[2] = wheelHit2.forwardSlip;
			Slip[3] = wheelHit3.forwardSlip;

	}		
		
	private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontDriverW, frontDriverT);
		UpdateWheelPose(frontPassengerW, frontPassengerT);
		UpdateWheelPose(rearDriverW, rearDriverT);
		UpdateWheelPose(rearPassengerW, rearPassengerT);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}

	private void FixedUpdate()
	{
		GetInput();
		Steer();
		Accelerate();
		Braking();
		UpdateWheelPoses();
		MoveWheel();
		HandBraking();
	    GetFriction();
		lastValue = engineRPM;

	}
	
    private void getObjects(){
       Rigidbody CarBody = GetComponent<Rigidbody>();
	   centerofMass = GameObject.Find ("CenterofMass");
	   CarBody.centerOfMass = centerofMass.transform.localPosition;   
    }
	
	private float m_horizontalInput;
	private float m_verticalInput;
	private float m_AccelerationInput;
	private float m_BrakingInput;
	private float m_steeringAngle;
	private float m_HandBrakingInput;
	public WheelHit wheelHit;
	public WheelHit wheelHit1;
	public WheelHit wheelHit2;
	public WheelHit wheelHit3;
	public WheelCollider[] wheels = new WheelCollider[4];
	public GameObject[] wheelModels = new GameObject[4];
	public WheelCollider frontDriverW, frontPassengerW;
	public WheelCollider rearDriverW, rearPassengerW;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	public GameObject centerofMass;
	public Rigidbody CarBody;
	public float maxSteerAngle = 31;
	public float ActualSteerAngle;
	public float ActualRotation;
	public float motorForce = 1000; //HorsePower
	public float KPH; //Speed in Kilometers
	//Not Implemented
    public float engineRPM;
	public float[] Slip = new float[4];
	public float driftFactor;
	public float wheelsRPM;
	public float brakPower = 0; //Car Gear Max
	public float HandbrakPower = 0; //Car Gear Max
	public float radius = 6;
	public float lastValue;
	private float DownForceValue = 10f;
	//For Future Systems of Speed,Gear, Drifting,Damage
	public float CarLife = 1000; //Speed
	public float Nitrox = 3; //Speed
	public float RPM; //Speed
	public float Gear; //Speed
	public float ReverseGear; //Car Max Speed

}
