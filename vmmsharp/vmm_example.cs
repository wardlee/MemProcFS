using System;
using System.Runtime.InteropServices;
using System.Threading;
using vmmsharp;

/*  
 *  Examples of using the PCILeech / LeechCore / MemProcFS "Lc" and "Vmm" APIs for C#
 *  
 *  1) Include the file 'vmmsharp.cs' in your project.
 *  2) Make sure your C# project is run as x64 (not x86 or AnyCPU). This is because the
 *     natíve 'leechcore.dll' and 'vmm.dll' only exists as 64-bit native binaries.
 *  3) Make sure the MemProcFS binaries (vmm.dll / leechcore.dll and related binaries)
 *     are located alongside your C# binary or in the "current directory".
 *     
 *  The examples in this file generally don't print anything on the screen, but if
 *  running it from within Visual Studio with breakpoints it should be fairly easy
 *  to follow the calls and have a look at the different return data.
 *  
 *  (c) Ulf Frisk, 2020-2022
 *  Author: Ulf Frisk, pcileech@frizk.net
 *  
 */
class vmm_example
{
    static bool ExampleVfsCallBack_AddFile(ulong h, [MarshalAs(UnmanagedType.LPUTF8Str)] string wszName, ulong cb, IntPtr pExInfo)
    {
        ulong ft = 0;
        if (pExInfo != IntPtr.Zero)
        {
            Vmm.VMMDLL_VFS_FILELIST_EXINFO n = Marshal.PtrToStructure<Vmm.VMMDLL_VFS_FILELIST_EXINFO>(pExInfo);
            ft = n.ftLastWriteTime;
        }
        Console.WriteLine("VFS LIST CALLBACK: HANDLE: " + h + " FILE: '" + wszName + "' SIZE '" + cb + "'\tFileWriteTime " + ft);
        return true;
    }

    static bool ExampleVfsCallBack_AddDirectory(ulong h, [MarshalAs(UnmanagedType.LPUTF8Str)] string wszName, IntPtr pExInfo)
    {
        ulong ft = 0;
        if (pExInfo != IntPtr.Zero)
        {
            Vmm.VMMDLL_VFS_FILELIST_EXINFO n = Marshal.PtrToStructure<Vmm.VMMDLL_VFS_FILELIST_EXINFO>(pExInfo);
            ft = n.ftLastWriteTime;
        }
        Console.WriteLine("VFS LIST CALLBACK: HANDLE: " + h + " FILE: '" + wszName + "'\tFileWriteTime " + ft);
        return true;
    }



    //constexpr auto Offset_GObjects = 0x090B1C20;
    //constexpr auto Offset_GWorld = 0x090F2FD0;
    //constexpr auto Offset_XenuineDecrypt = 0x07AE2528;
    //constexpr auto Offset_FNameEntry = 0x092D19F0;
    //constexpr auto Offset_ChunkSize = 0x3F70;
    //constexpr auto Offset_ObjID = 0x0014;

    //constexpr auto Offset_XorKey1 = 0xF2086122;
    //constexpr auto Offset_XorKey2 = 0x2A9398EB;
    //constexpr auto Offset_RorValue = 0x0E;
    //constexpr auto Offset_IsingRor = true;

    //constexpr auto Offset_CurrentLevel = 0x0320;
    //constexpr auto Offset_Actors = 0x00A0;
    //constexpr auto Offset_GameInstance = 0x0310;
    //constexpr auto Offset_LocalPlayers = 0x00E0;
    //constexpr auto Offset_PlayerController = 0x0030;
    //constexpr auto Offset_AcknowledgedPawn = 0x0490;
    //constexpr auto Offset_PlayerCameraManager = 0x04B0;
    //constexpr auto Offset_LocalPlayersPTR = 0x0922E3E0;

    //constexpr auto Offset_PlayerState = 0x0418;
    //constexpr auto Offset_PlayerStatistics = 0x0868;
    //constexpr auto Offset_SpectatedCount = 0x0EB0;
    //constexpr auto Offset_LastRenderTime = 0x075C;
    //constexpr auto Offset_Health = 0x0870;
    //constexpr auto Offset_GroggyHealth = 0x0E88;
    //constexpr auto Offset_Team = 0x21D0;
    //constexpr auto Offset_LastTeamNum = 0x21D8;
    //constexpr auto Offset_LastSquadMemberIndex = 0x21E0;
    //constexpr auto Offset_CharacterName = 0x12D0;
    //constexpr auto Offset_BaseAnimInstance = 0x2B28;
    //constexpr auto Offset_LastAimOffsets = 0x16AC;
    //constexpr auto Offset_AimOffsets = 0x16A0;
    //constexpr auto Offset_ControlRotation = 0x0428;

