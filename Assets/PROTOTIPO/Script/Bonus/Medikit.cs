using UnityEngine;
using System.Collections;

public class Medikit : MonoBehaviour {

    Player player;
    UIController elementiUi;
	
	void Start () {

        player = FindObjectOfType<Player>();
        elementiUi = FindObjectOfType<UIController>();

	}
	
	void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Player" && player.currentHealth < player.maxHealth)
        {
            player.currentHealth += 5;
            elementiUi.IncreaseLife();

            if (player.currentHealth > player.maxHealth)
            {
                player.currentHealth = player.maxHealth;
            }

                Destroy(this.gameObject);
        }
    }

	void Update ()
    {
        this.transform.Rotate(0, 3, 0);
	}
}
