using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ShowSliderValue : MonoBehaviour
{
    Text lbl;
	public void UpdateLabel (float value)
	{
	
		if (lbl != null)
			lbl.text = Mathf.RoundToInt (value +60)*2 + "%";
	}

    private void Awake()
    {
        lbl = GetComponent<Text>();
        lbl.text = 100 + "%";
    }
}