    //constexpr auto Offset_RootComponent = 0x0390;
    //constexpr auto Offset_ComponentLocation = 0x0290;

    //constexpr auto Offset_Mesh = 0x04E8;
    //constexpr auto Offset_StaticMesh = 0x0AD0;
    //constexpr auto Offset_ComponentToWorld = 0x0280;

    //constexpr auto Offset_CameraLocation = 0x1C74;
    //constexpr auto Offset_CameraRotation = 0x16E4;
    //constexpr auto Offset_CameraFov = 0x16E0;

    //constexpr auto Offset_ItemPackage = 0x0570;
    //constexpr auto Offset_ItemInformationComponent = 0x00A8;
    //constexpr auto Offset_ItemID = 0x0248;
    //constexpr auto Offset_DroppedItem = 0x0430;
    //constexpr auto Offset_DroppedItemGroup = 0x0228;
    //constexpr auto Offset_DroppedItemGroup_UItem = 0x0738;

    //constexpr auto Offset_WeaponProcessor = 0x0888;
    //constexpr auto Offset_EquippedWeapons = 0x01F8;
    //constexpr auto Offset_CurrentWeaponIndex = 0x02D9;
    //constexpr auto Offset_WeaponTrajectoryData = 0x1010;
    //constexpr auto Offset_TrajectoryGravityZ = 0x0FC0;
    //constexpr auto Offset_TrajectoryConfig = 0x0100;
    //constexpr auto Offset_FloatCurves = 0x0030;
    //constexpr auto Offset_CharacterMovement = 0x0458;
    //constexpr auto Offset_Acceleration = 0x0398;
    //constexpr auto Offset_MaxAcceleration = 0x02F8;
    //constexpr auto Offset_LastUpdateVelocity = 0x03C0;

    //constexpr auto Offset_AnimScriptInstance = 0x0C70;
    //constexpr auto Offset_ControlRotation_CP = 0x076C;
    //constexpr auto Offset_RecoilADSRotation_CP = 0x09CC;
    //constexpr auto Offset_LeanLeftAlpha_CP = 0x0DDC;
    //constexpr auto Offset_LeanRightAlpha_CP = 0x0DE0;
    //constexpr auto Offset_bIsScoping_CP = 0x0CF6;
    //constexpr auto Offset_bIsReloading_CP = 0x08B8;

    //constexpr auto Offset_VehicleRiderComponent = 0x1C80;
    //constexpr auto Offset_SeatIndex = 0x0218;
    //constexpr auto Offset_LastVehiclePawn = 0x0250;
    //constexpr auto Offset_ReplicatedMovement = 0x0078;

    //constexpr auto Offset_BackpackState = 0x093029C1;
    //constexpr auto Offset_IsHall = 0x08EF0C78;
    //constexpr auto Offset_MapState = 0x06D0;
    //constexpr auto Offset_WorldOrigin = 0x02B4;

    ////TslWheeledVehicle
    //constexpr auto Offset_VehicleCommonComponent = 0x0B40;
    //constexpr auto Offset_vehiclesHealth = 0x02C8;
    //constexpr auto Offset_vehiclesHealthMax = 0x02CC;
    //constexpr auto Offset_vehiclesFuel = 0x02D0;
    //constexpr auto Offset_vehiclesFuelMax = 0x02D4;
    //constexpr auto Offset_ExplosionTimer = 0x02D8;
    //constexpr auto Offset_ReplicatedSkinParam = 0x0458;

    ////ATslProjectile
    //constexpr auto Offset_TickStartVelocity = 0x0820;
    //constexpr auto Offset_TimeTillExplosion = 0x072C;
    //constexpr auto Offset_PreExplosionTime = 0x0728;
    //constexpr auto Offset_ExplosionDelay = 0x05C8;
    //constexpr auto Offset_ProjectileConfig = 0x05D0;
    //constexpr auto Offset_ExplodeState = 0x0570;

    ////ATslGameState
    //constexpr auto Offset_GameState = 0x0340;

    //constexpr auto Offset_TotalWarningDuration = 0x0660;
    //constexpr auto Offset_NumAlivePlayers = 0x0664;
    //constexpr auto Offset_NumJoinPlayers = 0x06F4;

    //constexpr auto Offset_LerpSafetyZonePosition = 0x06C8;
    //constexpr auto Offset_LerpSafetyZoneRadius = 0x0624;

