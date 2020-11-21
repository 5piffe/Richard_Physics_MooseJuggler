using UnityEngine;

public class IceSkates : MonoBehaviour
{
	[SerializeField] private GameObject snowBallParticle;
    private PhysicalObject booster;
	private float tiltCompensation = 0.4f;
	private Controller controller;

	private float spawnTime = 0.03f;
	private float spawnTimer;
	private float particleThreshold = 5f;

	private void Start()
	{
		booster = GetComponentInParent<PhysicalObject>();
		controller = GetComponentInParent<Controller>();
		spawnTimer = spawnTime;
	}
	void Update()
    {
		WheelsStuff();
    }

	private void WheelsStuff()
	{
		transform.eulerAngles = new Vector3(controller.mouseRotation, 0, 0) * tiltCompensation;

		spawnTimer -= Time.deltaTime;
		if (spawnTimer <= 0f)
		{
			if (booster.velocity.z > particleThreshold)
				SnowParticles(-Mathf.Abs(booster.velocity.z) * 1.5f);

			else if (booster.velocity.z < -particleThreshold)
				SnowParticles(Mathf.Abs(booster.velocity.z) * 1.5f);

				spawnTimer = spawnTime;
		}
	}
	
	private void SnowParticles(float zDirection)
	{
		Instantiate(snowBallParticle, transform.position, Quaternion.identity);
		snowBallParticle.GetComponent<PhysicalObject>().velocity =
			new Vector3(0f, Random.Range(Mathf.Abs(booster.velocity.z * 0.5f), Mathf.Abs(booster.velocity.z * 1.5f)), zDirection);
	}
}