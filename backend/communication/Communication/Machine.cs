using Opc.UaFx.Client;
using System.Diagnostics;
namespace communication.Communication
{
    public class Machine
    {
        private static Machine? instance;
        //private string opcUrl = "opc.tcp://localhost:4840";
        private Machine(){}
        public static Machine GetInstance()
        {
            if (instance == null)
            {
                instance = new Machine();
            }
            return instance;
        }

       

        //public void testRead()
        //{
        //    string tagName = "ns=6;s=::Program:Cube.Command.Parameter[0].Value";
        //    var client = new OpcClient(opcUrl);
        //    client.Connect();
        //    var r = client.ReadNode(tagName);
        //    client.Disconnect();
        //    Debug.WriteLine(r.ToString());
        //}

    }
}
