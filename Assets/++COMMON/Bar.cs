using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using System;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class Bar : MonoBehaviour
{
	// COLOR OPTIONS //
	public Image slowBarImage;
	public Image fastBarImage;
	public enum ColorMode
	{
		Single,
		Gradient
	}
	public ColorMode colorMode;
	public Color barColor = Color.white;
	public Gradient barGradient = new Gradient();
	public float currentFraction;

	private float maxValue = 0.0f;
	private float currentValue = 0.0f;
	private float targetFill = 0.0f;
	private float smoothingFactor = .25f;






	void Update()
	{
		UpdateBarFill();

	}

	void UpdateGradient()
	{
		if (colorMode == ColorMode.Gradient)
			fastBarImage.color = barGradient.Evaluate(currentFraction);
	}

	#region PUBLIC FUNCTIONS

	public void UpdateBar(float _currentValue, float _maxValue)
	{
	

		currentFraction = _currentValue / _maxValue;

		if (currentFraction < 0 || currentFraction > 1)
			currentFraction = currentFraction < 0 ? 0 : 1;


		this.maxValue = _maxValue;
		this.currentValue = _currentValue;

		UpdateGradient();
		UpdateBarFill();
	}



	private void UpdateBarFill()
	{

		targetFill = currentFraction;
		if (slowBarImage != null)
		{

			slowBarImage.fillAmount = Mathf.Lerp(slowBarImage.fillAmount, targetFill, smoothingFactor);
		}
		if (fastBarImage != null)
		{
			fastBarImage.fillAmount = targetFill;
		}
	}

	public void AddToBar(float add)
	{
		UpdateBar(currentValue + add, maxValue);
	}

	private void OnEnable()
	{
		UpdateColor(barColor);
		UpdateColor(barGradient);
		UpdateBarFill();
	}

	public void UpdateColor(Color targetColor)
	{
		if (colorMode != ColorMode.Single || slowBarImage == null)
			return;
		barColor = targetColor;
		slowBarImage.color = barColor;
	}

	public void UpdateColor(Gradient targetGradient)
	{
		// If the color is not set to gradient, then return.
		if (colorMode != ColorMode.Gradient || slowBarImage == null)
			return;

		barGradient = targetGradient;
		UpdateGradient();
	}

	#endregion

	public void EmptyBar()
	{
		UpdateBar(0 ,1);
	}
}