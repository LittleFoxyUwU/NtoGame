using System;
using UnityEngine;

namespace GameScripts.Logic.Player
{
	public class PlayerMoney : MonoBehaviour
	{ 
		public int Money;
		public Action<int> OnMoneyChanged;
	
		public void AddMoney(int n)
		{
			Money += n;
			OnMoneyChanged?.Invoke(Money);
		}
	}
}