using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMonitorController : MonoBehaviour
{

    public string firstRow = "HELLO";
    public string secondRow = "WORLD";

    string CHARS_IN_FONT = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789c";

    public GameObject charToDuplicate;

    Sprite[] resourceLoadedFontCharacterSprites;

    List<GameObject> instantiatedCharacters = new List<GameObject>();

    void Awake()
    {
        resourceLoadedFontCharacterSprites = Resources.LoadAll<Sprite>("font");
        // for (int i = 0; i < resourceLoadedFontCharacterSprites.Length; i++) {
        //     Debug.Log(resourceLoadedFontCharacterSprites[i]);
        // }
        SetText(firstRow, secondRow);
    }

    void SetText(string firstRow, string secondRow) {
        // Clear previous text
        for (int i = 0; i < instantiatedCharacters.Count; i++) {
            Destroy(instantiatedCharacters[i]);
        }
        instantiatedCharacters.Clear();

        // Set the first row of text
        for (int i = 0; i < firstRow.Length; i++) {
            int spriteIndex = CHARS_IN_FONT.IndexOf(firstRow[i]);
            Vector3 startPosition = this.transform.position + new Vector3(
                (float) (-(firstRow.Length / 2.0) * 0.375 + 0.25),
                (float) (0.5 * 0.375),
                0f
            );
            GameObject fontCharacter = GameObject.Instantiate(
                charToDuplicate,
                startPosition + new Vector3((float) i * 0.375f, 0f, 0f),
                Quaternion.identity
            ) as GameObject;
            SpriteRenderer characterSpriteRenderer = fontCharacter.transform.GetComponent<SpriteRenderer>();
            characterSpriteRenderer.sprite = resourceLoadedFontCharacterSprites[spriteIndex];
            characterSpriteRenderer.enabled = true;
            instantiatedCharacters.Add(fontCharacter);
        }

        // Set the second row of text
        for (int i = 0; i < secondRow.Length; i++) {
            int spriteIndex = CHARS_IN_FONT.IndexOf(secondRow[i]);
            Vector3 startPosition = this.transform.position + new Vector3(
                (float) (-(secondRow.Length / 2.0) * 0.375 + 0.25),
                (float) (-0.5 * 0.375),
                0f
            );
            GameObject fontCharacter = GameObject.Instantiate(
                charToDuplicate,
                startPosition + new Vector3((float) i * 0.375f, 0f, 0f),
                Quaternion.identity
            ) as GameObject;
            SpriteRenderer characterSpriteRenderer = fontCharacter.transform.GetComponent<SpriteRenderer>();
            characterSpriteRenderer.sprite = resourceLoadedFontCharacterSprites[spriteIndex];
            characterSpriteRenderer.enabled = true;
            instantiatedCharacters.Add(fontCharacter);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