    //constexpr auto Offset_PoisonGasWarningPosition = 0x06D8;
    //constexpr auto Offset_PoisonGasWarningRadius = 0x0658;

    //constexpr auto Offset_RedZonePosition = 0x06BC;
    //constexpr auto Offset_RedZoneRadius = 0x06D4;

    //constexpr auto Offset_BlackZonePosition = 0x0AE0;
    //constexpr auto Offset_BlackZoneRadius = 0x0AEC;

    //constexpr auto Offset_SafetyZoneBeginPosition = 0x0600;
    //constexpr auto Offset_SafetyZoneRadius = 0x0720;


    public static long bytesToLong(byte[] buffer)
    {
        if (buffer == null || buffer.Length < 8)
            return 0;
        long values = 0;
        for (int i = 0; i < 8; i++)
        {
            values <<= 8;
            values |= (buffer[i] & 0xff);
        }
        return values;
    }


    static void ExampleVmm()
    {
        bool result;
        uint nt;
        // initialize vmm with verbose mode with fpga device
        Vmm vmm = new Vmm("-printf", "-v", "-device", "fpga");

        uint cid;
        //vmm.PidGetFromName("Conquer.exe", out cid);

        //vmm.PidGetFromName("Conquer.exe", out cid);
        uint pid = 8956;
        var m = vmm.Map_GetModuleFromName(8956, "TslGame.exe");
        byte[] worldBase = vmm.MemRead(pid, m.vaBase + 0x090F2FD0, 64, 0);

        byte[] temp = vmm.MemRead(pid, (ulong)bytesToLong(worldBase)+3, 64, 0);
        ulong u = (ulong)bytesToLong(temp)+7;

        ulong p = (ulong)bytesToLong(worldBase)  + u;


        byte[] nameBase = vmm.MemRead(pid, m.vaBase + 0x92D19F0, 128, 0);
        


        //Console.WriteLine(System.Text.Encoding.Unicode.GetString(t));
        Console.ReadKey();

        //var s = System.Text.Encoding.Unicode.GetString(t);
        //while (true)
        //{
        //    for (int i = 0; i < 1000000; i++)
        //    {
        //        byte[] t = vmm.MemRead(cid, 0x0FFD0824, 128, 0);
        //        Console.WriteLine(System.Text.Encoding.Unicode.GetString(t));
        //        Thread.Sleep(100);
        //    }
        //    if (Console.ReadKey().Key == ConsoleKey.A  )
        //    {
        //        break;
        //    }
        //}

        
        return;
        // initialize vmm with verbose mode with dump file
        //Vmm vmm = new Vmm("-printf", "-v", "-device", "c:\\dumps\\WIN7-X64-SP1-1.pmem");

        // get / set vmm config options
        ulong ulOptionMM, ulOptionVV;
        result = vmm.ConfigGet(Vmm.OPT_CORE_MEMORYMODEL, out ulOptionMM);
        result = vmm.ConfigGet(Vmm.OPT_CORE_VERBOSE_EXTRA, out ulOptionVV);
        result = vmm.ConfigSet(Vmm.OPT_CORE_VERBOSE_EXTRA, 1);
        result = vmm.ConfigGet(Vmm.OPT_CORE_VERBOSE_EXTRA, out ulOptionVV);

        // initialize plugins (required for vfs)
        vmm.InitializePlugins();

        // vfs (virtual file system) list / read / write
        result = vmm.VfsList("\\", 1, ExampleVfsCallBack_AddFile, ExampleVfsCallBack_AddDirectory);
        byte[] pbMemoryRead;
        nt = vmm.VfsRead("\\memory.pmem", 0x200, 0x1000, out pbMemoryRead);
        nt = vmm.VfsWrite("\\memory.pmem, 0x200", pbMemoryRead, 0x1000);

        // memory read : physical with scatter function (2 pages)
        MEM_SCATTER[] MEMsPhysical = vmm.MemReadScatter(0xffffffff, 0, 0x1000, 0x2000);

        // retrieve all PIDs in the system as a sorted list.
        uint[] dwPidAll = vmm.PidList();

        // retrieve PID of explorer.exe (it's assumed it's started, otherwise example will fail)
        uint dwExplorerPID;
        vmm.PidGetFromName("explorer.exe", out dwExplorerPID);

        // get kernel path of explorer.exe
        string strKernel32KernelPath = vmm.ProcessGetInformationString(dwExplorerPID, Vmm.VMMDLL_PROCESS_INFORMATION_OPT_STRING_PATH_KERNEL);

        // retrieve process information of explorer.exe
        Vmm.PROCESS_INFORMATION ProcInfo = vmm.ProcessGetInformation(dwExplorerPID);

        // get procaddress of kernel32.dll!GetTickCount64 and module base
        ulong vaTickCount64 = vmm.ProcessGetProcAddress(dwExplorerPID, "kernel32.dll", "GetTickCount64");
        ulong vaKernel32Base = vmm.ProcessGetModuleBase(dwExplorerPID, "kernel32.dll");

        // retrieve Directories/Sections/IAT/EAT from kernel32.dll of explorer.exe
        Vmm.IMAGE_DATA_DIRECTORY[] DIRs = vmm.ProcessGetDirectories(dwExplorerPID, "kernel32.dll");
        Vmm.IMAGE_SECTION_HEADER[] SECTIONs = vmm.ProcessGetSections(dwExplorerPID, "kernel32.dll");

        // retrieve different "map" structures related to explorer.exe and the system.
        Vmm.MAP_PTEENTRY[] mPte = vmm.Map_GetPte(dwExplorerPID);
        Vmm.MAP_VADENTRY[] mVad = vmm.Map_GetVad(dwExplorerPID);
        Vmm.MAP_VADEXENTRY[] mVadEx = vmm.Map_GetVadEx(dwExplorerPID, 0, 10);
        Vmm.MAP_MODULEENTRY[] mModule = vmm.Map_GetModule(dwExplorerPID);
        Vmm.MAP_MODULEENTRY mModuleKernel32 = vmm.Map_GetModuleFromName(dwExplorerPID, "kernel32.dll");
        Vmm.MAP_UNLOADEDMODULEENTRY[] mUnloadedModule = vmm.Map_GetUnloadedModule(dwExplorerPID);
        Vmm.MAP_EATINFO EatInfo;
        Vmm.MAP_EATENTRY[] mEAT = vmm.Map_GetEAT(dwExplorerPID, "kernel32.dll", out EatInfo);
        Vmm.MAP_IATENTRY[] mIAT = vmm.Map_GetIAT(dwExplorerPID, "kernel32.dll");
        Vmm.MAP_HEAP mHeap = vmm.Map_GetHeap(dwExplorerPID);
        Vmm.MAP_HEAPALLOCENTRY[] mHeapAlloc = vmm.Map_GetHeapAlloc(dwExplorerPID, 2);
        Vmm.MAP_THREADENTRY[] mThreads = vmm.Map_GetThread(dwExplorerPID);
        Vmm.MAP_HANDLEENTRY[] mHandles = vmm.Map_GetHandle(dwExplorerPID);
        Vmm.MAP_NETENTRY[] mNetworkConnections = vmm.Map_GetNet();
        Vmm.MAP_PHYSMEMENTRY[] mPhysMemRanges = vmm.Map_GetPhysMem();
        Vmm.MAP_POOLENTRY[] mPoolAllocations = vmm.Map_GetPool();
        Vmm.MAP_USERENTRY[] mUsers = vmm.Map_GetUsers();
        Vmm.MAP_SERVICEENTRY[] mServices = vmm.Map_GetServices();
        Vmm.MAP_PFNENTRY[] mPfn = vmm.Map_GetPfn(1, 2, 1024);

        // read first 128 bytes of kernel32.dll
        byte[] dataKernel32MZ = vmm.MemRead(dwExplorerPID, mModuleKernel32.vaBase, 128, 0);

        // translate virtual address of 1st page in kernel32.dll to physical address
        ulong paBaseKernel32;
        result = vmm.MemVirt2Phys(dwExplorerPID, mModuleKernel32.vaBase, out paBaseKernel32);

        // read two independent chunks of memory in one single efficient call.
        // also use the nocache flag.
        VmmScatter scatter = vmm.Scatter_Initialize(dwExplorerPID, Vmm.FLAG_NOCACHE);
        if (scatter != null)
        {
            // prepare multiple ranges to read
            scatter.Prepare(mModuleKernel32.vaBase, 0x100);
            scatter.Prepare(mModuleKernel32.vaBase + 0x2000, 0x100);
            // execute actual read operation to underlying system
            scatter.Execute();
            byte[] pbKernel32_100_1 = scatter.Read(mModuleKernel32.vaBase, 0x80);
            byte[] pbKernel32_100_2 = scatter.Read(mModuleKernel32.vaBase + 0x2000, 0x100);
            // if scatter object is to be reused for additional reads after a
            // Execute() call it should be cleared before preparing new ranges.
            scatter.Clear(dwExplorerPID, Vmm.FLAG_NOCACHE);
            scatter.Prepare(mModuleKernel32.vaBase + 0x3000, 0x100);
            scatter.Prepare(mModuleKernel32.vaBase + 0x4000, 0x100);
            scatter.Execute();
            byte[] pbKernel32_100_3 = scatter.Read(mModuleKernel32.vaBase + 0x3000, 0x100);
            byte[] pbKernel32_100_4 = scatter.Read(mModuleKernel32.vaBase + 0x4000, 0x100);
            // clean up scatter handle hS (free native memory)
            // NB! hS handle should not be used after this!
            scatter.Close();
        }

        // load .pdb of kernel32 from microsoft symbol server and query it
        // also do some lookups for kernel symbols.
        string szPdbModuleName = "";
        result = vmm.PdbLoad(dwExplorerPID, mModuleKernel32.vaBase, out szPdbModuleName);
        if (result)
        {
            uint dwSymbolOffset = (uint)(mModuleKernel32.vaEntry - mModuleKernel32.vaBase);
            string szEntryPoint;
            uint dwEntryPointDisplacement;
            result = vmm.PdbSymbolName(szPdbModuleName, dwSymbolOffset, out szEntryPoint, out dwEntryPointDisplacement);
        }
        ulong vaKeQueryOwnerMutant;
        result = vmm.PdbSymbolAddress("nt", "KeQueryOwnerMutant", out vaKeQueryOwnerMutant);
        uint oOptionalHeaders;
        result = vmm.PdbTypeChildOffset("nt", "_IMAGE_NT_HEADERS64", "OptionalHeader", out oOptionalHeaders);

        // WINDOWS REGISTRY QUERY / READ / WRITE
        Vmm.REGISTRY_HIVE_INFORMATION[] RegHives = vmm.RegHiveList();
        if (RegHives.Length > 0)
        {
            byte[] RegHiveData = vmm.RegHiveRead(RegHives[0].vaCMHIVE, 0x1000, 0x100, 0);
        }
        Vmm.REGISTRY_ENUM RegEnum = vmm.RegEnum("HKLM\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion");
        if (RegEnum.ValueList.Count > 0)
        {
            uint RegValueType;
            byte[] RegValueData = vmm.RegValueRead(RegEnum.wszFullPathKey + "\\" + RegEnum.ValueList[0].name, out RegValueType);
        }

        // search efficiently in explorer.exe for "This program cannot be run in DOS mode"
        // (in essence perform a search for PE headers).
        // The search function may take up quite a lot of performance / time depending on memory amount.
        // There is also a vmm.MemSearchM function which allows for searching multiple strings at a time.
        ulong[] vaExplorerPE = vmm.MemSearch1(dwExplorerPID, System.Text.Encoding.ASCII.GetBytes("cannot be run in DOS mode"), 0, 0x7fffffffffff);



        // CLOSE
        vmm.Close();
    }

