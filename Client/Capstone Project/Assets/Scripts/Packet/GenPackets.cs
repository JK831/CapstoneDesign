using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using ServerCore;

public enum PacketID
{
	S_BroadcastEnterGame = 1,
	C_LeaveGame = 2,
	S_BroadcastLeaveGame = 3,
	S_PlayerList = 4,
	C_Move = 5,
	S_BroadcastMove = 6,
	S_ChallengeTotalStars = 7,
	S_ChallengeCheckMyRanking = 8,
	S_GetStudyMaxStage = 9,
	S_LoadChallengeStar = 10,
	S_ChallengeTop30 = 11,
	C_ChallengeUpdateStars = 12,
	C_RequestMyChallengeProgress = 13,
	C_RequestMyChallengeRanking = 14,
	C_OpenNextStudyStage = 15,
	C_RequestTotalStars = 16,
	C_RequestStudyProgress = 17,
	C_RequestChallengeTop30 = 18,
	
}

public interface IPacket
{
	ushort Protocol { get; }
	void Read(ArraySegment<byte> segment);
	ArraySegment<byte> Write();
}


public class S_BroadcastEnterGame : IPacket
{
	public int playerId;
	public float posX;
	public float posY;
	public float posZ;

	public ushort Protocol { get { return (ushort)PacketID.S_BroadcastEnterGame; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		this.playerId = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
		this.posX = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
		this.posY = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
		this.posZ = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_BroadcastEnterGame), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.playerId), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
		Array.Copy(BitConverter.GetBytes(this.posX), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);
		Array.Copy(BitConverter.GetBytes(this.posY), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);
		Array.Copy(BitConverter.GetBytes(this.posZ), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_LeaveGame : IPacket
{
	

	public ushort Protocol { get { return (ushort)PacketID.C_LeaveGame; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_LeaveGame), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_BroadcastLeaveGame : IPacket
{
	public int playerId;

	public ushort Protocol { get { return (ushort)PacketID.S_BroadcastLeaveGame; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		this.playerId = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_BroadcastLeaveGame), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.playerId), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_PlayerList : IPacket
{
	public class Player
	{
		public bool isSelf;
		public int playerId;
		public float posX;
		public float posY;
		public float posZ;
	
		public void Read(ArraySegment<byte> segment, ref ushort count)
		{
			this.isSelf = BitConverter.ToBoolean(segment.Array, segment.Offset + count);
			count += sizeof(bool);
			this.playerId = BitConverter.ToInt32(segment.Array, segment.Offset + count);
			count += sizeof(int);
			this.posX = BitConverter.ToSingle(segment.Array, segment.Offset + count);
			count += sizeof(float);
			this.posY = BitConverter.ToSingle(segment.Array, segment.Offset + count);
			count += sizeof(float);
			this.posZ = BitConverter.ToSingle(segment.Array, segment.Offset + count);
			count += sizeof(float);
		}
	
		public bool Write(ArraySegment<byte> segment, ref ushort count)
		{
			bool success = true;
			Array.Copy(BitConverter.GetBytes(this.isSelf), 0, segment.Array, segment.Offset + count, sizeof(bool));
			count += sizeof(bool);
			Array.Copy(BitConverter.GetBytes(this.playerId), 0, segment.Array, segment.Offset + count, sizeof(int));
			count += sizeof(int);
			Array.Copy(BitConverter.GetBytes(this.posX), 0, segment.Array, segment.Offset + count, sizeof(float));
			count += sizeof(float);
			Array.Copy(BitConverter.GetBytes(this.posY), 0, segment.Array, segment.Offset + count, sizeof(float));
			count += sizeof(float);
			Array.Copy(BitConverter.GetBytes(this.posZ), 0, segment.Array, segment.Offset + count, sizeof(float));
			count += sizeof(float);
			return success;
		}	
	}
	public List<Player> players = new List<Player>();

	public ushort Protocol { get { return (ushort)PacketID.S_PlayerList; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		this.players.Clear();
		ushort playerLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		for (int i = 0; i < playerLen; i++)
		{
			Player player = new Player();
			player.Read(segment, ref count);
			players.Add(player);
		}
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_PlayerList), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)this.players.Count), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		foreach (Player player in this.players)
			player.Write(segment, ref count);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_Move : IPacket
{
	public float posX;
	public float posY;
	public float posZ;

	public ushort Protocol { get { return (ushort)PacketID.C_Move; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		this.posX = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
		this.posY = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
		this.posZ = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_Move), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.posX), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);
		Array.Copy(BitConverter.GetBytes(this.posY), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);
		Array.Copy(BitConverter.GetBytes(this.posZ), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_BroadcastMove : IPacket
{
	public int playerId;
	public float posX;
	public float posY;
	public float posZ;

	public ushort Protocol { get { return (ushort)PacketID.S_BroadcastMove; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		this.playerId = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
		this.posX = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
		this.posY = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
		this.posZ = BitConverter.ToSingle(segment.Array, segment.Offset + count);
		count += sizeof(float);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_BroadcastMove), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.playerId), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);
		Array.Copy(BitConverter.GetBytes(this.posX), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);
		Array.Copy(BitConverter.GetBytes(this.posY), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);
		Array.Copy(BitConverter.GetBytes(this.posZ), 0, segment.Array, segment.Offset + count, sizeof(float));
		count += sizeof(float);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_ChallengeTotalStars : IPacket
{
	public byte TotalStars;

	public ushort Protocol { get { return (ushort)PacketID.S_ChallengeTotalStars; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		this.TotalStars = (byte)segment.Array[segment.Offset + count];
		count += sizeof(byte);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_ChallengeTotalStars), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		segment.Array[segment.Offset + count] = (byte)this.TotalStars;
		count += sizeof(byte);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_ChallengeCheckMyRanking : IPacket
{
	public int ranking;

	public ushort Protocol { get { return (ushort)PacketID.S_ChallengeCheckMyRanking; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		this.ranking = BitConverter.ToInt32(segment.Array, segment.Offset + count);
		count += sizeof(int);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_ChallengeCheckMyRanking), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(this.ranking), 0, segment.Array, segment.Offset + count, sizeof(int));
		count += sizeof(int);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_GetStudyMaxStage : IPacket
{
	public byte maxStage;

	public ushort Protocol { get { return (ushort)PacketID.S_GetStudyMaxStage; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		this.maxStage = (byte)segment.Array[segment.Offset + count];
		count += sizeof(byte);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_GetStudyMaxStage), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		segment.Array[segment.Offset + count] = (byte)this.maxStage;
		count += sizeof(byte);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_LoadChallengeStar : IPacket
{
	public class StageStar
	{
		public byte stageId;
		public byte numberOfStars;
	
		public void Read(ArraySegment<byte> segment, ref ushort count)
		{
			this.stageId = (byte)segment.Array[segment.Offset + count];
			count += sizeof(byte);
			this.numberOfStars = (byte)segment.Array[segment.Offset + count];
			count += sizeof(byte);
		}
	
		public bool Write(ArraySegment<byte> segment, ref ushort count)
		{
			bool success = true;
			segment.Array[segment.Offset + count] = (byte)this.stageId;
			count += sizeof(byte);
			segment.Array[segment.Offset + count] = (byte)this.numberOfStars;
			count += sizeof(byte);
			return success;
		}	
	}
	public List<StageStar> stageStars = new List<StageStar>();

	public ushort Protocol { get { return (ushort)PacketID.S_LoadChallengeStar; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		this.stageStars.Clear();
		ushort stageStarLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		for (int i = 0; i < stageStarLen; i++)
		{
			StageStar stageStar = new StageStar();
			stageStar.Read(segment, ref count);
			stageStars.Add(stageStar);
		}
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_LoadChallengeStar), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)this.stageStars.Count), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		foreach (StageStar stageStar in this.stageStars)
			stageStar.Write(segment, ref count);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class S_ChallengeTop30 : IPacket
{
	public class Rank
	{
		public string UId;
		public byte ranking;
		public byte totalStars;
	
		public void Read(ArraySegment<byte> segment, ref ushort count)
		{
			ushort UIdLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
			count += sizeof(ushort);
			this.UId = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, UIdLen);
			count += UIdLen;
			this.ranking = (byte)segment.Array[segment.Offset + count];
			count += sizeof(byte);
			this.totalStars = (byte)segment.Array[segment.Offset + count];
			count += sizeof(byte);
		}
	
		public bool Write(ArraySegment<byte> segment, ref ushort count)
		{
			bool success = true;
			ushort UIdLen = (ushort)Encoding.Unicode.GetBytes(this.UId, 0, this.UId.Length, segment.Array, segment.Offset + count + sizeof(ushort));
			Array.Copy(BitConverter.GetBytes(UIdLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
			count += sizeof(ushort);
			count += UIdLen;
			segment.Array[segment.Offset + count] = (byte)this.ranking;
			count += sizeof(byte);
			segment.Array[segment.Offset + count] = (byte)this.totalStars;
			count += sizeof(byte);
			return success;
		}	
	}
	public List<Rank> ranks = new List<Rank>();

	public ushort Protocol { get { return (ushort)PacketID.S_ChallengeTop30; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		this.ranks.Clear();
		ushort rankLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		for (int i = 0; i < rankLen; i++)
		{
			Rank rank = new Rank();
			rank.Read(segment, ref count);
			ranks.Add(rank);
		}
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.S_ChallengeTop30), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)this.ranks.Count), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		foreach (Rank rank in this.ranks)
			rank.Write(segment, ref count);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_ChallengeUpdateStars : IPacket
{
	public string UId;
	public byte stageId;
	public byte numberOfStars;

	public ushort Protocol { get { return (ushort)PacketID.C_ChallengeUpdateStars; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		ushort UIdLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.UId = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, UIdLen);
		count += UIdLen;
		this.stageId = (byte)segment.Array[segment.Offset + count];
		count += sizeof(byte);
		this.numberOfStars = (byte)segment.Array[segment.Offset + count];
		count += sizeof(byte);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_ChallengeUpdateStars), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		ushort UIdLen = (ushort)Encoding.Unicode.GetBytes(this.UId, 0, this.UId.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(UIdLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += UIdLen;
		segment.Array[segment.Offset + count] = (byte)this.stageId;
		count += sizeof(byte);
		segment.Array[segment.Offset + count] = (byte)this.numberOfStars;
		count += sizeof(byte);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_RequestMyChallengeProgress : IPacket
{
	public string UId;

	public ushort Protocol { get { return (ushort)PacketID.C_RequestMyChallengeProgress; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		ushort UIdLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.UId = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, UIdLen);
		count += UIdLen;
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_RequestMyChallengeProgress), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		ushort UIdLen = (ushort)Encoding.Unicode.GetBytes(this.UId, 0, this.UId.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(UIdLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += UIdLen;

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_RequestMyChallengeRanking : IPacket
{
	public string UId;

	public ushort Protocol { get { return (ushort)PacketID.C_RequestMyChallengeRanking; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		ushort UIdLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.UId = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, UIdLen);
		count += UIdLen;
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_RequestMyChallengeRanking), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		ushort UIdLen = (ushort)Encoding.Unicode.GetBytes(this.UId, 0, this.UId.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(UIdLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += UIdLen;

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_OpenNextStudyStage : IPacket
{
	public string UId;
	public byte stageId;

	public ushort Protocol { get { return (ushort)PacketID.C_OpenNextStudyStage; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		ushort UIdLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.UId = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, UIdLen);
		count += UIdLen;
		this.stageId = (byte)segment.Array[segment.Offset + count];
		count += sizeof(byte);
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_OpenNextStudyStage), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		ushort UIdLen = (ushort)Encoding.Unicode.GetBytes(this.UId, 0, this.UId.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(UIdLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += UIdLen;
		segment.Array[segment.Offset + count] = (byte)this.stageId;
		count += sizeof(byte);

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_RequestTotalStars : IPacket
{
	public string UId;

	public ushort Protocol { get { return (ushort)PacketID.C_RequestTotalStars; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		ushort UIdLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.UId = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, UIdLen);
		count += UIdLen;
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_RequestTotalStars), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		ushort UIdLen = (ushort)Encoding.Unicode.GetBytes(this.UId, 0, this.UId.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(UIdLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += UIdLen;

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_RequestStudyProgress : IPacket
{
	public string UId;

	public ushort Protocol { get { return (ushort)PacketID.C_RequestStudyProgress; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		ushort UIdLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.UId = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, UIdLen);
		count += UIdLen;
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_RequestStudyProgress), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		ushort UIdLen = (ushort)Encoding.Unicode.GetBytes(this.UId, 0, this.UId.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(UIdLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += UIdLen;

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

public class C_RequestChallengeTop30 : IPacket
{
	public string UId;

	public ushort Protocol { get { return (ushort)PacketID.C_RequestChallengeTop30; } }

	public void Read(ArraySegment<byte> segment)
	{
		ushort count = 0;
		count += sizeof(ushort);
		count += sizeof(ushort);
		ushort UIdLen = BitConverter.ToUInt16(segment.Array, segment.Offset + count);
		count += sizeof(ushort);
		this.UId = Encoding.Unicode.GetString(segment.Array, segment.Offset + count, UIdLen);
		count += UIdLen;
	}

	public ArraySegment<byte> Write()
	{
		ArraySegment<byte> segment = SendBufferHelper.Open(4096);
		ushort count = 0;

		count += sizeof(ushort);
		Array.Copy(BitConverter.GetBytes((ushort)PacketID.C_RequestChallengeTop30), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		ushort UIdLen = (ushort)Encoding.Unicode.GetBytes(this.UId, 0, this.UId.Length, segment.Array, segment.Offset + count + sizeof(ushort));
		Array.Copy(BitConverter.GetBytes(UIdLen), 0, segment.Array, segment.Offset + count, sizeof(ushort));
		count += sizeof(ushort);
		count += UIdLen;

		Array.Copy(BitConverter.GetBytes(count), 0, segment.Array, segment.Offset, sizeof(ushort));

		return SendBufferHelper.Close(count);
	}
}

