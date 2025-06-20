/*using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(RandomEventManager))]
public class RandomEventManagerEditor : Editor
{
    public SerializedProperty Events;
    private ReorderableList EventsList;

    private void OnEnable()
    {
        // Step 1 "link" the SerializedProperties to the properties of YourOtherClass
        Events = serializedObject.FindProperty(nameof(RandomEventManager.Events));

        EventsList = new ReorderableList(serializedObject, Events)
        {
            // Can your objects be dragged an their positions changed within the List?
            draggable = true,
            // Can you add elements by pressing the "+" button?
            displayAdd = true,
            // Can you remove Elements by pressing the "-" button?
            displayRemove = true,

            // Make a header for the list
            drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "This are your Elements");
            },

            // Now to the interesting part: Here you setup how elements look like
            drawElementCallback = (rect, index, active, focused) =>
            {
                // Get the currently to be drawn element from YourList
                var element = Events.GetArrayElementAtIndex(index);

                // Get the elements Properties into SerializedProperties
                var Weight = element.FindPropertyRelative(nameof(RandomEvent.Weight));
                var OnEventChosen = element.FindPropertyRelative(nameof(RandomEvent.OnEventChosen));

                // Draw the Weight field
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), Weight);
                // start the next property in the next line
                rect.y += EditorGUIUtility.singleLineHeight;

                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), OnEventChosen);
            },

            //set height of editor.
            elementHeight = EditorGUIUtility.singleLineHeight * 10,

            // optional: Set default Values when adding a new element
            // (otherwise the values of the last list item will be copied)
            onAddCallback = list =>
            {
                // The new index will be the current List size ()before adding
                var index = list.serializedProperty.arraySize;

                // Since this method overwrites the usual adding, we have to do it manually:
                // Simply counting up the array size will automatically add an element
                list.serializedProperty.arraySize++;
                list.index = index;
                var element = list.serializedProperty.GetArrayElementAtIndex(index);

                if (element != null)
                {
                    // again link the properties of the element in SerializedProperties
                    var Weight = element.FindPropertyRelative(nameof(RandomEvent.Weight));
                    var OnEventChosen = element.FindPropertyRelative(nameof(RandomEvent.OnEventChosen));

                    // and set default values
                    Weight.intValue = 0;
                    // OnEventChosen.stringValue = "";
                }
            }
        };
    }

    public override void OnInspectorGUI()
    {
        // copy the values of the real Class to the linked SerializedProperties
        serializedObject.Update();

        // print the reorderable list
        EventsList.DoLayoutList();

        // apply the changed SerializedProperties values to the real class
        serializedObject.ApplyModifiedProperties();
    }
}*/