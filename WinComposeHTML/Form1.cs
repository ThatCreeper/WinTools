using static System.Net.Mime.MediaTypeNames;

namespace WinComposeHTML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
            else if (e.KeyCode == Keys.Return)
            {
                string text = label1.Text;
                Program.lastText = textBox1.Text;
                Close();

                if (e.Shift)
                {
                    Clipboard.SetText(text);
                }
                else
                {
                    SendKeys.Send(text);
                }
            }
            else if (e.KeyCode == Keys.C && e.Control)
            {
                Clipboard.SetText(label1.Text);
                Program.lastText = textBox1.Text;
                Close();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Form1_KeyUp(sender, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetDesktopLocation(
                x: Cursor.Position.X - PointToScreen(textBox1.Location).X + Location.X,
                y: Cursor.Position.Y - PointToScreen(textBox1.Location).Y + Location.Y - textBox1.Size.Height);

            Focus();
            textBox1.Focus();
            textBox1.Text = Program.lastText;
            textBox1.SelectAll();

            textBox1_TextChanged(sender, e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           string text = textBox1.Text;
            string? res;
            if (Entities.map.TryGetValue(text, out res))
                label1.Text = res;
            else if (Entities.map.TryGetValue($"{text};", out res))
                label1.Text = res;
            else if (label1.Text.Length > 1 && text[0] == '&' && Entities.map.TryGetValue($"{text.Substring(1, text.Length - 1)};", out res))
                label1.Text = res;
            else if (label1.Text.Length > 1 && text.EndsWith(";") && Entities.map.TryGetValue(text.Substring(0, text.Length - 1), out res))
                label1.Text = res;
            else if (label1.Text.Length > 1 && text[0] == '&' && Entities.map.TryGetValue(text.Substring(1, text.Length - 1), out res))
                label1.Text = res;
            else if (text.Length > 2 && text[0] == '&' && text.EndsWith(";") && Entities.map.TryGetValue(text.Substring(1, text.Length - 2), out res))
                label1.Text = res;
            else
                label1.Text = text;
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            Form1_KeyUp(sender, e);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            Form1_KeyDown(sender, e);
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            Close();
        }
    }
}
