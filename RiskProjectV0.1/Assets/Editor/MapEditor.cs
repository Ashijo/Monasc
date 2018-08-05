using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapEditor : EditorWindow {
    static EditorWindow window;

    [MenuItem("Custom Tools/ MapEditor/ Create new Map")]
    private static void CreateNewMap() {
        window = GetWindow<CreateNewMap>();
    }

    [MenuItem("Custom Tools/ MapEditor/ Edit Maps")]
    public static void EditMaps() {
        window = GetWindow<EditMaps>();
    }

    public static void CloseWindow() {
        window.Close();
    }


}

public class EditMaps : EditorWindow {
    static string[] mapsNames;
    int mapIndex = 0;
    static Map[] maps;

    Texture colorTexture;
    GUIStyle centerStyle;


    int newHeight = 1;
    int newCaseType = 0;
    GV.CASETYPES newCaseTypeEnum = 0;
    bool spawnable;
    int team;


    void OnEnable() {
        maps = MapManager.Instance.GetAllMaps();
        colorTexture = EditorGUIUtility.whiteTexture;
    }

    private void OnGUI() {
        //I don't know why, but uniy force to init styl in OnGUI...
        centerStyle = GUI.skin.GetStyle("Label");
        centerStyle.alignment = TextAnchor.MiddleCenter;
        centerStyle.fontStyle = FontStyle.Bold;
        centerStyle.fontSize = 10;


        DoControls();
        DoCanvas();
    }


    void DoControls() {


        mapsNames = MapManager.Instance.GetAllMapsStr();


        GUILayout.BeginHorizontal();
        GUILayout.BeginVertical();
        GUILayout.Label("Select map", EditorStyles.boldLabel);
        mapIndex = GUILayout.SelectionGrid(mapIndex, mapsNames, 1, GUILayout.MaxWidth(150));

        GUILayout.Label("Cases", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();

        GUILayout.Label("Heigth : ");
        newHeight = EditorGUILayout.IntSlider(newHeight, 1, 3);

        GUILayout.EndHorizontal();

        string[] currentCasesTypes = new string[4];

        currentCasesTypes[3] = GV.CASETYPES.Canyon.ToString();

        switch (maps[mapIndex].type) {
            case GV.BATTLEFIELDTYPE.Desert:
                currentCasesTypes[0] = GV.CASETYPES.Sand.ToString();
                currentCasesTypes[1] = GV.CASETYPES.Oasis.ToString();
                currentCasesTypes[2] = GV.CASETYPES.Cactus.ToString();
                break;
            case GV.BATTLEFIELDTYPE.Temper:
                currentCasesTypes[0] = GV.CASETYPES.Grass.ToString();
                currentCasesTypes[1] = GV.CASETYPES.River.ToString();
                currentCasesTypes[2] = GV.CASETYPES.Oak.ToString();
                break;
            case GV.BATTLEFIELDTYPE.Ice:
                currentCasesTypes[0] = GV.CASETYPES.Snow.ToString();
                currentCasesTypes[1] = GV.CASETYPES.Ice.ToString();
                currentCasesTypes[2] = GV.CASETYPES.Fir.ToString();
                break;
        }

        newCaseType = GUILayout.SelectionGrid(newCaseType, currentCasesTypes, 1, GUILayout.MaxWidth(150));
        newCaseTypeEnum = Utils.Instance.StringToEnum<GV.CASETYPES>(currentCasesTypes[newCaseType]);

        spawnable = EditorGUILayout.Toggle("Is spawnable", spawnable);
        if (spawnable) {
            team = EditorGUILayout.IntSlider(team, 1, 3);
        }
        else {
            team = -1;
        }


        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Save current map")) {
            MapManager.Instance.SaveMap(maps[mapIndex]);
        }
        if (GUILayout.Button("Save all maps")) {
            for (int i = 0; i < maps.Length; i++) {
                MapManager.Instance.SaveMap(maps[i]);
            }
        }


        GUILayout.EndVertical();

    }

