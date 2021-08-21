using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace LiberatingMarsCLI
{
    class Program
    {

        static byte[] ctbContent = null;
        static String ctbLocation = null;

        public static bool isWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        public static bool isLinux = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        public static bool isMac = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        
        static DirectoryInfo tempDir = Directory.CreateDirectory(Path.GetTempPath() + "/LiberatingMars");
        static String headerTempLoc = tempDir + "/Mars3Temp.ctb";
        static long oldHeaderFinalPos = 0;

        static OldHeader oldH = new OldHeader();
        static NewHeader newH = new NewHeader();

        static OldLayers oldLay = new OldLayers();
        static NewLayers newLay = new NewLayers();

        static PullCrypto crypto = new PullCrypto();
        static AES aes = new AES();

        public static String chituLocation;

        public static void Main(string[] args)
        {
            chituLocation = args[0];

            Console.WriteLine("\n> LiberatingMars CLI v0.1 <");
            if (isWindows)
                Console.WriteLine("> Detected OS: Windows <");
            else if (isLinux)
                Console.WriteLine("> Detected OS: Linux <");
            else if (isMac)
                Console.WriteLine("> Detected OS: MacOS <");

            ctbLocation = args[1];

            if (args[0] != null)
                if (File.Exists(args[0]))
                    translateHeader();
                else
                    Console.WriteLine("ERROR: Invalid ChiTuBox path!\nClosing...");

            if (args[0] == null)
                Console.WriteLine("ERROR: ChiTuBox path not provided!");
        }

        public String chituLocation2 = chituLocation;
        public static void translateHeader()
        {
            ctbContent = File.ReadAllBytes(ctbLocation);
            MemoryStream ms = new MemoryStream(ctbContent);
            BinaryReader binRead = new BinaryReader(ms);

            oldH.Magic = binRead.ReadUInt32();
            oldH.Version = binRead.ReadUInt32();

            oldH.BedSizeX = binRead.ReadSingle();
            oldH.BedSizeY = binRead.ReadSingle();
            oldH.BedSizeZ = binRead.ReadSingle();

            oldH.Unknown1 = binRead.ReadUInt32();
            oldH.Unknown2 = binRead.ReadUInt32();

            oldH.TotalHeightMilimeter = binRead.ReadSingle();
            oldH.LayerHeightMilimeter = binRead.ReadSingle();
            oldH.LayerExposureSeconds = binRead.ReadSingle();
            oldH.BottomExposureSeconds = binRead.ReadSingle();
            oldH.LightOffDelay = binRead.ReadSingle();
            oldH.BottomLayersCount = binRead.ReadUInt32();
            oldH.ResolutionX = binRead.ReadUInt32();
            oldH.ResolutionY = binRead.ReadUInt32();
            oldH.PreviewLargeOffsetAddress = binRead.ReadUInt32();
            oldH.LayersDefinitionOffsetAddress = binRead.ReadUInt32();
            oldH.LayerCount = binRead.ReadUInt32();
            oldH.PreviewSmallOffsetAddress = binRead.ReadUInt32();
            oldH.PrintTime = binRead.ReadUInt32();
            oldH.ProjectorType = binRead.ReadUInt32();
            oldH.PrintParametersOffsetAddress = binRead.ReadUInt32();
            oldH.PrintParametersSize = binRead.ReadUInt32();
            oldH.AntiAliasLevel = binRead.ReadUInt32();
            oldH.LightPWM = binRead.ReadUInt16();
            oldH.BottomLightPWM = binRead.ReadUInt16();
            oldH.EncryptionKey = binRead.ReadUInt32();
            oldH.SlicerOffset = binRead.ReadUInt32();
            oldH.SlicerSize = binRead.ReadUInt32();

            oldH.pResolutionX = binRead.ReadUInt32();
            oldH.pResolutionY = binRead.ReadUInt32();
            oldH.pImageOffset = binRead.ReadUInt32();
            oldH.pImageLength = binRead.ReadUInt32();
            oldH.pUnknown1 = binRead.ReadUInt32();
            oldH.pUnknown2 = binRead.ReadUInt32();
            oldH.pUnknown3 = binRead.ReadUInt32();
            oldH.pUnknown4 = binRead.ReadUInt32();
            binRead.BaseStream.Position = oldH.pImageOffset;
            oldH.pData = new byte[oldH.pImageLength];
            oldH.pData = binRead.ReadBytes((int)oldH.pImageLength);

            oldH.pResolutionX2 = binRead.ReadUInt32();
            oldH.pResolutionY2 = binRead.ReadUInt32();
            oldH.pImageOffset2 = binRead.ReadUInt32();
            oldH.pImageLength2 = binRead.ReadUInt32();
            oldH.pUnknown1_2 = binRead.ReadUInt32();
            oldH.pUnknown2_2 = binRead.ReadUInt32();
            oldH.pUnknown3_2 = binRead.ReadUInt32();
            oldH.pUnknown4_2 = binRead.ReadUInt32();
            binRead.BaseStream.Position = oldH.pImageOffset2;
            oldH.pData2 = new byte[oldH.pImageLength2];
            oldH.pData2 = binRead.ReadBytes((int)oldH.pImageLength2);

            oldH.ppBottomLiftHeight = binRead.ReadSingle();
            oldH.ppBottomLiftSpeed = binRead.ReadSingle();
            oldH.ppLiftHeight = binRead.ReadSingle();
            oldH.ppLiftSpeed = binRead.ReadSingle();
            oldH.ppRetractSpeed = binRead.ReadSingle();
            oldH.ppVolumeMl = binRead.ReadSingle();
            oldH.ppWeightG = binRead.ReadSingle();
            oldH.ppCostDollars = binRead.ReadSingle();
            oldH.ppBottomLightOffDelay = binRead.ReadSingle();
            oldH.ppLightOffDelay = binRead.ReadSingle();

            oldH.ppBottomLayerCount = binRead.ReadUInt32();
            oldH.ppPadding1 = binRead.ReadUInt32();
            oldH.ppPadding2 = binRead.ReadUInt32();
            oldH.ppPadding3 = binRead.ReadUInt32();
            oldH.ppPadding4 = binRead.ReadUInt32();

            oldH.sBottomLiftDistance2 = binRead.ReadSingle();
            oldH.sBottomLiftSpeed2 = binRead.ReadSingle();
            oldH.sLiftHeight2 = binRead.ReadSingle();
            oldH.sLiftSpeed2 = binRead.ReadSingle();
            oldH.sRetractHeight2 = binRead.ReadSingle();
            oldH.sRetractSpeed2 = binRead.ReadSingle();
            oldH.sRestTimeAfterLift = binRead.ReadSingle();

            oldH.sMachineNameAddress = binRead.ReadUInt32();
            oldH.sMachineNameSize = binRead.ReadUInt32();
            oldH.sEncryptionMode = binRead.ReadUInt32();
            oldH.sMysteriousId = binRead.ReadUInt32();
            oldH.sAntiAliasLevel = binRead.ReadUInt32();
            oldH.sSoftwareVersion = binRead.ReadUInt32();
            oldH.sRestTimeAfterRetract = binRead.ReadSingle();
            oldH.sRestTimeAfterLift2 = binRead.ReadSingle();
            oldH.sTransitionLayerCount = binRead.ReadUInt32();
            oldH.sPadding1 = binRead.ReadUInt32();
            oldH.sPadding2 = binRead.ReadUInt32();
            oldH.sPadding3 = binRead.ReadUInt32();

            oldHeaderFinalPos = binRead.BaseStream.Position;

            newH.Magic = 0x12FD0107;
            newH.HeaderSize = 0x120;
            newH.HeaderOffset = 0x30;
            newH.Unknown1 = 0;
            newH.Unknown2 = 0x4;
            newH.SignatureSize = 0x20;
            newH.SignatureOffset = 0; // EOL - SignatureSize
            newH.Unknown3 = 0;
            newH.Unknown4 = 0x1;
            newH.Unknown5 = 0x1;
            newH.Unknown6 = 0;
            newH.Unknown7 = 0xBAADF02A;
            newH.Unknown9 = 0xBAADF00D;

            newH.ChecksumValue = 0x610D91DE;
            newH.LayerTableOffset = 0;
            newH.SizeX = oldH.BedSizeX;
            newH.SizeY = oldH.BedSizeY;
            newH.SizeZ = oldH.BedSizeZ;
            newH.Padding = new uint[2];
            newH.TotalHeightMilimeter = oldH.TotalHeightMilimeter;
            newH.LayerHeight = oldH.LayerHeightMilimeter;
            newH.ExposureTime = oldH.LayerExposureSeconds;
            newH.BottomExposureTime = oldH.BottomExposureSeconds;
            newH.LightOffDelay = (uint)oldH.LightOffDelay;
            newH.BottomLayerCount = oldH.BottomLayersCount;
            newH.ResolutionX = oldH.ResolutionX;
            newH.ResolutionY = oldH.ResolutionY;
            newH.LayerCountPlusOne = oldH.LayerCount;
            newH.LargePreviewOffset = oldH.PreviewLargeOffsetAddress + 0xE0;
            newH.SmallPreviewOffset = oldH.PreviewSmallOffsetAddress + 0xD0;
            newH.PrintTime = oldH.PrintTime;
            newH.unknown5 = 0x1;
            newH.BottomLiftDistance = oldH.ppBottomLiftHeight;
            newH.BottomLiftSpeed = oldH.ppBottomLiftSpeed;
            newH.LiftDistance = oldH.ppLiftHeight;
            newH.LiftSpeed = oldH.ppLiftSpeed;
            newH.RetractSpeed = oldH.ppRetractSpeed;
            newH.ModelVolume = oldH.ppVolumeMl;
            newH.ModelWeight = oldH.ppWeightG;
            newH.Cost = oldH.ppCostDollars;
            newH.BottomLightOffDelay = oldH.ppBottomLightOffDelay;
            newH.unknown9 = 0x1;
            newH.LightPWM = oldH.LightPWM;
            newH.BottomLightPWM = oldH.BottomLightPWM;
            newH.LayerXorKey = oldH.EncryptionKey;
            newH.BottomLiftDistance2 = oldH.sBottomLiftSpeed2;
            newH.BottomLiftSpeed2 = oldH.sBottomLiftSpeed2;
            newH.LiftingDistance2 = oldH.sLiftHeight2;
            newH.LiftingSpeed2 = oldH.sLiftSpeed2;
            newH.RetractDistance2 = oldH.sRetractHeight2;
            newH.RetractSpeed2 = oldH.sRetractSpeed2;
            newH.RestTimeAfterLift = oldH.sRestTimeAfterLift;
            newH.PrinterNameOffset = oldH.sMachineNameAddress;
            newH.PrinterNameSize = oldH.sMachineNameSize;
            newH.unknown12 = 0xF;
            newH.unknown13 = 0;
            newH.unknown14 = 0x8;
            newH.RestTimeAfterRetract = oldH.sRestTimeAfterRetract;
            newH.RestTimeAfterLift_2 = oldH.sRestTimeAfterLift2;
            newH.unknown15 = 0;
            newH.BottomRetractSpeed = oldH.ppRetractSpeed;
            newH.BottomRetractSpeed2 = oldH.sRetractSpeed2;
            newH.unknown15_2 = 0;
            newH.unknown16 = 4;
            newH.unknown17 = 0;
            newH.unknown18 = 4;
            newH.RestTimeAfterRetract_2 = newH.RestTimeAfterRetract;
            newH.RestTimeAfterLift_3 = newH.RestTimeAfterLift;
            newH.RestTimeBeforeLift = oldH.sRestTimeAfterRetract;
            newH.BottomRetractDistance = oldH.ppBottomLiftHeight;
            newH.unknown23 = 302;
            newH.unknown24 = 0x101;
            newH.unknown25 = 0x4;
            newH.LayerCount = oldH.LayerCount - 1;
            newH.unknown26 = new uint[4];
            newH.DisclaimerOffset = 0x29285;
            newH.DisclaimerSize = 0x140;
            newH.padding = new uint[4];

            newH.pResolutionX = oldH.pResolutionX;
            newH.pResolutionY = oldH.pResolutionY;
            newH.pImageOffset = 0x160;
            newH.pImageLength = oldH.pImageLength;
            newH.pData = new byte[newH.pImageLength];
            newH.pData = oldH.pData;

            newH.pResolutionX2 = oldH.pResolutionX2;
            newH.pResolutionY2 = oldH.pResolutionY2;
            newH.pImageOffset2 = oldH.pImageOffset + oldH.pImageLength + 0xE0;
            newH.pImageLength2 = oldH.pImageLength2;
            newH.pData2 = new byte[newH.pImageLength2];
            newH.pData2 = oldH.pData2;

            newH.PrinterNameOffset = newH.pImageOffset2 + newH.pImageLength2;
            newH.LayerTableOffset = newH.PrinterNameOffset + newH.PrinterNameSize + 0x140;

            BinaryWriter binWrite = new BinaryWriter(File.Open(headerTempLoc, FileMode.Create));

            binWrite.Write(newH.Magic);
            binWrite.Write(newH.HeaderSize);
            binWrite.Write(newH.HeaderOffset);
            binWrite.Write(newH.Unknown1);
            binWrite.Write(newH.Unknown2);
            binWrite.Write(newH.SignatureSize);
            binWrite.Write(newH.SignatureOffset);
            binWrite.Write(newH.Unknown3);
            binWrite.Write(newH.Unknown4);
            binWrite.Write(newH.Unknown5);
            binWrite.Write(newH.Unknown6);
            binWrite.Write(newH.Unknown7);
            binWrite.Write(newH.Unknown9);

            binWrite.Write(newH.ChecksumValue);
            binWrite.Write(newH.LayerTableOffset);
            binWrite.Write(newH.SizeX);
            binWrite.Write(newH.SizeY);
            binWrite.Write(newH.SizeZ);
            binWrite.Write(newH.Padding[0]);
            binWrite.Write(newH.Padding[1]);
            binWrite.Write(newH.TotalHeightMilimeter);
            binWrite.Write(newH.LayerHeight);
            binWrite.Write(newH.ExposureTime);
            binWrite.Write(newH.BottomExposureTime);
            binWrite.Write(newH.LightOffDelay);
            binWrite.Write(newH.BottomLayerCount);
            binWrite.Write(newH.ResolutionX);
            binWrite.Write(newH.ResolutionY);
            binWrite.Write(newH.LayerCountPlusOne);
            binWrite.Write(newH.LargePreviewOffset);
            binWrite.Write(newH.SmallPreviewOffset);
            binWrite.Write(newH.PrintTime);
            binWrite.Write(newH.unknown5);
            binWrite.Write(newH.BottomLiftDistance);
            binWrite.Write(newH.BottomLiftSpeed);
            binWrite.Write(newH.LiftDistance);
            binWrite.Write(newH.LiftSpeed);
            binWrite.Write(newH.RetractSpeed);
            binWrite.Write(newH.ModelVolume);
            binWrite.Write(newH.ModelWeight);
            binWrite.Write(newH.Cost);
            binWrite.Write(newH.BottomLightOffDelay);
            binWrite.Write(newH.unknown9);
            binWrite.Write(newH.LightPWM);
            binWrite.Write(newH.BottomLightPWM);
            binWrite.Write(newH.LayerXorKey);
            binWrite.Write(newH.BottomLiftDistance2);
            binWrite.Write(newH.BottomLiftSpeed2);
            binWrite.Write(newH.LiftingDistance2);
            binWrite.Write(newH.LiftingSpeed2);
            binWrite.Write(newH.RetractDistance2);
            binWrite.Write(newH.RetractSpeed2);
            binWrite.Write(newH.RestTimeAfterLift);
            binWrite.Write(newH.PrinterNameOffset);
            binWrite.Write(newH.PrinterNameSize);
            binWrite.Write(newH.unknown12);
            binWrite.Write(newH.unknown13);
            binWrite.Write(newH.unknown14);
            binWrite.Write(newH.RestTimeAfterRetract);
            binWrite.Write(newH.RestTimeAfterLift_2);
            binWrite.Write(newH.unknown15);
            binWrite.Write(newH.BottomRetractSpeed);
            binWrite.Write(newH.BottomRetractSpeed2);
            binWrite.Write(newH.unknown15_2);
            binWrite.Write(newH.unknown16);
            binWrite.Write(newH.unknown17);
            binWrite.Write(newH.unknown18);
            binWrite.Write(newH.RestTimeAfterRetract_2);
            binWrite.Write(newH.RestTimeAfterLift_3);
            binWrite.Write(newH.RestTimeBeforeLift);
            binWrite.Write(newH.BottomRetractDistance);
            binWrite.Write(newH.unknown23);
            binWrite.Write(newH.unknown24);
            binWrite.Write(newH.unknown25);
            binWrite.Write(newH.LayerCount);
            for (int i = 0; i < newH.unknown26.Length; i++)
                binWrite.Write(newH.unknown26[i]);
            binWrite.Write(newH.DisclaimerOffset);
            binWrite.Write(newH.DisclaimerSize);
            for (int j = 0; j < 4; j++)
                binWrite.Write(newH.padding[j]);

            binWrite.Write(newH.pResolutionX);
            binWrite.Write(newH.pResolutionY);
            binWrite.Write(newH.pImageOffset);
            binWrite.Write(newH.pImageLength);
            for (int p1 = 0; p1 < newH.pData.Length; p1++)
                binWrite.Write(newH.pData[p1]);
            binWrite.Write(newH.pResolutionX2);
            binWrite.Write(newH.pResolutionY2);
            binWrite.Write(newH.pImageOffset2);
            binWrite.Write(newH.pImageLength2);
            for (int p2 = 0; p2 < newH.pData2.Length; p2++)
                binWrite.Write(newH.pData2[p2]);

            binWrite.BaseStream.Position = newH.PrinterNameOffset;

            binRead.BaseStream.Position = oldH.sMachineNameAddress;
            char[] MachineName = binRead.ReadChars((int)newH.PrinterNameSize);
            binWrite.Write(MachineName);

            const string disclaimer = "Layout and record format for the ctb and cbddlp file types are the copyrighted programs or codes of CBD Technology (China) Inc..The Customer or User shall not in any manner reproduce, distribute, modify, decompile, disassemble, decrypt, extract, reverse engineer, lease, assign, or sublicense the said programs or codes.";
            char[] Disclaimer = new char[0x140];
            Disclaimer = disclaimer.ToCharArray();
            binWrite.Write(Disclaimer);

            binWrite.Close();
            binRead.Close();

            translateLayers();
        }

        public static void translateLayers()
        {
            ctbContent = File.ReadAllBytes(ctbLocation);
            MemoryStream Stream = new MemoryStream(ctbContent);
            BinaryReader binRead = new BinaryReader(Stream);
            BinaryWriter binWrite = new BinaryWriter(File.Open(headerTempLoc, FileMode.Append));

            binRead.BaseStream.Position = oldH.LayersDefinitionOffsetAddress;
            var currentPos = binRead.BaseStream.Position;
            var Offset = binWrite.BaseStream.Position;
            var Offset2 = (uint)binWrite.BaseStream.Position + (0x10 * newH.LayerCountPlusOne);

            for (int i = 0; i < newH.LayerCountPlusOne; i++)
            {
                binRead.BaseStream.Position = currentPos;

                oldLay.LayerPositionZ = binRead.ReadSingle();
                oldLay.LayerExposure = binRead.ReadSingle();
                oldLay.LightOffSeconds = binRead.ReadSingle();
                oldLay.DataAddress = binRead.ReadUInt32();
                oldLay.DataSize = binRead.ReadUInt32();
                oldLay.Unknown1 = binRead.ReadUInt32();
                oldLay.Unknown2 = binRead.ReadUInt32();
                oldLay.Unknown3 = binRead.ReadUInt32();
                oldLay.Unknown4 = binRead.ReadUInt32();

                binRead.BaseStream.Position = oldLay.DataAddress;

                oldLay.layerDataBlock = new byte[oldLay.DataSize];
                for (int a = 0; a < oldLay.DataSize; a++)
                    oldLay.layerDataBlock[a] = binRead.ReadByte();

                currentPos = currentPos + 0x24;

                binWrite.BaseStream.Position = Offset;
                newLay.Offset = Offset2;
                newLay.Unknown1 = 0;
                newLay.Unknown2 = 88;
                newLay.Unknown3 = 0;
                Offset = Offset + 0x10;

                binWrite.Write(newLay.Offset);
                binWrite.Write(newLay.Unknown1);
                binWrite.Write(newLay.Unknown2);
                binWrite.Write(newLay.Unknown3);

                var beforePos = binWrite.BaseStream.Position;

                newLay.LayerHeaderLength = newLay.Unknown2;
                newLay.ZHeight = oldLay.LayerPositionZ;
                if (i < oldH.BottomLayersCount)
                    newLay.BottomExposureDuration = oldH.BottomExposureSeconds;
                else
                    newLay.BottomExposureDuration = oldH.LayerExposureSeconds;
                newLay.LightOffDelay = oldH.LightOffDelay;
                newLay.LayerDataOffset = Offset2 + newLay.LayerHeaderLength;
                newLay.unknown2 = 0;
                newLay.LayerDataLength = oldLay.DataSize;
                newLay.unknown3 = 0;
                newLay.EncryptedDataOffset = 0;
                newLay.EncryptedDataLength = 0;
                newLay.LiftDistance = oldH.ppLiftHeight;
                newLay.LiftSpeed = oldH.ppLiftSpeed;
                newLay.LiftDistance2 = 0;
                newLay.LiftSpeed2 = 0;
                newLay.RetractSpeed = oldH.ppRetractSpeed;
                newLay.RetractDistance2 = 0;
                newLay.RetractSpeed2 = 0;
                newLay.RestTimeBeforeLift = 0;
                newLay.RestTimeAfterLift = oldH.sRestTimeAfterLift;
                newLay.RestTimeAfterRetract = oldH.sRestTimeAfterRetract;
                newLay.LightPWM = oldH.LightPWM;
                newLay.unknown6 = 0;

                newLay.layerData = new byte[newLay.LayerDataLength];
                for (int ld = 0; ld < newLay.LayerDataLength; ld++)
                    newLay.layerData[ld] = oldLay.layerDataBlock[ld];

                binWrite.BaseStream.Position = newLay.Offset;

                binWrite.Write(newLay.LayerHeaderLength);
                binWrite.Write(newLay.ZHeight);
                binWrite.Write(newLay.BottomExposureDuration);
                binWrite.Write(newLay.LightOffDelay);
                binWrite.Write(newLay.LayerDataOffset);
                binWrite.Write(newLay.unknown2);
                binWrite.Write(newLay.LayerDataLength);
                binWrite.Write(newLay.unknown3);
                binWrite.Write(newLay.EncryptedDataOffset);
                binWrite.Write(newLay.EncryptedDataLength);
                binWrite.Write(newLay.LiftDistance);
                binWrite.Write(newLay.LiftSpeed);
                binWrite.Write(newLay.LiftDistance2);
                binWrite.Write(newLay.LiftSpeed2);
                binWrite.Write(newLay.RetractSpeed);
                binWrite.Write(newLay.RetractDistance2);
                binWrite.Write(newLay.RetractSpeed2);
                binWrite.Write(newLay.RestTimeBeforeLift);
                binWrite.Write(newLay.RestTimeAfterLift);
                binWrite.Write(newLay.RestTimeAfterRetract);
                binWrite.Write(newLay.LightPWM);
                binWrite.Write(newLay.unknown6);

                for (int ld2 = 0; ld2 < newLay.LayerDataLength; ld2++)
                    binWrite.Write(newLay.layerData[ld2]);

                Offset2 = Offset2 + newLay.unknown2 + newLay.LayerDataLength + 0x58;
            }

            binWrite.Close();
            long length = new System.IO.FileInfo(headerTempLoc).Length;
            BinaryWriter hash = new BinaryWriter(File.Open(headerTempLoc, FileMode.Open));
            hash.BaseStream.Position = 0x18;
            hash.Write(length);
            hash.Close();

            saveNewCtb();
        }

        public static void saveNewCtb()
        {
            sign();
            encrypt();
            save();
        }

        public static void sign()
        {
            crypto.pullCrypto(chituLocation);
            byte[] signature = new byte[newH.SignatureSize];
            BinaryReader read = new BinaryReader(File.Open(headerTempLoc, FileMode.Open));
            read.BaseStream.Position = 0x30;
            SHA256 hash = SHA256.Create();

            byte[] first8Bytes = new byte[8];
            byte[] hashed8Bytes = new byte[8];
            first8Bytes = read.ReadBytes(8);
            hashed8Bytes = hash.ComputeHash(first8Bytes);
            read.Close();

            byte[] encryptedHashed = new byte[8];
            var EncHash = AES.AesCryptBytes(hashed8Bytes, true, chituLocation);
            encryptedHashed = EncHash;

            BinaryWriter write = new BinaryWriter(File.Open(headerTempLoc, FileMode.Append));
            write.Write(encryptedHashed);
            write.Close();
        }

        public static void encrypt()
        {
            crypto.pullCrypto(chituLocation);
            BinaryReader read = new BinaryReader(File.Open(headerTempLoc, FileMode.Open));
            read.BaseStream.Position = newH.HeaderOffset;
            byte[] header = new byte[newH.HeaderSize];
            byte[] encHeader = new byte[newH.HeaderSize];
            header = read.ReadBytes((int)newH.HeaderSize);
            encHeader = AES.AesCryptBytes(header, true, chituLocation);
            read.Close();

            BinaryWriter write = new BinaryWriter(File.Open(headerTempLoc, FileMode.Open));
            write.BaseStream.Position = newH.HeaderOffset;
            write.Write(encHeader);
            write.Close();
        }

        public static void save()
        {
            File.Copy(headerTempLoc, ctbLocation.Replace(".ctb", "_MARS3.ctb"), true);
            Console.WriteLine("> File saved at: " + ctbLocation.Replace(".ctb", "_MARS3.ctb") + " <");
        }
    }
}
