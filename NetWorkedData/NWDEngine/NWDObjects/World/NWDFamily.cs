﻿using System;

using SQLite4Unity3d;

//=====================================================================================================================
namespace NetWorkedData
{
	//-------------------------------------------------------------------------------------------------------------
	[NWDClassServerSynchronizeAttribute (true)]
	[NWDClassTrigrammeAttribute ("FAM")]
	[NWDClassDescriptionAttribute ("Families descriptions Class")]
	[NWDClassMenuNameAttribute ("Families")]
	//-------------------------------------------------------------------------------------------------------------
	public partial class NWDFamily :NWDBasis<NWDFamily>
	{
		//-------------------------------------------------------------------------------------------------------------
		//public bool DiscoverItYourSelf { get; set; }
		public NWDLocalizableStringType Name{ get; set; }
		//-------------------------------------------------------------------------------------------------------------
		public NWDFamily()
		{
			//Init your instance here
			//DiscoverItYourSelf = true;
		}
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================