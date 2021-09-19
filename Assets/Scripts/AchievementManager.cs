using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum RewardType
{
    Gold, Experience
}

public class AchievementManager : MonoBehaviour
{
    public Achievement ZombieKiller;
    public Achievement ZombieKeeper;
    public Achievement GraveyardKeeper;
    public Achievement CemeteryWorker;
    public Achievement TreasureHunter;

    //can be moved to the prefab
    private readonly int[] zombieKillerLevels = { 1, 50, 100, 500 }; //item in array is target for next lvl
    private readonly int[] zombieKeeperLevels = { 5, 15, 30, 100 };
    private readonly int[] graveyardKeeperLevels = { 5, 10, 25, 100 };
    private readonly int[] cemeteryWorkerLevels = { 10, 50, 250, 1000 };
    private readonly int[] treasureHunterLevels = { 1, 10, 25, 50 };

    //can be moved to the prefab
    private readonly int[] zombieKillerRewards = { 100, 250, 500, 2000 };//gold
    private readonly int[] zombieKeeperRewards = { 100, 250, 500, 2000 };//gold
    private readonly int[] graveyardKeeperRewards = { 200, 1000, 3000, 10000 };//gold - this is for reached lvl so give diamonds or items/unlocks
    private readonly int[] cemeteryWorkerRewards = { 300, 500, 750, 1500 };//xp - can't give too much cos we still have fixed lvling
    private readonly int[] treasureHunterRewards = { 300, 500, 750, 1500 };//xp


    public AchievementData Save()
    {
        return new AchievementData
        {
            zombieKillerLevel = ZombieKiller.Level,
            zombieKillerCounter = ZombieKiller.Counter,
            zombieKillerPendingReward = ZombieKiller.PendingReward,
            zombieKeeperLevel = ZombieKeeper.Level,
            zombieKeeperCounter = 0, //this needs to reset every time
            zombieKeeperPendingReward = ZombieKeeper.PendingReward,
            graveyardKeeperLevel = GraveyardKeeper.Level,
            graveyardKeeperCounter = GraveyardKeeper.Counter,
            graveyardKeeperPendingReward = GraveyardKeeper.PendingReward,
            cemeteryWorkerLevel = CemeteryWorker.Level,
            cemeteryWorkerCounter = CemeteryWorker.Counter,
            cemeteryWorkerPendingReward = CemeteryWorker.PendingReward,
            treasureHunterLevel = TreasureHunter.Level,
            treasureHunterCounter = TreasureHunter.Counter,
            treasureHunterPendingReward = TreasureHunter.PendingReward
        };
    }

    public void Load(AchievementData data)
    {
        ZombieKiller.Init(zombieKillerLevels, zombieKillerRewards, data.zombieKillerCounter, data.zombieKillerLevel, data.zombieKillerPendingReward);
        ZombieKeeper.Init(zombieKeeperLevels, zombieKeeperRewards, 0, data.zombieKeeperLevel, data.zombieKeeperPendingReward);
        GraveyardKeeper.Init(graveyardKeeperLevels, graveyardKeeperRewards, data.graveyardKeeperCounter, data.graveyardKeeperLevel, data.graveyardKeeperPendingReward);
        CemeteryWorker.Init(cemeteryWorkerLevels, cemeteryWorkerRewards, data.cemeteryWorkerCounter, data.cemeteryWorkerLevel, data.cemeteryWorkerPendingReward);
        TreasureHunter.Init(treasureHunterLevels, treasureHunterRewards, data.treasureHunterCounter, data.treasureHunterLevel, data.treasureHunterPendingReward);
    }

    public void Reset()
    {
        ZombieKiller.Init(zombieKillerLevels, zombieKillerRewards);
        ZombieKeeper.Init(zombieKeeperLevels, zombieKeeperRewards);
        GraveyardKeeper.Init(graveyardKeeperLevels, graveyardKeeperRewards, 1, 0, false);
        CemeteryWorker.Init(cemeteryWorkerLevels, cemeteryWorkerRewards);
        TreasureHunter.Init(treasureHunterLevels, treasureHunterRewards);
    }

    public void OnZombieKill()
    {
        ZombieKiller.UpdateProgress();
        ZombieKeeper.UpdateProgress(-1);
    }

    public void OnZombieEscape()
    {
        ZombieKeeper.UpdateProgress(-1);
    }

    public void OnZombieSpawn()
    {
        ZombieKeeper.UpdateProgress();
    }

    public void OnLevelUp()
    {
        GraveyardKeeper.UpdateProgress();
    }

    public void OnTreasureFound()
    {
        TreasureHunter.UpdateProgress();
    }

    public void OnCorpseBurried()
    {
        CemeteryWorker.UpdateProgress();
    }
}
