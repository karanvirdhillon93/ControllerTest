//-----------------------------------------------------------------------------
// Description:
//      A service that identifies and counts events which are associated with a
//      specific sequence of operations.
//
//      The input logs are comprised of lines of text that indicate the time of a
//      recording, and the value recorded
//
//      1998-03-07 06:25:32	2
//      1998-03-07 09:15:55	3
//      1998-03-07 12:00:02	3
//      1998-03-07 14:28:27	0
//
//      The columns (1) date+time in ISO-8601 format, (2) value indicating HVAC
//      unit stage by tabs.
//-----------------------------------------------------------------------------
namespace RC.CodingChallenge
{
    using System.IO;

    public interface IEventCounter
    {
        /// <summary>
        /// Parse and accumulate event information from the given log data.
        /// </summary>
        /// <param name="deviceID">ID of the device that the log is associated with (ex: "HV1")</param>
        /// <param name="eventLog">A stream of lines representing time/value recordings.</param>
        void ParseEvents(string deviceID, StreamReader eventLog);

        /// <summary>
        /// Gets the current count of events detected for the given device
        /// </summary>
        /// <returns>An integer representing the number of detected events</returns>
        int GetEventCount(string deviceID);
    }
}
