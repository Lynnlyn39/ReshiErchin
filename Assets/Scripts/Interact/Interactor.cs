using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _intreactionPoint;
    [SerializeField] private float _intreactionRadius;
    [SerializeField] private LayerMask _interactionMask;

    [SerializeField] private InteractionPromptUI _interactionPrompUI;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;

    public IInteractable interactable;
    public bool canInteract = false;

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_intreactionPoint.position, _intreactionRadius, _colliders, _interactionMask);

        if(_numFound > 0)
        {
            interactable = _colliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (!_interactionPrompUI.isDisplayed)
                    _interactionPrompUI.SetUp(interactable.interactionPrompt);

                canInteract = true;

            }
        }
        else
        {
            if (interactable != null)
            {
                interactable = null;
                canInteract = false;
            }

            if(_interactionPrompUI.isDisplayed)
                _interactionPrompUI.Close();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_intreactionPoint.position, _intreactionRadius);
    }

    public void Interact()
    {
    }
}
