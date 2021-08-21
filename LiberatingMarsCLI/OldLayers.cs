using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberatingMarsCLI
{
    class OldLayers
    {
        public float LayerPositionZ;
        public float LayerExposure;
        public float LightOffSeconds;
        public uint DataAddress;
        public uint DataSize;
        public uint Unknown1;
        public uint Unknown2;
        public uint Unknown3;
        public uint Unknown4;

        public byte[] layerDataBlock; // [size]
    }
}
