using BasicLegionInfected.Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CrossPlatformInputEventHelper : MonoBehaviour
{
	public UnityEvent OnClicked = new();

	private void OnMouseDown()
	{
		OnClicked.Invoke();
	}

	private void OnMouseEnter()
	{
		InputManager.Instance.IsInputBlocked = true;
	}

	private void OnMouseExit()
	{
		InputManager.Instance.IsInputBlocked = false;
	}
}
