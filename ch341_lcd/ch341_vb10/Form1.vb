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

#End Region


    Private Sub btnConnect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConnect.Click

        Label1.Text = "DLL version: " & CH341GetVersion()
        If CH341OpenDevice(0) Then
            Label2.Text = "Device is Open"
        Else
            Label2.Text = "Device failed to open"
        End If
        'CH341Label2.Text = ResetDevice(0)
        Label3.Text = "Driver version: " & CH341GetDrvVersion()
        text1.MaxLength = 32
        lcdInit()
        clear()
        cursorTo(0, 4)
        display(&H48)
        display(&H69)
    End Sub

    'clock pulse from ERR
    Private Sub clock()
        CH341SetOutput(0, 3, &H100, &H100)
        CH341SetOutput(0, 3, &H100, 0)
        CH341SetOutput(0, 3, &H100, &H100)
    End Sub
    Private Sub clear()
        CH341Set_D5_D0(0, 31, 0) 'p=0, RS=0
        clock()
        CH341Set_D5_D0(0, 31, 1) '
        clock()
        CH341Set_D5_D0(0, 31, 0) '
        clock()
        CH341Set_D5_D0(0, 31, 2) '
        clock()
        Thread.Sleep(2)

    End Sub
    Private Sub display(ByVal fig)
        CH341Set_D5_D0(0, 31, 16 + (fig >> 4)) 'High 4 bits
        clock()
        CH341Set_D5_D0(0, 31, 16 + (fig And 15)) 'low 4 bits
        clock()
        'CH341Set_D5_D0(0, 31, 5) 'RS=0

    End Sub
    Private Sub cmnd(ByVal cmd)

        CH341Set_D5_D0(0, 31, cmd >> 4) 'High 4 bits
        clock()
        CH341Set_D5_D0(0, 31, cmd And 15) 'low 4 bits
        clock()

    End Sub
    Private Sub lcdInit()
        Thread.Sleep(100)
        cmnd(&H32)  'init command
        cmnd(&H28)  'function set: 1 line=0, 2 lines=8
        cmnd(12)    'display set:  curzor off=12, curzor on=14, curzor on and flashing=15, curzor and display off=8 
        cmnd(1) 'display clear
        Thread.Sleep(2)
        cmnd(6) 'Entry mode:increment, no shift 

    End Sub
    Private Sub cursorTo(ByVal line As Byte, ByVal c As Byte)
        CH341Set_D5_D0(0, 31, 0) 'return home
        clock()
        CH341Set_D5_D0(0, 31, 2) 'p=2
        clock()
        Thread.Sleep(2)
        If line > 0 Then newLine()
        For i = 0 To c - 1
            CH341Set_D5_D0(0, 31, 1) 'p=1
            clock()
            CH341Set_D5_D0(0, 31, 4) 'p=4
            clock()
        Next

    End Sub
    Private Sub newLine()
        CH341Set_D5_D0(0, 31, 12) 'return home
        clock()
        CH341Set_D5_D0(0, 31, 0) 'p=0
        clock()
        Thread.Sleep(2)
    End Sub
    'btn send
    Private Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim text As String

        clear()
        cursorTo(0, 0)
        text = text1.Text
        For i = 1 To text.Length
            If i = 17 Then newLine()
            If i > 32 Then Exit For
            display(Asc(Mid(text, i, 1)))

        Next
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        CH341CloseDevice(0)
    End Sub

End Class
