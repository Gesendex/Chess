using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chess
{
    class ChessEngine : IDisposable
    {
        Process engine;
        private bool mesIsReceived = false;
        public bool isStarted { get; private set; } = false;
        private string responceFromEngine;
        public string responce
        { 
            get
            {
                return responceFromEngine;
            }
            set 
            {
                responceFromEngine = value;
                mesIsReceived = true;
            }
        }
        public ChessEngine()
        {
            engine = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = $@"stockfish_13_win_x64.exe",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardInput = true,
                    CreateNoWindow = true,
                }
            };
            
        }


        public void Start()
        {
            engine.Start();
            engine.BeginOutputReadLine();
            engine.OutputDataReceived += new DataReceivedEventHandler(Recive);
            isStarted = true;
        }
        public void Recive(object sender, DataReceivedEventArgs e)
        {
            Match match = Regex.Match(e.Data, "bestmove (.*?) ponder");
            if (match.Length != 0)
                responce = match.Groups[1].ToString();
        }
        public string MakeMove(string command)
        {
                engine.StandardInput.WriteLine(command);
                while (!mesIsReceived)
                {
                }
                mesIsReceived = false;
                return responce;
        }
        public void RunCommand(string command)
        {
            engine.StandardInput.WriteLine(command);
        }

        public void Dispose()
        {
            engine.Dispose();
        }
    }
}
