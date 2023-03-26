Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
Public Class Form1

    Public Const DLL_PATH As String = "C:\Windows\SysWOW64\CH341DLL.DLL"


#Region "Device"

    'ULONG	WINAPI	CH341GetVersion( );  // Get the DLL version number, return the version number
    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341GetVersion() As Integer
    End Function
    'ULONG	WINAPI	CH341GetDrvVersion( );  // Get the driver version number, return the version number, or return 0 if there is an error
    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341GetDrvVersion() As Integer
    End Function
    'HANDLE	WINAPI	CH341OpenDevice(  // Open the CH341 device, return the handle, if an error occurs, it will be invalid
    'ULONG			iIndex );  // Specify the device serial number of CH341, 0 corresponds to the first device
    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341OpenDevice(ByVal index As Integer) As IntPtr
    End Function
    'VOID	WINAPI	CH341CloseDevice(  // Close the CH341 device
    'ULONG			iIndex );  // Specify the serial number of the CH341 device
    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Sub CH341CloseDevice(ByVal index As Integer)
    End Sub
    'BOOL	WINAPI	CH341ResetDevice(  // Reset USB device
    'ULONG			iIndex );  // Specify the serial number of the CH341 device
    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341ResetDevice(ByVal index As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341Set_D5_D0(ByVal iIndex As Integer, ByVal iSetDirOut As Integer, ByVal iSetDataOut As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341SetOutput(ByVal iIndex As Integer, ByVal iEnable As Integer, ByVal iSetDirOut As Integer, ByVal iSetDataOut As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    '    BOOL	WINAPI	CH341GetStatus(  // Input data and status directly through CH341
    '	ULONG			iIndex,  // Specify the serial number of the CH341 device
    '	PULONG			iStatus );  // Points to a double word unit, used to save status data, refer to the following bit description
    '// Bit 7-bit 0 correspond to D7-D0 pins of CH341
    '// Bit 8 corresponds to ERR# pin of CH341, Bit 9 corresponds to PEMP pin of CH341, Bit 10 corresponds to INT# pin of CH341, Bit 11 corresponds to SLCT pin of CH341, Bit 23 corresponds to SDA pin of CH341
    '// Bit 13 corresponds to BUSY/WAIT# pin of CH341, Bit 14 corresponds to AUTOFD#/DATAS# pin of CH341, Bit 15 corresponds to SLCTIN#/ADDRS# pin of CH341
    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341GetStatus(ByVal iIndex As Integer, ByRef iStatus As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    '    BOOL	WINAPI	CH341GetInput(  // Using the CH341 to directly enter data and status is more efficient than using the CH341GetStatus function
    '	ULONG			iIndex,  // Specify the CH341 device number
    '	PULONG			iStatus );  // Points to a two-word unit that holds state data, as described in the bit description below
    '// Bits 7-0 correspond to the D7-D0 pins of CH341
    '// Bit 8 corresponds to the ERR# pin of CH341, bit 9 to the PEMP pin of CH341, bit 10 to the INT# pin of CH341, bit 11 to the SLCT pin of CH341, and bit 23 to the SDA pin of CH341
    '// Bit 13 corresponds to the BUSY/WAIT# pin of CH341, bit 14 corresponds to the AUTOFD#/ data # pin of CH341, and bit 15 corresponds to the SLCTIN#/ADDRS# pin of CH341
    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341GetInput(ByVal iIndex As Integer, ByRef iStatus As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

#End Region


    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = "DLL version: " & CH341GetVersion()
        If CH341OpenDevice(0) Then
            Label2.Text = "Device is Open"
        Else
            Label2.Text = "Device failed to open"
        End If
        'CH341Label2.Text = ResetDevice(0)
        Label3.Text = "Driver version: " & CH341GetDrvVersion()

        CH341SetOutput(0, 8, 15, 15)    'set D0-D3 as output
        Timer1.Interval = 100
        Timer1.Enabled = True
    End Sub
    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim out As Byte = 0
        Dim pins As Integer = 0

        CH341GetInput(0, pins)
        'Label4.Text = pins And &HF0
        If pins And &H10 Then Panel1.BackColor = Color.Green Else Panel1.BackColor = Color.Red
        If pins And &H20 Then Panel2.BackColor = Color.Green Else Panel2.BackColor = Color.Red
        If pins And &H40 Then Panel3.BackColor = Color.Green Else Panel3.BackColor = Color.Red
        If pins And &H80 Then Panel4.BackColor = Color.Green Else Panel4.BackColor = Color.Red

        If cb1.Checked = True Then out = 1
        If cb2.Checked = True Then out = out + 2
        If cb3.Checked = True Then out = out + 4
        If cb4.Checked = True Then out = out + 8
        CH341SetOutput(0, 4, 15, out)   'set outputs

    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        CH341CloseDevice(0)
    End Sub

End Class
