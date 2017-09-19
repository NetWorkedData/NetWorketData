﻿//=====================================================================================================================
//
// ideMobi copyright 2017 
// All rights reserved by ideMobi
//
//=====================================================================================================================

using System;
using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;
#endif

//=====================================================================================================================
namespace NetWorkedData
{//-------------------------------------------------------------------------------------------------------------
	//-------------------------------------------------------------------------------------------------------------
	[AttributeUsage (AttributeTargets.Class, AllowMultiple = true)]
	//-------------------------------------------------------------------------------------------------------------
	public class NWDInternalKeyNotEditableAttribute : Attribute
	{
	}
	//-------------------------------------------------------------------------------------------------------------
	[AttributeUsage (AttributeTargets.Property, AllowMultiple = true)]
	//-------------------------------------------------------------------------------------------------------------
	public class NWDEntitledAttribute : Attribute
	{
		public string Entitled = "";
		public string ToolsTips = "";
		public NWDEntitledAttribute (string sEntitled)
		{
			this.Entitled = sEntitled;
		}
		public NWDEntitledAttribute (string sEntitled, string sToolsTips)
		{
			this.Entitled = sEntitled;
			this.ToolsTips = sToolsTips;
		}
	}
	//-------------------------------------------------------------------------------------------------------------
	[AttributeUsage (AttributeTargets.Property, AllowMultiple = true)]
	//-------------------------------------------------------------------------------------------------------------
	public class NWDHeaderAttribute : Attribute
	{
		public string mHeader;
		public NWDHeaderAttribute (string sHeader)
		{
			this.mHeader = sHeader;
		}
	}
	//-------------------------------------------------------------------------------------------------------------
	[AttributeUsage (AttributeTargets.Property, AllowMultiple = true)]
	//-------------------------------------------------------------------------------------------------------------
	public class NWDSpaceAttribute : Attribute
	{
		public float mSize;
		public NWDSpaceAttribute ()
		{
			this.mSize = 10.0f;
		}
		public NWDSpaceAttribute (float sSize)
		{
			this.mSize = sSize;
		}
	}
	//-------------------------------------------------------------------------------------------------------------
	[AttributeUsage (AttributeTargets.Property, AllowMultiple = true)]
	//-------------------------------------------------------------------------------------------------------------
	public class NWDGroupStartAttribute : Attribute
	{
		public string mGroupName;
		public bool mBoldHeader;
		public bool mReducible;
		public bool mOpen;

		public NWDGroupStartAttribute Parent;

		//-------------------------------------------------------------------------------------------------------------
		#if UNITY_EDITOR
		//-------------------------------------------------------------------------------------------------------------
		private string Key (String sClassName)
		{
			return sClassName + mGroupName;
		}
		//-------------------------------------------------------------------------------------------------------------
		public bool IsDrawable (String sClassName)
		{
			bool rReturn = GetDrawable (sClassName);
			if (Parent != null) {
				rReturn = IsDrawable (sClassName);
			}
			return rReturn;
		}
		//-------------------------------------------------------------------------------------------------------------
		public void SetDrawable (String sClassName, bool sBool)
		{
			string tKey = Key(sClassName);
			EditorPrefs.HasKey (tKey);
			EditorPrefs.SetBool (tKey, sBool);
		}
		//-------------------------------------------------------------------------------------------------------------
		public bool GetDrawable (String sClassName)
		{
			string tKey = Key(sClassName);
			EditorPrefs.HasKey (tKey);
			return EditorPrefs.GetBool (tKey);
		}
		//-------------------------------------------------------------------------------------------------------------
		#endif
		//-------------------------------------------------------------------------------------------------------------
		public NWDGroupStartAttribute (string sGroupName, bool sBoldHeader = false, bool sReducible = true, bool sOpen = true)
		{
			this.mGroupName = sGroupName;
			this.mBoldHeader = sBoldHeader;
			this.mOpen = sOpen;
			this.mReducible = sReducible;
		}
	}
	//-------------------------------------------------------------------------------------------------------------
	[AttributeUsage (AttributeTargets.Property, AllowMultiple = true)]
	//-------------------------------------------------------------------------------------------------------------
	public class NWDGroupEndAttribute : Attribute
	{
	}
	//-------------------------------------------------------------------------------------------------------------
	[AttributeUsage (AttributeTargets.Property, AllowMultiple = true)]
	//-------------------------------------------------------------------------------------------------------------
	public class NWDSeparatorAttribute : Attribute
	{
	}
	//-------------------------------------------------------------------------------------------------------------
	[AttributeUsage (AttributeTargets.Property, AllowMultiple = true)]
	//-------------------------------------------------------------------------------------------------------------
	public class NWDNotEditableAttribute : Attribute
	{
	}
	//-------------------------------------------------------------------------------------------------------------
	[AttributeUsage (AttributeTargets.Property, AllowMultiple = true)]
	//-------------------------------------------------------------------------------------------------------------
	public class NWDFloatSliderAttribute : Attribute
	{
		public float mMin;
		public float mMax;
		public NWDFloatSliderAttribute (float sMin, float sMax)
		{
			this.mMin = sMin;
			this.mMax = sMax;
		}
	}
	//-------------------------------------------------------------------------------------------------------------
	[AttributeUsage (AttributeTargets.Property, AllowMultiple = true)]
	//-------------------------------------------------------------------------------------------------------------
	public class NWDIntSliderAttribute : Attribute
	{
		public int mMin;
		public int mMax;
		public NWDIntSliderAttribute (int sMin, int sMax)
		{
			this.mMin = sMin;
			this.mMax = sMax;
		}
	}
	//-------------------------------------------------------------------------------------------------------------
	[AttributeUsage (AttributeTargets.Property, AllowMultiple = true)]
	//-------------------------------------------------------------------------------------------------------------
	public class NWDEnumAttribute : Attribute
	{
		public int[] mEnumInt;
		public string[] mEnumString;
		public NWDEnumAttribute (int[] sEnumInt, string[] sEnumString)
		{
			this.mEnumInt = sEnumInt;
			this.mEnumString = sEnumString;
		}
	}
	//-------------------------------------------------------------------------------------------------------------
	[AttributeUsage (AttributeTargets.Property, AllowMultiple = true)]
	//-------------------------------------------------------------------------------------------------------------
	public class NWDEnumStringAttribute : Attribute
	{
		public string[] mEnumString;
		public NWDEnumStringAttribute (string[] sEnumString)
		{
			this.mEnumString = sEnumString;
		}
	}
	//-------------------------------------------------------------------------------------------------------------
}
//=====================================================================================================================