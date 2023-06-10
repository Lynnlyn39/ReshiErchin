using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pestle : MonoBehaviour
{
    [SerializeField] private int _maxIngredients;
    [SerializeField] private Transform[] _dropPoints;
    [SerializeField] private UnityEngine.UI.Button _mixButton;
    [SerializeField] private Image _progressBar;
    [SerializeField] private GameObject _progressBarGameObject;
    
    private List<InventoryItem> _ingredients;
    private Inventory _inventory;
    private int _nextDropPointIndex = 0;

    public Vector3 DropPoint => _dropPoints[_nextDropPointIndex++ % _dropPoints.Length].position;

    public List<InventoryItem> Ingredients => _ingredients;
    public int MaxIngredients => _maxIngredients;

    public bool IsFull => _ingredients.Count == MaxIngredients;

    private void Awake()
    {
        _ingredients = new List<InventoryItem>();
        _inventory = FindObjectOfType<Inventory>();
        _mixButton.interactable = false;
        _progressBarGameObject.SetActive(false);
    }

    public void AddIngredient(InventoryItem ingredient)
    {
        if (Ingredients.Count < MaxIngredients)
        {
            _ingredients.Add(ingredient);
            if (!_mixButton.interactable)
                _mixButton.interactable = true;
        }
    }

    public void ResetMix()
    {
        foreach (InventoryItem item in Ingredients)
        {
            _inventory.AddItem(item.Data);
            item.gameObject.SetActive(false);
            Destroy(item, 1f);
        }
        _ingredients.Clear();
        _mixButton.interactable = false;        
    }

    private void ConsumeIngredients()
    {
        foreach (InventoryItem item in Ingredients)
        {
            //item.gameObject.SetActive(false);
            Destroy(item.gameObject, 0.25f);
        }
        _ingredients.Clear();
    }

    public void MixIngredients()
    {
        _mixButton.interactable = false;
        StartCoroutine(MixCoroutine());
    }

    IEnumerator MixCoroutine()
    {
        _progressBarGameObject.SetActive(true);
        float duration = 5f;
        float time = 0f;
        _progressBar.fillAmount = 0;
        while (time < duration)
        {
            _progressBar.fillAmount = Mathf.Lerp(0, 1, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        _progressBar.fillAmount = 1f;
        yield return new WaitForSeconds(1f);
        _progressBarGameObject.SetActive(false);

        // Consume ingredients
        ConsumeIngredients();

        // If it is a valid recipe, instantiate object
        // TO-DO
    }

}
