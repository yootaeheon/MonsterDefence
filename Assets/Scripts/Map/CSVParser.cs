using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVParser
{
    private bool isLoaded;
    public bool IsLoaded => isLoaded;

    private string path;
    public string Path => path;

    public int SizeX => datas.GetLength(1);
    public int SizeY => datas.GetLength(0);

    private string[,] datas;

    public string this[int y, int x]
    {
        get
        {
            if (!isLoaded)
                return string.Empty;

            if (y < 0 || y >= datas.GetLength(0) ||
                x < 0 || x >= datas.GetLength(1))
                return string.Empty;

            return datas[y, x];
        }
    }

    public void Load(string filePath)
    {
        path = System.IO.Path.Combine(Application.streamingAssetsPath, filePath);

        if (!File.Exists(path))
        {
            Debug.LogError($"CSV 파일을 찾을 수 없습니다: {path}");
            return;
        }

        string[] lines = File.ReadAllLines(path);

        if (lines.Length == 0)
        {
            Debug.LogError("CSV 파일이 비어 있습니다.");
            return;
        }

        int rowCount = lines.Length;
        int colCount = lines[0].Split(',', '\t').Length;

        datas = new string[rowCount, colCount];

        for (int y = 0; y < rowCount; y++)
        {
            string[] values = lines[y].Split(',', '\t');

            for (int x = 0; x < colCount; x++)
            {
                if (x < values.Length)
                {
                    datas[y, x] = values[x].Trim();
                }
                else
                {
                    datas[y, x] = string.Empty;
                }
            }
        }

        isLoaded = true;
    }
}
