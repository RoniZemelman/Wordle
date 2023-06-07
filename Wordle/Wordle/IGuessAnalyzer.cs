
namespace Wordle
{
     public interface IGuessAnalyzer
    {
        GuessResult Analyze(string guess);
    }
}
