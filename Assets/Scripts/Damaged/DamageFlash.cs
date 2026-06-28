using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DamageFlash : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashDuration = 0.15f;

    private static readonly int FlashAmountID = Shader.PropertyToID("_FlashAmount");
    private static readonly int FlashColorID = Shader.PropertyToID("_FlashColor");

    private MaterialPropertyBlock propBlock;
    private Coroutine flashCoroutine;

    private void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        propBlock = new MaterialPropertyBlock();
    }

    public void Flash()
    {
        if (flashCoroutine != null)
            StopCoroutine(flashCoroutine);

        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.GetPropertyBlock(propBlock);
        propBlock.SetColor(FlashColorID, flashColor);
        propBlock.SetFloat(FlashAmountID, 1f);
        spriteRenderer.SetPropertyBlock(propBlock);

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.GetPropertyBlock(propBlock);
        propBlock.SetFloat(FlashAmountID, 0f);
        spriteRenderer.SetPropertyBlock(propBlock);
    }
}