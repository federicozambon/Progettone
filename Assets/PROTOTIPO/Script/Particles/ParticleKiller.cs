using UnityEngine;
using System.Collections;

public class ParticleKiller : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(GetComponent<ParticleSystem>().duration);
        Destroy(this.gameObject);
    }
}
