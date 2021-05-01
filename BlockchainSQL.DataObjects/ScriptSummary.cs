namespace BlockchainSQL.DataObjects
{
    public class ScriptSummary {
        public string Type { get; set; }

        public string Class { get; set; }

        public int Size { get; set; }

        public ScriptInstructionSummary[] Instructions { get; set; }
    }
}
