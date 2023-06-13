using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDead : MonoBehaviour {
    private static PlayerDead _instance;
    public static PlayerDead Instance{
        get{
            return _instance;
        }

    }
	private void OnEnable()
	{
        _instance = this;
	}
    public UnityEvent OnDead;
    private void Start()
    {
        if (OnDead == null)
        {
            OnDead = new UnityEvent();
        }

        OnDead.AddListener(() => Death());
    }
    void Death()
    {
        GameObjectsContainer.Instance.currentLevelController_Script.PlayerDeathCallBack();
        //GameObjectsContainer.Instance.enemiesCarContainer.Remove(this.gameObject);
    }
    public void InvokeOnDeadEvent()
    {
        OnDead.Invoke();
    }
}
