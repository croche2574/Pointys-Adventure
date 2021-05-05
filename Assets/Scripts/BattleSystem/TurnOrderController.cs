using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrderController : MonoBehaviour
{
    public const string RoundBeganNotification = "TurnOrderController.roundBegan";
    public const string TurnCheckNotification = "TurnOrderController.turnCheck";
    public const string TurnCompletedNotification = "TurnOrderController.turnCompleted";
    public const string RoundEndedNotification = "TurnOrderController.roundEnded";
    public const string TurnBeganNotification = "TurnOrderController.TurnBeganNotification";
    
    public IEnumerator Round()
    {
        BattleMachine b = GetComponent<BattleMachine>();;
        while (true)
        {
            this.PostNotification(RoundBeganNotification);
            List<Unit> units = b.units;
            b.player.GetComponent<Stats>()[StatTypes.CTR]++;
            b.monster.GetComponent<Stats>()[StatTypes.CTR]++;

            for (int i = units.Count - 1; i >= 0; --i)
            {
                if (CanTakeTurn(units[i]))
                {
                    b.turn.ChangeUnit(units[i]);
                    units[i].PostNotification(TurnBeganNotification);
                    yield return units[i];
                    units[i].takeTurn = false;
                    units[i].PostNotification(TurnCompletedNotification);
                }
            }
            b.player.takeTurn = true;
            b.monster.takeTurn = true;
            this.PostNotification(RoundEndedNotification);
        }
    }
    bool CanTakeTurn(Unit target)
    {
        BaseException exc = new BaseException(target.takeTurn);
        target.PostNotification(TurnCheckNotification, exc);
        return exc.toggle;
    }
    int GetCounter(Unit target)
    {
        return target.GetComponent<Stats>()[StatTypes.CTR];
    }
}
