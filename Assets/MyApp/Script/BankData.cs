using UnityEngine;
using TMPro;

public class BankData : MonoBehaviour
{
    public string bankName;
    public float money;
    public TextMeshProUGUI bankTextUI;

    public BankManager bankManager; // Reference to the BankManager
    private void Start()
    {
        // Find the BankManager in the scene
        bankManager = GameObject.FindObjectOfType<BankManager>();

        // Check if the reference is still null
        if (bankManager == null)
        {
            Debug.LogError("BankManager not found in the scene. Make sure it is present.");
        }
    }
    public void Update()
    {
        // Use a format string to display the floating-point number with two decimal places
        bankTextUI.text = $"{bankName}: RM{money.ToString("F2")}";
    }
    // Update UI text using TextMeshPro
    public void UpdateUI()
    {
        // Use a format string to display the floating-point number with two decimal places
        bankTextUI.text = $"{bankName}: RM{money.ToString("F2")}";
    }
    public void DeleteBank()
    {        // Call the DeleteBank function in BankManager
        if (bankManager != null)
        {
            bankManager.DeleteBank(this);
        }
    }
}
