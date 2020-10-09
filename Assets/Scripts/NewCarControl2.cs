using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Made with a youtube Tutorial, very simple Code that will be a lot changed,Aceleration and Braking
public class NewCarControl2 : MonoBehaviour
{	

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
		rearDriverW.motorTorque = m_AccelerationInput * motorForce;
        rearPassengerW.motorTorque =  m_AccelerationInput * motorForce;
		frontDriverW.motorTorque =  m_AccelerationInput * motorForce;
        frontPassengerW.motorTorque =  m_AccelerationInput * motorForce;
		KPH = rigidbody.velocity.magnitude * 3.6f;
}
		
	private void Braking()
	{


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
		lastValue = engineRPM;

	}
	
    private void getObjects(){
       rigidbody = gameObject.GetComponent<Rigidbody>();
    }
	
	private float m_horizontalInput;
	private float m_verticalInput;
	private float m_AccelerationInput;
	private float m_BrakingInput;
	private float m_steeringAngle;

	public WheelCollider frontDriverW, frontPassengerW;
	public WheelCollider rearDriverW, rearPassengerW;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	private GameObject centerOfMass;
	private Rigidbody rigidbody;
	public float maxSteerAngle = 31;
	public float ActualSteerAngle;
	public float ActualRotation;
	public float motorForce = 1000; //HorsePower
	public float KPH; //Speed in Kilometers
	//Not Implemented
	[HideInInspector]public float engineRPM;
	public float SpeedForce = 0; //Top Speed
	public float DownForce = 0; //Car DownForce Power
    public float GripForce = 0; //Car Grip
	public float ControlForce = 0; //Car Handling or Drift Power
	public float MaxSpeed = 0; //Car Max Speed
	public float MaxGear = 0; //Car Gear Max
	public float driftFactor;
	public float wheelsRPM;
	public float brakPower = 0; //Car Gear Max
	private float radius = 6;
	private float lastValue;
	public float DownForceValue = 10f;
	//For Future Systems of Speed,Gear, Drifting,Damage
	public float CarLife = 1000; //Speed
	public float Nitrox = 3; //Speed
	public float Radius = 6; //Neccesary for Ackerman Steering
	public float RPM; //Speed
	public float Gear; //Speed
	public float ReverseGear; //Car Max Speed

}
