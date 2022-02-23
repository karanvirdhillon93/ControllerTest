namespace RC.CodingChallenge {
  using System.IO;
  using System.Collections;
  using System.Text.RegularExpressions;
  using System.Linq;
  public class EventCounter: IEventCounter {
    private Object thisLock = new Object();
/// <param name="EventCount">EventCount will keep track of the number of cycles found in the timestamps provided</param>
    private int eventCount;
    private string deviceID;

  /// <param name="fileContents">fileContents will hold our data read from the inputfile</param>
    private ArrayList fileContents = new ArrayList();
    public EventCounter(string deviceID) {
      this.deviceID = deviceID;
      this.eventCount = 0;
    }



    public EventCounter() {
      this.deviceID = "";
      this.eventCount = 0;
    }

    public string DeviceID {
      get {
        return deviceID;
      }
      set {
        deviceID = value;
      }
    }

///  <summary>
/// Parse and accumulate event information from the given log data.
/// </summary>
/// <param name="deviceID">ID of the device that the log is associated with (ex: "HV1")</param>
/// <param name="eventLog">A stream of lines representing time/value recordings.</param>
    public void ParseEvents(string deviceID, StreamReader eventLog) {
      lock(thisLock) {
        this.deviceID = deviceID;
        while (!eventLog.EndOfStream) {
          string[] currentLine = eventLog.ReadLine().Split("");
          foreach(string column in currentLine) {
            string pattern = @"([^\s]+)";
            Regex rgx = new Regex(pattern);
            foreach(Match match in rgx.Matches(column)) {
              fileContents.Add(match.Value);
            }
          }
        }
        eventLog.Close();
      }
    }
///  <summary>
/// Checking for A design flaw found in a type of HVAC unit:
/// 1) We are checking if the unit is in Stage 3 for five minutes or more
/// </summary>
    private bool StageTwoValueFault() {
      lock(thisLock) {
        bool StageTwoValueFault = false;
        for (int i = 0; i < fileContents.Count; i++) {
          Console.WriteLine(fileContents[i]);
          if (fileContents[i].Equals("3") && i + 3 <= fileContents.Count) {
            if (fileContents[i + 3].Equals("3")) {
              DateTime startTime = Convert.ToDateTime(fileContents[i - 1]);
              DateTime endtime = Convert.ToDateTime(fileContents[i + 2]);
              TimeSpan duration = endtime - startTime;
              if (duration.Minutes >= 5) {
                StageTwoValueFault = true;
                break;
              }
            }
          }

        }

        return StageTwoValueFault;
      }
    }
///  <summary>
/// Checking for A design flaw found in a type of HVAC unit:
/// 1) We are checking if the unit is in Stage 3 for five minutes or more
/// 2) Followed by a direct transition to stage 2,
/// </summary>
    private bool StageTwoValueFault() {
      lock(thisLock) {
        bool stageTwoFault = false;
        if (StageTwoValueFault()) {
          for (int i = 0; i < fileContents.Count; i++) {
            if (fileContents[i].Equals("3") && i + 3 <= fileContents.Count) {
              if (fileContents[i + 3].Equals("2")) {
                stageTwoFault = true;
                break;
              }
            }
          }

        }
        return stageTwoFault;
      }
    }
///  <summary>
/// Checking for A design flaw found in a type of HVAC unit:
/// 1) We are checking if the unit is in Stage 3 for five minutes or more
/// 2) Followed by a direct transition to stage 2
/// 3) Followed by any number of cycles between stage 3 and 2
/// </summary>
    private bool CycleFault() {
      lock(thisLock) {
        Console.WriteLine("entry");
        bool cyclesFault = false;
        if (stageTwoValueFault()) {
          for (int i = 0; i < fileContents.Count; i++) {
            if (fileContents[i].Equals("3") && i + 3 <= fileContents.Count) {
              if (fileContents[i + 3].Equals("2") || fileContents[i + 3].Equals("3")) {
                cyclesFault = true;
                Console.WriteLine("exit");
                break;
              }
            }
          }
        }

        return cyclesFault;
      }
    }
///  <summary>
/// Checking for A design flaw found in a type of HVAC unit:
/// 1) We are checking if the unit is in Stage 3 for five minutes or more
/// 2) Followed by a direct transition to stage 2
/// 3) Followed by any number of cycles between stage 3 and 2
/// 4) Followed by a transition to stage 0 from either stage 2 or 3.
/// </summary>
    private bool StageZeroFault() {
      lock(thisLock) {
        bool StageZeroFault = false;
        if (StageTwoValueFault()) {
          for (int i = 0; i < fileContents.Count; i++) {
            if ((fileContents[i].Equals("2") || fileContents.Equals("3")) && i + 3 <= fileContents.Count) {
              if (fileContents[i + 3].Equals("0")) {
                StageZeroFault = true;
                break;
              }
            }
          }
        }
        Console.WriteLine("complete");
        return StageZeroFault;
      }
    }
/// <summary>
/// Gets the current count of events detected for the given device
/// </summary>
/// <returns>An integer representing the number of detected events</returns>
    public int GetEventCount(string deviceID) {
      lock(thisLock) {
        if (this.deviceID.Equals(deviceID)) {
          Console.WriteLine("Trused Device");
        }
        for (int i = 5; i < fileContents.Count; i += 3) {
          string value = (string) fileContents[i];
          if (value.All(char.IsDigit))
          this.eventCount++;
        }
        Console.WriteLine("eventCount:"+eventCount);
        return this.eventCount;
      }
    }
  }

}