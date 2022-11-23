using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;
namespace SnowGlobalConflict
{
    public enum MessageType
    {
        PrivateMessage = 0,
        PositionUpdate = 1,
        RotationUpdate = 2,
        PositionAndRotationUpdate = 3,
    }
    public abstract class Message
    {

        public abstract byte[] GetData { get; }
        //protected const int messageType = 0;//Instead of having const variables for the 'opertation type' just use an enum.
        protected int nonStringStuff = 3;//Only usable when sending a chatmessage.
        protected float floatSize = sizeof(float);
        protected int arrayShift = 3;

        public void ConvertFloatToByteArray(byte[] result, int shift, float number)
        {
            var x = BitConverter.GetBytes(number);//Changing each float in the vector3 of the position into a byte array then adding it to "result".
            for (int i = 0; i < x.Length; i++)
            {
                result[i + shift] = x[i];
            }
        }
    }

    public class PrivateChatMessage : Message
    {
        private byte senderID;
        private byte receiverID;
        private string message;

        public byte SenderID => senderID;

        public override byte[] GetData
        {
            get
            {
                var stringBytes = Encoding.UTF8.GetBytes(message);
                var result = new byte[stringBytes.Length + nonStringStuff];
                result[0] = (byte)MessageType.PrivateMessage;
                result[1] = senderID;
                result[2] = receiverID;

                for (int i = 0; i < stringBytes.Length; i++)
                {
                    result[i + 3] = stringBytes[i];//Adding all the stuff in the "stringBytes" array to the final "result" array.
                }

                //return stringBytes;??
                return result;
            }

        }

        public PrivateChatMessage(byte senderID, byte receiverID, string message)
        {
            this.senderID = senderID;
            this.receiverID = receiverID;
            this.message = message;
        }

        public PrivateChatMessage(byte[] message)
        {
            senderID = message[1];
            receiverID = message[2];

            this.message = Encoding.UTF8.GetString(message, 3, message.Length - 3);
        }
    }

    public class TransformUpdateMessage : Message
    {
        private Vector3 position;
        private Vector3 rotation;

        private bool positionSent;
        private bool rotationSent;

        private readonly byte senderID;
        private readonly byte objectID;
        public override byte[] GetData
        {
            get
            {

                return null;
            }
        }

        public TransformUpdateMessage(Vector3 position, Quaternion rotation, byte senderID, byte objectID)
        {
            this.position = position;
            this.rotation = rotation.eulerAngles;
            this.senderID = senderID;
            this.objectID = objectID;
            positionSent = true;
            rotationSent = true;
        }

        /* public TransformUpdateMessage(Vector3 position, byte senderID, byte objectID)
         {
             this.position = position;
             this.senderID = senderID;
             this.objectID = objectID;
             positionSent = true;
             rotationSent = false;
         }*/

        public TransformUpdateMessage(Quaternion rotation, byte senderID, byte objectID)
        {
            this.rotation = rotation.eulerAngles;
            this.senderID = senderID;
            this.objectID = objectID;
            positionSent = false;
            rotationSent = true;
        }

    }

    public class PositionUpdateMessage : Message
    {
        private Vector3 position;
        private byte senderID;
        private byte objectID;

        private bool positionSent;
        private bool rotationSent;

        public override byte[] GetData
        {
            get
            {
                arrayShift = 0;
                //var floatSize = sizeof(float);//"sizeof" is useful just incase you forget how many bytes a data type is.
                var result = new byte[3 + 3 * (int)floatSize];
                result[0] = (byte)MessageType.PositionUpdate;
                result[1] = senderID;
                result[2] = objectID;

                //var shift = 3;

                ConvertFloatToByteArray(result, arrayShift, position.x);
                arrayShift += (int)floatSize;//We need to update the shift amount so that new bytes do not get added to a place in the array thats already 'taken'.
                ConvertFloatToByteArray(result, arrayShift, position.y);
                arrayShift += (int)floatSize;
                ConvertFloatToByteArray(result, arrayShift, position.z);
                //arrayShift += (int)floatSize;

                return result;
            }
        }


        public PositionUpdateMessage(Vector3 position, byte senderID, byte objectID)
        {
            this.position = position;
            this.senderID = senderID;
            this.objectID = objectID;
            positionSent = true;
            rotationSent = false;
        }

        public PositionUpdateMessage(byte[] message)
        {
            this.senderID = message[1];
            this.objectID = message[2];

            this.position.x = BitConverter.ToSingle(message, arrayShift);
            this.position.y = BitConverter.ToSingle(message, arrayShift + (int)floatSize);
            this.position.z = BitConverter.ToSingle(message, arrayShift + ((int)floatSize * 2));
        }

    }

    public class RotationUpdateMessage : Message
    {
        private Vector3 rotation;
        private byte senderID;
        private byte objectID;
        public override byte[] GetData
        {
            get
            {
                arrayShift = 0;
                var result = new byte[3 + 3 * (int)floatSize];
                result[0] = (byte)MessageType.RotationUpdate;
                result[1] = senderID;
                result[2] = objectID;

                ConvertFloatToByteArray(result, arrayShift, rotation.x);
                arrayShift += (int)floatSize;
                ConvertFloatToByteArray(result, arrayShift, rotation.y);
                arrayShift += (int)floatSize;
                ConvertFloatToByteArray(result, arrayShift, rotation.z);
                //arrayShift += (int)floatSize;

                return result;
            }
        }

        public RotationUpdateMessage(Quaternion rotation, byte senderID, byte objectID)
        {
            this.rotation = rotation.eulerAngles;
            this.senderID = senderID;
            this.objectID = objectID;
        }
    }
}
