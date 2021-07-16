namespace BlockchainSQL.Processing {
	public interface IDifficultyCalculator {
        float CalculateDifficulty(uint bits);
    }
}
