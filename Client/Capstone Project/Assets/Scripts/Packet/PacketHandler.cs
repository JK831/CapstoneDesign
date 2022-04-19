using DummyClient;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Text;

class PacketHandler
{
	public static void S_BroadcastEnterGameHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastEnterGame pkt = packet as S_BroadcastEnterGame;
		ServerSession serverSession = session as ServerSession;
	}

	public static void S_BroadcastLeaveGameHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastLeaveGame pkt = packet as S_BroadcastLeaveGame;
		ServerSession serverSession = session as ServerSession;
	}

	public static void S_PlayerListHandler(PacketSession session, IPacket packet)
	{
		S_PlayerList pkt = packet as S_PlayerList;
		ServerSession serverSession = session as ServerSession;
	}

	public static void S_BroadcastMoveHandler(PacketSession session, IPacket packet)
	{
		S_BroadcastMove pkt = packet as S_BroadcastMove;
		ServerSession serverSession = session as ServerSession;
	}

    internal static void S_ChallengeTotalStarsHandler(PacketSession session, IPacket packet)
    {
        S_ChallengeTotalStars pkt = packet as S_ChallengeTotalStars;
        ServerSession serverSession = session as ServerSession;

        Managers.User.TotalStars = pkt.TotalStars;
    }

    internal static void S_ChallengeCheckMyRankingHandler(PacketSession session, IPacket packet)
    {
        S_ChallengeCheckMyRanking pkt = packet as S_ChallengeCheckMyRanking;
        ServerSession serverSession = session as ServerSession;

        Managers.User.Ranking = pkt.ranking;
    }

    internal static void S_GetStudyMaxStageHandler(PacketSession session, IPacket packet)
    {
        S_GetStudyMaxStage pkt = packet as S_GetStudyMaxStage;
        ServerSession serverSession = session as ServerSession;

        Managers.User.StudyProgress = pkt.maxStage;
    }

    internal static void S_LoadChallengeStarHandler(PacketSession session, IPacket packet)
    {
        S_LoadChallengeStar pkt = packet as S_LoadChallengeStar;
        ServerSession serverSession = session as ServerSession;

        Managers.User.SetChallengeInfoByPacket(pkt);
    }

    internal static void S_ChallengeTop30Handler(PacketSession session, IPacket packet)
    {
        S_ChallengeTop30 pkt = packet as S_ChallengeTop30;
        ServerSession serverSession = session as ServerSession;

        Managers.User.SetChallengeTop30(pkt);
    }
}
