using System;
using System.Collections;
using System.Linq;
using Abilities;
using Modifiers;
using UnityEngine;

public class ListController : MonoBehaviour
{
    public Ability playersAbilities;

    public ListItem listItem;

    private void Start()
    {
        playersAbilities.OnModifierChanged += UpdateList;
        StartCoroutine(UpdateListItemsCoroutine());
    }

    private IEnumerator UpdateListItemsCoroutine()
    {
        while (true)
        {
            foreach (Transform i in transform)
            {
                var item = i.GetComponent<ListItem>();
                item.bar.SetHealthBar((int) item.modifier.Remained, (int) item.modifier.Duration, false);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void UpdateList()
    {
        foreach (Transform child in transform)
            if (child.GetComponent<ListItem>().modifier.IsOver)
                Destroy(child.gameObject);

        var allChildren = GetComponentsInChildren<ListItem>();
        
        foreach (var modifier in playersAbilities.modifiers.Where(modifier => allChildren.All(c => c.modifier != modifier)))
            InstantNewListItem(modifier);
    }

    private void InstantNewListItem(Modifier modifier)
    {
        var instance = Instantiate(listItem, Vector3.one, Quaternion.identity, gameObject.transform);
        instance.modifier = modifier;
        instance.image.sprite = modifier.Icon;
        instance.bar.SetHealthBar((int) modifier.Remained, (int) modifier.Duration, true);
    }
}