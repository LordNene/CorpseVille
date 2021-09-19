using System;

[System.Serializable]
public class AchievementData
{
    public int zombieKillerCounter;
    public int zombieKeeperCounter;
    public int graveyardKeeperCounter;
    public int cemeteryWorkerCounter;
    public int treasureHunterCounter;

    public bool zombieKillerPendingReward;
    public bool zombieKeeperPendingReward;
    public bool graveyardKeeperPendingReward;
    public bool cemeteryWorkerPendingReward;
    public bool treasureHunterPendingReward;

    public int zombieKillerLevel;
    public int zombieKeeperLevel;
    public int graveyardKeeperLevel;
    public int cemeteryWorkerLevel;
    public int treasureHunterLevel;
}