    static void ExampleLeechCore()
    {
        bool result;
        // CREATE LEECHCORE CONTEXT
        lc.CONFIG cfg = new lc.CONFIG();
        cfg.dwVersion = lc.CONFIG_VERSION;
        cfg.dwPrintfVerbosity = lc.CONFIG_PRINTF_ENABLED | lc.CONFIG_PRINTF_V;
        cfg.szDevice = "file://c:\\dumps\\WIN7-X64-SP1-1.pmem";
        ulong hLC = lc.Create(ref cfg);
        if (hLC == 0) { return; }

        // read 128 bytes from address 0x1000
        byte[] MemRead = lc.Read(hLC, 0x1000, 128);

        // scatter read two memory pages in one single run
        MEM_SCATTER[] MEMs = lc.ReadScatter(hLC, 0x1000, 0x2000);

        // get/set LeechCore option
        ulong qwOptionValue;
        result = lc.GetOption(hLC, lc.OPT_CORE_VERBOSE_EXTRA, out qwOptionValue);
        result = lc.SetOption(hLC, lc.OPT_CORE_VERBOSE_EXTRA, 1);
        result = lc.GetOption(hLC, lc.OPT_CORE_VERBOSE_EXTRA, out qwOptionValue);

        // get memory map via command
        string strMemMap;
        byte[] dataMemMap;
        result = lc.Command(hLC, lc.CMD_MEMMAP_GET, null, out dataMemMap);
        if (result)
        {
            strMemMap = System.Text.Encoding.UTF8.GetString(dataMemMap);
        }

        // CLOSE
        lc.Close(hLC);
    }

    static void Main(string[] args)
    {
        //ExampleLeechCore();
        ExampleVmm();
    }
}
