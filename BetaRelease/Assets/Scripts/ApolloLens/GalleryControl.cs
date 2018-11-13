using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices.WindowsRuntime;


#if WINDOWS_UWP
using Windows.Storage;
using Windows.Storage.Streams;
#endif


public class GalleryControl : MonoBehaviour {

    public RawImage Image;

    private List<string> SubFolderNames;
    private string CurrentFolder;
    private int CurrentIndex;

#if WINDOWS_UWP
    private Dictionary<string, List<StorageFile>> ImageFileDict = new Dictionary<string, List<StorageFile>>();
#endif

    // Use this for initialization
    void Start () {
#if WINDOWS_UWP
        Initialize();
#endif
    }

    // Update is called once per frame
    void Update() { }

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

#if WINDOWS_UWP
    private async void Initialize()
    {
        // get and store all subfolders of Pictures/Surgery
        StorageFolder SurgeryFolder = await KnownFolders.PicturesLibrary.GetFolderAsync("Surgery");
        var SubFolders = await SurgeryFolder.GetFoldersAsync();
        SubFolderNames = SubFolders.Select(f => f.Name).ToList();

        // get and store all files in each subdirectory
        foreach (StorageFolder folder in SubFolders)
        {
            var ImagesFiles = await folder.GetFilesAsync();
            ImageFileDict[folder.Name] = ImagesFiles.ToList();
        }

        // set current folder and image index within folder
        CurrentFolder = SubFolderNames.First();
        CurrentIndex = 0;

        // set image to current image
        SetTexture();
    }

    private async void SetTexture()
    {
        // get the storage file corresponding to current image
        StorageFile ImageFile = ImageFileDict[CurrentFolder][CurrentIndex];

        // get byte array for image
        byte[] ImageBytes = await GetBytesAsync(ImageFile);

        // create texture2d from byte array
        Texture2D ImageTexture = new Texture2D(2, 2);
        ImageTexture.LoadImage(ImageBytes);

        // set image texture to newly created texture
        Image.texture = ImageTexture;
    }

    private static async Task<byte[]> GetBytesAsync(StorageFile file)
    {
        IBuffer buffer = await FileIO.ReadBufferAsync(file);
        byte[] bytes = buffer.ToArray();
        return bytes;
    }
#endif
}