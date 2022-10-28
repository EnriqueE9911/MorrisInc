using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header ("Health")]
   [SerializeField] private float startingHealth;
   public float currentHealth { get; private set; }
   private Animator anim;
   private  bool dead; 

   [Header ("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private float numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header ("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header ("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurthSound;

   private void Awake ()
   {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
   }
   public void TakeDamage(float _damage)
   {
        if(invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0 , startingHealth);
        
        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            SoundManager.instance.PlaySound(hurthSound);
        }
        else 
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;


                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
            
        }
   }
   public void AddHealth(float _value)
   {
    currentHealth = Mathf.Clamp(currentHealth + _value, 0 , startingHealth);
   }   
    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0.5f);
            yield return new WaitForSeconds(1);
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(1);
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }

private void Deactivate()
{
    gameObject.SetActive(false);
}
}