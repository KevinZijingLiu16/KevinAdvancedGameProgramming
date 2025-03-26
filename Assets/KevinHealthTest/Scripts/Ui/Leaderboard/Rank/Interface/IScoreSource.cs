

public interface IScoreSource
{
    //this interface is used to get the scores from the source. in our case is the UI, but can be from a database in future.
    int[] GetScores();
}
