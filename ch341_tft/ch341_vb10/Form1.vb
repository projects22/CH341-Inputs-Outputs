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
    Private Shared Function CH341SetOutput(ByVal iIndex As Integer, ByVal iEnable As Integer, ByVal iSetDirOut As Integer, ByVal iSetDataOut As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
#End Region

#Region "I2C"

    '    BOOL	WINAPI	CH341SetStream(  // Set the serial port flow mode
    '	ULONG			iIndex,  // Specify the CH341 device number
    '	ULONG			iMode );  // To specify the mode, see Downlink
    '// Bit 1-bit 0: I2C interface speed /SCL frequency, 00= low speed /20KHz,01= standard /100KHz(default),10= fast /400KHz,11= high speed /750KHz
    '// Bit 2: SPI I/O number /IO pins, 0= single in/single out (D3 clock /D5 out /D7 in)(default),1= double in/double out (D3 clock /D5 out D4 out /D7 in D6 in)
    '// Bit 7: Bit order in SPI bytes, 0= low first, 1= high first
    '// All other reservations must be 0
    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Protected Friend Shared Function CH341SetStream(ByVal iIndex As Integer, ByVal iMode As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    'BOOL	WINAPI	CH341SetDelaymS(  // Set the hardware asynchronous delay to a specified number of milliseconds before the next stream operation
    'ULONG			iIndex,  // Specify the CH341 device number
    'ULONG			iDelay );  // Specifies the number of milliseconds to delay
    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341SetDelaymS(ByVal iIndex As Integer, ByVal iDelay As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    'BOOL	WINAPI	CH341StreamI2C(  // Process I2C data stream, 2-wire interface, clock line for SCL pin, data line for SDA pin (quasi-bidirectional I/O), speed of about 56K bytes
    'ULONG			iIndex,  // Specify the CH341 device number
    'ULONG			iWriteLength,  // Number of bytes of data to write out
    'PVOID			iWriteBuffer,  // Points to a buffer to place data to be written, usually with the I2C device address and read/write direction bits as the first byte
    'ULONG			iReadLength,  // Number of bytes of data to be read
    'PVOID			oReadBuffer );  // Points to a buffer and returns the data read in
    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341StreamI2C(ByVal iIndex As Integer, ByVal iWriteLength As Integer, ByVal iWriteBuffer As Byte(), ByVal iReadLength As Integer, ByVal oReadBuffer As Byte()) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    'BOOL	WINAPI	CH341ReadI2C(  // Read one byte of data from the I2C interface
    'ULONG			iIndex,  // Specify the serial number of the CH341 device
    'UCHAR			iDevice,  // The lower 7 bits specify the I2C device address
    'UCHAR			iAddr,  // Address of specified data unit
    'PUCHAR			oByte );  // Address of specified data unit
    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341ReadI2C(ByVal iIndex As Integer, ByVal iDevice As Byte, ByVal iAddr As Byte, ByRef oByte As Byte) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    'BOOL	WINAPI	CH341WriteI2C(  // Write a byte of data to the I2C interface
    'ULONG			iIndex,  // Specify the serial number of the CH341 device
    'UCHAR			iDevice,  // The lower 7 bits specify the I2C device address
    'UCHAR			iAddr,  // Address of specified data unit
    'UCHAR			iByte );  // Byte data to be written
    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341WriteI2C(ByVal iIndex As Integer, ByVal iDevice As Byte, ByVal iAddr As Byte, ByVal iByte As Byte) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

#End Region
#Region "SPI"

    '    BOOL	WINAPI	CH341StreamSPI4(  // SPI data stream processing, 4-line interface, clock line is DCK/D3 pin, output data line is DOUT/D5 pin, input data line is DIN/D7 pin, chip line is D0/D1/D2, speed is about 68K bytes
    '/* SPI timing: DCK/D3 pin is clock output, default low level, DOUT/D5 pin is output during low level before clock rising edge, DIN/D7 pin is input during high level before clock falling edge */
    '	ULONG			iIndex,  // Specify the CH341 device number
    '	ULONG			iChipSelect,  // Chip selection control. If bit 7 is 0, chip selection control is ignored. If bit 7 is 1, the parameter is effective: if bit 1 is 00/01/10, D0/D1/D2 pins are selected as low level effective chip selection respectively
    '	ULONG			iLength,  // Number of bytes of data to be transferred
    '	PVOID			ioBuffer );  // Points to a buffer, places data to be written out from DOUT, and returns data to be read in from DIN
    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341StreamSPI4(ByVal index As Integer, ByVal сhipSelect As Integer, ByVal writeLength As Integer, ByVal ioBuffer As Byte()) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function
    '    BOOL	WINAPI	CH341BitStreamSPI(  // SPI bit data flow processing,4 line /5 line interface, clock line for DCK/D3 pin, output data line for DOUT/DOUT2 pin, input data line for DIN/DIN2 pin, chip line for D0/D1/D2, speed about 8K bit *2
    '	ULONG			iIndex,  // Specify the CH341 device number
    '	ULONG			iLength,  // The number of bits to be transmitted at a time is a maximum of 896. It is recommended that the number not exceed 256
    '	PVOID			ioBuffer );  // Point to a buffer, place data to be written from DOUT/DOUT2/ d2-d0, and return data to be read from DIN/DIN2
    '/* SPI timing: DCK/D3 pins are clock outputs and are low by default, DOUT/D5 and DOUT2/D4 pins are output during the low period before the rising edge of the clock, and DIN/D7 and DIN2/D6 pins are input during the high period before the falling edge of the clock*/
    '/* One byte of the ioBuffer has 8 bits corresponding to d7-D0 pins. Bit 5 is output to DOUT, bit 4 to DOUT2, bit 2-0 to D2-d0, bit 7 is input from DIN, bit 6 is input from DIN2, bit 3 is ignored */
    '/* Before calling this API, CH341Set_D5_D0 should be called to set the I/O direction of the D5-D0 pins of CH341 and set the default level of the pins */
    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341BitStreamSPI(ByVal iIndex As Integer, ByVal iLength As Integer, ByVal ioBuffer As Byte()) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport(DLL_PATH, SetLastError:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function CH341Set_D5_D0(ByVal iIndex As Integer, ByVal iSetDirOut As Integer, ByVal iSetDataOut As Integer) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

#End Region

 
    Private Sub btnConnect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConnect.Click

        Label1.Text = "DLL version: " & CH341GetVersion()
        If CH341OpenDevice(0) Then
            Label2.Text = "Device is Open"
        Else
            Label2.Text = "Device failed to open"
        End If
        Label3.Text = "Driver version: " & CH341GetDrvVersion()
        If CH341SetStream(0, &H81) Then
            Label5.Text = "SPI set to 4 wires" '0x81=MSB first
        Else
            Label5.Text = "SPI settings failed"
        End If
        'CH341SetDelaymS(0, 2)
        CH341SetOutput(0, 8, 15, 15)    'DC=ERR
        tftInit()
    End Sub
    'btn tft
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub
    Private Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
        ofd1.ShowDialog()
    End Sub

    Private Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Dim rec, len As Long
        Dim color As Byte

        FileOpen(1, ofd1.FileName, OpenMode.Binary, OpenAccess.Read)
        len = LOF(1)
        area(0, 0, 159, 127)
        For rec = 55 To len
            FileGet(1, color, rec)
            SPI(color)
        Next

        Label4.Text = "File uploaded"
        FileClose(1)

    End Sub
    Private Sub tftInit()
        command(&H1)   'sw reset
        Thread.Sleep(200)
        command(&H11) ' Sleep out
        Thread.Sleep(200)
        command(&H3A)   'color mode
        send_data(&H6)   '18 bits
        command(&H36) 'Memory access ctrl (directions)
        send_data(&H28)  '0x60
        'command(&H21) 'inversion on

        'command(&H2D) 'color look up table for 16 bits colors
        'send_data(0)
        'For i = 1 To 31
        '    send_data(i * 2)
        'Next
        'For i = 0 To 63
        '    send_data(i)
        'Next
        'send_data(0)
        'For i = 1 To 31
        '    send_data(i * 2)
        'Next
        command(&H13) 'Normal display on
        command(&H29) 'main screen turn on

    End Sub

    Private Sub SPI(ByVal val)
        Dim c(1) As Byte
        c(0) = val
        CH341StreamSPI4(0, 128, 1, c)
    End Sub

    Private Sub command(ByVal cmnd)
        CH341SetOutput(0, 3, &H100, 0)   'DC=0
        SPI(cmnd)
        CH341SetOutput(0, 3, &H100, &H100)   'DC=1,

    End Sub
    Private Sub send_data(ByVal data)
        CH341SetOutput(0, 3, &H100, &H100)   'DC=1,
        SPI(data)

    End Sub
    Private Sub area(ByVal x0 As Byte, ByVal y0 As Byte, ByVal x1 As Byte, ByVal y1 As Byte)
        command(&H2A) 'Column addr set
        send_data(&H0)
        send_data(x0) 'XSTART 
        send_data(&H0)
        send_data(x1) ' XEND
        command(&H2B) 'Row addr set
        send_data(0)
        send_data(y0) 'YSTART
        send_data(0)
        send_data(y1) ' YEND
        command(&H2C) ' write to RAM

    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        CH341CloseDevice(0)
    End Sub

End Class

