using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Assets.CleanGameArchitecture.Scripts.Manager
{
    public class ComputerManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] Screens;

        public void OpenScreen(string name)
        {
            foreach (var screen in Screens)
            {
                screen.SetActive(false);
            }

            Screens.FirstOrDefault(x => x.name == name).SetActive(true);
        }
    }
}
