using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pestle : MonoBehaviour
{
    [SerializeField] private int _maxIngredients;
    [SerializeField] private Transform[] _dropPoints;
    [SerializeField] private UnityEngine.UI.Button[] _mixButtons;
    [SerializeField] private Image _progressBar;
    [SerializeField] private GameObject _progressBarGameObject;
    [SerializeField] private RecipeSO[] _recipes;

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
        EnableMix(false);
        _progressBarGameObject.SetActive(false);
    }

    private void Start()
    {
        //_recipes = FindObjectsByType<RecipeSO>(FindObjectsSortMode.None);
        foreach (RecipeSO recipe in _recipes)
            Debug.Log($"Recipe added: {recipe.Name}");
    }

    public void AddIngredient(InventoryItem ingredient)
    {
        if (Ingredients.Count < MaxIngredients)
        {
            _ingredients.Add(ingredient);
            EnableMix(true);
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
        EnableMix(false);
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

    private void EnableMix(bool enabled)
    {
        foreach (UnityEngine.UI.Button but in _mixButtons)
            but.interactable = enabled;
    }    

    public void PrepareInfusion()
    {
        MixIngredients(PreparationType.INFUSION);
    }
    public void PrepareDecoction()
    {
        MixIngredients(PreparationType.DECOCTION);
    }

    public void PrepareCompress()
    {
        MixIngredients(PreparationType.COMPRESS);
    }
    public void PrepareCream()
    {
        MixIngredients(PreparationType.CREAM);
    }
    public void PrepareLeaves()
    {
        MixIngredients(PreparationType.LEAVES);
    }
    public void PreparePotion()
    {
        MixIngredients(PreparationType.POTION);
    }
    public void PreparePoultice()
    {
        MixIngredients(PreparationType.POULTICE);
    }

    public void MixIngredients(PreparationType preparationType)
    {
        EnableMix(false);
        StartCoroutine(MixCoroutine(preparationType));
    }

    IEnumerator MixCoroutine(PreparationType preparationType)
    {
        Debug.Log($"Preparing {preparationType} ...");
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

        // If it is a valid recipe, instantiate object
        RecipeSO result = ValidateRecipe(preparationType);
        if (result)
        {
            Debug.Log($"{result.Name} created!!");
            _inventory.AddItem(result);
        }
        else
        {
            Debug.Log($"Preparation FAILED!");
        }

        // Consume ingredients
        ConsumeIngredients();
    }

    private RecipeSO ValidateRecipe(PreparationType preparationType)
    {
        Debug.Log($"Recipes: {_recipes.Length}");
        RecipeSO result = null;
        foreach(RecipeSO recipe in _recipes)
        {
            foreach(RecipeIngredient ing in recipe.Ingredients)
            {
                if (ing.Match(Ingredients) && recipe.PreparationType.Equals(preparationType))
                {
                    result = recipe;
                    break;
                }
            }                
        }
        return result;
    }


}
