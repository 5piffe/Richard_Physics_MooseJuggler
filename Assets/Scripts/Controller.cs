using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(PhysicalObject))]
public class Controller : MonoBehaviour
{
	[SerializeField] private float directionalBoostPower = 5f;
	[SerializeField] private GameObject snowBall;
	private GameManager gm;
	private PhysicalObject body = null;
	private Plane horns = null;

	[System.NonSerialized] public float mouseRotation; 
	private float hornRotateSpeed = 5f;
	private float maxHornTilt = 12f;
	private float maxTilt = 45f;
	private float zoneLimitBounceBack = 23.5f;

	private void Awake()
	{
		body = GetComponent<PhysicalObject>();
		horns = GetComponentInChildren<Plane>();
		gm = FindObjectOfType<GameManager>();
		Cursor.lockState = CursorLockMode.Locked;
	}

	private void Update()
	{
		MooseControl();
	}

	private void MooseControl()
	{
		if (Input.GetAxisRaw("Horizontal") < 0f)
			body.AddForce(body, new Vector3(0f, 0f, directionalBoostPower));
		
		if (Input.GetAxisRaw("Horizontal") > 0f)
			body.AddForce(body, new Vector3(0f, 0f, -directionalBoostPower));

		BounceBack(snowBall.transform, snowBall.GetComponent<PhysicalObject>());
		BounceBack(transform, body);

		horns.transform.localRotation = Quaternion.Euler(maxHornTilt * Mathf.Sin(Time.time * hornRotateSpeed), 0f, 0f);

		mouseRotation += Input.GetAxis("Mouse X");
		mouseRotation = Mathf.Clamp(mouseRotation, -maxTilt, maxTilt);
		transform.eulerAngles = new Vector3(-mouseRotation, 0, 0);

		if (snowBall.transform.position.y < snowBall.GetComponent<PhysicalObject>().SphereRadius)
		{
			gm.currentScore = 0;
			SceneManager.LoadScene("01");
		}
	}

	private void BounceBack(Transform pos, PhysicalObject body)
	{
		if (pos.position.z > zoneLimitBounceBack || pos.position.z < -zoneLimitBounceBack)
			body.velocity.z = -body.velocity.z;
	}
}