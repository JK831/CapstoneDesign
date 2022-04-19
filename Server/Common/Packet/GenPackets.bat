START ../../PacketGenerator/bin/PacketGenerator.exe ../../PacketGenerator/PDL.xml
XCOPY /Y GenPackets.cs "../../DummyClient/Packet"
XCOPY /Y GenPackets.cs "../../../Client/Capstone Project/Assets/Scripts/Packet"
XCOPY /Y GenPackets.cs "../../Server/Packet"
XCOPY /Y ClientPacketManager.cs "../../DummyClient/Packet"
XCOPY /Y ClientPacketManager.cs "../../../Client/Capstone Project/Assets/Scripts/Packet"
XCOPY /Y ServerPacketManager.cs "../../Server/Packet"