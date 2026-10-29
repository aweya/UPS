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
        characterName = currentCharacter.characterName;
    }
}
