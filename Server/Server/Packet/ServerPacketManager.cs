using ServerCore;
using System;
using System.Collections.Generic;

public class PacketManager
{
	#region Singleton
	static PacketManager _instance = new PacketManager();
	public static PacketManager Instance { get { return _instance; } }
	#endregion

	PacketManager()
	{
		Register();
	}

	Dictionary<ushort, Func<PacketSession, ArraySegment<byte>, IPacket>> _makeFunc = new Dictionary<ushort, Func<PacketSession, ArraySegment<byte>, IPacket>>();
	Dictionary<ushort, Action<PacketSession, IPacket>> _handler = new Dictionary<ushort, Action<PacketSession, IPacket>>();

	public void Register()
	{
		_makeFunc.Add((ushort)PacketID.C_Request_Name_input, MakePacket<C_Request_Name_input>);
		_handler.Add((ushort)PacketID.C_Request_Name_input, PacketHandler.C_Request_Name_inputHandler);
		_makeFunc.Add((ushort)PacketID.C_Request_Challenge_MyPage, MakePacket<C_Request_Challenge_MyPage>);
		_handler.Add((ushort)PacketID.C_Request_Challenge_MyPage, PacketHandler.C_Request_Challenge_MyPageHandler);
		_makeFunc.Add((ushort)PacketID.C_Request_Challenge_Top30Rank, MakePacket<C_Request_Challenge_Top30Rank>);
		_handler.Add((ushort)PacketID.C_Request_Challenge_Top30Rank, PacketHandler.C_Request_Challenge_Top30RankHandler);
		_makeFunc.Add((ushort)PacketID.C_ChallengeUpdateStars, MakePacket<C_ChallengeUpdateStars>);
		_handler.Add((ushort)PacketID.C_ChallengeUpdateStars, PacketHandler.C_ChallengeUpdateStarsHandler);

	}

	public void OnRecvPacket(PacketSession session, ArraySegment<byte> buffer, Action<PacketSession, IPacket> onRecvCallback = null)
	{
		ushort count = 0;

		ushort size = BitConverter.ToUInt16(buffer.Array, buffer.Offset);
		count += 2;
		ushort id = BitConverter.ToUInt16(buffer.Array, buffer.Offset + count);
		count += 2;
		if(size < 1000 && id >=1 && id <= 8)
        {
			Func<PacketSession, ArraySegment<byte>, IPacket> func = null;
			if (_makeFunc.TryGetValue(id, out func))
			{
				IPacket packet = func.Invoke(session, buffer);
				if (onRecvCallback != null)
					onRecvCallback.Invoke(session, packet);
				else
					HandlePacket(session, packet);
			}
		}
		
	}

	T MakePacket<T>(PacketSession session, ArraySegment<byte> buffer) where T : IPacket, new()
	{
		T pkt = new T();
		pkt.Read(buffer);
		return pkt;
	}

	public void HandlePacket(PacketSession session, IPacket packet)
	{
		Action<PacketSession, IPacket> action = null;
		if (_handler.TryGetValue(packet.Protocol, out action))
			action.Invoke(session, packet);
	}
}