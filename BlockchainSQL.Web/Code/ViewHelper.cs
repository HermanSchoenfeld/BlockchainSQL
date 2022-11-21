using System.Data;
using BlockchainSQL.DataObjects;
using BlockchainSQL.Processing;
using BlockchainSQL.Web.Models;
using Hydrogen.Web.AspNetCore.AnimateCss;


namespace BlockchainSQL.Web.Code {
	public static class ViewHelper {

		public static Animation[] EntryAnimationClasses = {
            #region Entry classes
            // Attenion Seekers
            //Animation.bounce,
            //Animation.flash,
            //Animation.pulse,
            //Animation.rubberBand,
            //Animation.shake,
            //Animation.swing,
            //Animation.tada,
            //Animation.wobble,
            //Animation.jello,

            // Bouncing Entrances
            Animation.bounceIn,
			Animation.bounceInDown,
			Animation.bounceInLeft,
			Animation.bounceInRight,
			Animation.bounceInUp,

            // Fading Entrances
            Animation.fadeIn,
			Animation.fadeInDown,
			Animation.fadeInDownBig,
			Animation.fadeInLeft,
			Animation.fadeInLeftBig,
			Animation.fadeInRight,
			Animation.fadeInRightBig,
			Animation.fadeInUp,
			Animation.fadeInUpBig,

            // Flippers
            Animation.flip,
			Animation.flipInX,
			Animation.flipInY,

            // Lightspeed
            Animation.lightSpeedIn,

            // Rotating Entraces
            Animation.rotateIn,
			Animation.rotateInDownLeft,
			Animation.rotateInDownRight,
			Animation.rotateInUpLeft,
			Animation.rotateInUpRight,

            // Sliding Entrances
            Animation.slideInUp,
			Animation.slideInDown,
			Animation.slideInLeft,
			Animation.slideInRight,

            // Specials
            Animation.rollIn,

            // Zoom Entrances 
            Animation.zoomIn,
			Animation.zoomInDown,
			Animation.zoomInLeft,
			Animation.zoomInRight,
			Animation.zoomInUp,

            #endregion
        };

		public static Animation[] ExitAnimationClasses = {
            #region Exit classes
            // Bouncing Exits   
            Animation.bounceOut,
			Animation.bounceOutDown,
			Animation.bounceOutLeft,
			Animation.bounceOutRight,
			Animation.bounceOutUp,

            // Fading Exits
            Animation.fadeOut,
			Animation.fadeOutDown,
			Animation.fadeOutDownBig,
			Animation.fadeOutLeft,
			Animation.fadeOutLeftBig,
			Animation.fadeOutRight,
			Animation.fadeOutRightBig,
			Animation.fadeOutUp,
			Animation.fadeOutUpBig,

            // Flippers
            Animation.flipOutX,
			Animation.flipOutY,

            // Rotating Exits
            Animation.rotateOut,
			Animation.rotateOutDownLeft,
			Animation.rotateOutDownRight,
			Animation.rotateOutUpLeft,
			Animation.rotateOutUpRight,

            // Sliding Exits
            Animation.slideOutUp,
			Animation.slideOutDown,
			Animation.slideOutLeft,
			Animation.slideOutRight,

            // Specials
            Animation.hinge,
			Animation.rollOut,

            // Zoom Exists
            Animation.zoomOut,
			Animation.zoomOutDown,
			Animation.zoomOutLeft,
			Animation.zoomOutRight,
			Animation.zoomOutUp
            #endregion
        };

		public static string ToFriendlyBranchString(int branchID) {
			switch (branchID) {
				case (int)KnownBranches.MainChain:
					return "Main Chain";
				case (int)KnownBranches.Invalid:
					return "Invalid";
				default:
					return "Orphan";
			}
		}

		public static string PrintCell(DataColumn col, DataRow row) {
			var cellData = row.ItemArray[col.Ordinal];
			var cellString = Tools.Html.Beautify(cellData);
			var colName = col.ColumnName?.ToLowerInvariant() ?? string.Empty;

			if (cellData != null) {
				var cellDataByteArray = cellData as byte[];
				var cellDataAsString = cellData as string;
				if ((cellDataByteArray != null && BitcoinProtocolHelper.IsValidHashByteArray(cellDataByteArray) && !colName.Contains("merkle") && !colName.Contains("wtxid")) ||
					(cellDataAsString != null && BitcoinProtocolHelper.IsValidAddress(cellDataAsString))) {
					if (cellString != BitcoinProtocolHelper.EmptyHashString) {
						cellString = string.Format("<small><a href='/Search?term={0}' target='_blank'>{0}</a></small>", cellString);
					} else {
						cellString = string.Format("<small>{0}</small>", cellString);
					}
				}
			}
			return cellString;
		}

		public static string RandomEntryAnimationClass(AnimationDelay delay = AnimationDelay.Seconds_1_0) {
			return AnimationClass(Tools.Array.RandomElement(EntryAnimationClasses), delay);
		}

		public static string RandomExitAnimationClass(AnimationDelay delay = AnimationDelay.Seconds_1_0) {
			return AnimationClass(Tools.Array.RandomElement(ExitAnimationClasses), delay);
		}

		public static string AnimationClass(Animation animation, AnimationDelay delay = AnimationDelay.Seconds_1_0) {
			return string.Format("animated {0} {1}", Tools.Enums.GetDescription(animation), Tools.Enums.GetDescription(delay));
		}

		public static string LoadingGifUrl {
			get { return "/images/preload.gif"; }
		}

		public static string AddressLineLink(AddressPageModel.LineItem line) {
			return string.Format(
				"<a href='/explorer/transaction/{0}' target='_blank'><b>TX:</b>{0} <b>{1}X:</b>{2}</a>",
				line.TXID,
				line.ItemType == AddressPageModel.LineItemType.Credit ? "O" : "I",
				line.Index
			);
		}
	}
}