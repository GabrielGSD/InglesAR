using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCategory : MonoBehaviour
{
	public Sprite[] sprites;
	private Image imagem;
	private int aux = 0;

	void Start()
	{
		imagem = GetComponent<Image>();
	}

	public void nextImage()
	{
		if (aux < sprites.Length - 1)
		{
			aux++;
			imagem.sprite = sprites[aux];
		}
		print(aux);
	}

	public void backImage()
	{
		if (aux > 0)
		{
			aux--;
			imagem.sprite = sprites[aux];
		}
		print(aux);

	}
}
