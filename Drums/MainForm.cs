using System;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace Drums
{
	public partial class MainForm : Form
	{
		static bool _continue;
		static SerialPort _serialPort;
		public MainForm()
		{
			InitializeComponent();
			Thread readThread = new Thread(Read);
			_serialPort = new SerialPort();
			_serialPort.PortName = "COM3"; //поменять на свой, запилить падающее меню на выбор порта
			_serialPort.BaudRate = 9600;
			_serialPort.ReadTimeout = 500;
			_serialPort.WriteTimeout = 500;
			_serialPort.Open();
			_continue = true;
			readThread.Start();
       

		}
		public void Read()
		{
			while (_continue) {
				try {
					string message = _serialPort.ReadLine();
					if (message.IndexOf("x") != -1)
						playSound("bum.wav");
					if (message.IndexOf("o") != -1)
						playSound("tish.wav");
            
				} catch (TimeoutException) {
				}
			}
		}
		void playSound(string path)
		{
			System.Media.SoundPlayer player =
				new System.Media.SoundPlayer();
			player.SoundLocation = path;
			player.Load();
			player.Play();
		}
		void MainFormFormClosed(object sender, FormClosedEventArgs e)
		{
		
			_serialPort.Close();
		}
	}
}
