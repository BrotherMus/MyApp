using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class NoteManager : MonoBehaviour
{
    public TextMeshProUGUI notesText;
    public TMP_InputField noteInput;

    private List<string> pages;
    private int currentPageIndex = 0;

    private void Start()
    {
        // Initialize the pages list and load existing notes on start
        pages = new List<string>();
        LoadNotes();
    }

    public void AddNote()
    {
        // Get the new note from the input field
        string newNote = noteInput.text;

        // Add the new note to the current page
        pages[currentPageIndex] += "\n" + newNote;

        // Save the updated notes
        PlayerPrefs.SetString("Notes", SerializePages());
        PlayerPrefs.Save();

        // Update the displayed notes
        LoadNotes();

        // Clear the input field
        noteInput.text = "";
    }

    public void SwitchPage(int pageIndex)
    {
        // Save the current page before switching
        pages[currentPageIndex] = notesText.text;

        // Switch to the new page
        currentPageIndex = pageIndex;

        // Load and display the notes for the new page
        LoadNotes();
    }

    void LoadNotes()
    {
        // Load existing notes and display them
        DeserializePages(PlayerPrefs.GetString("Notes", ""));
        notesText.text = pages[currentPageIndex];
    }

    string SerializePages()
    {
        // Serialize the list of pages into a single string for PlayerPrefs
        return string.Join("|", pages.ToArray());
    }

    void DeserializePages(string serializedPages)
    {
        // Deserialize the string into a list of pages
        string[] pageArray = serializedPages.Split('|');
        pages = new List<string>(pageArray);
    }
}
