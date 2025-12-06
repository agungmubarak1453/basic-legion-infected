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
}
