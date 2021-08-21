using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberatingMarsCLI
{
    class NewLayers
    {
        public uint Offset;
        public uint Unknown1;
        public uint Unknown2;
        public uint Unknown3;

        public uint LayerHeaderLength;
        public float ZHeight;
        public float BottomExposureDuration;
        public float LightOffDelay;
        public uint LayerDataOffset;
        public uint unknown2;
        public uint LayerDataLength;
        public uint unknown3;
        public uint EncryptedDataOffset;
        public uint EncryptedDataLength;
        public float LiftDistance;
        public float LiftSpeed;
        public float LiftDistance2;
        public float LiftSpeed2;
        public float RetractSpeed;
        public float RetractDistance2;
        public float RetractSpeed2;
        public float RestTimeBeforeLift;
        public float RestTimeAfterLift;
        public float RestTimeAfterRetract;
        public float LightPWM;
        public uint unknown6;

        public byte[] layerData; // [LayerDataLength]

        public byte[] Sha256Hash; // [0x20]
    }
}
