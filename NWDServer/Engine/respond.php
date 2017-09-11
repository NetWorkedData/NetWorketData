<?php
	//NWD File at 2017-05-16
	//Copyright NetWorkedDatas ideMobi 2017
	//Created by Jean-François CONTART
	//--------------------
	// RESPOND FUNCTIONS
	//--------------------
	// datas output
	$REP;
	//--------------------
	// get timestamp of server compute
	$REP['timestamp'] = time();
	//--------------------
	// add dictionary enter in respond
	function respondAdd($sKey, $sValue)
	{
		global $REP;
		$REP[$sKey] = $sValue;
	}
		//--------------------
	function respondRemove($sKey)
	{
		global $REP;
		if (isset($REP[$sKey]))
		{
		unset($REP[$sKey]);
		}
	}
		//--------------------
	function respondUUID($sValue)
	{
		global $REP, $uuid;
//		$uuid = $sValue;
		$REP['uuid'] = $sValue;
	}
	//--------------------
	function respondToken($sValue)
	{
		global $REP, $token;
//		$token = $sValue;
		$REP['token'] = $sValue;
	}
	//--------------------
	function respondNeedReloadData()
	{
		global $REP;
		$REP['reloaddatas'] = true;
	}
		//--------------------
	function respondNewUser()
	{
		global $REP;
		$REP['newuser'] = true;
	}
	//--------------------
?>