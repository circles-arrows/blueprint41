﻿using Blueprint41.Modeller.Schemas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Model = Blueprint41.Modeller.Schemas.Modeller;

namespace Blueprint41.Modeller
{
    public class Recovery
    {
        readonly Timer timer;
        MainForm form;
        public string RecoveryFile => Path.Combine(Path.GetTempPath(), "modeller.recovery.bak");

        private string BackupFile => Path.Combine(Path.GetTempPath(), "modeller.bak");

        Action saveComplete;

        private static Lazy<Recovery> instance = new Lazy<Recovery>(() => new Recovery());
        public static Recovery Instance => instance.Value;

        const int INTERVAL = 10000;

        private Recovery()
        {
            timer = new Timer(INTERVAL);
            timer.Elapsed += Timer_ElapsedAsync;
        }

        private async void Timer_ElapsedAsync(object sender, ElapsedEventArgs e)
        {
            //Run on background
            await Task.Run(() =>
            {
                if (form.Model != null)
                {
                    try
                    {
                        Model newModel = new Model(null, (modeller)form.Model.Xml.Clone());
                        newModel.Save(RecoveryFile);
                        newModel = null;
                        Console.WriteLine($"Saved to {RecoveryFile}");
                    }
                    catch { }
                }
            });

            saveComplete?.Invoke();
        }

        public void Start(MainForm form, Action onSaveComplete = null)
        {
            this.form = form;
            saveComplete = onSaveComplete;

            timer.Enabled = true;
            timer.Start();
        }

        public void Stop()
        {
            if (File.Exists(BackupFile))
                File.Delete(BackupFile);

            if (File.Exists(RecoveryFile))
                File.Move(RecoveryFile, BackupFile);

            timer.Enabled = false;
            timer.Stop();
        }
    }
}
