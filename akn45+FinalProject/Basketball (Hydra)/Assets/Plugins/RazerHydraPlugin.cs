using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
public class RazerHydraPlugin {

	public struct sixenseControllerData{
		public Vector3 position;
		public Vector3 rot_mat_x;
		public Vector3 rot_mat_y;
		public Vector3 rot_mat_z;
		public float joystick_x;
		public float joystick_y;
		public byte trigger;
		public int buttons;
		public byte sequence_number;
		public Quaternion rotation; 
		public short firmware_revision;
		public short hardware_revision;
		public short packet_type;
		public short magnetic_frequency;
		public int enabled;
		public int controller_index;
		public byte is_docked;
		public byte which_hand;
	};

	public sixenseControllerData data;

	[DllImport ("sixense")] private static extern int sixenseInit( );
	[DllImport ("sixense")] private static extern int sixenseExit( );
	[DllImport ("sixense")] private static extern int
	sixenseGetNewestData(int which, out sixenseControllerData data);
	[DllImport ("sixense")] private static extern int
	sixenseSetFilterEnabled(int filterEnabled);

	[DllImport ("sixense")] private static extern int sixenseGetMaxBases(
	);
	[DllImport ("sixense")] private static extern int sixenseSetActiveBase(
	int baseNum);
	[DllImport ("sixense")] private static extern int
	sixenseIsBaseConnected(int baseNum );
	[DllImport ("sixense")] private static extern int
	sixenseGetMaxControllers( );
	[DllImport ("sixense")] private static extern int
	sixenseGetNumActiveControllers( );
	[DllImport ("sixense")] private static extern int
	sixenseIsControllerEnabled(int which);
	[DllImport ("sixense")] private static extern int
	sixenseGetHistorySize( );
	[DllImport ("sixense")] private static extern int
	sixenseGetFilterEnabled(out int filterEnabled);
	[DllImport ("sixense")] private static extern int
	sixenseSetFilterParams(float nearRange, float nearVal, float farRange, float
		farVal );
	[DllImport ("sixense")] private static extern int
	sixenseGetFilterParams(out float nearRange,out float nearVal,out float
		farRange,out float farVal );
	[DllImport ("sixense")] private static extern int
	sixenseTriggerVibration(int controllerId, int duration100ms, int patternId );
	[DllImport ("sixense")] private static extern int
	sixenseAutoEnableHemisphereTracking(int whichController );
	[DllImport ("sixense")] private static extern int
	sixenseSetHighPriorityBindingEnabled(int onOrOff );
	[DllImport ("sixense")] private static extern int
	sixenseGetHighPriorityBindingEnabled(out int onOrOff );

	public int init(){ 
		return sixenseInit();
	}

	public int exit(){
		return sixenseExit();
	}
	public void setFilterEnabled(int enabled){
		sixenseSetFilterEnabled(enabled);
	}

	public int getNewestData(int id){
		sixenseGetNewestData(id, out data);
		return 0;
	}
} 