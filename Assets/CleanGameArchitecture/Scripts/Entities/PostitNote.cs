using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CleanGameArchitecture.Scripts.Entities
{
    [System.Serializable]
    public class PostitNote
    {
        public GameObject gameObject;
        public Vector3 originalPosition;
        public Quaternion originalRotation;
        public string type;
        public bool isInPlaceholder = false;
    }
}
