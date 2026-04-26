using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [field: SerializeField] public WeaponDataSO Data {  get; private set; }

    public event Action OnEnter;
    public event Action OnExit;

    private Animator anim;
    public GameObject BaseGameObject { get; private set; }
    public GameObject WeaponSpriteGameObject { get; private set; }

    private WeaponAnimationEventHandler eventHandler;

    public void Enter()
    {
        print($"{transform.name} enter");

        anim.SetBool("active", true);

        OnEnter?.Invoke();
    }

    private void Exit()
    {
        anim.SetBool("active", false);

        OnExit?.Invoke();
    }

    private void Awake()
    {
        BaseGameObject = transform.Find("Base").gameObject;
        WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;
        
        anim = BaseGameObject.GetComponent<Animator>();

        eventHandler = BaseGameObject.GetComponent<WeaponAnimationEventHandler>();
    }

    private void OnEnable()
    {
        eventHandler.OnFinish += Exit;
    }

    private void OnDisable()
    {
        eventHandler.OnFinish -= Exit;
    }
}
