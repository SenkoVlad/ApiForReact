namespace ApiForReact.Services
{
    public interface ITextGeneratorService
    {
        public string GenerateText(int length, int wordsCount = 1);
    }
}
