#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.

namespace KTANE_Diffusal_Assistant
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void onLoad(object sender, EventArgs e)
        {
            Expert expert = new Expert(this);
            foreach (string voice in expert.GetAllVoices())
            {
                comboBox1.Items.Add(voice);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void onVoiceChanged(object sender, EventArgs e)
        {
            Expert.instance.SetVoice(comboBox1.Items[comboBox1.SelectedIndex].ToString());
        }

        public void setResponse(string response)
        {
            txtSpeech.Text = response;
        }

        public void setSpeech(string command)
        {
            txtRecognised.Text = command;
        }

        private void listenButtonClicked(object sender, EventArgs e)
        {
            if (!Expert.instance.isListening)
            {
                button1.BackColor = Color.Red;
                button1.Text = "Stop Listening";
                Expert.instance.startListening();
            }
            else
            {
                button1.BackColor = Color.Green;
                button1.Text = "Start Listening";
                Expert.instance.stopListening();
            }
        }
    }
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.