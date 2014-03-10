using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Infragistics.Controls.Editors
{
	/// <summary>
	/// A ReadOnlyCollection that can be indexed by a string identifier.
	/// </summary>
	/// <typeparam propertyName="T"></typeparam>

	[InfragisticsFeature(FeatureName = FeatureInfo.FeatureName_MultiColumnCombo, Version = FeatureInfo.Version_11_2)]

	public class ReadOnlyKeyedComboColumnCollection<T> : ReadOnlyCollection<T> where T : ComboColumn
	{
		#region Constructor

		/// <summary>
		/// Creates a new instance of the <see cref="ReadOnlyKeyedComboColumnCollection&lt;T&gt;"/> object.
		/// </summary>
		/// <param propertyName="list"></param>
		public ReadOnlyKeyedComboColumnCollection(IList<T> list)
			: base(list)
		{

		}
		#endregion // Constructor

		#region Indexer
		/// <summary>
		/// Gets the <see cref="ComboColumn"/> that has the specified key. 
		/// </summary>
		/// <param propertyName="key"></param>
		/// <returns>
		/// The column with the specified Key. 
		/// If more than one <see cref="ComboColumn"/> has the same key, the first Column is returned.
		/// </returns>
		public T this[string key]
		{
			get
			{
				T val = null;

				foreach (T item in this.Items)
				{
					if (item.Key == key)
					{
						val = item;
						break;
					}
				}

				return val;
			}

		}
		#endregion // Indexer
	}
}

#region Copyright (c) 2001-2012 Infragistics, Inc. All Rights Reserved
/* ---------------------------------------------------------------------*
*                           Infragistics, Inc.                          *
*              Copyright (c) 2001-2012 All Rights reserved               *
*                                                                       *
*                                                                       *
* This file and its contents are protected by United States and         *
* International copyright laws.  Unauthorized reproduction and/or       *
* distribution of all or any portion of the code contained herein       *
* is strictly prohibited and will result in severe civil and criminal   *
* penalties.  Any violations of this copyright will be prosecuted       *
* to the fullest extent possible under law.                             *
*                                                                       *
* THE SOURCE CODE CONTAINED HEREIN AND IN RELATED FILES IS PROVIDED     *
* TO THE REGISTERED DEVELOPER FOR THE PURPOSES OF EDUCATION AND         *
* TROUBLESHOOTING. UNDER NO CIRCUMSTANCES MAY ANY PORTION OF THE SOURCE *
* CODE BE DISTRIBUTED, DISCLOSED OR OTHERWISE MADE AVAILABLE TO ANY     *
* THIRD PARTY WITHOUT THE EXPRESS WRITTEN CONSENT OF INFRAGISTICS, INC. *
*                                                                       *
* UNDER NO CIRCUMSTANCES MAY THE SOURCE CODE BE USED IN WHOLE OR IN     *
* PART, AS THE BASIS FOR CREATING A PRODUCT THAT PROVIDES THE SAME, OR  *
* SUBSTANTIALLY THE SAME, FUNCTIONALITY AS ANY INFRAGISTICS PRODUCT.    *
*                                                                       *
* THE REGISTERED DEVELOPER ACKNOWLEDGES THAT THIS SOURCE CODE           *
* CONTAINS VALUABLE AND PROPRIETARY TRADE SECRETS OF INFRAGISTICS,      *
* INC.  THE REGISTERED DEVELOPER AGREES TO EXPEND EVERY EFFORT TO       *
* INSURE ITS CONFIDENTIALITY.                                           *
*                                                                       *
* THE END USER LICENSE AGREEMENT (EULA) ACCOMPANYING THE PRODUCT        *
* PERMITS THE REGISTERED DEVELOPER TO REDISTRIBUTE THE PRODUCT IN       *
* EXECUTABLE FORM ONLY IN SUPPORT OF APPLICATIONS WRITTEN USING         *
* THE PRODUCT.  IT DOES NOT PROVIDE ANY RIGHTS REGARDING THE            *
* SOURCE CODE CONTAINED HEREIN.                                         *
*                                                                       *
* THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE.              *
* --------------------------------------------------------------------- *
*/
#endregion Copyright (c) 2001-2012 Infragistics, Inc. All Rights Reserved