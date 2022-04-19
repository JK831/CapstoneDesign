using Server;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Data;
using static Server.DB.DbManager;

class PacketHandler
{
	public static void C_ChallengeUpdateStarsHandler(PacketSession session, IPacket packet)
	{
		C_ChallengeUpdateStars pkt = packet as C_ChallengeUpdateStars;
		ClientSession clientSession = session as ClientSession;
		Server.DB.DbManager.challenge_UpdateStar(pkt.UId, pkt.stageId, pkt.numberOfStars);
		session.Disconnect();
	}

	public static void C_OpenNextStudyStageHandler(PacketSession session, IPacket packet)
	{
		C_OpenNextStudyStage pkt = packet as C_OpenNextStudyStage;
		ClientSession clientSession = session as ClientSession;
		Server.DB.DbManager.study_Open_Next_Stage(pkt.UId, pkt.stageId);
		session.Disconnect();
	}

	public static void C_RequestStudyProgressHandler(PacketSession session, IPacket packet)
	{
		C_RequestStudyProgress pkt = packet as C_RequestStudyProgress;
		ClientSession clientSession = session as ClientSession;
		S_GetStudyMaxStage s_pkt = packet as S_GetStudyMaxStage;
		s_pkt.maxStage = study_Get_Max_Stage(pkt.UId);
		clientSession.Send(s_pkt.Write());
	}

	public static void C_RequestMyChallengeProgressHandler(PacketSession session, IPacket packet)
    {
		C_RequestMyChallengeProgress pkt = packet as C_RequestMyChallengeProgress;
		ClientSession clientSession = session as ClientSession;
		S_LoadChallengeStar s_pkt = packet as S_LoadChallengeStar;
		Dictionary<byte, byte> star_Dic = Server.DB.DbManager.Load_star(pkt.UId);
		foreach (KeyValuePair<byte, byte> items in star_Dic)
        {
			s_pkt.stageStars.Add(new S_LoadChallengeStar.StageStar()
			{
				stageId = items.Key,
				numberOfStars = items.Value
			});
        }
		clientSession.Send(s_pkt.Write());
	}

	public static void C_RequestMyChallengeRankingHandler(PacketSession session, IPacket packet)
	{
		C_RequestMyChallengeProgress pkt = packet as C_RequestMyChallengeProgress;
		ClientSession clientSession = session as ClientSession;
		S_ChallengeCheckMyRanking s_pkt = packet as S_ChallengeCheckMyRanking;
		s_pkt.ranking = Server.DB.DbManager.challenge_Check_Ranking(pkt.UId);
		clientSession.Send(s_pkt.Write());
	}
	public static void C_RequestTotalStarsHandler(PacketSession session, IPacket packet)
	{
		C_RequestMyChallengeProgress pkt = packet as C_RequestMyChallengeProgress;
		ClientSession clientSession = session as ClientSession;
		S_ChallengeTotalStars s_pkt = packet as S_ChallengeTotalStars;
		s_pkt.TotalStars = Server.DB.DbManager.challenge_Total_Star(pkt.UId);
		clientSession.Send(s_pkt.Write());
	}

    public static void C_RequestChallengeTop30Handler(PacketSession session, IPacket packet)
    {
        C_RequestMyChallengeProgress pkt = packet as C_RequestMyChallengeProgress;
        S_ChallengeTop30 s_pkt = packet as S_ChallengeTop30;
        ClientSession clientSession = session as ClientSession;
		List < S_ChallengeTop30.Rank > top_30 = new List<S_ChallengeTop30.Rank>();
        s_pkt.ranks = Server.DB.DbManager.Study_ChallengeTop30(top_30);
		clientSession.Send(s_pkt.Write());
	}
}