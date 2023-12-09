using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class BankManager : MonoBehaviour
{
    //public TextMeshProUGUI bankText;
    public GameObject bankDataPrefab;
    public Transform bankDataContainer;

    public TMP_InputField banknameInputField;
    public TMP_InputField bankmoneyInputField;


    // List to store bank data
    public List<BankData> bankDataList = new List<BankData>();
    private void Start()
    {
        LoadBankData();
    }
    public void Add()
    {
        AddBank(banknameInputField.text,float.Parse(bankmoneyInputField.text));
        SaveBankData();
    }
    // Function to add a new bank
    public void AddBank(string bankName, float money)
    {
        // Check if the bank with the same name already exists
        BankData existingBank = bankDataList.Find(bank => bank.bankName == bankName);

        if (existingBank != null)
        {
            // If the bank exists, update its data
            existingBank.money = money;
        }
        else
        {
            // If the bank doesn't exist, add a new bank
            GameObject newBankObject = Instantiate(bankDataPrefab, bankDataContainer);
            BankData newBankData = newBankObject.GetComponent<BankData>();

            newBankData.bankName = bankName;
            newBankData.money = money;

            //newBankData.bankTextUI = bankText;
            bankDataList.Add(newBankData);
        }

        // Update the UI
        UpdateUI();
    }

    // Function to update the UI
    private void UpdateUI()
    {
        // Clear the current UI text
        //bankText.text = "";

        // Update the UI text for each bank data
        foreach (BankData bankData in bankDataList)
        {
            bankData.UpdateUI();
        }
    } // Save bank data to PlayerPrefs
    private void SaveBankData()
    {
        for (int i = 0; i < bankDataList.Count; i++)
        {
            PlayerPrefs.SetString("BankName_" + i, bankDataList[i].bankName);
            PlayerPrefs.SetFloat("BankMoney_" + i, bankDataList[i].money);
        }
        PlayerPrefs.SetInt("BankCount", bankDataList.Count);
        PlayerPrefs.Save();
    }

    // Load bank data from PlayerPrefs
    private void LoadBankData()
    {
        int bankCount = PlayerPrefs.GetInt("BankCount", 0);

        for (int i = 0; i < bankCount; i++)
        {
            string bankName = PlayerPrefs.GetString("BankName_" + i, "");
            float bankMoney = PlayerPrefs.GetFloat("BankMoney_" + i, 0f);
            AddBank(bankName, bankMoney);
        }
    }
    // Function to delete an existing bank
    public void DeleteBank(BankData bankData)
    {
        if (bankData != null)
        {
            // Remove PlayerPrefs entries associated with the deleted bank
            PlayerPrefs.DeleteKey("BankName_" + bankDataList.IndexOf(bankData));
            PlayerPrefs.DeleteKey("BankMoney_" + bankDataList.IndexOf(bankData));

            // Remove the bank from the list
            bankDataList.Remove(bankData);

            // Destroy the GameObject associated with this BankData
            Destroy(bankData.gameObject);

            SaveBankData();
        }
    }
}
