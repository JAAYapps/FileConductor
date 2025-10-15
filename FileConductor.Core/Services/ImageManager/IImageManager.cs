using Zio;

namespace FileConductor.Core.Services.ImageManager;

public interface IImageManager
{
    public bool ImageExists { get; }
    
    public void CreateAssetImage(int span);

    public bool ExtractFile(string name, UPath destination);

    public bool ReplaceFile(string name, UPath source);
    
    public void ReadFile();
}