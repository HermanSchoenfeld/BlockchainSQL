using System;
using NBitcoin.DataEncoders;

namespace BlockchainSQL.Processing {
	public static class Bech32Helper {
		public static bool IsValidAddress(string adr) {
			try {
				Encoders.Bech32("bc").Decode(adr, out _);
				return true;
			} catch (FormatException) {
				return false;
			}
		}
	}
}
