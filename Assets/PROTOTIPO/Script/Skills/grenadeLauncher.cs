using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class grenadeLauncher : MonoBehaviour {

    
    public GameObject grenadeObj;
    float speed = 10;
    public Transform weapon;
    bool lancioAttivo = true;
    public Image share;




    IEnumerator LancioGranata()
    {
        GameObject nuovaGranata = Instantiate(grenadeObj);
        
        nuovaGranata.transform.position = new Vector3 (weapon.transform.position.x, weapon.transform.position.y, weapon.transform.position.z) ;

        nuovaGranata.GetComponent<Rigidbody>().AddForce(weapon.transform.forward * speed, ForceMode.Impulse);
        yield return new WaitForSeconds(0.1f);

        yield return new WaitForSeconds(1);
        lancioAttivo = true;
    }

    void FixedUpdate()
    {
        if (lancioAttivo == false )
            share.fillAmount += 0.02f;
    }
}
