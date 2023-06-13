using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EnemyDeadAI : MonoBehaviour {

    public UnityEvent OnDead;
	//public UnityEngine.EventSystems.EventHandle OnDead;
	private void Start()
	{
        if(OnDead == null){
            OnDead = new UnityEvent();
        }

        OnDead.AddListener(() => Death());
    }

    void Death() {
        GameStateHandler.Instance.enemiesToKillRemaining--;
        GameStateHandler.Instance.enemiesKilled++;
        GameObjectsContainer.Instance.currentLevelController_Script.EnemiesDeathCallBack();
        //GameObjectsContainer.Instance.enemiesCarContainer.Remove(this.gameObject);
    }
    public void InvokeOnDeadEvent()
    {
        OnDead.Invoke();
    }
}
