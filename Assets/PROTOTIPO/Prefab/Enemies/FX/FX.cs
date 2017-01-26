using UnityEngine;
using System.Collections;

public abstract class FX : MonoBehaviour
{
    protected ParticleSystem pS;
    public Color color;
    float durata = 0;
    bool startParticle;

    void Awake()
    {
        pS = GetComponent<ParticleSystem>();
        pS.Clear();
    }

    public virtual void Start()
    {

    }

    public virtual void enabledParticle()
    {
        pS.startSize = 0;
        durata = 0;
        GetComponent<Renderer>().material.SetColor("_TintColor", color);

        StartCoroutine(lifeParticle());
    }

    IEnumerator lifeParticle()
    {
        pS.Play();
        pS.startSize += 0.5f;
        durata += 0.1f;

        yield return new WaitForSeconds(0.1f);

        if (durata <= 2)
            StartCoroutine(lifeParticle());
    }

    public IEnumerator ParticleExplosion()
    {
        pS.startSize += 3f;
        yield return new WaitForSeconds(0.5f);
        StopParticle();
        StopAllCoroutines();
    }
	
    public void StopParticle()
    {
        pS.Stop();
        StopAllCoroutines();
    }
}
