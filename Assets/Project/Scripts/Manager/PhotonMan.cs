using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using Photon;
using System;
using ExitGames.Client.Photon;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BC
{
	public class PhotonMan : PunBehaviour
	{
		public static PhotonMan i;

		[NonSerialized]
		public int[] _PlayerInputCnt = new int[2];

		[System.Serializable]
		public struct PlayerInput
		{
			public int _PlayerId;
			public int _Frame;
			public UnitType _Type;
			public Vector2Int _Pos;

			static public byte[] Serialize(object aObj)
			{
				BinaryFormatter bf = new BinaryFormatter();
				using (MemoryStream ms = new MemoryStream())
				{
					bf.Serialize(ms, aObj);
					return ms.ToArray();
				}
			}

			static public object Deserialize(byte[] aByteArr)
			{
				BinaryFormatter bf = new BinaryFormatter();
				using (MemoryStream ms = new MemoryStream(aByteArr))
				{
					return bf.Deserialize(ms);
				}
			}
		}

		void Awake()
		{
			i = this;
			PhotonPeer.RegisterType(typeof(PlayerInput), (byte)'p', PlayerInput.Serialize, PlayerInput.Deserialize);

			PhotonNetwork.SendMonoMessageTargets = new HashSet<GameObject>();
			PhotonNetwork.SendMonoMessageTargets.Add(gameObject);
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

			ExitGames.Client.Photon.Hashtable propertiesToSet = new ExitGames.Client.Photon.Hashtable();
			propertiesToSet.Add("cas0", 0);
			propertiesToSet.Add("cas1", 0);
			PhotonNetwork.room.SetCustomProperties(propertiesToSet);
		}

		public override void OnPhotonCustomRoomPropertiesChanged(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
		{
			base.OnPhotonCustomRoomPropertiesChanged(propertiesThatChanged);

			if (propertiesThatChanged.ContainsKey("start_time"))
			{
				BattleGameMan.i._StartTime = (int)propertiesThatChanged["start_time"];
				if (PhotonNetwork.isMasterClient)
				{
					BattlePlayerMan.i._MyPlayerId = 0;
				}
				else
				{ 
					BattlePlayerMan.i._MyPlayerId = 1;
					BattleCameraMan.i.transform.SetEulerAnglesY(180);
				}

				BattleGameMan.i.StartCountDown();
			}
			else if(propertiesThatChanged.ContainsKey("pi"))
			{
				PlayerInput pi = (PlayerInput)propertiesThatChanged["pi"];
				_PlayerInputCnt[pi._PlayerId]++;
				BattleGameMan.i._PlayerInputList.Add(pi);
			}
		}
	}
}

