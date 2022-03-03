namespace ServerPlugin.Tags
{
	public class Tags
	{
        //request tags
        public static readonly ushort PlayerDataRequest = 0;
        public static readonly ushort JoinRoomRequest = 1;
        public static readonly ushort LeaveRoomRequest = 2;
        public static readonly ushort CreateRoomRequest = 3;

        public static readonly ushort StartGame = 4;

        public static readonly ushort AddCategory = 5;
        public static readonly ushort RemoveCategory = 6;
        public static readonly ushort SetRoundsNumber = 7;
        public static readonly ushort PlayerReady = 8;
        public static readonly ushort ContinueToGame = 9;
        public static readonly ushort StopTheGame = 10;
        public static readonly ushort SubmitTheAnswers = 11;


        //response tags
        public static readonly ushort CreateRoomResponseSucess = 100;
        public static readonly ushort CreateRoomResponseFail = 101;
        public static readonly ushort UpdateRoomState = 102;

        public static readonly ushort JoinRoomResponseSucess = 103;
        public static readonly ushort JoinRoomResponseFail = 104;


    }
}
