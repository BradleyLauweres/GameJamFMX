using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CleanGameArchitecture.Scripts.Interfaces
{
    public interface IItemWithImages
    {
        Sprite[] GetImages();
        string GetImageDescription(int index);
    }
}
