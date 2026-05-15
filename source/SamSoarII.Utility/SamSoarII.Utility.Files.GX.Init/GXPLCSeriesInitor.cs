namespace SamSoarII.Utility.Files.GX.Init;

public class GXPLCSeriesInitor
{
	public static void Init()
	{
		GXPLCSeries gXPLCSeries = GXPLCSeries.Items[0];
		gXPLCSeries.Remap.Clear();
		gXPLCSeries.Remap.Add("M8000", new GXSystemRemap("M8000", "M8151"));
		gXPLCSeries.Remap.Add("M8001", new GXSystemRemap("M8001", "M8152"));
		gXPLCSeries.Remap.Add("M8002", new GXSystemRemap("M8002", "M8150"));
		gXPLCSeries.Remap.Add("M8003", new GXSystemRemap("M8003", null));
		gXPLCSeries.Remap.Add("M8004", new GXSystemRemap("M8004", "M8174"));
		gXPLCSeries.Remap.Add("M8005", new GXSystemRemap("M8005", "M8153"));
		gXPLCSeries.Remap.Add("M8006", new GXSystemRemap("M8006", "M8153"));
		gXPLCSeries.Remap.Add("M8007", new GXSystemRemap("M8007", null));
		gXPLCSeries.Remap.Add("M8008", new GXSystemRemap("M8008", null));
		gXPLCSeries.Remap.Add("M8009", new GXSystemRemap("M8009", "M8154"));
		gXPLCSeries.Remap.Add("M8011", new GXSystemRemap("M8011", "M8158"));
		gXPLCSeries.Remap.Add("M8012", new GXSystemRemap("M8012", "M8159"));
		gXPLCSeries.Remap.Add("M8013", new GXSystemRemap("M8013", "M8160"));
		gXPLCSeries.Remap.Add("M8014", new GXSystemRemap("M8014", "M8161"));
		gXPLCSeries.Remap.Add("M8015", new GXSystemRemap("M8015", null));
		gXPLCSeries.Remap.Add("M8016", new GXSystemRemap("M8016", null));
		gXPLCSeries.Remap.Add("M8017", new GXSystemRemap("M8017", null));
		gXPLCSeries.Remap.Add("M8018", new GXSystemRemap("M8018", null));
		gXPLCSeries.Remap.Add("M8019", new GXSystemRemap("M8019", null));
		gXPLCSeries.Remap.Add("M8020", new GXSystemRemap("M8020", "M8171"));
		gXPLCSeries.Remap.Add("M8021", new GXSystemRemap("M8021", "M8169"));
		gXPLCSeries.Remap.Add("M8022", new GXSystemRemap("M8022", "M8169"));
		gXPLCSeries.Remap.Add("M8023", new GXSystemRemap("M8023", null));
		gXPLCSeries.Remap.Add("M8024", new GXSystemRemap("M8024", null));
		gXPLCSeries.Remap.Add("M8025", new GXSystemRemap("M8025", null));
		gXPLCSeries.Remap.Add("M8026", new GXSystemRemap("M8026", null));
		gXPLCSeries.Remap.Add("M8027", new GXSystemRemap("M8027", null));
		gXPLCSeries.Remap.Add("M8028", new GXSystemRemap("M8028", null));
		gXPLCSeries.Remap.Add("M8029", new GXSystemRemap("M8029", null));
		gXPLCSeries.Remap.Add("M8030", new GXSystemRemap("M8030", null));
		gXPLCSeries.Remap.Add("M8031", new GXSystemRemap("M8031", "M8162"));
		gXPLCSeries.Remap.Add("M8032", new GXSystemRemap("M8032", "M8163"));
		gXPLCSeries.Remap.Add("M8033", new GXSystemRemap("M8033", null));
		gXPLCSeries.Remap.Add("M8034", new GXSystemRemap("M8034", "M8164"));
		gXPLCSeries.Remap.Add("M8035", new GXSystemRemap("M8035", null));
		gXPLCSeries.Remap.Add("M8036", new GXSystemRemap("M8036", null));
		gXPLCSeries.Remap.Add("M8037", new GXSystemRemap("M8037", null));
		gXPLCSeries.Remap.Add("M8038", new GXSystemRemap("M8038", null));
		gXPLCSeries.Remap.Add("M8039", new GXSystemRemap("M8039", null));
		gXPLCSeries.Remap.Add("M8040", new GXSystemRemap("M8040", "M8020"));
		gXPLCSeries.Remap.Add("M8041", new GXSystemRemap("M8041", "M8021"));
		gXPLCSeries.Remap.Add("M8042", new GXSystemRemap("M8042", "M8022"));
		gXPLCSeries.Remap.Add("M8043", new GXSystemRemap("M8043", "M8023"));
		gXPLCSeries.Remap.Add("M8044", new GXSystemRemap("M8044", "M8024"));
		gXPLCSeries.Remap.Add("M8045", new GXSystemRemap("M8045", "M8025"));
		gXPLCSeries.Remap.Add("M8046", new GXSystemRemap("M8046", "M8026"));
		gXPLCSeries.Remap.Add("M8047", new GXSystemRemap("M8047", "M8027"));
		gXPLCSeries.Remap.Add("M8048", new GXSystemRemap("M8048", null));
		gXPLCSeries.Remap.Add("M8049", new GXSystemRemap("M8049", null));
		gXPLCSeries.Remap.Add("M8050", new GXSystemRemap("M8050", null));
		gXPLCSeries.Remap.Add("M8051", new GXSystemRemap("M8051", null));
		gXPLCSeries.Remap.Add("M8052", new GXSystemRemap("M8052", null));
		gXPLCSeries.Remap.Add("M8053", new GXSystemRemap("M8053", null));
		gXPLCSeries.Remap.Add("M8054", new GXSystemRemap("M8054", null));
		gXPLCSeries.Remap.Add("M8055", new GXSystemRemap("M8055", null));
		gXPLCSeries.Remap.Add("M8056", new GXSystemRemap("M8056", null));
		gXPLCSeries.Remap.Add("M8057", new GXSystemRemap("M8057", null));
		gXPLCSeries.Remap.Add("M8058", new GXSystemRemap("M8058", null));
		gXPLCSeries.Remap.Add("M8059", new GXSystemRemap("M8059", null));
		gXPLCSeries.Remap.Add("M8060", new GXSystemRemap("M8060", "M8155"));
		gXPLCSeries.Remap.Add("M8061", new GXSystemRemap("M8061", "M8156"));
		gXPLCSeries.Remap.Add("M8062", new GXSystemRemap("M8062", null));
		gXPLCSeries.Remap.Add("M8063", new GXSystemRemap("M8063", null));
		gXPLCSeries.Remap.Add("M8064", new GXSystemRemap("M8064", null));
		gXPLCSeries.Remap.Add("M8065", new GXSystemRemap("M8065", null));
		gXPLCSeries.Remap.Add("M8066", new GXSystemRemap("M8066", null));
		gXPLCSeries.Remap.Add("M8067", new GXSystemRemap("M8067", null));
		gXPLCSeries.Remap.Add("M8068", new GXSystemRemap("M8068", null));
		gXPLCSeries.Remap.Add("M8069", new GXSystemRemap("M8069", null));
	}
}
