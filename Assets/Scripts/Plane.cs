using UnityEngine;

public class Plane : MonoBehaviour
{
	public Vector3 PlaneNormal => transform.up;
	private PhysicalObject[] physicalObjects;
	[Tooltip("Lower will reserve more energy in the reflected object, " +
		"making a higher value seem like a damper surface, absorbing the energy.")]
	[SerializeField, Range(0f, 1f)] private float energyAbsorb = 0.5f;
	private GameManager gm;

	private void Start()
	{
		physicalObjects = FindObjectsOfType<PhysicalObject>();
		gm = FindObjectOfType<GameManager>();
	}

	private void Update()
	{
		CheckForCollidingObjects();
	}

	public float Distance(PhysicalObject body)
	{
		Vector3 bodyToPlane = transform.position - body.transform.position;
		return Vector3.Dot(bodyToPlane, PlaneNormal);
	}

	public bool Collision(PhysicalObject body)
	{
		if (Mathf.Abs(Distance(body)) <= body.SphereRadius)
		{
			gm.currentScore++;
			return true;
		}
		else
		{
			return false;
		}
	}

	public void OnImpact(PhysicalObject body, float energyDissipation)
	{
		if (!Collision(body))
			return;

		if (body.velocity.magnitude > 0f)
			body.transform.position = CorrectedPosition(body);

		body.velocity = Reflect(body.velocity, energyDissipation);
	}


	public Vector3 CorrectedPosition(PhysicalObject body)
	{
		return Projection(body) + PlaneNormal * body.SphereRadius;
	}

	public Vector3 Projection(PhysicalObject body)
	{
		Vector3 bodyToProjection = Distance(body) * PlaneNormal;
		return body.transform.position + bodyToProjection;
	}

	private Vector3 Reflect(Vector3 v, float energyAbsorb = 0f)
	{
		Vector3 r = (v - 2f * Vector3.Dot(v, PlaneNormal) * PlaneNormal) * (1f - energyAbsorb);
		return r;
	}

	private void CheckForCollidingObjects()
	{
		foreach (PhysicalObject body in physicalObjects)
		{
			if (body.onPlane == this)
				OnImpact(body, energyAbsorb);
		}
	}
}