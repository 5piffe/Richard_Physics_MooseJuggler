using UnityEngine;

public class PhysicalObject : Gravity
{
	public float SphereRadius => transform.localScale.x * 0.5f;
	[System.NonSerialized] public bool onPlane = false;

	private void Update()
	{
		GravityCalculation(this);
	}
	
	private void OnTriggerEnter(Collider other)
	{
		PlaneEnter(other);
	}

	private void PlaneEnter(Collider other)
	{
		Plane plane = other.GetComponent<Plane>();
		if (plane == null)
			onPlane = false;
		
		else
			onPlane = true;
	}

	private void OnTriggerExit(Collider other)
	{
		PlaneExit(other);
	}

	private void PlaneExit(Collider other)
	{
		Plane plane = other.GetComponent<Plane>();
		if (plane != null)
			onPlane = false;
	}
}