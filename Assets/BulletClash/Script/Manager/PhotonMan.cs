using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RM;
using Photon;

namespace BC
{
	public class PhotonMan : PunBehaviour
	{
		public static PhotonMan i;



		void Awake()
		{
			i = this;
		}

		void Start()
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
	}
}

