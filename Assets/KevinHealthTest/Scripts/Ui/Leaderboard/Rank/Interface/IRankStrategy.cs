public interface IRankStrategy
{ //This interface is used to calculate the ranks of the scores. The DefaultRankStrategy class implements this interface by ranking the scores in descending order. but can add more strategies in the future like KDA, etc.
    int[] CalculateRanks(int[] scores);
}
