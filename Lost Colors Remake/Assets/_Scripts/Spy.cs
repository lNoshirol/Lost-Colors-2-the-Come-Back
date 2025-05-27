using TMPro;
using UnityEngine;

public class Spy : MonoBehaviour
{
    [Header("Id")]
    public string cloudProjectId;
    public TextMeshProUGUI cloudProjectIdText;

    [Header("Compagny name")]
    public string compagnyName;
    public TextMeshProUGUI compagnyNameText;

    [Header("Identifier")]
    public string identifier;
    public TextMeshProUGUI identifierText;

    [Header("installer Name")]
    public string installerName;
    public TextMeshProUGUI installerNameText;

    [Header("installe mode")]
    public ApplicationInstallMode mode;
    public TextMeshProUGUI modeText;

    [Header("network reachability")]
    public NetworkReachability networkReachability;
    public TextMeshProUGUI networkReachabilityText;

    [Header("isMobilePlatform")]
    public bool isMobilePlatform;
    public TextMeshProUGUI mobilePlatformText;

    [Header("product name")]
    public string productName;
    public TextMeshProUGUI productNameText;

    [Header("system language")]
    public SystemLanguage language;
    public TextMeshProUGUI languageText;

    [Header("version")]
    public string version;
    public TextMeshProUGUI versionText;

    private void Awake()
    {
        cloudProjectId = Application.cloudProjectId;
        cloudProjectIdText.text = "Project id : " + Application.cloudProjectId;

        compagnyName = Application.companyName;
        compagnyNameText.text = "Company name : " + Application.companyName;
        
        identifier = Application.identifier;
        identifierText.text = "Identifier : " + Application.identifier;
        
        installerName = Application.installerName;
        installerNameText.text = "Installer name : " + Application.installerName;

        mode = Application.installMode;
        modeText.text = "Install mode : " + Application.installMode.ToString();

        networkReachability = Application.internetReachability;
        networkReachabilityText.text = "Network Reachability : " + Application.internetReachability.ToString();

        isMobilePlatform = Application.isMobilePlatform;
        mobilePlatformText.text = "isMobilePlatform : " + Application.isMobilePlatform.ToString() + " (of course it is)";
        
        productName = Application.productName;
        productNameText.text = "Product name : " + Application.productName;

        language = Application.systemLanguage;
        languageText.text = "System language : " + Application.systemLanguage.ToString();

        version = Application.version;
        versionText.text = "Version : " + Application.version;
    }
}
