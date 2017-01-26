using UnityEngine;
using System.Collections;

public class FX_FuriaEsplosiva :FX
{	
	override public void Start ()
    {
        color = Color.blue;
    }

    public override void enabledParticle()
    {
        this.gameObject.SetActive(true);
    }
}
