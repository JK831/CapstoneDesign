using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using System;

public class UserManager
{
    string UId;
    string name;
    ushort challengeProgress;
    ushort totalStars;
    int ranking;
    Dictionary<ushort, ushort> _challengeStageInfo = new Dictionary<ushort, ushort>();
    Dictionary<string, int> _challengeTop30 = new Dictionary<string, int>();

    public string UID
    {
        get { return UId; }
        set { UId = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public ushort ChallengeProgress
    {
        get { return challengeProgress; }
        set { challengeProgress = value; }
    }

    public ushort TotalStars
    {
        get { return totalStars; }
        set { totalStars = value; }
    }

    public int Ranking
    {
        get { return ranking; }
        set { ranking = value; }
    }

    public Dictionary<ushort, ushort> ChallangeStageInfo
    {
        get { return _challengeStageInfo; }
    }


    public void SetChallengeInfoByPacket(S_Challenge_Load_Star packet)
    {
        foreach (S_Challenge_Load_Star.StageStar s in packet.stageStars)
        {
            _challengeStageInfo.Add(s.stageId, s.numberOfStars);
        }
    }

    public void SetChallengeTop30(S_Challenge_Top30Rank packet)
    {
        foreach (S_Challenge_Top30Rank.Rank s in packet.ranks)
        {
            _challengeTop30.Add(s.UId, s.ranking);
        }
    }
}