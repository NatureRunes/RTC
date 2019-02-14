using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { set; get; }

	public GameObject mainMenu;
	public GameObject serverMenu;
	public GameObject connectMenu;

	public GameObject serverPrefab;
	public GameObject clientPrefab;

	public InputField ChangeNameBox;

	private void Start ()
	{
		Instance = this;

		mainMenu.SetActive(true);
		serverMenu.SetActive(false);
		connectMenu.SetActive(false);

		DontDestroyOnLoad(gameObject);
	}

	public void ConnectButton()
	{
		mainMenu.SetActive(false);
		serverMenu.SetActive(false);
		connectMenu.SetActive(true);
	}
	public void HostButton()
	{
		try
		{
			Server s = Instantiate(serverPrefab).GetComponent<Server>();
			s.Init();

			Client c = Instantiate(clientPrefab).GetComponent<Client>();
			c.clientName = ChangeNameBox.text;
			c.isHost = true;

			if (c.clientName == "")
				c.clientName = "Player One";

			c.ConnectToServer(6321, "127.0.0.1");

		}
		catch (Exception e)
		{
			Debug.Log(e.Message);
		}
		mainMenu.SetActive(false);
		serverMenu.SetActive(true);
		connectMenu.SetActive(false);
	}
	public void ConnectToServerButton()
	{

		string hostAddress = GameObject.Find("HostInput").GetComponent<InputField>().text;
		if (hostAddress == "")
			hostAddress = "127.0.0.1";

		try
		{
			Client c = Instantiate(clientPrefab).GetComponent<Client>();
			c.clientName = ChangeNameBox.text;
			if (c.clientName == "Player One")
				c.clientName = "Player Two";

			c.ConnectToServer(6321, hostAddress);
			connectMenu.SetActive(false);

		}
		catch (Exception e)
		{
			Debug.Log(e.Message);
		}
	}
	public void BackButton()
	{
		mainMenu.SetActive(true);
		serverMenu.SetActive(false);
		connectMenu.SetActive(false);


		Server s = FindObjectOfType<Server>();
		if (s != null)
			Destroy(s.gameObject);

		Client c = FindObjectOfType<Client>();
		if (c != null)
			Destroy(c.gameObject);
	}

	public void StartGame()
	{
		SceneManager.LoadScene("Checkers");
	}
}
