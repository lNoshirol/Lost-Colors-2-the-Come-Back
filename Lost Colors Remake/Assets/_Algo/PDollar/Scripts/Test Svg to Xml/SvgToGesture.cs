using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
//using System.Numerics; // Pour Vector3
using System.Xml.Linq;
using System.Text.RegularExpressions;
using NaughtyAttributes;
using UnityEngine;

class SvgToGesture : MonoBehaviour
{
    public string pathSvg;
    public string GeneratedXmlLocationPath;
    public int resampleSize;
    public string ShapeName;

    [Button("Convert Svg to Xml")]
    public void Main(/*string[] args*/)
    {
        string svgPath = pathSvg;
        string xmlOutputPath = GeneratedXmlLocationPath;

        var vectors = ParseSvgFile(svgPath);

        // === Ajout ici : resampling à 128 points ===
        vectors = Resample(vectors, resampleSize);

        SaveVectorsAsGestureXml(vectors, xmlOutputPath);

        Debug.Log("Conversion terminée !");
    }

    static List<Vector3> ParseSvgFile(string filePath)
    {
        var vectors = new List<Vector3>();

        string svgContent = File.ReadAllText(filePath);

        // 1. Cherche d'abord <path>
        var matchPath = Regex.Match(svgContent, @"<path[^>]*d\s*=\s*[""']([^""']+)[""']", RegexOptions.IgnoreCase);
        if (matchPath.Success)
        {
            string pathData = matchPath.Groups[1].Value;
            vectors = ParsePathData(pathData);
            Debug.Log("Path trouvé !");
        }
        else
        {
            // 2. Sinon cherche <polygon>
            var matchPolygon = Regex.Match(svgContent, @"<polygon[^>]*points\s*=\s*[""']([^""']+)[""']", RegexOptions.IgnoreCase);
            if (matchPolygon.Success)
            {
                string pointsData = matchPolygon.Groups[1].Value;
                vectors = ParsePolygonPoints(pointsData);
                Debug.Log("Polygon trouvé !");
            }
            else
            {
                // 3. Sinon cherche <polyline>
                var matchPolyline = Regex.Match(svgContent, @"<polyline[^>]*points\s*=\s*[""']([^""']+)[""']", RegexOptions.IgnoreCase);
                if (matchPolyline.Success)
                {
                    string pointsData = matchPolyline.Groups[1].Value;
                    vectors = ParsePolygonPoints(pointsData); // UTILISE LA MÊME FONCTION
                    Debug.Log("Polyline trouvé !");
                }
                else
                {
                    Debug.Log("Aucun <path>, <polygon> ou <polyline> trouvé dans le SVG.");
                }
            }
        }

        return vectors;
    }

    static List<Vector3> ParsePolygonPoints(string pointsData)
    {
        var vectors = new List<Vector3>();

        var regex = new Regex(@"-?\d*\.?\d+");
        var matches = regex.Matches(pointsData);

        for (int i = 0; i < matches.Count; i += 2)
        {
            float x = float.Parse(matches[i].Value, CultureInfo.InvariantCulture);
            float y = float.Parse(matches[i + 1].Value, CultureInfo.InvariantCulture);

            vectors.Add(new Vector3(x, y, 0));
        }

        return vectors;
    }

    static List<Vector3> ParsePathData(string pathData)
    {
        var vectors = new List<Vector3>();
        var regex = new Regex(@"([MmLl])([^MmLlZz]+)");
        var matches = regex.Matches(pathData);

        Vector2 currentPosition = Vector2.zero;

        foreach (Match match in matches)
        {
            string command = match.Groups[1].Value;
            string parameters = match.Groups[2].Value;

            var numbers = ParseNumbers(parameters);

            for (int i = 0; i < numbers.Count; i += 2)
            {
                float x = numbers[i];
                float y = numbers[i + 1];

                if (command == "m" || command == "l")
                {
                    currentPosition.x += x;
                    currentPosition.y += y;
                }
                else
                {
                    currentPosition.x = x;
                    currentPosition.y = y;
                }

                vectors.Add(new Vector3(currentPosition.x, currentPosition.y, 0));
            }
        }

        return vectors;
    }

    static List<float> ParseNumbers(string s)
    {
        var floats = new List<float>();
        var regex = new Regex(@"-?\d*\.?\d+");

        foreach (Match m in regex.Matches(s))
        {
            floats.Add(float.Parse(m.Value, CultureInfo.InvariantCulture));
        }

        return floats;
    }

    public void SaveVectorsAsGestureXml(List<Vector3> vectors, string outputPath)
    {
        var gesture = new XElement("Gesture",
            new XAttribute("Name", ShapeName),
            new XAttribute("Subject", "user"),
            new XAttribute("InputType", "stylus"),
            new XAttribute("Speed", "MEDIUM"),
            new XAttribute("NumPts", vectors.Count)
        );

        var stroke = new XElement("Stroke",
            new XAttribute("index", 1)
        );

        long baseTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds(); // Timestamp actuel
        long timeIncrement = 10; // ms entre les points

        for (int i = 0; i < vectors.Count; i++)
        {
            var vec = vectors[i];
            var point = new XElement("Point",
                new XAttribute("X", vec.x),
                new XAttribute("Y", vec.y),
                new XAttribute("T", baseTimestamp + i * timeIncrement),
                new XAttribute("Pressure", 128) // Valeur fixe de pression
            );
            stroke.Add(point);
        }

        gesture.Add(stroke);

        var doc = new XDocument(
            new XDeclaration("1.0", "utf-8", "yes"),
            gesture
        );

        doc.Save(outputPath);
    }

    static List<Vector3> Resample(List<Vector3> originalPoints, int targetCount)
    {
        if (originalPoints.Count < 2)
            throw new ArgumentException("Pas assez de points pour resampler.");

        // 1. Calcul de la distance totale du chemin
        float totalLength = 0f;
        var cumulativeDistances = new List<float> { 0f };
        for (int i = 1; i < originalPoints.Count; i++)
        {
            float dist = Vector3.Distance(originalPoints[i - 1], originalPoints[i]);
            totalLength += dist;
            cumulativeDistances.Add(totalLength);
        }

        // 2. Distance cible entre les points
        float interval = totalLength / (targetCount - 1);

        var resampled = new List<Vector3> { originalPoints[0] }; // commence par le premier point
        float currentDistance = interval;
        int currIndex = 1;

        while (resampled.Count < targetCount)
        {
            if (currIndex >= originalPoints.Count)
                break;

            float prevCumDist = cumulativeDistances[currIndex - 1];
            float nextCumDist = cumulativeDistances[currIndex];

            if (currentDistance <= nextCumDist)
            {
                float t = (currentDistance - prevCumDist) / (nextCumDist - prevCumDist);
                Vector3 newPoint = Vector3.Lerp(originalPoints[currIndex - 1], originalPoints[currIndex], t);
                resampled.Add(newPoint);
                currentDistance += interval;
            }
            else
            {
                currIndex++;
            }
        }

        // Si jamais il manque un point à la fin, on rajoute le dernier
        if (resampled.Count < targetCount)
            resampled.Add(originalPoints[originalPoints.Count - 1]);

        return resampled;
    }
}