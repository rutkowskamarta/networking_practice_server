namespace ServerPlugin.Tags
{
	public class Tags
	{
        //request tags
        public static readonly ushort PlayerDataRequest = 0;
        public static readonly ushort JoinRoomRequest = 1;
        public static readonly ushort LeaveRoomRequest = 2;
        public static readonly ushort CreateRoomRequest = 3;

        //response tags
        public static readonly ushort CreateRoomResponseSucess = 100;
        public static readonly ushort CreateRoomResponseFail = 101;

    }
}
