using System.Linq;

namespace BlockchainSQL.Processing {

	/// <summary>
	/// Used for segwit testing/diagnostics
	/// </summary>
	internal class SegwitBlockLocator : BlockLocator {
		public override BlockLocators GetBlockLocators() {
			const long SegwitStartBlock = 481824;
			const long PreSegwitRange = 1000;
			const long StartBlock = SegwitStartBlock - PreSegwitRange;
			long[] indices =  DetermineBlockLocatorIndices(StartBlock);
			
			return new BlockLocators {
				Locations = base.DAC.GetBlockLocators(indices).ToArray()
			};
		}

	}
}
