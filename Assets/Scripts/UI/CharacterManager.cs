using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private CharacterData currentCharacter;
    private GameObject characterObject;
    private string characterName;

    public void SetNewCharacter(CharacterData data)
    {
        currentCharacter = data;
        characterObject = Instantiate(data.visuals, Vector3.zero, Quaternion.identity, this.transform);
        characterObject.transform.localPosition = Vector3.zero;
        characterObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
        characterName = currentCharacter.characterName;
    }
}
