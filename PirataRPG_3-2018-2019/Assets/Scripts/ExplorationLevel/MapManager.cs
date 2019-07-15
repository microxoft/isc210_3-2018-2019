using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public GameObject E;
    public GameObject F;
    public GameObject G;
    public GameObject H;
    public GameObject I;
    public GameObject J;
    public GameObject K;
    public GameObject L;
    public GameObject M;
    public GameObject N;
    public GameObject O;
    public GameObject P;
    public GameObject Q;
    public GameObject R;
    public GameObject S;
    public GameObject T;
    public GameObject U;
    public GameObject V;
    public GameObject W;
    public GameObject X;
    public GameObject Y;
    public GameObject Z;
    public GameObject a;
    public GameObject b;
    public GameObject c;
    public GameObject d;
    public GameObject e;
    public GameObject f;
    public GameObject g;
    public GameObject h;
    public GameObject i;
    public GameObject j;
    public GameObject k;
    public GameObject l;
    public GameObject m;
    XmlDocument xmlDoc;
    const string xmlPath = "Level1";
    GameObject newCell;
    public GameObject Player;
    public GameObject ChestBanana;
    public GameObject ChestCherry;
    public GameObject ChestGrape;
    public GameObject ChestLemon;
    public GameObject ChestOrange;
    public GameObject ChestSeaweed;


    private void Awake()
    {
        xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(Resources.Load<TextAsset>(xmlPath).text);
    }

    private void Start()
    {
        LoadMap(0, 73, 0, 40);
    }

    void LoadMap(int xFrom, int xTo, int yFrom, int yTo)
    {
        int xFromCopy = xFrom;

        var selectedNodes = xmlDoc.SelectNodes(string.Format("//level/map/row[position()>={0} and position()<={1}]", yFrom, yTo));

        foreach (XmlNode currentNode in selectedNodes) // For every row...
        {
            for (xFrom = xFromCopy; xFrom <= xTo && xFrom < currentNode.InnerText.Length; xFrom++) // Get every column.
            {
                switch (currentNode.InnerText[xFrom])
                {
                    case 'A':
                        newCell = A;
                        break;
                    case 'B':
                        newCell = B;
                        break;
                    case 'C':
                        newCell = C;
                        break;
                    case 'D':
                        newCell = D;
                        break;
                    case 'E':
                        newCell = E;
                        break;
                    case 'F':
                        newCell = F;
                        break;
                    case 'G':
                        newCell = G;
                        break;
                    case 'H':
                        newCell = H;
                        break;
                    case 'I':
                        newCell = I;
                        break;
                    case 'J':
                        newCell = J;
                        break;
                    case 'K':
                        newCell = K;
                        break;
                    case 'L':
                        newCell = L;
                        break;
                    case 'M':
                        newCell = M;
                        break;
                    case 'N':
                        newCell = N;
                        break;
                    case 'O':
                        newCell = O;
                        break;
                    case 'P':
                        newCell = P;
                        break;
                    case 'Q':
                        newCell = Q;
                        break;
                    case 'R':
                        newCell = R;
                        break;
                    case 'S':
                        newCell = S;
                        break;
                    case 'T':
                        newCell = T;
                        break;
                    case 'U':
                        newCell = U;
                        break;
                    case 'V':
                        newCell = V;
                        break;
                    case 'W':
                        newCell = W;
                        break;
                    case 'X':
                        newCell = X;
                        break;
                    case 'Y':
                        newCell = Y;
                        break;
                    case 'Z':
                        newCell = Z;
                        break;
                    case 'a':
                        newCell = a;
                        break;
                    case 'b':
                        newCell = b;
                        break;
                    case 'c':
                        newCell = c;
                        break;
                    case 'd':
                        newCell = d;
                        break;
                    case 'e':
                        newCell = e;
                        break;
                    case 'f':
                        newCell = f;
                        break;
                    case 'g':
                        newCell = g;
                        break;
                    case 'h':
                        newCell = h;
                        break;
                    case 'i':
                        newCell = i;
                        break;
                    case 'j':
                        newCell = j;
                        break;
                    case 'k':
                        newCell = k;
                        break;
                    case 'l':
                        newCell = l;
                        break;
                    case 'm':
                        newCell = m;
                        break;
                }
                Instantiate(newCell, new Vector3(xFrom, -yFrom), Quaternion.identity);
            }
            yFrom++;
        }

        selectedNodes = xmlDoc.SelectNodes(string.Format("//level/characters/character"));

        foreach (XmlNode currentNode in selectedNodes) // For every character...
        {
            switch(currentNode.Attributes["prefabName"].Value)
            {
                case "Player":
                    newCell = Player;
                    break;
            }
            newCell = Instantiate(newCell, new Vector3(Convert.ToSingle(currentNode.Attributes["posX"].Value), -Convert.ToSingle(currentNode.Attributes["posY"].Value)), Quaternion.identity);
            newCell.name = currentNode.Attributes["uniqueObjectName"].Value;
            newCell.tag = currentNode.Attributes["tag"].Value;
            if(newCell.tag == "Player")
            {
                Camera.main.transform.SetParent(newCell.transform);
                Camera.main.transform.localPosition = new Vector3(0, 0, -10);
            }
        }

        selectedNodes = xmlDoc.SelectNodes(string.Format("//level/items/item"));

        foreach (XmlNode currentNode in selectedNodes) // For every item...
        {
            switch (currentNode.Attributes["prefabName"].Value)
            {
                case "ChestBanana":
                    newCell = ChestBanana;
                    break;
                case "ChestCherry":
                    newCell = ChestCherry;
                    break;
                case "ChestGrape":
                    newCell = ChestGrape;
                    break;
                case "ChestLemon":
                    newCell = ChestLemon;
                    break;
                case "ChestOrange":
                    newCell = ChestOrange;
                    break;
                case "ChestSeaweed":
                    newCell = ChestSeaweed;
                    break;
            }
            newCell = Instantiate(newCell, new Vector3(Convert.ToSingle(currentNode.Attributes["posX"].Value), -Convert.ToSingle(currentNode.Attributes["posY"].Value)), Quaternion.identity);
            newCell.name = currentNode.Attributes["uniqueObjectName"].Value;
            newCell.tag = currentNode.Attributes["tag"].Value;
        }
    }
}
