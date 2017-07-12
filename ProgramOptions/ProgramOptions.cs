﻿using CommandLine;
using CommandLine.Text;
using System;

namespace AppDynamics.OfflineData
{
    class ProgramOptions
    {
        [Option('j', "jobfile", Required = true, HelpText = "Input file defining the job to process.")]
        public string InputJobFilePath { get; set; }

        [Option('r', "restart", Required = false, HelpText = "Restart processing of job file if it is in the middle of processing already.")]
        public bool RestartJobFromBeginning { get; set; }

        [Option('o', "outputfolder", Required = false, HelpText = "Output folder where to store results of jobs.")]
        public string OutputFolderPath { get; set; }

        public string OutputJobFolderPath { get; set;}

        public string OutputJobFilePath { get; set; }

        public string JobName { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }

        public override string ToString()
        {
            return String.Format("ProgramOptions:\r\nInputJobFilePath='{0}'\r\nRestartJobFromBeginning='{1}'\r\nOutputFolderPath='{2}'\r\nOutputJobFolderPath='{3}'\r\nOutputJobFilePath='{4}'", this.InputJobFilePath, this.RestartJobFromBeginning, this.OutputFolderPath, this.OutputJobFolderPath, this.OutputJobFilePath);
        }
    }
}
