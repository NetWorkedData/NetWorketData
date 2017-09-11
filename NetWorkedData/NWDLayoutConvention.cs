﻿//
//  Copyright 2017  Kortex
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.

// Convention of layout for all our unity project!

//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

//=====================================================================================================================
namespace NetWorkedData
{
	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	#if UNITY_EDITOR
	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	public partial class NWDLayoutClass //In ChamelCase, start by TRIGRAM of project or package
	{
		//-------------------------------------------------------------------------------------------------------------
		public const string K_MENU_BASE = "NAME"; //In UpperCases start by 'K' and plit by '_' if public
		const string kOverlayLogLimitKey = "myKey"; // //In ChamelCase, start by 'k' if private for class
		//-------------------------------------------------------------------------------------------------------------
		public bool MyProperties; //In ChamelCase, start by UpperCase ... (no prefix : '_', 'm', 'my', …)
		public bool AnotherProperty; //In ChamelCase, start by UpperCase ... (no prefix : '_', 'm', 'my', …)
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// My the method. In ChamelCase, start by UpperCase 
		/// </summary>
		/// <returns><c>true</c>, if method was myed, <c>false</c> otherwise.</returns>
		/// <param name="sSentData">If set to <c>true</c> s sent data.</param>
		public bool MyMethod (bool sSentData) // s to prefix sent Data
		{
			bool rReturnData = false; // r to prefix futur return Data
			bool tTemporaryData = !sSentData; // t to prefix temporary return Data
			if (tTemporaryData == false) { // no Ternary 
				rReturnData = true;
			}
			return rReturnData;
		}
	//-------------------------------------------------------------------------------------------------------------
	}
	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
	#endif
	//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
}
//=====================================================================================================================