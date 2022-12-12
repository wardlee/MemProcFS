using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vmmsharp;


public class VmmH
{
    public uint PID { get; set; }
    public string NAME { get; set; }
    public Vmm vmm { get; set; }
    public VmmH(uint pId, string name)
    {
        PID = pId;
        NAME = name;
        vmm = new Vmm("-printf", "-v", "-device", "fpga");
        
    }

    public byte[] Read(ulong addr, uint size)
    {
        return vmm.MemRead(PID, addr, size, 0);

    }
    public ulong ReadUL(ulong addr)
    {
        byte[] data = Read(addr, 64);
        return BitConverter.ToUInt64(data, 0);
    }

    public uint ReadUI(ulong addr)
    {
        byte[] data = Read(addr, 32);
        return BitConverter.ToUInt32(data, 0);
    }

    public string ReadStr(ulong addr)
    {
        byte[] data = Read(addr, 2048);
        string s = Encoding.Default.GetString(data);
        var index = s.IndexOf("\0");
        if (index > 0)
            return s.Substring(0, index);
        return s;
    }
    public string ReadStrUni(ulong addr)
    {
        byte[] data = Read(addr, 2048);
        string s = Encoding.Unicode.GetString(data);
        var index = s.IndexOf("\0");
        if (index > 0)
            return s.Substring(0, index);
        return s;
    }
    public Vmm.MAP_MODULEENTRY GetModuleFromName()
    {
        return vmm.Map_GetModuleFromName(PID, NAME);
    }

}

