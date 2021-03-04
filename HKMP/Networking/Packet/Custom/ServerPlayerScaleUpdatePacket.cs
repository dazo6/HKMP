﻿using UnityEngine;

namespace HKMP.Networking.Packet.Custom {
    public class ServerPlayerScaleUpdatePacket : Packet, IPacket {
        
        public Vector3 Scale { get; set; }

        public ServerPlayerScaleUpdatePacket() {
        }
        
        public ServerPlayerScaleUpdatePacket(Packet packet) : base(packet) {
        }
        
        public void CreatePacket() {
            Reset();
            
            Write(PacketId.ServerPlayerScaleUpdate);

            Write(Scale);

            WriteLength();
        }

        public void ReadPacket() {
            Scale = ReadVector3();
        }
    }
}