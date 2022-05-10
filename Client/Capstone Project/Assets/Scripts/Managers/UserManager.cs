using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using System;

public class UserManager
{
    string UId = "TestId";
    string name;
    ushort challengeProgress;
    ushort totalStars;
    int ranking;
    Dictionary<ushort, byte> _challengeStageInfo = new Dictionary<ushort, byte>();
    Dictionary<int, ChallengeRankerInfo> _challengeTop30 = new Dictionary<int, ChallengeRankerInfo>();

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

    public Dictionary<ushort, byte> ChallangeStageInfo
    {
        get { return _challengeStageInfo; }
    }

    public Dictionary<int, ChallengeRankerInfo> ChallengeTop30
    {
        get { return _challengeTop30; }
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
            _challengeTop30.Add(s.ranking, new ChallengeRankerInfo(s.name, s.totalStars));
        }
    }

}