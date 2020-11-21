using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public int currentScore = 0;
	private Text currentScoreUI;

	private void Start()
	{
		currentScoreUI = GetComponentInChildren<Text>();
	}

	private void Update()
	{
		currentScoreUI.text = ($"SCORE: {currentScore}");
	}
}
