using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChipSecuritySystem;

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ColorChip> chips = new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Purple)
            };

            List<ColorChip> longestSequence = FindLongestSequence(chips);

            if (longestSequence != null)
            {
                Console.WriteLine("Longest sequence found:");
                Console.WriteLine("Blue -> " + string.Join(" -> ", longestSequence) + " -> Green");
            }
            else
            {
                Console.WriteLine(Constants.ErrorMessage);
            }
        }

        static List<ColorChip> FindLongestSequence(List<ColorChip> chips)
        {
            List<ColorChip> bestSequence = null;
            bool[] used = new bool[chips.Count];

            // Start with chips that have a StartColor of Blue
            for (int i = 0; i < chips.Count; i++)
            {
                if (chips[i].StartColor == Color.Blue)
                {
                    List<ColorChip> currentSequence = new List<ColorChip> { chips[i] };
                    used[i] = true;
                    FindSequence(chips, currentSequence, used, ref bestSequence);
                    used[i] = false;
                }
            }

            return bestSequence;
        }

        static void FindSequence(List<ColorChip> chips, List<ColorChip> currentSequence, bool[] used, ref List<ColorChip> bestSequence)
        {
            // If the current sequence ends with a chip having EndColor Green, check if it's the longest sequence
            if (currentSequence[^1].EndColor == Color.Green)
            {
                if (bestSequence == null || currentSequence.Count > bestSequence.Count)
                {
                    bestSequence = new List<ColorChip>(currentSequence);
                }
            }

            // Try to extend the sequence with unused chips that match the current end color
            Color currentEndColor = currentSequence[^1].EndColor;
            for (int i = 0; i < chips.Count; i++)
            {
                if (!used[i] && chips[i].StartColor == currentEndColor)
                {
                    used[i] = true;
                    currentSequence.Add(chips[i]);
                    FindSequence(chips, currentSequence, used, ref bestSequence);
                    currentSequence.RemoveAt(currentSequence.Count - 1);
                    used[i] = false;
                }
            }
        }
    }
}
