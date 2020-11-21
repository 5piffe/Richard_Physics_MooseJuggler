using UnityEngine;

public class Gravity : MonoBehaviour
{
	[Header("Gravity stuff")]
	[Tooltip("Cool facts:\n" +
		"Earth: 9.81 m/s²\n" +
		"Our moon: 1.63 m/s²\n" +
		"Mars: 3.71 m/s²\n" +
		"...")]
	[SerializeField] private float gravitationalForce = 5f;

	[Tooltip("If false, Unitys gravity settings are used")]
	[SerializeField] private bool useCustomGravitationalForce = true;
	[SerializeField] private bool useVerletIntegration = true;
	
	public Vector3 velocity;
	public float mass = 1f;
	[System.NonSerialized] public Vector3 gravity;

	private void Awake()
	{
		gravity = new Vector3(0f, useCustomGravitationalForce ? -gravitationalForce : Physics.gravity.y, 0f);
	}

	public void GravityCalculation(PhysicalObject body)
	{
		Vector3 force = mass * gravity;
		Vector3 acceleration = force / mass;

		if (!useVerletIntegration)
		{
			velocity += acceleration * Time.deltaTime;
			body.transform.position += velocity * Time.deltaTime;
		}
		else
		{
			body.transform.position += velocity * Time.deltaTime + acceleration * Time.deltaTime * Time.deltaTime * 0.5f;
			velocity += acceleration * Time.deltaTime * 0.5f;
		}
	}

	public void AddForce(PhysicalObject body, Vector3 force)
	{
		Vector3 acceleration = force / mass;
		body.velocity += acceleration * Time.deltaTime;
	}
}