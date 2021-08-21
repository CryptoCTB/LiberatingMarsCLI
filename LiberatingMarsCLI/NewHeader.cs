using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberatingMarsCLI
{
    public partial class NewHeader
    {
        // New header
        public uint Magic;
        public uint HeaderSize;
        public uint HeaderOffset;
        public uint Unknown1;
        public uint Unknown2;
        public uint SignatureSize;
        public uint SignatureOffset;
        public uint Unknown3;
        public ushort Unknown4;
        public ushort Unknown5;
        public uint Unknown6;
        public uint Unknown7;
        public uint Unknown9;

        // New header (dec)
        public UInt64 ChecksumValue;
        public uint LayerTableOffset;
        public float SizeX;
        public float SizeY;
        public float SizeZ;
        public uint[] Padding; //[2]
        public float TotalHeightMilimeter;
        public float LayerHeight;
        public float ExposureTime;
        public float BottomExposureTime;
        public float LightOffDelay;
        public uint BottomLayerCount;
        public uint ResolutionX;
        public uint ResolutionY;
        public uint LayerCountPlusOne;
        public uint LargePreviewOffset;
        public uint SmallPreviewOffset;
        public uint PrintTime;
        public uint unknown5;
        public float BottomLiftDistance;
        public float BottomLiftSpeed;
        public float LiftDistance;
        public float LiftSpeed;
        public float RetractSpeed;
        public float ModelVolume;
        public float ModelWeight;
        public float Cost;
        public float BottomLightOffDelay;
        public uint unknown9;
        public ushort LightPWM;
        public ushort BottomLightPWM;
        public uint LayerXorKey;
        public float BottomLiftDistance2;
        public float BottomLiftSpeed2;
        public float LiftingDistance2;
        public float LiftingSpeed2;
        public float RetractDistance2;
        public float RetractSpeed2;
        public float RestTimeAfterLift;
        public uint PrinterNameOffset;
        public uint PrinterNameSize;
        public uint unknown12;
        public uint unknown13;
        public uint unknown14;
        public float RestTimeAfterRetract;
        public float RestTimeAfterLift_2;
        public uint unknown15;
        public float BottomRetractSpeed;
        public float BottomRetractSpeed2;
        public uint unknown15_2;
        public float unknown16;
        public uint unknown17;
        public float unknown18;
        public float RestTimeAfterRetract_2;
        public float RestTimeAfterLift_3;
        public float RestTimeBeforeLift;
        public float BottomRetractDistance;
        public float unknown23;
        public uint unknown24;
        public uint unknown25;
        public uint LayerCount;
        public uint[] unknown26; // [4]
        public uint DisclaimerOffset;
        public uint DisclaimerSize;
        public uint[] padding; // [4]

        //Preview
        public uint pResolutionX;
        public uint pResolutionY;
        public uint pImageOffset;
        public uint pImageLength;

        public byte[] pData;

        public uint pResolutionX2;
        public uint pResolutionY2;
        public uint pImageOffset2;
        public uint pImageLength2;

        public byte[] pData2;
    }
}
