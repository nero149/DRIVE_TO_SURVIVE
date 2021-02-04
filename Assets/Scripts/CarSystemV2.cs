using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarSystemV2 : MonoBehaviour
{
		public enum driveType{
    frontWheelDrive,
    rearWheelDrive,
    allWheelDrive
    }
    [SerializeField]public driveType CarType;
	
	public enum SpeedType{
    MPH,
    KPH
    }
    [SerializeField]public SpeedType SpeedType2;
	
	public enum GearType{
    Manual,
    Automatic
    }
    [SerializeField]public GearType GearType2;
	
	//Non funcional Yet
	public enum WheelType{
	SoftSlickers,
	Slickers,
	HardSlickers,
	OffRoad,
	Snow,
	Spike,
	Hydro,
	Street
    }
    [SerializeField]public WheelType WheelType2;
	
	//Non funcional yet
	public enum SkillType{
    TEKHyperbass,
	TEKJetTruster,
	DRIFTTFourxFourchanger,
    MMHook,
	MMMetalWheel,
	MMMetalBody,
	MMMetalBlock,
	HOLLOBFlameHood,
	FLATHFlameExaust,
	SILGrip,
	SILEMP,
	SILdiguise,
	DRZBallattack,
	DRZGroundTrap,
	DRZWDChanger,
	SelfDrestruction
    }
    [SerializeField]public SkillType SkillType1;
	
	//Non funcional yet
	public enum SkillType2{
    TEKHyperbass,
	TEKJetTruster,
	DRIFTTFourxFourchanger,
    MMHook,
	MMMetalWheel,
	MMMetalBody,
	MMMetalBlock,
	HOLLOBFlameHood,
	FLATHFlameExaust,
	SILGrip,
	SILEMP,
	SILdiguise,
	DRZBallattack,
	DRZGroundTrap,
	DRZWDChanger,
	SelfDrestruction
    }
    [SerializeField]public SkillType2 SkillTypeB;
	
	
	public SpeedmeterManager speedmeterManager;
	[HideInInspector] public float m_horizontalInput;
	[HideInInspector] public float m_verticalInput;
	[HideInInspector] public float m_AccelerationInput;
	[HideInInspector] public float m_BrakingInput;
	[HideInInspector] public float m_steeringAngle;
	[HideInInspector] public float  m_HandBrakingInput;
	[HideInInspector] public bool  m_NitrousInput;
	[HideInInspector] public bool  m_GearDownInput;
	[HideInInspector] public bool  m_GearUpInput;
	[HideInInspector] public bool  m_Gear1;
	[HideInInspector] public bool  m_Gear2;
	[HideInInspector] public bool  m_Gear3;
	[HideInInspector] public bool  m_Gear4;
	[HideInInspector] public bool  m_Gear5;
	[HideInInspector] public bool  m_Gear6;
	[HideInInspector] public bool  m_Gear7;
	[HideInInspector] public bool  m_Gear8;
	[HideInInspector] public bool  m_Gear9;
	[HideInInspector] public bool  m_GearR;
	private WheelCollider[] wheels = new WheelCollider[4];
	private GameObject[] WheelMesh = new GameObject[4];
	private Transform[] WheelT = new Transform[4];
	private GameObject FrontWheelLeftModel;
	private GameObject FrontWheelRightModel;
	private GameObject BackWheelLeftModel;	
	private GameObject BackWheelRightModel;
    private GameObject centerofMass;
	private Rigidbody CarBody;
	private GameObject Main;
	private GameObject Base;
	private GameObject BASEMODEL;
	public int BackLanternNumber;
	public bool drifting = false;
	public float Momentum = 0;
	[Range(-90,90)]
	public float MomentumAngle = 0;
	public float MomentumPressure = 0;
	[Range(0f,50f)]
	public float BaseTresHold = 0;
	public float MomentumTresHold = 0;
	public float ActiveTresHold2 = 0;
	public float PosActiveTresHold = 0;
	public float NegActiveTresHold = 0;
	[Range(0f,2f)]
	public float GripBonus = 0f;
	[Range(0f,2f)]
	public float SideExtremumSlip = 0f;
	[Range(0f,2f)]
	public float SideAsySlip = 0f;
	[Range(0f,2f)]
	public float FrontExtremumSlip = 0f;
	[Range(0f,2f)]
	public float FrontAsySlip = 0f;
	[Range(0f,1f)]
	public float SuspensionHeight = 0f;
	public float RearBaseStiffness = 2;
	public float ActualSFrictionManipulation = 0;
	public float ActualFFrictionManipulation = 0;
	public bool BacklightsFlag = false;
	public Renderer Backren;
	public Material[] Backmat;
	public AnimationCurve enginePower;
	public float brakPower = 0;
	public float radius = 6;
    [HideInInspector]public float SPEED;
	public float VisualSPEED;
	public float VisualMPH;
	public float VisualKPH;
	public float totalPower;
    public float engineRPM;
	[HideInInspector]public int gearNum;
	[HideInInspector]public int PrevManualgear;
	[HideInInspector]public int NextManualgear;
	public float ManualgearSpeedLimit;
	public bool GearChanging = false;
	public bool reverse = false;
	public bool mreverse = false;
	public bool braking = false;
	public bool hbraking = false;
	[HideInInspector]private WheelHit wheelHit;
	[HideInInspector]private WheelHit wheelHit1;
	[HideInInspector]private WheelHit wheelHit2;
	[HideInInspector]private WheelHit wheelHit3;
	[HideInInspector]public float[] Slip = new float[4];
	[HideInInspector]public float[] SideSlip = new float[4];
	[HideInInspector]public float[] wheelSlip = new float[4];
	public float wheelsRPM;
	[HideInInspector]public float lastValue;
	[HideInInspector]public bool flag=false;
	[Range(1f,2f)]
	public float handBrakeFriction = 1f;
	private float handBrakeFrictionMultiplier = 3f;
	private float driftFactor;
	public WheelFrictionCurve  forwardFriction,sidewaysFriction;
	[Range(0f,2f)]
	public float BaseFriction;
    public float maxRPM , minRPM;
    public float[] gears;
    public float[] gearChangeSpeed;
	public float Reversegear;
	public float ReversegearSpeed;
	[HideInInspector]public bool test; //engine sound boolean
	private float smoothTime = 0.09f;
	
	[Range(8f,20f)]
	public float DownForceValue = 10f;
	
	[Range(60000,100000)]
	public float spring = 75000;
    // Start is called before the first frame update
    private void Awake() 
	{
        getObjects();
		StartCoroutine(timedLoop());
    }
	


 public void GetInput()
	{
		m_horizontalInput = Input.GetAxis("Horizontal");
		m_verticalInput = Input.GetAxis("Vertical");
		m_AccelerationInput = Input.GetAxis("Accelerate");
		m_BrakingInput = Input.GetAxis("Braking");
		m_HandBrakingInput = Input.GetAxis("Handbrake");
		m_GearUpInput = (Input.GetAxis ("GearUp") != 0) ? true : false;
		m_GearDownInput = (Input.GetAxis ("GearDown") != 0) ? true : false;
		m_NitrousInput = (Input.GetAxis ("Nitrous") != 0) ? true : false;
		m_Gear1= (Input.GetAxis("Gear1")!= 0) ? true : false;
		m_Gear2= (Input.GetAxis("Gear2")!= 0) ? true : false;
		m_Gear3= (Input.GetAxis("Gear3")!= 0) ? true : false;
		m_Gear4= (Input.GetAxis("Gear4")!= 0) ? true : false;
		m_Gear5= (Input.GetAxis("Gear5")!= 0) ? true : false;
		m_Gear6= (Input.GetAxis("Gear6")!= 0) ? true : false;
		m_Gear7= (Input.GetAxis("Gear7")!= 0) ? true : false;
		m_Gear8= (Input.GetAxis("Gear8")!= 0) ? true : false;
		m_Gear9= (Input.GetAxis("Gear9")!= 0) ? true : false;
		m_GearR= (Input.GetAxis("GearR")!= 0) ? true : false;
	}

    // Update is called once per frame
    private void FixedUpdate()
    {
        GetInput();
		Steer();
		Accelerate();
		Braking();
		addDownForce();
		UpdateWheelPoses();
	    GetFriction();
		BackactivateLights();
		lastValue = engineRPM;
		calculateEnginePower();
		adjustTraction();
		shifter();
		DriftCar();
		Momentummanager();
    }
	
	//

private void calculateEnginePower(){

        wheelRPM();
			if(!reverse){
            if (m_AccelerationInput != 0){
                GetComponent<Rigidbody>().drag = 0.025f; 
            }
            if (m_AccelerationInput == 0){
                GetComponent<Rigidbody>().drag = 0.01f;
            }
			
			
			if(checkGears()){		
			totalPower = 1.6f * enginePower.Evaluate(engineRPM) /6;
			}else{
			totalPower = 6.6f * enginePower.Evaluate(engineRPM) * (m_AccelerationInput);	
			}
	
	    float velocity  = 0.0f;
        if (engineRPM >= maxRPM || flag ){
            engineRPM = Mathf.SmoothDamp(engineRPM, maxRPM - 500, ref velocity, 0.05f);

            flag = (engineRPM >= maxRPM)?  true : false;
            test = (lastValue > engineRPM) ? true : false;
        }
        else { 
            engineRPM = Mathf.SmoothDamp(engineRPM,(Mathf.Abs(wheelsRPM) * 3.6f * (gears[gearNum])), ref velocity , smoothTime);
            test = false;
        }
        if (engineRPM >= maxRPM + 1000) engineRPM = maxRPM + 1000; // clamp at max
			}else{
				
			if (m_BrakingInput != 0){
                GetComponent<Rigidbody>().drag = 0.025f; 
            }
            if (m_BrakingInput == 0){
                GetComponent<Rigidbody>().drag = 0.01f;
            }
			
			
			if(checkGears()){		
			totalPower = -1.6f * enginePower.Evaluate(engineRPM) /3;
			}else{
			totalPower = -3.6f * enginePower.Evaluate(engineRPM) * (m_BrakingInput);	
			}
	
	    float velocity  = 0.0f;
        if (engineRPM >= maxRPM || flag ){
            engineRPM = Mathf.SmoothDamp(engineRPM, maxRPM - 500, ref velocity, 0.05f);

            flag = (engineRPM >= maxRPM)?  true : false;
            test = (lastValue > engineRPM) ? true : false;
        }
        else { 
			engineRPM = Mathf.SmoothDamp(engineRPM,(Mathf.Abs(wheelsRPM) * 3.6f * (Reversegear)), ref velocity , smoothTime);
			test = false;
        }
        if (engineRPM >= maxRPM + 1000) engineRPM = maxRPM + 1000; // clamp at max
				
				
				
				
			}
        Accelerate();
		shifter();
    }
	
	  private void wheelRPM(){
        float sum = 0;
        int R = 0;
        for (int i = 0; i < 4; i++)
        {
            sum += wheels[i].rpm;
            R++;
        }
		wheelsRPM = (R != 0) ? sum / R : 0;		
		
	if (GearType2 == GearType.Automatic){

        if(m_AccelerationInput > 0 && reverse ){
            reverse = false;
        }

        if(m_BrakingInput > 0 && wheelsRPM < 0 && !reverse && gearNum == 0){
            reverse = true;
        }
        else if(m_BrakingInput == 0 && wheelsRPM > 0 && reverse && gearNum == 0){
            reverse = false;
        }
		
		
		
		
		
	}else{
		
		if (reverse){
		if(wheelsRPM < 0 && reverse){
        reverse = true;
			}
		}else{
		if(wheelsRPM < 0 && !reverse ){
         reverse = false;
		}
		}
	}
			
		

 
    }

	    
	private bool checkGears(){
		if(!reverse){
        if(SPEED >= gearChangeSpeed[gearNum] ) return true;
        else return false;
		}else{
		if(SPEED >= ReversegearSpeed) return true;
        else return false;	
		}
    }
	
   private void shifter(){

	if(!GearChanging){

		if (GearType2 == GearType.Automatic){
     if(!isGrounded())return;
           //automatic
       if(engineRPM > maxRPM && gearNum < gears.Length-1 && !reverse && checkGears() ){
            gearNum++;
			speedmeterManager.changeGear();
        }
        if(engineRPM < minRPM && gearNum > 0 && !reverse){
            gearNum --;
			speedmeterManager.changeGear();
        }

		
        }else{
			
     if(!isGrounded())return;
        if(m_GearUpInput){

		if(gearNum < gears.Length-1 && !reverse){
			StartCoroutine(ChangeGearsManualUp());
			speedmeterManager.changeGear();
				}
		if(gearNum == 0 && reverse){
		StartCoroutine(ExitReverse());
		speedmeterManager.changeGear();
				}
			}
		
        if(m_GearDownInput){
			
		if(gearNum > 0){
		StartCoroutine(ChangeGearsManualDown());
		speedmeterManager.changeGear();
				}
		if(gearNum == 0 && !reverse){
			
		StartCoroutine(EnterReverse());
		speedmeterManager.changeGear();
				}
			}
		if(gearNum == 0){
		PrevManualgear = 0;
		NextManualgear = 1;
		ManualgearSpeedLimit = gearChangeSpeed[0];}
		
		if(gearNum == 1){
		PrevManualgear = 0;	
		NextManualgear = 2;}
		ManualgearSpeedLimit = gearChangeSpeed[1];}
		
		if(gearNum == 2){
		PrevManualgear = 1;
		NextManualgear = 3;
		ManualgearSpeedLimit = gearChangeSpeed[2];}

		if(gearNum == 3){
		PrevManualgear = 2;	
		NextManualgear = 4;
		ManualgearSpeedLimit = gearChangeSpeed[3];}
		
		if(gearNum == 4){
		PrevManualgear = 3;
		NextManualgear = 5;
		ManualgearSpeedLimit = gearChangeSpeed[4];}
		
		if(gearNum == 5){
		PrevManualgear = 4;
		NextManualgear = 6;}
		ManualgearSpeedLimit = gearChangeSpeed[5];}
		
		if(5 < gears.Length){
		if(gearNum == 6){
		PrevManualgear = 5;
		NextManualgear = 7;
		ManualgearSpeedLimit = gearChangeSpeed[6];
			}
		}
		
		if(6 < gears.Length){
		if(gearNum == 7){
		PrevManualgear = 6;
		NextManualgear = 8;
		ManualgearSpeedLimit = gearChangeSpeed[7];}
		}
		
		if(7 < gears.Length){
		if(gearNum == 8){
		PrevManualgear = 7;
		NextManualgear = 9;
		ManualgearSpeedLimit = gearChangeSpeed[8];}
		}	
		
		if(8 < gears.Length){
		if(gearNum == 9){
		PrevManualgear = 8;
		ManualgearSpeedLimit = gearChangeSpeed[9];}
		}	
		
		if(reverse){
		gearNum = 0;
		PrevManualgear = 0;
		NextManualgear = 0;
				}	
				
		if(m_Gear1){
		StartCoroutine(EnterGear1());
		speedmeterManager.changeGear();
		}
		if(m_Gear2){
		StartCoroutine(EnterGear2());
		speedmeterManager.changeGear();
		}
		
		if(m_Gear3){
		StartCoroutine(EnterGear3());
		speedmeterManager.changeGear();
		}
			
		if(m_Gear4){
		StartCoroutine(EnterGear4());
		speedmeterManager.changeGear();
		}
			
		if(m_Gear5){
		StartCoroutine(EnterGear5());
		speedmeterManager.changeGear();
		}
			
		if(5 < gears.Length){
		if(m_Gear6){
		StartCoroutine(EnterGear6());
		speedmeterManager.changeGear();}
		}
			
		if(6 < gears.Length){
		if(m_Gear7){
		StartCoroutine(EnterGear7());
		speedmeterManager.changeGear();}
		}

		if(7 < gears.Length){
		if(m_Gear8){
		StartCoroutine(EnterGear8());
		speedmeterManager.changeGear();}
		}
		
		if(8 < gears.Length){
		if(m_Gear9){
		StartCoroutine(EnterGear9());
		speedmeterManager.changeGear();}
		}
		
		if(m_GearR){
		StartCoroutine(EnterReverse());
		speedmeterManager.changeGear();
		}
}

			
	public IEnumerator ChangeGearsManualUp(){
		GearChanging = true;
		yield return new WaitForSeconds(0.1f);
		gearNum = NextManualgear;
		GearChanging = false;
	}
	
	public IEnumerator ChangeGearsManualDown(){
	GearChanging = true;
	yield return new WaitForSeconds(0.1f);
	gearNum = PrevManualgear;
	yield return new WaitForSeconds(0.1f);
	GearChanging = false;
	}
	
	public IEnumerator EnterGear1(){
	GearChanging = true;
	yield return new WaitForSeconds(0.1f);
	gearNum = 0;
	reverse = false;
	PrevManualgear = 0;
	NextManualgear = 1;
	yield return new WaitForSeconds(0.1f);
	GearChanging = false;
	}
	
	public IEnumerator EnterGear2(){
	GearChanging = true;
	yield return new WaitForSeconds(0.1f);
	gearNum = 1;
	reverse = false;
	PrevManualgear = 0;
	NextManualgear = 2;
	yield return new WaitForSeconds(0.1f);
	GearChanging = false;
	}
	
	public IEnumerator EnterGear3(){
	GearChanging = true;
	yield return new WaitForSeconds(0.1f);
	gearNum = 2;
	reverse = false;
	PrevManualgear = 1;
	NextManualgear = 3;
	yield return new WaitForSeconds(0.1f);
	GearChanging = false;
	}
		
	public IEnumerator EnterGear4(){
	GearChanging = true;
	yield return new WaitForSeconds(0.1f);
	gearNum = 3;
	reverse = false;
	PrevManualgear = 2;
	NextManualgear = 4;
	yield return new WaitForSeconds(0.1f);
	GearChanging = false;
	}
	
	public IEnumerator EnterGear5(){
	GearChanging = true;
	yield return new WaitForSeconds(0.1f);
	gearNum = 4;
	reverse = false;
	PrevManualgear = 3;
	NextManualgear = 5;
	yield return new WaitForSeconds(0.1f);
	GearChanging = false;
	}
	
	public IEnumerator EnterGear6(){
	GearChanging = true;
	yield return new WaitForSeconds(0.1f);
	gearNum = 5;
	reverse = false;
	PrevManualgear = 4;
	NextManualgear = 6;
	yield return new WaitForSeconds(0.1f);
	GearChanging = false;
	}
	
	public IEnumerator EnterGear7(){
	GearChanging = true;
	yield return new WaitForSeconds(0.1f);
	gearNum = 6;
	reverse = false;
	PrevManualgear = 5;
	NextManualgear = 7;
	yield return new WaitForSeconds(0.1f);
	GearChanging = false;
	
	}	public IEnumerator EnterGear8(){
	GearChanging = true;
	yield return new WaitForSeconds(0.1f);
	gearNum = 7;
	reverse = false;
	PrevManualgear = 6;
	NextManualgear = 8;
	yield return new WaitForSeconds(0.1f);
	GearChanging = false;
	
	}	public IEnumerator EnterGear9(){
	GearChanging = true;
	yield return new WaitForSeconds(0.1f);
	gearNum = 8;
	reverse = false;
	PrevManualgear = 7;
	NextManualgear = 9;
	yield return new WaitForSeconds(0.1f);
	GearChanging = false;
	}
	
	public IEnumerator EnterReverse(){
	GearChanging = true;
	yield return new WaitForSeconds(0.1f);
	gearNum = 0;
	reverse = true;
	yield return new WaitForSeconds(0.1f);
	GearChanging = false;
	}
	public IEnumerator ExitReverse(){
	GearChanging = true;
	yield return new WaitForSeconds(0.1f);
	gearNum = 0;
	reverse = false;
	yield return new WaitForSeconds(0.1f);
	GearChanging = false;
	}
	
	    private bool isGrounded(){
        if(wheels[0].isGrounded &&wheels[1].isGrounded &&wheels[2].isGrounded &&wheels[3].isGrounded )
            return true;
        else
            return false;
    }
	
	
private void Steer()
	{
		if (m_horizontalInput > 0 ) {
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * m_horizontalInput;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * m_horizontalInput;
			MomentumAngle = wheels[1].steerAngle ;
        } else if (m_horizontalInput < 0 ) {                                                          
            wheels[0].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius - (1.5f / 2))) * m_horizontalInput;
            wheels[1].steerAngle = Mathf.Rad2Deg * Mathf.Atan(2.55f / (radius + (1.5f / 2))) * m_horizontalInput;
			MomentumAngle = wheels[0].steerAngle ;

        } else {
            wheels[0].steerAngle =0;
            wheels[1].steerAngle =0;
			MomentumAngle = 0 ;
        }

    }
	
	

	
		private void Braking() 
	{
		if (!reverse){
        if (m_BrakingInput > 0){
            brakPower =(SPEED >= 0)? 1000 : 0;
			braking = true;
        }
        else 
			if (m_BrakingInput== 0 &&(SPEED <= 0 || SPEED >= -1)){
            brakPower = 0;
			braking = false;
        }
		}else{
		    if (m_AccelerationInput > 0){
            brakPower =(SPEED >= 0)? 1000 : 0;
			braking = true;
        }
        else 
			if (m_AccelerationInput== 0 &&(SPEED <= 0 || SPEED >= -1)){
            brakPower = 0;
			braking = false;
        }	
			
		}
 
	}
	
	   private void getObjects(){
       CarBody = GetComponent<Rigidbody>();
	   centerofMass = GameObject.Find ("CenterOfMass");
	   CarBody.centerOfMass = centerofMass.transform.localPosition;   
	   

	   FrontWheelLeftModel = gameObject.transform.Find("FrontWheelLeft").gameObject;
	   FrontWheelRightModel = gameObject.transform.Find("FrontWheelRight").gameObject;
	   BackWheelLeftModel = gameObject.transform.Find("BackWheelLeft").gameObject;
	   BackWheelRightModel = gameObject.transform.Find("BackWheelRight").gameObject;
	   Main = gameObject.transform.Find("MainBody").gameObject;
	   Base = Main.transform.Find("Base(noMods)").gameObject;
	   BASEMODEL = Base.transform.Find("BASE").gameObject;
	   
	    wheels[0] = gameObject.transform.Find("FrontWheelLeft").gameObject.GetComponent<WheelCollider>();
        wheels[1] = gameObject.transform.Find("FrontWheelRight").gameObject.GetComponent<WheelCollider>();
        wheels[2] = gameObject.transform.Find("BackWheelLeft").gameObject.GetComponent<WheelCollider>();
        wheels[3] = gameObject.transform.Find("BackWheelRight").gameObject.GetComponent<WheelCollider>();
	   
	    WheelMesh[0] = FrontWheelLeftModel.transform.Find("Tire").gameObject;
        WheelMesh[1] = FrontWheelRightModel.transform.Find("Tire").gameObject;
        WheelMesh[2] = BackWheelLeftModel.transform.Find("Tire").gameObject;
        WheelMesh[3] = BackWheelRightModel.transform.Find("Tire").gameObject;
	   
		WheelT[0] = FrontWheelLeftModel.gameObject.transform.GetChild(0);
		WheelT[1] = FrontWheelRightModel.gameObject.transform.GetChild(0);
		WheelT[2] = BackWheelLeftModel.gameObject.transform.GetChild(0);
		WheelT[3] = BackWheelRightModel.gameObject.transform.GetChild(0);

    }
	
		private void GetFriction() 
	{
			wheels[0].GetGroundHit(out wheelHit);
			wheels[1].GetGroundHit(out wheelHit1);
			wheels[2].GetGroundHit(out wheelHit2);
			wheels[3].GetGroundHit(out wheelHit3);
			Slip[0] = wheelHit.forwardSlip;
			Slip[1] = wheelHit1.forwardSlip;
			Slip[2] = wheelHit2.forwardSlip;
			Slip[3] = wheelHit3.forwardSlip;
			SideSlip[0] = wheelHit.sidewaysSlip;
			SideSlip[1] = wheelHit1.sidewaysSlip;
			SideSlip[2] = wheelHit2.sidewaysSlip;
			SideSlip[3] = wheelHit3.sidewaysSlip;
	}		
	
			
    private void addDownForce(){

        CarBody.AddForce(-transform.up * DownForceValue * CarBody.velocity.magnitude );

    }		
	
		private void UpdateWheelPoses()
	{
		UpdateWheelPose(wheels[0], WheelT[0]);
		UpdateWheelPose(wheels[1], WheelT[1]);
		UpdateWheelPose(wheels[2], WheelT[2]);
		UpdateWheelPose(wheels[3], WheelT[3]);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}

	private void adjustTraction(){
            //tine it takes to go from normal drive to drift 
        float driftSmothFactor = .2f * Time.deltaTime;

		if(m_HandBrakingInput > 0){
            sidewaysFriction = wheels[0].sidewaysFriction;
            forwardFriction = wheels[0].forwardFriction;

            float velocity = 0;
            sidewaysFriction.extremumValue =sidewaysFriction.asymptoteValue = forwardFriction.extremumValue = forwardFriction.asymptoteValue =
                Mathf.SmoothDamp(forwardFriction.asymptoteValue,driftFactor * handBrakeFrictionMultiplier,ref velocity ,driftSmothFactor );

            for (int i = 0; i < 4; i++) {
                wheels [i].sidewaysFriction = sidewaysFriction;
                wheels [i].forwardFriction = forwardFriction;
            }

            sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = forwardFriction.extremumValue = forwardFriction.asymptoteValue =  BaseFriction + handBrakeFriction;
                //extra grip for the front wheels
            for (int i = 0; i < 2; i++) {
                wheels [i].sidewaysFriction = sidewaysFriction;
                wheels [i].forwardFriction = forwardFriction;
            }
			hbraking = true;
			MomentumPressure = 2;
			
		}
            //executed when handbrake is being held
        else{
			hbraking = false;
			MomentumPressure = 0;
			forwardFriction = wheels[0].forwardFriction;
			sidewaysFriction = wheels[0].sidewaysFriction;

			forwardFriction.extremumValue = forwardFriction.asymptoteValue = sidewaysFriction.extremumValue = sidewaysFriction.asymptoteValue = 
                ((BaseFriction + SPEED * handBrakeFrictionMultiplier) / 800) + 1;

			for (int i = 0; i < 4; i++) {
				wheels [i].forwardFriction = forwardFriction;
				wheels [i].sidewaysFriction = sidewaysFriction;

			}
        }

            //checks the amount of slip to control the drift
		for(int i = 2;i<4 ;i++){

            WheelHit wheelHit;

            wheels[i].GetGroundHit(out wheelHit);
                //smoke
            if(wheelHit.sidewaysSlip >= 0.2f || wheelHit.sidewaysSlip <= -0.2f ||wheelHit.forwardSlip >= .2f || wheelHit.forwardSlip <= -0.2f)
                //playPauseSmoke = true;
            //else
                //playPauseSmoke = false;
                        

			if(wheelHit.sidewaysSlip < 0 )	driftFactor = (1 + -m_horizontalInput) * Mathf.Abs(wheelHit.sidewaysSlip) ;

			if(wheelHit.sidewaysSlip > 0 )	driftFactor = (1 + m_horizontalInput )* Mathf.Abs(wheelHit.sidewaysSlip );
		}	
		
	}

