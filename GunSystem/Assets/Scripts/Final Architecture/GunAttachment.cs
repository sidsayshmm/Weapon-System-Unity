using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GunSystem
{
    [CreateAssetMenu(fileName = "new GunAttachment", menuName = "Guns/Attachment/GunAttachment", order = 0)]
    public class GunAttachment : SerializedScriptableObject
    {
        public Dictionary<AttributeType, ModDetails> attachmentDetails;
    }
}