using UnityEngine;
using System.Collections;

public class FSM : MonoBehaviour {


    public enum State
    {
        Idle,Initialize,Setup,Combat,Looting,
    }

    public enum Combat
    {
        FindEnemy,AttackEnemy, EnemyDead,
    }

    private State _state; // local varibale that represents state what we will be switrching on

    public Combat combat;
    // Use this for initialization
    IEnumerator Start()
    {

        _state = State.Initialize; // starting state
        combat = FSM.Combat.FindEnemy;

        while (true)
        {
            switch (_state)
            {

                case State.Initialize:
                    InitMe();
                    break;
                case State.Setup:
                    SetmeUp();
                    break;
                case State.Combat:
                    inCombat();
                    break;
                case State.Looting:
                    Looting();
                    break;
            }
            yield return 0;

        }
    }
	
    private void InitMe()
    {
        Debug.Log("Yhis is the initme function");
        _state = State.Setup;
    }

    private void SetmeUp()
    {
        Debug.Log("this is the setup");
        _state = State.Combat;
    }
    private void Looting()
    {

    }

    private void inCombat()
    {
        Debug.Log("In Combat");
        switch (combat)
        {
            case Combat.FindEnemy:
                FindEnemy();
                break;
            case Combat.AttackEnemy:
                AttackEnemy();
                break;
            case Combat.EnemyDead:
                EnemyDead();
                break;
        }
    }

    private void FindEnemy()
    {
        Debug.Log("Finfing Enenmy");
        combat = FSM.Combat.AttackEnemy;
    }

    private void AttackEnemy()
    {
        Debug.Log("Attacking Enenmy");
        combat = FSM.Combat.EnemyDead;
    }

    private void EnemyDead()
    {
        Debug.Log("Enemy Dead");
        combat = FSM.Combat.FindEnemy;
        _state = State.Idle;
    }
	// Update is called once per frame
	void Update () {
	
	}
}
