using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Virtion.IM.View
{
    public class CommandExecutor
    {
        private Queue<Action> CommandQueue;
        public CommandExecutor()
        {
            this.CommandQueue = new Queue<Action>();
        }

        public void Push(Action cmd)
        {
            this.CommandQueue.Enqueue(cmd);

            BackgroundWorker bw = new BackgroundWorker();
            bw.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);
            bw.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
            bw.RunWorkerAsync("Tank");
        }


        void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var cmd = this.CommandQueue.Peek();
            try
            {
                cmd.DynamicInvoke();
            }
            catch (Exception ex)
            {
                //VDebug.Error(ex);
            }
            finally 
            {
                this.CommandQueue.Dequeue();

            }
        }

        void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //MainWindow.imMagr.CheckListeners();

        }


    }
}
