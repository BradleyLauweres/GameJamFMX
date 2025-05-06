using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CleanGameArchitecture.Scripts.Entities
{
    public class ItemHolder : MonoBehaviour
    {
        [Header("UI Feedback")]
        [SerializeField] private GameObject interactionPrompt;
        [SerializeField] private TMPro.TextMeshProUGUI promptText;

        public float pickupRange = 2f;
        public Transform holdPosition;

        private IHoldable currentHeldItem = null;
        private GameObject currentHeldObject = null;

        void Start()
        {
            if (holdPosition == null)
            {
                Debug.LogWarning("No hold position assigned to ItemHolder. Creating one at default position.");
                GameObject holdPositionObj = new GameObject("HoldPosition");
                holdPositionObj.transform.SetParent(transform);
                holdPositionObj.transform.localPosition = new Vector3(0.5f, 0f, 0.8f);
                holdPosition = holdPositionObj.transform;
            }
        }

        void Update()
        {
         
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentHeldItem == null)
                {
                    TryPickupItem();
                }
                else
                {
                    DropItem();
                }
            }

            CheckForPrompt();

        }

        public bool IsHoldingItem()
        {
            return currentHeldItem != null;
        }

        public GameObject GetHeldItem()
        {
            return currentHeldObject;
        }

        void CheckForPrompt()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, pickupRange))
            {
                if (hit.collider.gameObject.CompareTag("Pickable"))
                {
                    if (interactionPrompt != null)
                    {
                        interactionPrompt.SetActive(true);

                        if (promptText != null)
                        {
                            promptText.text = $"Press E to pickup";
                        }
                    }
                }
            }
            else
            {
                if (interactionPrompt != null)
                    interactionPrompt.SetActive(false);
            }
        }

        void TryPickupItem()
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, pickupRange))
            {
                interactionPrompt.SetActive(false);

                IHoldable holdableItem = hit.collider.GetComponent<IHoldable>();

                if (holdableItem != null)
                {
                    currentHeldItem = holdableItem;
                    currentHeldObject = hit.collider.gameObject;

                    holdableItem.OnPickup(this);

                    currentHeldObject.transform.SetParent(Camera.main.transform);
                    currentHeldObject.transform.position = holdPosition.position;
                    currentHeldObject.transform.rotation = holdPosition.rotation;

                    Collider itemCollider = currentHeldObject.GetComponent<Collider>();
                    if (itemCollider != null)
                    {
                        itemCollider.enabled = false;
                    }

                    Rigidbody itemRigidbody = currentHeldObject.GetComponent<Rigidbody>();
                    if (itemRigidbody != null)
                    {
                        itemRigidbody.isKinematic = true;
                    }
                }
            }
        }

        void DropItem()
        {
            if (currentHeldItem != null)
            {
                currentHeldItem.OnDrop();

                currentHeldObject.transform.SetParent(null);

                Collider itemCollider = currentHeldObject.GetComponent<Collider>();
                if (itemCollider != null)
                {
                    itemCollider.enabled = true;
                }

                Rigidbody itemRigidbody = currentHeldObject.GetComponent<Rigidbody>();
                if (itemRigidbody != null)
                {
                    itemRigidbody.isKinematic = false;
                    itemRigidbody.AddForce(transform.forward * 2f, ForceMode.Impulse);
                }

                Debug.Log("Dropped: " + currentHeldItem.GetItemName());

                currentHeldItem = null;
                currentHeldObject = null;
            }
        }
    }
}
