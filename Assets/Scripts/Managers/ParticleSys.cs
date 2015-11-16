using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CoupledParticle {
    public GameObject Particle;
    public ParticleEffect TheParticleEffect;
}

public enum ParticleEffect {
    Blood,
    Spark,
    ChickenSpawn,
    EggEffects
}

public class ParticleSys : MonoBehaviour {
    /*
    Particle system with the parameters: lifetime, position, direction, parent
    Called from anywhere.
    Bind particle prefab with name
    */
    public static ParticleSys instance = null;

    [SerializeField]
    private List<CoupledParticle> coupledParticleList;

    [SerializeField]

    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
        
    
               
    public void spawnParticle (ParticleEffect pe, Vector3 pPosition, bool looping, float lifetime, Transform pParent)
    {
        GameObject go = (GameObject)Instantiate(coupledParticleList.Find(x => x.TheParticleEffect == pe).Particle, pPosition, Quaternion.identity);
        go.GetComponent<ParticleSystem>().loop = looping;
        go.transform.parent = pParent;
        go.GetComponent<ParticleSystem>().startLifetime = lifetime;    
    }

    public void spawnParticle(ParticleEffect pe, Vector3 pPosition, bool looping, float lifetime) {
        GameObject go = (GameObject)Instantiate(coupledParticleList.Find(x => x.TheParticleEffect == pe).Particle, pPosition, Quaternion.identity);
        go.GetComponent<ParticleSystem>().loop = looping;
        go.GetComponent<ParticleSystem>().startLifetime = lifetime;
    }

    public void spawnParticle(ParticleEffect pe, Vector3 pPosition, bool looping) {
        GameObject go = (GameObject)Instantiate(coupledParticleList.Find(x => x.TheParticleEffect == pe).Particle, pPosition, Quaternion.identity);
        go.GetComponent<ParticleSystem>().loop = looping;

    }

    public void spawnParticle(ParticleEffect pe, Vector3 pPosition)
    {
        GameObject go = (GameObject)Instantiate(coupledParticleList.Find(x => x.TheParticleEffect == pe).Particle, pPosition, Quaternion.identity);
    }

    public void spawnParticleDestroyable(ParticleEffect pe, Vector3 pPosition, float delay)
    {
        GameObject go = (GameObject)Instantiate(coupledParticleList.Find(x => x.TheParticleEffect == pe).Particle, pPosition, Quaternion.identity);
        Destroy(go, delay);
    }
}
