using DeepMorphy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrequencyAnalysis
{
    public partial class MainForm : Form
    {
        private const double MARGIN = 0.05;
        private const int RUS_ALPH_FIRST_WORD = 1040;
        private const int RUS_ALPH_LAST_WORD = 1071;

        private readonly Point START_POINT;
        MorphAnalyzer morph;

        private Graphics g;
        private Pen pen;
        private Dictionary<char, int> RusSymbolsCount = new Dictionary<char, int>();
        private Dictionary<string, int> WordType;
        private string[] TextWords;
        private Bitmap gistogrammRus;
        private int RusMaxValue, WordTypeMaxCount;
        private int scaleXRus, scaleYRus;
        private string plainText;

        private readonly char[] separatorsForWords = new char[] { ' ', '.', ',', '<', '>', '"', ':', ';', '-', '+', '=', '\'', '/', '?', '!', '@', '*' };
        private readonly char[] separatorsForSentences = new char[] { '.', '?', '!'};


        public MainForm()
        {
            InitializeComponent();
            pen = new Pen(Color.Black, 2f);
            START_POINT = new Point(
                Convert.ToInt32(AnalyzeGistogrammRus.Width * MARGIN),
                Convert.ToInt32(AnalyzeGistogrammRus.Height * MARGIN));
            morph = new MorphAnalyzer();
        }

        private void WordTypesInit()
        {
            WordType = new Dictionary<string, int>();
            for (int i = 0; i < Constants.types.Length; i++)
            {
                WordType.Add(Constants.types[i], 0);
            }
        }

        private void CloseAppBut_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            gistogrammRus = new Bitmap(AnalyzeGistogrammRus.Width, AnalyzeGistogrammRus.Height);
            ResetGistogramm(gistogrammRus, AnalyzeGistogrammRus);
            
        }

        private void PlainText_TextChanged(object sender, EventArgs e)
        {

        }

        private void AnalyzeSymbolsType_Click(object sender, EventArgs e)
        {
            ResetGistogramm(gistogrammRus, AnalyzeGistogrammRus);
            SymbolsCountCalculate();
            BuildGistogrammForSymbols(RusSymbolsCount, RusMaxValue, gistogrammRus, AnalyzeGistogrammRus);
        }

        private void AnalyzeWordsType_Click(object sender, EventArgs e)
        {
            ResetGistogramm(gistogrammRus, AnalyzeGistogrammRus);
            WordsCountCalculate();
            WordTypesInit();
            var results = morph.Parse(TextWords).ToArray();
            for(int i = 0; i < results.Length; i++)
            {
                WordType[results[i].BestTag["чр"]]++;
            }
            WordTypeMaxCount = 0;
            for (int i = 0; i < Constants.types.Length; i++)
            {
                if (WordType[Constants.types[i]] > WordTypeMaxCount) WordTypeMaxCount = WordType[Constants.types[i]];
            }
            BuildGistogrammForWords(ref WordType, WordTypeMaxCount, gistogrammRus, AnalyzeGistogrammRus);
        }

        private void AnalyzeSentencesType_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Обновление поля для гистограммы
        /// </summary>
        /// <param name="map"></param>
        /// <param name="PB"></param>
        private void ResetGistogramm(Bitmap map, PictureBox PB)
        {
            for(int i = 0; i < map.Width; i++)
            {
                for (int j = 0; j < map.Height; j++)
                {
                    map.SetPixel(i, j, Color.White);
                }
            }
            DrawAxis(map);
            PB.Image = map;
        }

        /// <summary>
        /// Рисование осей гистограммы
        /// </summary>
        /// <param name="map"></param>
        private void DrawAxis(Bitmap map)
        {
            g = Graphics.FromImage(map);
            g.DrawLine(
                pen,
                new Point(Convert.ToInt32(map.Width * MARGIN), Convert.ToInt32(map.Height * (1 - MARGIN))),
                new Point(Convert.ToInt32(map.Width * (1 - MARGIN)), Convert.ToInt32(map.Height * (1 - MARGIN)))
                );
        }

        /// <summary>
        /// Инициализация и заполнение словарей для частотного анализа по буквам
        /// </summary>
        private void InitDictionaryForSymbols()
        {
            RusSymbolsCount.Clear();
            for(int i = RUS_ALPH_FIRST_WORD; i <= RUS_ALPH_LAST_WORD; i++)
            {
                RusSymbolsCount.Add(Convert.ToChar(i), 0);
            }
        }

        /// <summary>
        /// Подсчет букв в тексте и количества наиболее встречаемой буквы
        /// </summary>
        private void SymbolsCountCalculate()
        {
            RusMaxValue = 0;
            InitDictionaryForSymbols();
            plainText = PlainText.Text.ToUpper();
            for (int i = 0; i < plainText.Length; i++)
            {
                if (plainText[i] >= RUS_ALPH_FIRST_WORD && plainText[i] <= RUS_ALPH_LAST_WORD)
                    RusSymbolsCount[plainText[i]]++;
            }

            for (int i = RUS_ALPH_FIRST_WORD; i <= RUS_ALPH_LAST_WORD; i++)
            {
                if (RusSymbolsCount[(char)i] > RusMaxValue) RusMaxValue = RusSymbolsCount[(char)i];
            }
        }

        private string MakeVertical(string plainString)
        {
            string vertString = "";
            for(int i = 0; i < plainString.Length; i++)
            {
                vertString += plainString[i] + "\n";
            }
            return vertString;
        }

        /// <summary>
        /// Построение гистограммы частотного анализа по буквам
        /// </summary>
        /// <param name="alphSymbolsCount"></param>
        /// <param name="MaxCount"></param>
        /// <param name="alphMap"></param>
        /// <param name="PB"></param>

        private void BuildGistogrammForSymbols(Dictionary<char, int> alphSymbolsCount, int MaxCount, Bitmap alphMap, PictureBox PB)
        {
            if(MaxCount == 0) return;
            float scaleX = Convert.ToInt32(alphMap.Width * 0.9 / alphSymbolsCount.Count);
            float scaleY = Convert.ToInt32(alphMap.Height * 0.9 / MaxCount);
            g = Graphics.FromImage(alphMap);
            List<int> values = alphSymbolsCount.Values.ToList();
            List<char> keys = alphSymbolsCount.Keys.ToList();
            for (int i = 0; i < values.Count; i++)
            {
                float x = START_POINT.X + scaleX * i;
                float y = ((MaxCount - values[i]) * scaleY + START_POINT.Y) ;
                float height = Convert.ToInt32(alphMap.Height * (1-  2 * MARGIN)) - y + START_POINT.Y;
                if (values[i] != 0) g.DrawRectangle(pen, new Rectangle(Convert.ToInt32(x), Convert.ToInt32(y), 10, Convert.ToInt32(height)));
                else y = Convert.ToInt32(alphMap.Height * (1 - MARGIN));
                g.DrawString(MakeVertical(Convert.ToString(keys[i])), new Font("Arial", 9), Brushes.Black, new Point(Convert.ToInt32(START_POINT.X + scaleX * i), alphMap.Height - 20));
                g.DrawString(Convert.ToString(values[i].ToString()), new Font("Arial", 9), Brushes.Black, new Point(Convert.ToInt32(START_POINT.X + scaleX * i), Convert.ToInt32(y) - 20));

            }
            g.Dispose();
            PB.Image = alphMap;
        }

        private void BuildGistogrammForWords(ref Dictionary<string, int> wordType, int MaxCount, Bitmap map, PictureBox PB)
        {
            if (MaxCount == 0) return;
            float scaleX = Convert.ToInt32(map.Width * 0.9 / wordType.Count);
            float scaleY = Convert.ToInt32(map.Height * 0.9 / MaxCount);
            g = Graphics.FromImage(map);
            List<int> values = wordType.Values.ToList();
            List<string> keys = wordType.Keys.ToList();
            for (int i = 0; i < values.Count; i++)
            {
                float x = START_POINT.X + scaleX * i;
                float y = ((MaxCount - values[i]) * scaleY + START_POINT.Y);
                float height = Convert.ToInt32(map.Height * (1 - 2 * MARGIN)) - y + START_POINT.Y;
                if (values[i] != 0) g.DrawRectangle(pen, new Rectangle(Convert.ToInt32(x), Convert.ToInt32(y), 10, Convert.ToInt32(height)));
                else y = Convert.ToInt32(map.Height * (1 - MARGIN));
                g.DrawString(MakeVertical(Convert.ToString(keys[i])), new Font("Arial", 6), Brushes.Black, new Point(Convert.ToInt32(START_POINT.X + scaleX * i), map.Height - 45));
                g.DrawString(Convert.ToString(values[i].ToString()), new Font("Arial", 9), Brushes.Black, new Point(Convert.ToInt32(START_POINT.X + scaleX * i), Convert.ToInt32(y) - 20));

            }
            g.Dispose();
            PB.Image = map;
        }

        private int WordsCountCalculate()
        {
            plainText = PlainText.Text;
            
            TextWords = plainText.Split(separatorsForWords, StringSplitOptions.RemoveEmptyEntries).ToArray();
            return TextWords.Length;
        }
    }
}
