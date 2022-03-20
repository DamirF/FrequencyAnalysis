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
        private const int EN_ALPH_FIRST_WORD = 65;
        private const int EN_ALPH_LAST_WORD = 90;
        private const int RUS_ALPH_FIRST_WORD = 1040;
        private const int RUS_ALPH_LAST_WORD = 1071;

        private readonly Point START_POINT;

        private Graphics g;
        private Pen pen;
        private Dictionary<char, int> EnSymbolsCount = new Dictionary<char, int>();
        private Dictionary<char, int> RusSymbolsCount = new Dictionary<char, int>();
        private List<string> TextWords = new List<string>();
        private Bitmap gistogrammEn, gistogrammRus;
        private int EnMaxValue, RusMaxValue;
        private int scaleXEn, scaleYEn, scaleXRus, scaleYRus;
        private string plainText;
        

        public MainForm()
        {
            InitializeComponent();
            pen = new Pen(Color.Black, 2f);
            START_POINT = new Point(
                Convert.ToInt32(AnalyzeGistogrammEn.Width * MARGIN),
                Convert.ToInt32(AnalyzeGistogrammEn.Height * MARGIN));
        }

        private void CloseAppBut_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            gistogrammEn = new Bitmap(AnalyzeGistogrammEn.Width, AnalyzeGistogrammEn.Height);
            gistogrammRus = new Bitmap(AnalyzeGistogrammRus.Width, AnalyzeGistogrammRus.Height);
            ResetGistogramm(gistogrammEn, AnalyzeGistogrammEn);
            ResetGistogramm(gistogrammRus, AnalyzeGistogrammRus);
            
        }

        private void PlainText_TextChanged(object sender, EventArgs e)
        {

        }

        private void AnalyzeSymbolsType_Click(object sender, EventArgs e)
        {
            ResetGistogramm(gistogrammEn, AnalyzeGistogrammEn);
            ResetGistogramm(gistogrammRus, AnalyzeGistogrammRus);
            SymbolsCountCalculate();
            BuildGistogrammForSymbols(EnSymbolsCount, EnMaxValue, gistogrammEn, AnalyzeGistogrammEn);
            BuildGistogrammForSymbols(RusSymbolsCount, RusMaxValue, gistogrammRus, AnalyzeGistogrammRus);
        }

        private void AnalyzeWordsType_Click(object sender, EventArgs e)
        {
            WordsCountCalculate();
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
            EnSymbolsCount.Clear();
            RusSymbolsCount.Clear();
            for(int i = EN_ALPH_FIRST_WORD; i <= EN_ALPH_LAST_WORD; i++)
            {
                EnSymbolsCount.Add((char)i, 0);
            }
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
            EnMaxValue = 0;
            RusMaxValue = 0;
            InitDictionaryForSymbols();
            plainText = PlainText.Text.ToUpper();
            for (int i = 0; i < plainText.Length; i++)
            {
                if (plainText[i] >= EN_ALPH_FIRST_WORD && plainText[i] <= EN_ALPH_LAST_WORD)
                    EnSymbolsCount[plainText[i]]++;
                else if (plainText[i] >= RUS_ALPH_FIRST_WORD && plainText[i] <= RUS_ALPH_LAST_WORD)
                    RusSymbolsCount[plainText[i]]++;
            }

            for (int i = EN_ALPH_FIRST_WORD; i <= EN_ALPH_LAST_WORD; i++)
            {
                if (EnSymbolsCount[(char)i] > EnMaxValue) EnMaxValue = EnSymbolsCount[(char)i];
            }

            for (int i = RUS_ALPH_FIRST_WORD; i <= RUS_ALPH_LAST_WORD; i++)
            {
                if (RusSymbolsCount[(char)i] > RusMaxValue) RusMaxValue = RusSymbolsCount[(char)i];
            }
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
            int scaleX = Convert.ToInt32(alphMap.Width * 0.9 / alphSymbolsCount.Count);
            int scaleY = Convert.ToInt32(alphMap.Height * 0.9 / MaxCount);
            g = Graphics.FromImage(alphMap);
            List<int> values = alphSymbolsCount.Values.ToList();
            List<char> keys = alphSymbolsCount.Keys.ToList();
            for (int i = 0; i < values.Count; i++)
            {
                int x = START_POINT.X + scaleX * i;
                int y = ((MaxCount - values[i]) * scaleY + START_POINT.Y) ;
                int height = Convert.ToInt32(alphMap.Height * (1-  2 * MARGIN)) - y + START_POINT.Y;
                if (values[i] != 0) g.DrawRectangle(pen, new Rectangle(x, y, 10, height));
                else y = Convert.ToInt32(alphMap.Height * (1 - MARGIN));
                g.DrawString(Convert.ToString(keys[i]), new Font("Arial", 9), Brushes.Black, new Point(START_POINT.X + scaleX * i, alphMap.Height - 20));
                g.DrawString(Convert.ToString(values[i].ToString()), new Font("Arial", 9), Brushes.Black, new Point(START_POINT.X + scaleX * i, y - 20));

            }
            g.Dispose();
            PB.Image = alphMap;
        }

        private void WordsCountCalculate()
        {
            plainText = PlainText.Text;
            char[] separators = new char[] { ' ', '.', ',', '<', '>', '"', ':', ';', '-', '+', '=', '\'', '/', '?', '!', '@', '*' };

            TextWords = plainText.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
