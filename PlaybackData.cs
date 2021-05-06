using System;
using System.Collections.Generic;
using System.IO;

namespace PlaybackConverter
{
    public static class PlaybackData
    {
        public class ChaserState
        {

            public float positionX;
            public float positionY;
            public float timeStamp;

            public string animation;

            public enum Facings
            {
                Right = 1,
                Left = -1
            }

            public Facings facing;
            public bool onGround;
            public byte[] hairColor = new byte[3];
            public Int32 depth;

            public float scaleX;
            public float scaleY;
            public float dashDirectionX;
            public float dashDirectionY;
        }

        // Token: 0x06002A6B RID: 10859 RVA: 0x000F4BB0 File Offset: 0x000F2DB0
        public static void Export(List<ChaserState> list, string path)
        {
            float timeStamp = list[0].timeStamp;

            float positionX = list[0].positionX;
            float positionY = list[0].positionY;

            using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(path)))
            {
                binaryWriter.Write("TIMELINE");
                binaryWriter.Write(2);
                binaryWriter.Write(list.Count);
                foreach (ChaserState chaserState in list)
                {
                    binaryWriter.Write(chaserState.positionX - positionX);
                    binaryWriter.Write(chaserState.positionY - positionY);
                    binaryWriter.Write(chaserState.timeStamp - timeStamp);
                    binaryWriter.Write(chaserState.animation);
                    binaryWriter.Write((int)chaserState.facing);
                    binaryWriter.Write(chaserState.onGround);

                    byte[] hairColor = chaserState.hairColor;
                    binaryWriter.Write(hairColor[0]);
                    binaryWriter.Write(hairColor[1]);
                    binaryWriter.Write(hairColor[2]);
                    binaryWriter.Write(chaserState.depth);
                    binaryWriter.Write(chaserState.scaleX);
                    binaryWriter.Write(chaserState.scaleY);
                    binaryWriter.Write(chaserState.dashDirectionX);
                    binaryWriter.Write(chaserState.dashDirectionY);
                }
            }
        }


        // Token: 0x06002A70 RID: 10864 RVA: 0x000F50FC File Offset: 0x000F32FC
        public static List<ChaserState> Import(string filename)
        {
            List<ChaserState> list = new List<ChaserState>();
            using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(filename))) 
            {
                int num = 1;
                if (binaryReader.ReadString() == "TIMELINE")
                {
                    num = binaryReader.ReadInt32();
                }
                else
                {
                    binaryReader.BaseStream.Seek(0L, SeekOrigin.Begin);
                }
                int num2 = binaryReader.ReadInt32();
                for (int i = 0; i < num2; i++)
                {
                    ChaserState chaserState = new ChaserState();
                    chaserState.positionX = binaryReader.ReadSingle();
                    chaserState.positionY = binaryReader.ReadSingle();
                    chaserState.timeStamp = binaryReader.ReadSingle();
                    chaserState.animation = binaryReader.ReadString();
                    chaserState.facing = (ChaserState.Facings)binaryReader.ReadInt32();

                    chaserState.onGround = binaryReader.ReadBoolean();
                    chaserState.hairColor = new byte[3];

                    chaserState.hairColor[0] = binaryReader.ReadByte();
                    chaserState.hairColor[1] = binaryReader.ReadByte();
                    chaserState.hairColor[2] = binaryReader.ReadByte();

                    chaserState.depth = binaryReader.ReadInt32();
                    if (num == 1)
                    {
                        chaserState.scaleX = (float)chaserState.facing;
                        chaserState.scaleY = 1f;
                        chaserState.dashDirectionX = 0;
                        chaserState.dashDirectionY = 0;
                    }
                    else
                    {
                        chaserState.scaleX = binaryReader.ReadSingle();
                        chaserState.scaleY = binaryReader.ReadSingle();
                        chaserState.dashDirectionX = binaryReader.ReadSingle();
                        chaserState.dashDirectionY = binaryReader.ReadSingle();
                    }
                    list.Add(chaserState);
                }
            }
            return list;
        }

        public static void ExportXML(List<ChaserState> list, string path)
        {
            using (FileStream fileWriter = File.OpenWrite(path))
            {
                var ser = new System.Xml.Serialization.XmlSerializer(typeof(List<ChaserState>));
                ser.Serialize(fileWriter, list);
            }
        }

        public static List<ChaserState> ImportXML(string path)
        {
            List<ChaserState> list = new List<ChaserState>();
            using (FileStream fileReader = File.OpenRead(path))
            {
                var ser = new System.Xml.Serialization.XmlSerializer(typeof(List<ChaserState>));
                list = (List<ChaserState>)ser.Deserialize(fileReader);
            }
            return list;
        }

    }

}
