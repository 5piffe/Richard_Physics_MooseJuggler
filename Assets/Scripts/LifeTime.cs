using UnityEngine;

public class LifeTime : MonoBehaviour
{
	[SerializeField] private float lifeTime = 0.5f;

	private void FixedUpdate()
	{
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0f)
		{
			Destroy(gameObject);
		}
	}
}