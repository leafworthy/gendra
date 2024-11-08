using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player mainPlayer;
	public float money = 100;

	private void Start()
	{
		mainPlayer = this;
	}

	public void SpendMoney(float cost)
	{
		money -= cost;
	}
}