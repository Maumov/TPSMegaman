using UnityEngine;
using UnityEngine.UI;
public class Shooting : MonoBehaviour {
	[Header ("Info")]
	public float damage;

	Ray ray;
	Vector3 direction;
	GameObject endPoint,target;
	RaycastHit hit;
	float mouseX,mouseY,fire2;
	public GameObject PositionOfHit;
	public GameObject[] bullets;
	bool fire1;
	//Charge Bullet
	[Header ("Charge")]
	public float chargingThreshold = 0.25f;
	public float chargingTime = 0f;
	public LayerMask mask;

	//UI
	[Header ("UI")]
	public Text uICharged;

	//UI Target
	[Header ("Target UI")]
	public GameObject targetUI;
	public Text targetName;
	public Text targetHP;

	// Use this for initialization
	void Start () {
		ray = new Ray ();
		endPoint = GameObject.FindGameObjectWithTag ("Shoot");
	}
	
	// Update is called once per frame
	void Update () {
		GetInputs ();
		Aim ();
		Shoot ();
		UpdateUI();
	}
	void GetInputs(){
		
		mouseX = Input.GetAxisRaw("Mouse X");
		mouseY = Input.GetAxisRaw("Mouse Y");
		chargingTime += Input.GetAxisRaw("Fire1") * Time.deltaTime;
		fire1 = Input.GetButtonUp("Fire1");
		fire2 = Input.GetAxisRaw("Fire2");

	}
	void Aim(){
		
		direction = endPoint.transform.position - transform.position ;
		direction.Normalize ();
		ray.origin = transform.position;
		ray.direction = direction;

		if (Physics.Raycast (ray.origin,ray.direction, out hit,100f,mask)) {
			PositionOfHit.SetActive (true);
			PositionOfHit.transform.position = hit.point;
			if(hit.collider.GetComponent<IStats>() != null){
				ShowTargetStatus(hit.collider.GetComponent<IStats>());
			}
		} else {
			ResetTargetStatus();
			PositionOfHit.transform.position = ray.origin + (ray.direction * 10f);
		}

	}

	void Shoot(){
		if(fire1){
			chargingTime = (int)(chargingTime / chargingThreshold);
			GameObject bullet = (GameObject)Instantiate (bullets[(int)(Mathf.Clamp(chargingTime,0,bullets.Length-1f))],transform.position + (direction * 2f),transform.rotation);
			bullet.GetComponent<Bullet>().StartTravel(direction);	
			chargingTime = 0f;
		}

	}
	void ShowTargetStatus(IStats targetStats){
		targetUI.SetActive(true);
		targetName.text = targetStats.Name;
		targetHP.text = targetStats.HealthPoints.ToString();
	}
	void ResetTargetStatus(){
		targetUI.SetActive(false);
		targetName.text = "";
		targetHP.text = "";
	}
	void UpdateUI(){
		uICharged.text = ((int)(Mathf.Clamp(chargingTime / chargingThreshold,0,bullets.Length-1f))).ToString();
	}
}
