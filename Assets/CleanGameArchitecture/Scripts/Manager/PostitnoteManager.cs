using Assets.CleanGameArchitecture.Scripts.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostitNoteManager : MonoBehaviour
{
    private static PostitNoteManager _instance;

    public static PostitNoteManager Instance {  get { return _instance; } }

    [SerializeField] public List<Placeholder> placeholders = new List<Placeholder>();
    [SerializeField] private List<PostitNote> postitNotes = new List<PostitNote>();
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float moveSpeed = 10f;

    private GameObject currentlyMovingNote;
    private bool isNoteMoving = false;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private Queue<MoveOperation> moveQueue = new Queue<MoveOperation>();

    private class MoveOperation
    {
        public GameObject noteObject;
        public Vector3 targetPosition;
        public Quaternion targetRotation;
    }

    private void Start()
    {
        if(_instance == null)
            _instance = this;

        if (mainCamera == null)
            mainCamera = Camera.main;

        foreach (var note in postitNotes)
        {
            note.originalPosition = note.gameObject.transform.position;
            note.originalRotation = note.gameObject.transform.rotation;
            note.isInPlaceholder = false;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isNoteMoving)
        {
            HandleMouseClick();
        }

        if (!isNoteMoving && moveQueue.Count > 0)
        {
            MoveOperation op = moveQueue.Dequeue();
            currentlyMovingNote = op.noteObject;
            targetPosition = op.targetPosition;
            targetRotation = op.targetRotation;
            isNoteMoving = true;
        }

        if (isNoteMoving && currentlyMovingNote != null)
        {
            MoveNoteToTarget();
        }
    }

    private void HandleMouseClick()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject clickedObject = hit.collider.gameObject;

            PostitNote clickedNote = FindPostitNote(clickedObject);
            if (clickedNote != null)
            {
                Placeholder targetPlaceholder = placeholders.Find(p => p.type == clickedNote.type);
                if (targetPlaceholder != null)
                {
                    if (clickedNote.isInPlaceholder && targetPlaceholder.currentNote == clickedNote.gameObject)
                    {
                        ReturnNoteToOriginalPosition(clickedNote);
                        targetPlaceholder.currentNote = null;
                        return;
                    }

                    if (targetPlaceholder.currentNote != null && targetPlaceholder.currentNote != clickedNote.gameObject)
                    {
                        PostitNote currentNote = FindPostitNote(targetPlaceholder.currentNote);
                        if (currentNote != null)
                        {
                            ReturnNoteToOriginalPosition(currentNote);
                        }
                    }

                    MoveNoteToPlaceholder(clickedNote, targetPlaceholder);
                }
            }
        }
    }

    private void MoveNoteToPlaceholder(PostitNote note, Placeholder placeholder)
    {
        MoveOperation op = new MoveOperation
        {
            noteObject = note.gameObject,
            targetPosition = placeholder.transform.position,
            targetRotation = placeholder.transform.rotation
        };

        moveQueue.Enqueue(op);
        note.isInPlaceholder = true;
        placeholder.currentNote = note.gameObject;
    }

    private void ReturnNoteToOriginalPosition(PostitNote note)
    {
        MoveOperation op = new MoveOperation
        {
            noteObject = note.gameObject,
            targetPosition = note.originalPosition,
            targetRotation = note.originalRotation
        };

        moveQueue.Enqueue(op);
        note.isInPlaceholder = false;

        foreach (var placeholder in placeholders)
        {
            if (placeholder.currentNote == note.gameObject)
            {
                placeholder.currentNote = null;
                break;
            }
        }
    }

    private void MoveNoteToTarget()
    {
        Transform noteTransform = currentlyMovingNote.transform;

        noteTransform.position = Vector3.Lerp(noteTransform.position, targetPosition, Time.deltaTime * moveSpeed);
        noteTransform.rotation = Quaternion.Slerp(noteTransform.rotation, targetRotation, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(noteTransform.position, targetPosition) < 0.01f)
        {
            noteTransform.position = targetPosition;
            noteTransform.rotation = targetRotation;
            isNoteMoving = false;
            currentlyMovingNote = null;
        }
    }

    private PostitNote FindPostitNote(GameObject obj)
    {
        return postitNotes.Find(note => note.gameObject == obj);
    }
}