    void DoCanvas() {
        Event evt = Event.current;

        Color newColor = GUI.color;

        GUILayout.BeginVertical();

        for (int i = 0; i < maps[mapIndex].GetSize().y; ++i) {
            GUILayout.BeginHorizontal();

            for (int j = 0; j < maps[mapIndex].GetSize().x; ++j) {

                Vector2Int casePos = new Vector2Int(j, i);
                Rect colorRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true)); //Reserve a square, which will autofit to the size given


                if ((evt.type == EventType.MouseDown || evt.type == EventType.MouseDrag) && colorRect.Contains(evt.mousePosition)) //Can now paint while dragging update
                {
                    if (evt.button == 0) {    //If mouse button pressed is left
                        maps[mapIndex].UpdateCase(new Vector2Int(j, i), newCaseTypeEnum, newHeight, spawnable, team);
                    }

                    evt.Use();                        //The event was consumed, if you try to use event after this, it will be non-sensical
                }

                switch (maps[mapIndex].GetCaseType(casePos)) {
                    case GV.CASETYPES.DUMMY:
                        newColor = Color.magenta;
                        break;
                    case GV.CASETYPES.Rock:
                        newColor = Color.gray;
                        break;
                    case GV.CASETYPES.Canyon:
                        newColor = Color.black;
                        break;
                    case GV.CASETYPES.Sand:
                        newColor = Color.yellow;
                        break;
                    case GV.CASETYPES.Cactus:
                        newColor = Color.green;
                        break;
                    case GV.CASETYPES.Oasis:
                        newColor = Color.blue;
                        break;
                    case GV.CASETYPES.Snow:
                        newColor = Color.white;
                        break;
                    case GV.CASETYPES.Fir:
                        newColor = Color.green;
                        break;
                    case GV.CASETYPES.Ice:
                        newColor = Color.blue;
                        break;
                    case GV.CASETYPES.Grass:
                        newColor = new Color(.7f, 1f, 0f);
                        break;
                    case GV.CASETYPES.Oak:
                        newColor = Color.green;
                        break;
                    case GV.CASETYPES.River:
                        newColor = Color.blue;
                        break;
                    default:
                        break;
                }

                GUI.color = newColor;
                GUI.DrawTexture(colorRect, colorTexture);


                string text;
                text = maps[mapIndex].GetCaseHeight(casePos).ToString();
                if (maps[mapIndex].IsSpawnable(casePos)) {
                    text += "s";
                    if (maps[mapIndex].GetTeam(casePos) == -1) {
                        Debug.LogError("Team shouldn't be null here, please say it to the prog");
                    }
                    else {
                        text += maps[mapIndex].GetTeam(casePos);
                    }
                }

                GUI.TextField(colorRect, text, centerStyle);
            }
            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();
        GUILayout.EndHorizontal();
    }
}



public class CreateNewMap : EditorWindow {
        Vector2Int Size = new Vector2Int(0, 0);
        string Name;

        int MapKindInt = 0;



        private void OnGUI() {
            GUILayout.BeginVertical();

            GUILayout.Label("Create a new map", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Name : ", EditorStyles.boldLabel);
            Name = GUILayout.TextArea(Name, EditorStyles.textArea);

            GUILayout.EndHorizontal();

            Size.x = EditorGUILayout.IntSlider(Size.x, 1, 20);
            Size.y = EditorGUILayout.IntSlider(Size.y, 1, 20);

            string[] mapKind = { "Desert map", "Temp map", "Ice Map" };

            MapKindInt = GUILayout.SelectionGrid(MapKindInt, mapKind, 3);




            if (GUILayout.Button("Create")) {
                if (Name != "") {
                    MapManager.Instance.CreateNewMap(Name, Size, (GV.BATTLEFIELDTYPE)(MapKindInt + 1));
                    MapEditor.EditMaps();
                    this.Close();
                }
                else {
                    Debug.LogError("Please choose a name");
                }
            }

            GUILayout.EndVertical();
        }

}