private void Accelerate() 
	{					
		if (CarType == driveType.allWheelDrive){
		for (int i = 0; i < wheels.Length; i++){
			
		if(hbraking){
		wheels[i].motorTorque = totalPower /5;
		wheels[i].brakeTorque = 400;
		//wheels[3].brakeTorque = 1000;
		}else{
		wheels[i].motorTorque = totalPower /4;
		wheels[i].brakeTorque = brakPower;
		}
		
		}
        }else if(CarType == driveType.rearWheelDrive){
		for (int i = 0; i < wheels.Length; i++){
			
		if(hbraking){
		wheels[2].motorTorque = totalPower /5; // /2
        wheels[3].motorTorque = totalPower /5;
		wheels[i].brakeTorque = 400;
		//wheels[3].brakeTorque = 1000;
		}else{	
		wheels[2].motorTorque = totalPower /2; // /2
        wheels[3].motorTorque = totalPower /2;
		wheels[i].brakeTorque = brakPower;
		}
		
		}
        }else{
		for (int i = 0; i < wheels.Length; i++){
				
		if(hbraking){
		wheels[0].motorTorque = totalPower /5; // /2
        wheels[1].motorTorque = totalPower /5;
		wheels[i].brakeTorque = 400;
		//wheels[3].brakeTorque = 1000;
		}else{	
		wheels[0].motorTorque = totalPower /2; // /2
        wheels[1].motorTorque = totalPower /2;
		wheels[i].brakeTorque = brakPower;
		
		}
		}
		}
			
		VisualKPH = CarBody.velocity.magnitude * 3.6f;
		VisualMPH = CarBody.velocity.magnitude * 2.2f;	
		SPEED = CarBody.velocity.magnitude * 3.6f;
			
			
		if (SpeedType2 == SpeedType.KPH){
		VisualSPEED = VisualKPH;
		
        }else{
		VisualSPEED = VisualMPH;
		}				 

	}



	private IEnumerator timedLoop(){
		while(true){
			yield return new WaitForSeconds(.7f);
            radius = 4 + SPEED / 20;
            
		}
	}

	private void BackactivateLights() {
        if (braking) BackturnLightsOn();
        else BackturnLightsOff();
    }

    private void BackturnLightsOn(){
        if (BacklightsFlag) return;
			Backren = BASEMODEL.GetComponent<Renderer>();
			Backmat = Backren.materials;
			Backmat[BackLanternNumber].SetColor("_EmissionColor", Color.red *5);
        BacklightsFlag = true;
        //lights.SetActive(true);
    }    
    
    private void BackturnLightsOff(){
        if (!BacklightsFlag) return;
			Backren = BASEMODEL.GetComponent<Renderer>();
			Backmat = Backren.materials;
			Backmat[BackLanternNumber].SetColor("_EmissionColor", Color.black);
        BacklightsFlag = false;
        //lights.SetActive(false);
    }
	
	private void DriftCar() {
	
			wheels[0].GetGroundHit(out wheelHit);
			wheels[1].GetGroundHit(out wheelHit1);
			wheels[2].GetGroundHit(out wheelHit2);
			wheels[3].GetGroundHit(out wheelHit3);
			
			

		for (int i = 0; i < wheels.Length; i++){
			radius = 4 + (-Mathf.Abs(wheelHit.sidewaysSlip) * 2) + GetComponent<Rigidbody>().velocity.magnitude / 10;
			//WHEEL 1
			if(wheels[0].GetGroundHit(out wheelHit)){
			wheelSlip[0] = Mathf.Abs(wheelHit.forwardSlip) + Mathf.Abs(wheelHit.sidewaysSlip);
			
			//WHEEL 2
			if(wheels[1].GetGroundHit(out wheelHit1)){
			wheelSlip[1] = Mathf.Abs(wheelHit1.forwardSlip) + Mathf.Abs(wheelHit1.sidewaysSlip);
			
			//WHEEL 3
			if(wheels[2].GetGroundHit(out wheelHit2)){
			wheelSlip[2] = Mathf.Abs(wheelHit2.forwardSlip) + Mathf.Abs(wheelHit2.sidewaysSlip);

			
			//WHEEL 4
			if(wheels[3].GetGroundHit(out wheelHit3)){
			wheelSlip[3] = Mathf.Abs(wheelHit3.forwardSlip) + Mathf.Abs(wheelHit.sidewaysSlip);
			
			forwardFriction = wheels[0].forwardFriction;
			forwardFriction.stiffness = (drifting)? Mathf.SmoothDamp(forwardFriction.stiffness, .3f , ref SPEED , Time.deltaTime * 2) :1 ;
			wheels [0].forwardFriction = forwardFriction;		
			
			sidewaysFriction = wheels[0].sidewaysFriction;
			sidewaysFriction.stiffness = (drifting)? Mathf.SmoothDamp(sidewaysFriction.stiffness, .3f , ref SPEED , Time.deltaTime * 2) :1 ;
			wheels [0].sidewaysFriction = sidewaysFriction;
			
			forwardFriction = wheels[1].forwardFriction;
			forwardFriction.stiffness = (drifting)? Mathf.SmoothDamp(forwardFriction.stiffness, .3f , ref SPEED , Time.deltaTime * 2) :1 ;
			wheels [1].forwardFriction = forwardFriction;		
			
			sidewaysFriction = wheels[1].sidewaysFriction;
			sidewaysFriction.stiffness = (drifting)? Mathf.SmoothDamp(sidewaysFriction.stiffness, .3f , ref SPEED , Time.deltaTime * 2) :1 ;
			wheels [1].sidewaysFriction = sidewaysFriction;
			
			forwardFriction = wheels[2].forwardFriction;
			forwardFriction.stiffness = (drifting)? Mathf.SmoothDamp(forwardFriction.stiffness, .3f , ref SPEED , Time.deltaTime * 2) :1 ;
			wheels [2].forwardFriction = forwardFriction;		
			
			sidewaysFriction = wheels[2].sidewaysFriction;
			sidewaysFriction.stiffness = (drifting)? Mathf.SmoothDamp(sidewaysFriction.stiffness, .3f , ref SPEED , Time.deltaTime * 2) :2;
			wheels [2].sidewaysFriction = sidewaysFriction;
			
			forwardFriction = wheels[3].forwardFriction;
			forwardFriction.stiffness = (drifting)? Mathf.SmoothDamp(forwardFriction.stiffness, .3f , ref SPEED , Time.deltaTime * 2) :1 ;
			wheels [3].forwardFriction = forwardFriction;		
			
			sidewaysFriction = wheels[3].sidewaysFriction;
			sidewaysFriction.stiffness = (drifting)? Mathf.SmoothDamp(sidewaysFriction.stiffness, .3f , ref SPEED , Time.deltaTime * 2) :2;
			wheels [3].sidewaysFriction = sidewaysFriction;
			
			}
			else 
			wheels [i].forwardFriction = forwardFriction;		
			wheels [i].sidewaysFriction = sidewaysFriction;
			
		}
		SPEED = CarBody.velocity.magnitude * 3.6f;
	}
	
	
}
}
}

private void Momentummanager()
{
	
	
	float rotationbonus;
	NegActiveTresHold = (BaseTresHold * -1);
	PosActiveTresHold = BaseTresHold;
	sidewaysFriction = wheels[0].sidewaysFriction;
	rotationbonus = wheelsRPM / 20;
	Momentum = MomentumAngle + (sidewaysFriction.extremumValue - 1) * MomentumPressure;
	
	
	if(m_horizontalInput < 0){
	ActiveTresHold2 = NegActiveTresHold;
	MomentumPressure = -1;
	if (Momentum > ActiveTresHold2){
	drifting = false;
	}
	
	if (Momentum < ActiveTresHold2){
	drifting = true;

	}
	
	
	}
	if(m_horizontalInput > 0){
		ActiveTresHold2 = PosActiveTresHold;
		MomentumPressure = 1;

	if (Momentum < ActiveTresHold2){
	drifting = false;
	}
	
	if (Momentum > ActiveTresHold2){
	drifting = true;
	}
	
	}
	
	
}

}