using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleEndState : BattleState
{
    public override void Enter()
    {
        base.Enter();
        //player.GetComponentInChildren<Camera>().GetComponent<AudioListener>().enabled = false;
        SceneManager.LoadScene(player.lastScene);
    }
}
