using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GunSystem
{
    [CreateAssetMenu(fileName = "new BaseGun", menuName = "Guns/BaseGun", order = 0)]
    public abstract class BaseGun : SerializedScriptableObject
    {

        public GameObject gunMesh;

        public Dictionary<AttributeType, AttribProp> attributes;

        public Dictionary<AttachmentType , List<GunAttachment>> availableAttachments;

        [ReadOnly] public Dictionary<AttachmentType,GunAttachment> equippedAttachments;

        [Button("Button")]
        private void Awake()
        {
            equippedAttachments = new Dictionary<AttachmentType, GunAttachment>();
            foreach (var i in availableAttachments.Where(i => !equippedAttachments.ContainsKey(i.Key)))
            {
                equippedAttachments.Add(i.Key,null);
            }
            
            foreach (var i in availableAttachments)
            {
                if (!equippedAttachments.ContainsKey(i.Key))
                {
                    equippedAttachments.Add(i.Key,null);
                }
            }
        }
        
        public void AddAttachment(AttachmentType type,GunAttachment newAttachment)
        {
            if (equippedAttachments.ContainsKey(type))
            {
                equippedAttachments.Remove(type);
            }

            equippedAttachments.Add(type,newAttachment);
            UpdateStatOnAttachment();
        }

        public void RemoveAttachment(AttachmentType type)
        {
            GunAttachment removedAttachment =  null;
            if (equippedAttachments.ContainsKey(type))
            {
                removedAttachment = equippedAttachments[type];
                equippedAttachments.Remove(type);
            }
            Debug.Log($"Removing attachment : {removedAttachment}");
            UpdateStatOnAttachment();
        }

        private Dictionary<AttributeType, List<ModDetails>> UpdateStatOnAttachment()
        {
            Dictionary<AttributeType, List<ModDetails>> dictUpdatingAttributes = new Dictionary<AttributeType, List<ModDetails>>();

            foreach (var i in equippedAttachments)
            {
                GunAttachment attachment = i.Value;
                AttachmentType type = i.Key;
                foreach (var current in attachment.attachmentDetails)
                {
                    //add each attribute type to its own specific list
                    if (!dictUpdatingAttributes.ContainsKey(current.Key))
                    {
                        dictUpdatingAttributes.Add(current.Key,new List<ModDetails>());
                        dictUpdatingAttributes[current.Key].Add(current.Value);
                    }
                    else
                    {
                        dictUpdatingAttributes[current.Key].Add(current.Value);
                    }
                }
            }
            return dictUpdatingAttributes;
        }

        private void UpdateValue()
        {
            var x = UpdateStatOnAttachment();
            foreach (var i in x)
            {
                var newList = i.Value.OrderBy(c => (int) c.modType);
                AttributeType currentAttributeType = i.Key;
                
                if(!attributes.ContainsKey(currentAttributeType))
                    continue;
                
                float currentValue = attributes[currentAttributeType].baseValue;;
                
                foreach (var modDetails in newList)
                {
                    switch (modDetails.modType)
                    {
                        case ModifierType.Flat:
                            currentValue = modDetails.updatingValue;
                            break;
                        case ModifierType.Multiplicative:
                            currentValue *= modDetails.updatingValue;
                            break;
                        case ModifierType.Additive:
                            currentValue += modDetails.updatingValue;
                            break;
                        default : 
                            throw new Exception("Mod Type is wrong");
                    }
                } //double checking the if, as might add the stat above later

                if (attributes.ContainsKey(currentAttributeType))    
                    attributes[currentAttributeType].currentValue = currentValue;
                
                //above vs below
                
                if (attributes.ContainsKey(currentAttributeType))   
                {
                    if (attributes[currentAttributeType].currentValue != currentValue)
                        attributes[currentAttributeType].currentValue = currentValue;
                }
            }
        }

        protected abstract void Fire();
    }
}
