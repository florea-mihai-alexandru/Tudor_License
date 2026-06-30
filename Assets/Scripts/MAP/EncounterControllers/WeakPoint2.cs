using System;
using UnityEngine;

public class CoreWeakPoint : MonoBehaviour
{
    public HealthStats health;

    public Sprite destroyedSprite;

    public Action<CoreWeakPoint> OnDestroyed;

    public SpriteRenderer spriteRenderer;
    public Animator animator;
    private bool destroyed;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();

        health.OnDeath += Destroyed;
    }

    private void OnDestroy()
    {
        if (health != null)
            health.OnDeath -= Destroyed;
    }

    private void Destroyed()
    {
        Debug.Log("wsoikjgrjklwsgjklrgsw");
        if (destroyed)
            return;

        destroyed = true;

        health.canTakeDamage = false;

        if (animator != null)
            animator.enabled = false;
        spriteRenderer.sprite = destroyedSprite;
        spriteRenderer.transform.localScale /= 12.5f;

        OnDestroyed?.Invoke(this);
    }
}