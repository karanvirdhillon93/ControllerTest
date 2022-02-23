using System;

namespace RC.CodingChallenge {
  class Program {
    static void Main(string[] args) {



    /*
      How to:
      1)Assign a local tab-delimited .txt/.csv file to the textFile variable, or you may use the default settings provided
      2)Assign a value to deviceID or leave it as is
      3)Instantiate a new object of type EventCounter, or leave it as is
      4)use the start() method to begin project
    */
      string textFile = "eventLogs.csv";
      string deviceID="HVAC unitTest";
      EventCounter ec = new EventCounter(deviceID);
      start(textFile,deviceID,ec);
      ec.GetEventCount(deviceID);


  }
public static void start(string textFile,string deviceID,EventCounter ec){
try {
        using(FileStream fs = new FileStream(textFile, FileMode.Open, FileAccess.Read)) {
          StreamReader sr = new StreamReader(fs);
          ec.ParseEvents(deviceID, sr);
        }
      } catch (IOException e) {
        Console.WriteLine("The file could not be read:");
        Console.WriteLine(e.Message);
      }

        
    }
}


}