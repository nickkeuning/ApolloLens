using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections;
using HoloToolkit.Unity.InputModule;

#if WINDOWS_UWP
using Windows.Storage;
using Windows.Storage.Streams;
#endif


public class GalleryControl : MonoBehaviour {

    public RawImage Image;

    public GameObject Dropdown;
    private Dropdown DropdownScript;

    public GameObject GalleryKeyword;
    private SpeechInputSource GalleryKeywordScript;


    private List<string> SubFolderNames;
    private string CurrentFolder;
    private int CurrentIndex;
    private Dictionary<string, int> IndexInFolder = new Dictionary<string, int>();

    private IEnumerator ScrollCoroutine;

#if WINDOWS_UWP
    private Dictionary<string, List<StorageFile>> ImageFileDict = new Dictionary<string, List<StorageFile>>();
#endif

    // Use this for initialization
    void Start () {
        Initialize();
        GalleryKeywordScript = GalleryKeyword.GetComponent<SpeechInputSource>();
    }

    // Update is called once per frame
    void Update() { }

    private void OnEnable()
    {
        GalleryKeywordScript.StartKeywordRecognizer();
    }

    private void OnDisable()
    {
        GalleryKeywordScript.StopKeywordRecognizer();
    }


    void SetupDropdown()
    {
        DropdownScript = Dropdown.GetComponent<Dropdown>();
        DropdownScript.ClearOptions();

        foreach (string SubFolderName in SubFolderNames)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData();
            optionData.text = SubFolderName;
            DropdownScript.options.Add(optionData);

            IndexInFolder[SubFolderName] = 0;
        }

        DropdownScript.RefreshShownValue();
    }

    public void ChangeDirectoryDropdown()
    {
        string DirectoryName = SubFolderNames[DropdownScript.value];
        ChangeDirectory(DirectoryName);
    }

    public void ChangeDirectoryKeyword(string DirectoryName)
    {
        DropdownScript.value = SubFolderNames.IndexOf(DirectoryName);
        DropdownScript.RefreshShownValue();
        ChangeDirectory(DirectoryName);        
    }

    private void ChangeDirectory(string DirectoryName)
    {
        if (DirectoryName != CurrentFolder)
        {
            IndexInFolder[CurrentFolder] = CurrentIndex;
            CurrentIndex = IndexInFolder[DirectoryName];
            CurrentFolder = DirectoryName;
            SetTexture();
        }
    }

    public void StartHold(int Direction)
    {
        ScrollCoroutine = ScrollMethod(Direction);
        StartCoroutine(ScrollCoroutine);
    }


    public void StopHold()
    {
        StopCoroutine(ScrollCoroutine);
    }

    private IEnumerator ScrollMethod(int Direction)
    {
        float WaitTime = 0.3f;
        int NumImages = 0;
        while (true)
        {
            MoveImage(Direction);
            NumImages++;
            if (NumImages == 5)
            {
                WaitTime /= 2;
            }
            yield return new WaitForSeconds(WaitTime);
        }
    }


    public void MoveImage(int Amount)
    {
#if WINDOWS_UWP
        // adjust current index by specified amount
        int NewIndex = CurrentIndex + Amount;

        // constrain index within bounds
        int numImages = ImageFileDict[CurrentFolder].Count();
        NewIndex = (NewIndex < 0) ? 0 : NewIndex;
        NewIndex = (NewIndex >= numImages) ? numImages - 1 : NewIndex;

        // set the image texture to newly indexed image if needed
        if (NewIndex != CurrentIndex)
        {
            CurrentIndex = NewIndex;
            SetTexture();
        }
#endif
    }

    public void FirstImage()
    {
        CurrentIndex = 0;
        SetTexture();
    }

    public void LastImage()
    {
#if WINDOWS_UWP
        CurrentIndex = ImageFileDict[CurrentFolder].Count - 1;
        SetTexture();
#endif
    }

    public void MiddleImage()
    {
#if WINDOWS_UWP
        CurrentIndex = ImageFileDict[CurrentFolder].Count / 2;
        SetTexture();
#endif
    }


    private async void Initialize()
    {
#if WINDOWS_UWP
        await GetFiles();
        SetTexture();
        SetupDropdown();
#endif
    }

    private async Task GetFiles(){
#if WINDOWS_UWP
        // get and store all subfolders of Pictures/Surgery
        StorageFolder SurgeryFolder = await KnownFolders.PicturesLibrary.GetFolderAsync("Surgery");
        var SubFolders = await SurgeryFolder.GetFoldersAsync();
        SubFolderNames = SubFolders.Select(f => f.Name).ToList();

        // get and store all files in each subdirectory
        foreach (StorageFolder folder in SubFolders)
        {
            var ImageFiles = await folder.GetFilesAsync();
            if (ImageFiles.Count != 0)
            {
                ImageFileDict[folder.Name] = ImageFiles.ToList();
            }            
        }

        // set current folder and image index within folder
        CurrentFolder = SubFolderNames.First();
        CurrentIndex = 0;
#endif
    }

    private async void SetTexture()
    {
#if WINDOWS_UWP
        // get the storage file corresponding to current image
        StorageFile ImageFile = ImageFileDict[CurrentFolder][CurrentIndex];

        // get byte array for image
        byte[] ImageBytes = await GetBytesAsync(ImageFile);

        // create texture2d from byte array
        Texture2D ImageTexture = new Texture2D(2, 2);
        ImageTexture.LoadImage(ImageBytes);

        // set image texture to newly created texture
        Destroy(Image.texture);
        Image.texture = ImageTexture;
#endif
    }

#if WINDOWS_UWP
    private static async Task<byte[]> GetBytesAsync(StorageFile file)
    {
        IBuffer buffer = await FileIO.ReadBufferAsync(file);
        return buffer.ToArray();
    }
#endif

}