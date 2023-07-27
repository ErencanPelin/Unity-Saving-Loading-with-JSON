# Unity-Saving-Loading-with-JSON
JSON file saving and loading system with unity to save and load generic types. 
Supports multiple save slots and profiles.

## Dependencies
- newtonsoft.json (any version)

## Usage
1. Create a custom save profile data object to save custom data:
```csharp
using SaveLoad;

[System.Serializable]
public record MySaveData : SaveProfileData
{
    public Vector3 position;
    public int currentLevel;
    public float health;
    public float xp;
}
```
2. Save values to the save profile data object:
```csharp
var saveData = new MySaveData
{
    position = transform.position,
}
//OR
var saveData = new MySaveData();
saveData.position = transform.position;
```

3. Save the data:
```csharp
saveData.SaveAs(
/*save name*/ "mySave", 
/*overrite existing save with same name?*/ false);
```

The repo also contains a sample save profile inside [Saves](./Saves.cs)
```csharp
using SaveLoad;

Saves.playerSaveData.position = transform.position;
Saves.playerSaveData.SaveAs("playerData", "false");
```

