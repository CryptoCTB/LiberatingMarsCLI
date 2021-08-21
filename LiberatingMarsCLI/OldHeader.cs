using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiberatingMarsCLI
{
    public class OldHeader
    {
        // Old header
        public uint Magic;
        public uint Version;

        public float BedSizeX;
        public float BedSizeY;
        public float BedSizeZ;

        public uint Unknown1;
        public uint Unknown2;

        public float TotalHeightMilimeter;
        public float LayerHeightMilimeter;
        public float LayerExposureSeconds;
        public float BottomExposureSeconds;
        public float LightOffDelay;
        public uint BottomLayersCount;
        public uint ResolutionX;
        public uint ResolutionY;
        public uint PreviewLargeOffsetAddress;
        public uint LayersDefinitionOffsetAddress;
        public uint LayerCount;
        public uint PreviewSmallOffsetAddress;
        public uint PrintTime;
        public uint ProjectorType;
        public uint PrintParametersOffsetAddress;
        public uint PrintParametersSize;
        public uint AntiAliasLevel;
        public ushort LightPWM;
        public ushort BottomLightPWM;
        public uint EncryptionKey;
        public uint SlicerOffset;
        public uint SlicerSize;

        //Preview
        public uint pResolutionX;
        public uint pResolutionY;
        public uint pImageOffset;
        public uint pImageLength;
        public uint pUnknown1;
        public uint pUnknown2;
        public uint pUnknown3;
        public uint pUnknown4;

        public byte[] pData; //[pImageLength]

        public uint pResolutionX2;
        public uint pResolutionY2;
        public uint pImageOffset2;
        public uint pImageLength2;
        public uint pUnknown1_2;
        public uint pUnknown2_2;
        public uint pUnknown3_2;
        public uint pUnknown4_2;

        public byte[] pData2; //[pImageLength2]

        //Print Parameters
        public float ppBottomLiftHeight;
        public float ppBottomLiftSpeed;
        public float ppLiftHeight;
        public float ppLiftSpeed;
        public float ppRetractSpeed;
        public float ppVolumeMl;
        public float ppWeightG;
        public float ppCostDollars;
        public float ppBottomLightOffDelay;
        public float ppLightOffDelay;

        public uint ppBottomLayerCount;
        public uint ppPadding1;
        public uint ppPadding2;
        public uint ppPadding3;
        public uint ppPadding4;

        //Slicer Info
        public float sBottomLiftDistance2;
        public float sBottomLiftSpeed2;
        public float sLiftHeight2;
        public float sLiftSpeed2;
        public float sRetractHeight2;
        public float sRetractSpeed2;
        public float sRestTimeAfterLift;

        public uint sMachineNameAddress;
        public uint sMachineNameSize;
        public uint sEncryptionMode;
        public uint sMysteriousId;
        public uint sAntiAliasLevel;
        public uint sSoftwareVersion;
        public float sRestTimeAfterRetract;
        public float sRestTimeAfterLift2;
        public uint sTransitionLayerCount;
        public uint sPadding1;
        public uint sPadding2;
        public uint sPadding3;
    }
}
