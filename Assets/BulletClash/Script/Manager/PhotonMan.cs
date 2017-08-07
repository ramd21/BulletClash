using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using Photon;
using System;
using ExitGames.Client.Photon;

namespace BC
{
	public class PhotonMan : PunBehaviour
	{
		public static PhotonMan i;


		void Awake()
		{
			i = this;
		}

		public void Connect()
		{
			PhotonNetwork.ConnectUsingSettings("1");
		}

		public override void OnConnectedToPhoton()
		{
			base.OnConnectedToPhoton();
			Deb.MethodLog();
		}


		public override void OnJoinedLobby()
		{
			base.OnJoinedLobby();
			Deb.MethodLog();
			PhotonNetwork.JoinRandomRoom();
		}

		public override void OnJoinedRoom()
		{
			base.OnJoinedRoom();
			Deb.MethodLog();
		}

		public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
		{
			base.OnPhotonPlayerConnected(newPlayer);

			if (PhotonNetwork.isMasterClient)
			{
				if (PhotonNetwork.room.PlayerCount == 2)
				{
					ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
					propertiesToSet.Add("start_time", PhotonNetwork.ServerTimestamp);
					PhotonNetwork.room.SetCustomProperties(propertiesToSet);
				}
			}
		}

		public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
		{
			base.OnPhotonRandomJoinFailed(codeAndMsg);
			Deb.MethodLog();
			PhotonNetwork.CreateRoom("MyMatch");
		}

		public override void OnCreatedRoom()
		{
			base.OnCreatedRoom();
			Deb.MethodLog();
		}

		public override void OnPhotonCustomRoomPropertiesChanged(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
		{
			base.OnPhotonCustomRoomPropertiesChanged(propertiesThatChanged);

			if (propertiesThatChanged.ContainsKey("start_time"))
			{
				BattleGameMan.i._StartTime = (int)propertiesThatChanged["start_time"];
				BattleUIMan.i.StartCountDown();
			}
		}
	}
}

