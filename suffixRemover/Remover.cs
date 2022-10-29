using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace suffixRemover
{
    class Remover
    {
        private Regex lyricRecorgnize = new Regex("Lyric=(.+)");

        public Regex lyricRegex = new Regex("Lyric=([aiueon-] |)([a-z]+\\d+|[あ-ん|a-z]+|R|-|\\?)([一-鿕]+|)([A-G]\\d|)");


        public bool IsLyric(string line)
        {
            Match m = lyricRecorgnize.Match(line);

            return m.Success;
        }

        public string GetLyric(string str)
        {
            Match m = lyricRecorgnize.Match(str);

            if (m.Success)
            {
                return m.Groups[1].Value;
            }

            return str;
        }

        public string RemainOnlyGroupFirstSecondFifth(string lyric)
        {
            Match m = lyricRegex.Match(lyric);

            if (m.Success)
            {
                return $"{m.Groups[1].ToString()}{m.Groups[2].ToString()}{m.Groups[5].ToString()}";
            }

            return GetLyric(lyric);
        }

        public string RemainOnlyGroupFirstSecond(string lyric)
        {
            Match m = lyricRegex.Match(lyric);

            if (m.Success)
            {
                return $"{m.Groups[1].ToString()}{m.Groups[2].ToString()}";
            }

            return GetLyric(lyric);
        }

        public string RemainOnlyGroupSecond(string lyric)
        {
            Match m = lyricRegex.Match(lyric);

            if (m.Success)
            {
                return $"{m.Groups[2].ToString()}";
            }

            return GetLyric(lyric);
        }

        public string RemainOnlyGroupSecondFifth(string lyric)
        {
            Match m = lyricRegex.Match(lyric);

            if (m.Success)
            {
                return $"{m.Groups[2].ToString()}{m.Groups[5].ToString()}";
            }

            return GetLyric(lyric);
        }


    }
}
